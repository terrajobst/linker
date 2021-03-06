using System.Diagnostics;
using Mono.Cecil;

namespace Mono.Linker {
	[DebuggerDisplay ("{Override}")]
	public class OverrideInformation {
		public OverrideInformation (MethodDefinition @base, MethodDefinition @override, InterfaceImplementation matchingInterfaceImplementation = null)
		{
			Base = @base;
			Override = @override;
			MatchingInterfaceImplementation = matchingInterfaceImplementation;
		}

		public MethodDefinition Base { get; }
		public MethodDefinition Override { get; }
		public InterfaceImplementation MatchingInterfaceImplementation { get; }

		public bool IsOverrideOfInterfaceMember
		{
			get
			{
				if (MatchingInterfaceImplementation != null)
					return true;

				return Base.DeclaringType.IsInterface;
			}
		}

		public TypeDefinition InterfaceType
		{
			get
			{
				if (!IsOverrideOfInterfaceMember)
					return null;

				if (MatchingInterfaceImplementation != null)
					return MatchingInterfaceImplementation.InterfaceType.Resolve ();

				return Base.DeclaringType;
			}
		}
	}
}
