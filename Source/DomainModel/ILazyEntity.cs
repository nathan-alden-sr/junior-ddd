namespace Junior.Ddd.DomainModel
{
	/// <summary>
	/// Represents an entity relationship that will only be loaded when it is accessed.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface ILazyEntity<out TEntity>
		where TEntity : class
	{
		/// <summary>
		/// Gets a value determining if the entity has been loaded.
		/// </summary>
		bool IsValueCreated
		{
			get;
		}
		/// <summary>
		/// Gets the entity.
		/// </summary>
		TEntity Value
		{
			get;
		}
	}
}