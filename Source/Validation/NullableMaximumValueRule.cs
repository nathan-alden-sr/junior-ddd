using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Validates that a value is not greater than a maximum value.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	/// <typeparam name="TValue">A comparable type.</typeparam>
	public class NullableMaximumValueRule<TValidationError, TValue> : Rule<TValidationError>
		where TValidationError : IComparable
		where TValue : struct, IComparable
	{
		private readonly TValue _maximumValue;
		private readonly TValidationError _validationError;
		private readonly TValue? _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="MaximumValueRule{TValidationError,TValue}"/> class.
		/// </summary>
		/// <param name="maximumValue">The maximum allowable value for <paramref name="value"/>.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public NullableMaximumValueRule(TValue maximumValue, TValidationError validationError, TValue? value, params IRule<TValidationError>[] dependsOn)
			: this(maximumValue, validationError, value, Enumerable.Empty<TValidationError>(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MaximumValueRule{TValidationError,TValue}"/> class.
		/// </summary>
		/// <param name="maximumValue">The maximum allowable value for <paramref name="value"/>.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public NullableMaximumValueRule(TValue maximumValue, TValidationError validationError, TValue? value, TValidationError stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: this(maximumValue, validationError, value, stopValidatingOn.ToEnumerable(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MaximumValueRule{TValidationError,TValue}"/> class.
		/// </summary>
		/// <param name="maximumValue">The maximum allowable value for <paramref name="value"/>.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to one in <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="validationError"/> is null.</exception>
		public NullableMaximumValueRule(TValue maximumValue, TValidationError validationError, TValue? value, IEnumerable<TValidationError> stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: base(stopValidatingOn, dependsOn)
		{
			validationError.ThrowIfNull("validationError");

			_maximumValue = maximumValue;
			_validationError = validationError;
			_value = value;
		}

		/// <summary>
		/// Performs rule validation.
		/// </summary>
		/// <returns>Validation errors if the rule failed to validate.</returns>
		protected override IEnumerable<TValidationError> OnValidate()
		{
			if (_value != null && Comparer<TValue>.Default.Compare(_value.Value, _maximumValue) > 0)
			{
				yield return _validationError;
			}
		}
	}
}