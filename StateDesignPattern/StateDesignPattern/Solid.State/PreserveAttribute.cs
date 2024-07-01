using System;

// this attribute will prevent the linker from stripping a class or method
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
public sealed class PreserveAttribute : Attribute
{
	public bool AllMembers = true;
	public bool Conditional = false;
}
