using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

using Junior.Common;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// An exception thrown when rules fail to validate. <see cref="ValidationException{TValidationError}"/> contains the
	/// <typeparamref name="TValidationError"/> values for the validation errors.
	/// </summary>
	/// <typeparam name="TValidationError"></typeparam>
	public class ValidationException<TValidationError> : ApplicationException, IEnumerable<TValidationError>
		where TValidationError : IComparable
	{
		private readonly IEnumerable<TValidationError> _validationErrors;
		// ReSharper disable NotAccessedField.Local
		private string _validationErrorsForDebugger;
		// ReSharper restore NotAccessedField.Local

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException{TValidationError}"/> class.
		/// </summary>
		/// <param name="errors">Validation errors.</param>
		public ValidationException(IEnumerable<TValidationError> errors)
			: base("Object violates one or more rules.")
		{
			_validationErrors = (errors ?? Enumerable.Empty<TValidationError>()).Distinct();
			SetValidationErrorsForDebugger();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException{TValidationError}"/> class.
		/// </summary>
		/// <param name="errors">Validation errors.</param>
		/// <param name="message">The message that describes the error.</param>
		public ValidationException(IEnumerable<TValidationError> errors, string message)
			: base(message)
		{
			_validationErrors = (errors ?? Enumerable.Empty<TValidationError>()).Distinct();
			SetValidationErrorsForDebugger();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException{TValidationError}"/> class.
		/// </summary>
		/// <param name="errors">Validation errors.</param>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public ValidationException(IEnumerable<TValidationError> errors, string message, Exception innerException)
			: base(message, innerException)
		{
			_validationErrors = (errors ?? Enumerable.Empty<TValidationError>()).Distinct();
			SetValidationErrorsForDebugger();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException{TValidationError}"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected ValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>
		/// Returns the validation errors associated with the exception.
		/// </summary>
		[DebuggerDisplay("{_validationErrorsForDebugger}")]
		public IEnumerable<TValidationError> ValidationErrors
		{
			get
			{
				return _validationErrors;
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<TValidationError> GetEnumerator()
		{
			return _validationErrors.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Determines if the specified validation error is contained in this exception.
		/// </summary>
		/// <param name="validationError">A validation error.</param>
		/// <returns>true if <paramref name="validationError"/> is contained in this exception; otherwise, false.</returns>
		public bool ContainsValidationError(TValidationError validationError)
		{
			return _validationErrors.Contains(validationError);
		}

		/// <summary>
		/// Retrieves the exception message
		/// </summary>
		/// <typeparam name="T">The type being validated.</typeparam>
		/// <param name="errors">Validation errors generated during validation.</param>
		/// <returns>A formatted exception message.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="errors"/> is null.</exception>
		protected static string GetMessage<T>(IEnumerable<TValidationError> errors)
		{
			errors.ThrowIfNull("errors");

			return String.Format("{0} violates one or more rules: {1}", typeof(T).FullName, String.Join(", ", errors.Select(arg => arg.ToString())));
		}

		private void SetValidationErrorsForDebugger()
		{
			_validationErrorsForDebugger = String.Join(", ", _validationErrors.Select(arg => arg.ToString()));
		}
	}
}