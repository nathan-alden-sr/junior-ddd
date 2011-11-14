using System;
using System.Collections.Generic;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Represents a validation rule.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	public interface IRule<out TValidationError>
		where TValidationError : IComparable
	{
		/// <summary>
		/// Performs rule validation.
		/// </summary>
		/// <returns>Validation errors generated during validation.</returns>
		IEnumerable<TValidationError> Validate();
	}
}