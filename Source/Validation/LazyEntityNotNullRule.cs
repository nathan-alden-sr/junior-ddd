using System;
using System.Collections.Generic;

using Junior.Ddd.DomainModel;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Validates that a <see cref="LazyEntity{TEntity}"/> instance is not null and also that if its value has been loaded, the value is not null.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	/// <typeparam name="TEntity"></typeparam>
	public class LazyEntityNotNullRule<TValidationError, TEntity> : NotNullRule<TValidationError, object>
		where TValidationError : IComparable
		where TEntity : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LazyEntityNotNullRule{TValidationError,TEntity}"/> class.
		/// </summary>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public LazyEntityNotNullRule(TValidationError validationError, LazyEntity<TEntity> value, params IRule<TValidationError>[] dependsOn)
			: base(validationError, GetValueToTest(value), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LazyEntityNotNullRule{TValidationError,TEntity}"/> class.
		/// </summary>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public LazyEntityNotNullRule(TValidationError validationError, LazyEntity<TEntity> value, TValidationError stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: base(validationError, GetValueToTest(value), stopValidatingOn, dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LazyEntityNotNullRule{TValidationError,TEntity}"/> class.
		/// </summary>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to one in <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public LazyEntityNotNullRule(TValidationError validationError, LazyEntity<TEntity> value, IEnumerable<TValidationError> stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: base(validationError, GetValueToTest(value), stopValidatingOn, dependsOn)
		{
		}

		// ReSharper disable SuggestBaseTypeForParameter
		private static object GetValueToTest(LazyEntity<TEntity> value)
			// ReSharper restore SuggestBaseTypeForParameter
		{
			return value == null || !value.IsValueCreated ? (object)value : value.Value;
		}
	}
}