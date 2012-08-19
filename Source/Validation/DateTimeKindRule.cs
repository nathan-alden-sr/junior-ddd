using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Validates that a value is a certain <see cref="DateTimeKind"/>.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	public class DateTimeKindRule<TValidationError> : Rule<TValidationError>
		where TValidationError : IComparable
	{
		private readonly DateTimeKind _kind;
		private readonly TValidationError _validationError;
		private readonly DateTime? _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeKindRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="kind">A <see cref="DateTimeKind"/> with which to test.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public DateTimeKindRule(DateTimeKind kind, TValidationError validationError, DateTime value, params IRule<TValidationError>[] dependsOn)
			: this(kind, validationError, value, false, Enumerable.Empty<TValidationError>(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeKindRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="kind">A <see cref="DateTimeKind"/> with which to test.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public DateTimeKindRule(DateTimeKind kind, TValidationError validationError, DateTime value, TValidationError stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: this(kind, validationError, value, false, stopValidatingOn.ToEnumerable(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeKindRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="kind">A <see cref="DateTimeKind"/> with which to test.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to one in <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public DateTimeKindRule(DateTimeKind kind, TValidationError validationError, DateTime value, IEnumerable<TValidationError> stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: this(kind, validationError, value, false, stopValidatingOn, dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeKindRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="kind">A <see cref="DateTimeKind"/> with which to test.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="allowNull">Indicates if null values are allowed.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public DateTimeKindRule(DateTimeKind kind, TValidationError validationError, DateTime? value, bool allowNull, params IRule<TValidationError>[] dependsOn)
			: this(kind, validationError, value, allowNull, Enumerable.Empty<TValidationError>(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeKindRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="kind">A <see cref="DateTimeKind"/> with which to test.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="allowNull">Indicates if null values are allowed.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public DateTimeKindRule(DateTimeKind kind, TValidationError validationError, DateTime? value, bool allowNull, TValidationError stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: this(kind, validationError, value, allowNull, stopValidatingOn.ToEnumerable(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeKindRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="kind">A <see cref="DateTimeKind"/> with which to test.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="allowNull">Indicates if null values are allowed.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to one in <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="validationError"/> is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null and <paramref name="allowNull"/> is false.</exception>
		public DateTimeKindRule(DateTimeKind kind, TValidationError validationError, DateTime? value, bool allowNull, IEnumerable<TValidationError> stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: base(stopValidatingOn, dependsOn)
		{
			validationError.ThrowIfNull("validationError");
			if (!allowNull)
			{
				value.ThrowIfNull("value");
			}

			_kind = kind;
			_validationError = validationError;
			_value = value;
		}

		/// <summary>
		/// Performs rule validation.
		/// </summary>
		/// <returns>Validation errors if the rule failed to validate.</returns>
		protected override IEnumerable<TValidationError> OnValidate()
		{
			if (_value != null && _value.Value.Kind != _kind)
			{
				yield return _validationError;
			}
		}
	}
}