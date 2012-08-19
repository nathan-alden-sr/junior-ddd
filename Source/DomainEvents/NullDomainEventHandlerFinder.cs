using System.Collections.Generic;
using System.Linq;

namespace Junior.Ddd.DomainEvents
{
	/// <summary>
	/// Never returns domain event handlers for any domain event type.
	/// </summary>
	public class NullDomainEventHandlerFinder : IDomainEventHandlerFinder
	{
		/// <summary>
		/// Finds domain event handlers for a domain event.
		/// </summary>
		/// <typeparam name="TDomainEventHandler">A domain event handler type.</typeparam>
		/// <typeparam name="TDomainEvent">A domain event type.</typeparam>
		/// <returns>Domain event handlers for the specified domain event type.</returns>
		public IEnumerable<TDomainEventHandler> Find<TDomainEventHandler, TDomainEvent>()
			where TDomainEventHandler : IDomainEventHandler<TDomainEvent>
			where TDomainEvent : IDomainEvent
		{
			return Enumerable.Empty<TDomainEventHandler>();
		}
	}
}