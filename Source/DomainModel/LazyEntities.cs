using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Junior.Ddd.DomainModel
{
	/// <summary>
	/// A set of entity relationships that will only be loaded when the set is accessed.
	/// </summary>
	public class LazyEntities<TEntity> : Lazy<IEnumerable<TEntity>>, IEnumerable<TEntity>
		where TEntity : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LazyEntities{TEntity}"/> class with a pre-initialized set of entities.
		/// </summary>
		/// <param name="entities"></param>
		public LazyEntities(IEnumerable<TEntity> entities)
			: this(() => entities)
		{
			// Immediately initializes Value
#pragma warning disable 168
			IEnumerable<TEntity> enumerable = Value;
#pragma warning restore 168
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LazyEntities{TEntity}"/> class.
		/// </summary>
		/// <param name="valueDelegate">A delegate that will be called when the entities are accessed.</param>
		public LazyEntities(Func<IEnumerable<TEntity>> valueDelegate)
			: base(valueDelegate)
		{
		}

		/// <summary>
		/// Gets an instance of <see cref="LazyEntities{TEntity}"/> that contains no entities.
		/// </summary>
		public static LazyEntities<TEntity> Empty
		{
			get
			{
				return new LazyEntities<TEntity>(Enumerable.Empty<TEntity>());
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<TEntity> GetEnumerator()
		{
			return Value.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}