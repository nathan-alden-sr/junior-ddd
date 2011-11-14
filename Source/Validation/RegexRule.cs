using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Junior.Common;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Validates that a value regular expression.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	public class RegexRule<TValidationError> : Rule<TValidationError>
		where TValidationError : IComparable
	{
		private readonly bool _allowNull;
		private readonly string _regexPattern;
		private readonly TValidationError _validationError;
		private readonly string _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="RegexRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="regexPattern">A regular expression pattern.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="allowNull">Indicates if null values are allowed.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public RegexRule(string regexPattern, TValidationError validationError, string value, bool allowNull, params IRule<TValidationError>[] dependsOn)
			: this(regexPattern, validationError, value, allowNull, Enumerable.Empty<TValidationError>(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RegexRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="regexPattern">A regular expression pattern.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="allowNull">Indicates if null values are allowed.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public RegexRule(string regexPattern, TValidationError validationError, string value, bool allowNull, TValidationError stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: this(regexPattern, validationError, value, allowNull, stopValidatingOn.ToEnumerable(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RegexRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="regexPattern">A regular expression pattern.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="allowNull">Indicates if null values are allowed.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to one in <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="regexPattern"/> is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="validationError"/> is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null and <paramref name="allowNull"/> is false.</exception>
		public RegexRule(string regexPattern, TValidationError validationError, string value, bool allowNull, IEnumerable<TValidationError> stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: base(stopValidatingOn, dependsOn)
		{
			regexPattern.ThrowIfNull("regexPattern");
			validationError.ThrowIfNull("validationError");
			if (!allowNull)
			{
				value.ThrowIfNull("value");
			}

			_regexPattern = regexPattern;
			_validationError = validationError;
			_value = value;
			_allowNull = allowNull;
		}

		/// <summary>
		/// Performs rule validation.
		/// </summary>
		/// <returns>Validation errors if the rule failed to validate.</returns>
		protected override IEnumerable<TValidationError> OnValidate()
		{
			if ((_value == null && !_allowNull) || (_value != null && !Regex.IsMatch(_value, _regexPattern)))
			{
				yield return _validationError;
			}
		}
	}
}