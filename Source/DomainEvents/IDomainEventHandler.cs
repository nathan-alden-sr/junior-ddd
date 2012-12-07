namespace Junior.Ddd.DomainEvents
{
	/// <summary>
	/// Represents a way to handle a domain event.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDomainEventHandler<in T>
		where T : IDomainEvent
	{
		/// <summary>
		/// Handles a domain event.
		/// </summary>
		/// <param name="domainEvent">A domain event.</param>
		void Handle(T domainEvent);
	}
}