using System;
using System.Collections.Generic;

using Junior.Common;

namespace Junior.Ddd.DomainModel
{
	/// <summary>
	/// Extensions for the <see cref="IEnumerable{T}"/> type.
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Generates a <see cref="LazyEntities{TEntity}"/> instance that is pre-initialized with entities.
		/// </summary>
		/// <param name="entities">Entities.</param>
		/// <returns>A <see cref="LazyEntities{TEntity}"/> instance that is pre-initialized with entities.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="entities"/> is null.</exception>
		public static LazyEntities<TEntity> Lazy<TEntity>(this IEnumerable<TEntity> entities)
			where TEntity : class
		{
			entities.ThrowIfNull("entities");

			return new LazyEntities<TEntity>(entities);
		}
	}
}