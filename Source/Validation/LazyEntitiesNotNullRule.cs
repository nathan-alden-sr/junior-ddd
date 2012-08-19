using System;
using System.Collections.Generic;

using Junior.Ddd.DomainModel;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Validates that a <see cref="LazyEntities{TEntity}"/> instance is not null and also that if its value has been loaded, the value is not null.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	/// <typeparam name="TEntity"></typeparam>
	public class LazyEntitiesNotNullRule<TValidationError, TEntity> : NotNullRule<TValidationError, object>
		where TValidationError : IComparable
		where TEntity : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LazyEntitiesNotNullRule{TValidationError,TEntity}"/> class.
		/// </summary>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public LazyEntitiesNotNullRule(TValidationError validationError, LazyEntities<TEntity> value, params IRule<TValidationError>[] dependsOn)
			: base(validationError, GetValueToTest(value), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LazyEntitiesNotNullRule{TValidationError,TEntity}"/> class.
		/// </summary>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public LazyEntitiesNotNullRule(TValidationError validationError, LazyEntities<TEntity> value, TValidationError stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: base(validationError, GetValueToTest(value), stopValidatingOn, dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LazyEntitiesNotNullRule{TValidationError,TEntity}"/> class.
		/// </summary>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to one in <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public LazyEntitiesNotNullRule(TValidationError validationError, LazyEntities<TEntity> value, IEnumerable<TValidationError> stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: base(validationError, GetValueToTest(value), stopValidatingOn, dependsOn)
		{
		}

		private static object GetValueToTest(LazyEntities<TEntity> value)
		{
			return value == null || !value.IsValueCreated ? value : value.Value;
		}
	}
}