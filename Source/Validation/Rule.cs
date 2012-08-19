using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Base class for all validation rules.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	public abstract class Rule<TValidationError> : IRule<TValidationError>
		where TValidationError : IComparable
	{
		private readonly IEnumerable<IRule<TValidationError>> _dependsOn;
		private readonly IEnumerable<TValidationError> _stopValidatingOn;

		/// <summary>
		/// Initializes a new instance of the <see cref="Rule{TValidationError}"/> class.
		/// </summary>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		protected Rule(params IRule<TValidationError>[] dependsOn)
			: this(Enumerable.Empty<TValidationError>(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Rule{TValidationError}"/> class.
		/// </summary>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		protected Rule(TValidationError stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: this(stopValidatingOn.ToEnumerable(), dependsOn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Rule{TValidationError}"/> class.
		/// </summary>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to one in <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		protected Rule(IEnumerable<TValidationError> stopValidatingOn, params IRule<TValidationError>[] dependsOn)
		{
			_stopValidatingOn = stopValidatingOn ?? Enumerable.Empty<TValidationError>();
			_dependsOn = dependsOn ?? Enumerable.Empty<IRule<TValidationError>>();
		}

		/// <summary>
		/// Performs rule validation.
		/// </summary>
		/// <returns>Validation errors generated during validation.</returns>
		public IEnumerable<TValidationError> Validate()
		{
			TValidationError[] dependencyValidationErrors = ValidateDependencies().ToArray();

			return dependencyValidationErrors.Intersect(_stopValidatingOn).Any()
				       ? dependencyValidationErrors
				       : dependencyValidationErrors.Union(OnValidate()).Distinct();
		}

		/// <summary>
		/// Performs rule validation.
		/// </summary>
		/// <returns>Validation errors if the rule failed to validate.</returns>
		protected abstract IEnumerable<TValidationError> OnValidate();

		private IEnumerable<TValidationError> ValidateDependencies()
		{
			return _dependsOn
				.SelectMany(rule => rule.Validate())
				.Distinct();
		}
	}
}