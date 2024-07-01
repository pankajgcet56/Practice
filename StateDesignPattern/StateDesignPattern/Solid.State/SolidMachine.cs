using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.State
{
    public partial class SolidMachine<TTrigger> : SolidMachineBase
    {
        // Variables

        private const int DefaultStatehistoryTrimThreshold = 100;
        private const int MinStatehistoryTrimThreshold = 10;
        private const double StatehistoryTrimPercentage = 0.1; // Trim 10% of state history

        private readonly object _queueLockObject = new object();
        private readonly object _stateHistoryLockObject = new object();

        private readonly Dictionary<Type, StateConfiguration> _stateConfigurations;
        private readonly List<Action> _transitionQueue;
        private readonly List<StateConfiguration> _stateHistory;

        private StateConfiguration _initialState;
        private StateConfiguration _currentState;
        
        private bool _initialStateConfigured;
		private bool _isStartedFromStorage;
        private bool _isStarted;

        private object _context;
        private IStateResolver _stateResolver;

        private bool _stateResolverRequired;
        private bool _isProcessingQueue;
        
        private StateInstantiationMode _stateInstantiationMode;
        private int _stateHistoryTrimThreshold;

        public Dictionary<Type, StateConfiguration> StateConfigurations { get { return _stateConfigurations; } }

        // Private methods

		/// <summary>
		/// Enters a new state (if there is one) Does NOT raise the OnTransitioned event.
		/// </summary>
		private void EnterNewStateFromStorage(Type previousStateType, StateConfiguration state)
		{
			_currentState = state;
			
			//Type currentStateType = null;
			
			// Are we entering a new state?
			if (_currentState != null)
			{
				//currentStateType = _currentState.StateType;
				_currentState.EnterStateFromStorage();
			}
		}

		/// <summary>
        /// Enters a new state (if there is one) and raises the OnTransitioned event.
        /// </summary>
        private void EnterNewState(Type previousStateType, StateConfiguration state)
        {
            _currentState = state;

            Type currentStateType = null;

            // Are we entering a new state?
            if (_currentState != null)
            {
                currentStateType = _currentState.StateType;
                _currentState.Enter();
            }

            // Raise an event about the transition
            OnTransitioned(new TransitionedEventArgs(previousStateType, currentStateType));
        }

        /// <summary>
        /// Exits the current state and returns the Type of it.
        /// </summary>
        /// <returns></returns>
        private Type ExitCurrentState(bool addToHistory)
        {
            if (_currentState == null)
                return null;
            else
            {
                _currentState.Exit();

                // Record it in the history
                if (addToHistory)
                    AddStateToHistory(_currentState);

                var stateType = _currentState.StateType;
                _currentState = null;
                return stateType;
            }
        }

        /// <summary>
        /// Handles the processing of a trigger, calculating if it is valid and which target state we should go to.
        /// </summary>
        /// <param name="trigger"></param>
        private void DoTrigger(TTrigger trigger)
        {
            // Find all trigger configurations with a matching trigger
            var triggers = _currentState.TriggerConfigurations.Where(x => x.Trigger.Equals(trigger)).ToList();

            // No trigger configs found?
            if (triggers.Count == 0)
            {
				Debug.LogError( string.Format("Trigger {0} is not valid for state {1}!", trigger, _currentState.StateType.Name));
				return;
            }
            else
            {
                // Is it a single, unguarded trigger?
                if (triggers[0].GuardClause == null)
                {
                    var previousStateType = ExitCurrentState(addToHistory: true);
                    EnterNewState(previousStateType, triggers[0].TargetState);
                }
                else
                {
                    // First exit the current state, it may affect the evaluation of the guard clauses
                    var previousStateType = ExitCurrentState(addToHistory: true);

                    TriggerConfiguration matchingTrigger = null;
                    foreach (var tr in triggers)
                    {
                        if (tr.GuardClause())
                        {
                            if (matchingTrigger != null)
							{
								Debug.LogError(string.Format("State {0}, trigger {1} has multiple guard clauses that simultaneously evaulate to True!",
                                    previousStateType.Name, trigger));
								return;
							}
                            matchingTrigger = tr;
                        }
                    }

                    // Did we find a matching trigger?
                    if (matchingTrigger == null)
					{
						Debug.LogError(string.Format("State {0}, trigger {1} has no guard clause that evaulate to True!",
                            previousStateType.Name, trigger));
						return;
					}

                    // Queue up the transition
                    EnterNewState(previousStateType, matchingTrigger.TargetState);
                }
            }
        }

        private void AddStateToHistory(StateConfiguration state)
        {
            lock (_stateHistoryLockObject)
            {
                if (state != null)
                    _stateHistory.Insert(0, state);

                // Time to trim it?
                if (_stateHistory.Count > _stateHistoryTrimThreshold)
                {
                    var trimValue = (int)(_stateHistoryTrimThreshold * (1.0 - StatehistoryTrimPercentage));
                    while (_stateHistory.Count > trimValue)
                        _stateHistory.RemoveAt(trimValue);
                }
            }
        }

        /// <summary>
        /// Loops through the transition queue until it is empty, executing the queued
        /// calls to the ExecuteTransition method.
        /// </summary>
        private void ProcessTransitionQueue()
        {
            if (_isProcessingQueue)
                return;

            try
            {
                _isProcessingQueue = true;

                do
                {
                    Action nextAction = null;

                    // Lock queue during readout of next action
                    lock (_queueLockObject)
                    {
                        if (_transitionQueue.Count == 0)
                            return;

                        nextAction = _transitionQueue[0];
                        _transitionQueue.RemoveAt(0);
                    }

                    if (nextAction != null)
                        nextAction();
                } while (true);
            }
            finally
            {
                _isProcessingQueue = false;
            }
        }

        /// <summary>
        /// Sets the initial state of the state machine.
        /// </summary>
        private void SetInitialState(StateConfiguration initialStateConfiguration)
        {
            _initialState = initialStateConfiguration;
            _initialStateConfigured = true;
        }

		/// <summary>
		/// Sets the initial state of the state machine. This method is ONLY intended
		/// to restore the state after loading from storage. No transitions are evaluated.
		/// </summary>
        public override void SetStateFromStorage(string initialStateClassName)
		{
			Type initialStateType = Type.GetType(initialStateClassName);

			if (initialStateType != null && _stateConfigurations.ContainsKey(initialStateType))
			{
				_currentState = _stateConfigurations[initialStateType];
				_isStartedFromStorage = true;
			}
			else
			{
				Debug.LogError(string.Format("Cannot initialize into state '{0}'", initialStateClassName));
			}
		}
		
		/// <summary>
        /// Creates an instance of a specified state type, either through .NET activation
        /// or through a defined state resolver.
        /// </summary>
        private ISolidState InstantiateState(Type stateType)
        {
            // Do we have a state resolver?
            if (_stateResolver != null)
            {
                var instance = _stateResolver.ResolveState(stateType);
                if (instance == null)
				{
					Debug.LogError(string.Format("State resolver could not resolve a state for type '{0}'!", stateType.Name));
					return instance;
				}
                return instance;
            }
            else
                return (ISolidState) Activator.CreateInstance(stateType);
        }

        /// <summary>
        /// Checks if a state resolver will be required on state machine startup.
        /// </summary>
        /// <param name="stateType"></param>
        private void HandleResolverRequired(Type stateType)
        {
            // A state resolver is required if a configured state has no parameterless constructor
#if NETFX_CORE
			_stateResolverRequired = true;
			var constructors  = stateType.GetTypeInfo().DeclaredConstructors;
			foreach (var constructor in constructors)
			{
				if (constructor.GetParameters().Length == 0)
				{
					_stateResolverRequired = false;
					break;
				}
			}
#else
            _stateResolverRequired = (stateType.GetConstructor(Type.EmptyTypes) == null);
#endif
        }

        /// <summary>
        /// Gets the object that should be used as state context.
        /// </summary>
        /// <returns></returns>
        private object GetContext()
        {
            return _context ?? this;
        }

        /// <summary>
        /// Returns a list of all triggers that are valid to use on the current state.
        /// </summary>
        /// <returns></returns>
        private List<TTrigger> GetValidTriggers()
        {
            if (!_isStarted || (_currentState == null))
                return new List<TTrigger>();

            // Return a distinct list (no duplicates) of triggers
            return _currentState.TriggerConfigurations.Select(x => x.Trigger).Distinct().ToList();
        }

        // Protected methods

        /// <summary>
        /// Raises the Transitioned event.
        /// </summary>
        protected virtual void OnTransitioned(TransitionedEventArgs eventArgs)
        {
            if (Transitioned != null)
                Transitioned(this, eventArgs);
        }

        // Constructors

        public SolidMachine()
        {
            _stateConfigurations = new Dictionary<Type, StateConfiguration>();
            _transitionQueue = new List<Action>();
            _stateHistory = new List<StateConfiguration>();

            _stateHistoryTrimThreshold = DefaultStatehistoryTrimThreshold;
        }

        public SolidMachine(object context) : this()
        {
            _context = context;
        }

        public SolidMachine(object context, IStateResolver stateResolver) : this(context)
        {
            _context = context;
            _stateResolver = stateResolver;
        }

        // Events

        /// <summary>
        /// Raised when the state machine has transitioned from one state to another.
        /// </summary>
        #pragma warning disable 67
        public event TransitionedEventHandler Transitioned;
        #pragma warning restore 67

        // Public methods

        /// <summary>
        /// Defines a state that should be configured.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <returns></returns>
        public StateConfiguration State<TState>() where TState : ISolidState, new()
		{
            var type = typeof (TState);
            // Does the state have a parameterless constructor? Otherwise a state resolver is required
            HandleResolverRequired(type);

            // Does a configuration for this state exist already?
            StateConfiguration configuration;
            if (_stateConfigurations.ContainsKey(typeof(TState)))
                configuration = _stateConfigurations[typeof(TState)];
            else
            {
                configuration = new StateConfiguration(type, this);

                // If this is the first state that is added, it becomes the initial state
                if (_stateConfigurations.Count == 0)
                    _initialState = configuration;

                _stateConfigurations.Add(type, configuration);
            }

            return configuration;
        }

        /// <summary>
        /// Starts the state machine by going to the initial state.
        /// </summary>
        public override void Start()
        {
            if (_initialState == null)
			{
				Debug.LogError( "No states have been configured!");
				return;
			}
                
            // If there are states that has no parameterless constructor, we must have set the StateResolver property.
            if (_stateResolverRequired && (_stateResolver == null))
			{
				Debug.LogError("One or more configured states has no parameterless constructor. Add such constructors or make sure that the StateResolver property is set!");
			}

            _isStarted = true;
			// Enter the initial state differently depending on whether it was loaded from storage
			if (_isStartedFromStorage)
				EnterNewStateFromStorage(null, _currentState);
			else
				// Enter the initial state
				EnterNewState(null, _initialState);
        }

        /// <summary>
        /// Stops the state machine by exiting the current state without entering a new one.
        /// </summary>
        public override void Stop()
        {
            // Ignore this call if the machine hasn't been started yet
            if (!_isStarted)
                return;

            try
            {
                // Empty the queue
                lock (_queueLockObject)
                    _transitionQueue.Clear();

                // Exit the current state and raise an event about it
                var previousStateType = ExitCurrentState(false);
                OnTransitioned(new TransitionedEventArgs(previousStateType, null));
            }
            finally
            {
                _isStarted = false;
            }
        }

        /// <summary>
        /// Adds a request to fire a trigger to the processing queue.
        /// </summary>
        /// <param name="trigger"></param>
        public void Trigger(TTrigger trigger)
        {
			if (!_isStarted)
			{
				return;
			}

            // Queue it up
            lock (_queueLockObject)
            {
                var localTrigger = trigger;
                _transitionQueue.Add(() => DoTrigger(localTrigger));
            }

            ProcessTransitionQueue();
        }

        /// <summary>
        /// Moves the state machine back to the previous state, ignoring valid triggers, guard clauses etc.
        /// </summary>
        public void GoBack()
        {
            StateConfiguration targetState;
            Type previousStateType;

            lock (_stateHistoryLockObject)
            {
                // If the history is empty, we just ignore the call
                if (_stateHistory.Count == 0)
                    return;

                previousStateType = ExitCurrentState(addToHistory: false);

                targetState = _stateHistory[0];
                _stateHistory.RemoveAt(0);
            }

            if (targetState != null)
                EnterNewState(previousStateType, targetState);
        }

        public void GoBackToInitialState()
        {
            StateConfiguration targetState;
            Type previousStateType;

            lock (_stateHistoryLockObject)
            {
                previousStateType = ExitCurrentState(addToHistory: false);

                targetState = _initialState;
                _stateHistory.Clear();
            }

            if (targetState != null)
                EnterNewState(previousStateType, targetState);
        }

        // Properties

        /// <summary>
        /// The type of the machines initial state.
        /// </summary>
        public Type InitialState
        {
            get { return _initialState.StateType; }
        }

        /// <summary>
        /// Returns the state that the state machine is currently in.
        /// </summary>
        public override ISolidState CurrentState
        {
            get
            {
                if (_currentState == null)
                    return null;
                else
                    return _currentState.StateInstance;
            }
        }

        /// <summary>
        /// A list of the triggers that are valid to use on the current state.
        /// If the machine hasn't been started yet, this list is empty.
        /// </summary>
        public List<TTrigger> ValidTriggers
        {
            get { return GetValidTriggers(); }
        }

        /// <summary>
        /// An arbitrary object that will be passed on to the states in their entry and exit methods.
        /// If no context is defined, the state machine instance will be used as context.
        /// </summary>
        public object Context
        {
            get { return _context; }
            set { _context = value; }
        }

        /// <summary>
        /// The resolver for state machine states. If this is not specified the standard
        /// .NET activator is used and all states must then have parameterless constructors.
        /// </summary>
        public IStateResolver StateResolver
        {
            get { return _stateResolver; }
            set { _stateResolver = value; }
        }

        /// <summary>
        /// Controls whether state class instances should be Singletons or if they should be instantiated
        /// for each transition. Default value is Singleton.
        /// </summary>
        public StateInstantiationMode StateInstantiationMode
        {
            get { return _stateInstantiationMode; }
            set
            {
                // This is only OK to change 
                if (_isStarted)
				{
					Debug.LogError("The StateInstantiationMode must be set before the state machine is started!");
				}
                  
                _stateInstantiationMode = value;
            }
        }

        /// <summary>
        /// Returns an array of the states the state machine has been in. The last state is at index 0.
        /// The current state is not part of the list.
        /// </summary>
        public Type[] StateHistory
        {
            get
            {
                lock (_stateHistoryLockObject)
                    return _stateHistory.Select(x => x.StateType).ToArray();
            }
        }

        /// <summary>
        /// The number of entries that will be kept in the state history before an automatic
        /// trim is performed.
        /// </summary>
        public int StateHistoryTrimThreshold
        {
            get { return _stateHistoryTrimThreshold; }
            set
            {
                // Can't set a too low value
                if (value < MinStatehistoryTrimThreshold)
                    value = MinStatehistoryTrimThreshold;

                _stateHistoryTrimThreshold = value;
                
                // If the new value is lower we may need a trim right away
                AddStateToHistory(null);
            }
        }
    }

    /// <summary>
    /// Enumeration of possible state instantiation modes.
    /// </summary>
    public enum StateInstantiationMode
    {
        /// <summary>
        /// The state class is instantiated the first time it is used. All following
        /// transitions into that state will use the same instance.
        /// </summary>
        Singleton,

        /// <summary>
        /// The target state of a transition is instantiated on each transition.
        /// </summary>
        PerTransition
    }
}
