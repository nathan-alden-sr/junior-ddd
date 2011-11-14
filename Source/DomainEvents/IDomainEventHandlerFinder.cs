using System.Collections.Generic;

namespace Junior.Ddd.DomainEvents
{
	/// <summary>
	/// Represents a way to find domain event handlers for a domain event.
	/// </summary>
	public interface IDomainEventHandlerFinder
	{
		/// <summary>
		/// Finds domain event handlers for a domain event.
		/// </summary>
		/// <typeparam name="TDomainEventHandler">A domain event handler type.</typeparam>
		/// <typeparam name="TDomainEvent">A domain event type.</typeparam>
		/// <returns>Domain event handlers for the specified domain event type.</returns>
		IEnumerable<TDomainEventHandler> Find<TDomainEventHandler, TDomainEvent>()
			where TDomainEventHandler : IDomainEventHandler<TDomainEvent>
			where TDomainEvent : IDomainEvent;
	}
}