using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Validates many rules at once and optionally throw an exception if validation fails.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	public class RuleValidator<TValidationError> : IRuleValidator<TValidationError>
		where TValidationError : IComparable
	{
		// ReSharper disable StaticFieldInGenericType
		private static readonly IRuleValidator<TValidationError> _default = new RuleValidator<TValidationError>();
		// ReSharper restore StaticFieldInGenericType

		/// <summary>
		/// Gets a default rule validator for validation errors of type <typeparamref name="TValidationError"/>.
		/// </summary>
		public static IRuleValidator<TValidationError> Default
		{
			get
			{
				return _default;
			}
		}

		/// <summary>
		/// Validates a set of rules and, if validation fails, throws an exception returned by the specified delegate.
		/// </summary>
		/// <param name="getExceptionDelegate">A delegate that returns an exception to throw when validation fails.</param>
		/// <param name="rules">Rules to validate.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="getExceptionDelegate"/> is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="rules"/> is null.</exception>
		public void Validate(Func<IEnumerable<TValidationError>, Exception> getExceptionDelegate, params IRule<TValidationError>[] rules)
		{
			getExceptionDelegate.ThrowIfNull("getExceptionDelegate");
			rules.ThrowIfNull("rules");

			var validationErrors = new List<TValidationError>();

			foreach (var rule in rules)
			{
				validationErrors.AddRange(rule.Validate());
			}

			if (validationErrors.Any())
			{
				throw getExceptionDelegate(validationErrors.Distinct());
			}
		}

		/// <summary>
		/// Validates a set of rules and returns any validation errors generated during validation.
		/// Only one error per <typeparamref name="TValidationError"/> will be returned in the case where the same error occurs multiple times.
		/// </summary>
		/// <param name="rules">Rules to validate.</param>
		/// <returns>Validation errors generated during validation.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="rules"/> is null.</exception>
		public IEnumerable<TValidationError> Validate(params IRule<TValidationError>[] rules)
		{
			rules.ThrowIfNull("rules");

			var validationErrors = new List<TValidationError>();

			foreach (var rule in rules)
			{
				validationErrors.AddRange(rule.Validate());
			}

			return validationErrors.Distinct();
		}
	}
}