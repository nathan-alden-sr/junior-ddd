using System;
using System.Collections.Generic;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Represents a way to validate many rules at once and optionally throw an exception if validation fails.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	public interface IRuleValidator<TValidationError>
		where TValidationError : IComparable
	{
		/// <summary>
		/// Validates a set of rules and, if validation fails, throws an exception returned by the specified delegate.
		/// </summary>
		/// <param name="getExceptionDelegate">A delegate that returns an exception to throw when validation fails.</param>
		/// <param name="rules">Rules to validate.</param>
		void Validate(Func<IEnumerable<TValidationError>, Exception> getExceptionDelegate, params IRule<TValidationError>[] rules);

		/// <summary>
		/// Validates a set of rules and returns any validation errors generated during validation.
		/// </summary>
		/// <param name="rules">Rules to validate.</param>
		/// <returns>Validation errors generated during validation.</returns>
		IEnumerable<TValidationError> Validate(params IRule<TValidationError>[] rules);
	}
}