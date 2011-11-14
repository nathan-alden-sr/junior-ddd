using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Validates that a value is not null.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class NotNullRule<TValidationError, TValue> : Rule<TValidationError>
		where TValidationError : IComparable
		where TValue : class
	{
		private readonly TValidationError _validationError;
		private readonly object _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="NotNullRule{TValidationError,TValue}"/> class.
		/// </summary>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public NotNullRule(TValidationError validationError, TValue value, params IRule<TValidationError>[] dependsOn)
			: this(validationError, value, Enumerable.Empty<TValidationError>(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NotNullRule{TValidationError,TValue}"/> class.
		/// </summary>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public NotNullRule(TValidationError validationError, TValue value, TValidationError stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: this(validationError, value, stopValidatingOn.ToEnumerable(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NotNullRule{TValidationError,TValue}"/> class.
		/// </summary>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to one in <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public NotNullRule(TValidationError validationError, TValue value, IEnumerable<TValidationError> stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: base(stopValidatingOn, dependsOn)
		{
			validationError.ThrowIfNull("validationError");

			_validationError = validationError;
			_value = value;
		}

		/// <summary>
		/// Performs rule validation.
		/// </summary>
		/// <returns>Validation errors if the rule failed to validate.</returns>
		protected override IEnumerable<TValidationError> OnValidate()
		{
			if (_value == null)
			{
				yield return _validationError;
			}
		}
	}
}