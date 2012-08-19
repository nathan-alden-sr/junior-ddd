using System;

namespace Junior.Ddd.DomainModel
{
	/// <summary>
	/// An entity relationship that will only be loaded when it is accessed.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public class LazyEntity<TEntity> : Lazy<TEntity>, ILazyEntity<TEntity>
		where TEntity : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LazyEntity{TEntity}"/> class with a pre-initialized entity.
		/// </summary>
		/// <param name="entity">An entity.</param>
		public LazyEntity(TEntity entity)
			: this(() => entity)
		{
			// Immediately initializes Value
#pragma warning disable 168
			TEntity value = Value;
#pragma warning restore 168
		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="LazyEntity{TEntity}"/> class.
		/// </summary>
		/// <param name="valueDelegate">A delegate that will be called when the entity is accessed.</param>
		public LazyEntity(Func<TEntity> valueDelegate)
			: base(valueDelegate)
		{
		}
	}
}