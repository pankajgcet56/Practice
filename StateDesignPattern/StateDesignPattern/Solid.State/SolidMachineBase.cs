using System;
using Solid.State;

public abstract class SolidMachineBase
{
    public abstract void Start();

    public abstract ISolidState CurrentState { get; }

    public abstract void Stop();

    public abstract void SetStateFromStorage(string initialStateClassName);
}

