using System;

using Junior.Common;

namespace Junior.Ddd.DomainModel
{
	/// <summary>
	/// Extensions for entity types.
	/// </summary>
	public static class EntityExtensions
	{
		/// <summary>
		/// Generates a <see cref="LazyEntity{TEntity}"/> instance that is pre-initialized with an entity.
		/// </summary>
		/// <param name="entity">An entity.</param>
		/// <returns>A <see cref="LazyEntity{TEntity}"/> instance that is pre-initialized with an entity</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="entity"/> is null.</exception>
		public static LazyEntity<TEntity> Lazy<TEntity>(this TEntity entity)
			where TEntity : class
		{
			entity.ThrowIfNull("entity");

			return new LazyEntity<TEntity>(entity);
		}
	}
}