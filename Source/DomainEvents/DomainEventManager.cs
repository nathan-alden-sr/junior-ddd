using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Junior.Ddd.DomainEvents
{
	/// <summary>
	/// Registers and raises domain events and invokes global and thread-local delegates when domain events are raised.
	/// <see cref="DomainEventManager"/> is thread-safe.
	/// </summary>
	public class DomainEventManager
	{
		/// <summary>
		/// The singleton instance of <see cref="DomainEventManager"/>.
		/// </summary>
		public static readonly DomainEventManager Instance = new DomainEventManager();

		private readonly List<Delegate> _globalDelegates = new List<Delegate>();
		private readonly object _lockObject = new object();
		private readonly ThreadLocal<List<Delegate>> _threadLocalDelegates = new ThreadLocal<List<Delegate>>(() => new List<Delegate>());

		private DomainEventManager()
		{
			DomainEventHandlerFinder = new NullDomainEventHandlerFinder();
		}

		/// <summary>
		/// Gets or sets the domain event handler finder to use for finding domain event handlers.
		/// Setting this property to null prevents domain events from being handled.
		/// </summary>
		public IDomainEventHandlerFinder DomainEventHandlerFinder
		{
			get;
			set;
		}

		/// <summary>
		/// Registers a delegate for the specified domain event type. The delegate registration is application-scoped.
		/// </summary>
		/// <param name="delegate">A delegate to be executed when a domain event of type <typeparamref name="TDomainEvent"/> is raised.</param>
		/// <typeparam name="TDomainEvent">A domain event type.</typeparam>
		public void RegisterGlobalDelegate<TDomainEvent>(Action<TDomainEvent> @delegate)
			where TDomainEvent : IDomainEvent
		{
			lock (_lockObject)
			{
				_globalDelegates.Add(@delegate);
			}
		}

		/// <summary>
		/// Registers a delegate for the specified domain event type. The delegate registration is thread-local.
		/// </summary>
		/// <param name="delegate">A delegate to be executed when a domain event of type <typeparamref name="TDomainEvent"/> is raised.</param>
		/// <typeparam name="TDomainEvent">A domain event type.</typeparam>
		public void RegisterThreadLocalDelegate<TDomainEvent>(Action<TDomainEvent> @delegate)
			where TDomainEvent : IDomainEvent
		{
			_threadLocalDelegates.Value.Add(@delegate);
		}

		/// <summary>
		/// Clears all global delegate registrations as well as delegate registrations on the current thread.
		/// Equivalent to calling both <see cref="ClearGlobalDelegateRegistrations"/> and <see cref="ClearThreadLocalDelegateRegistrations"/>.
		/// </summary>
		public void ClearAllDelegateRegistrations()
		{
			lock (_lockObject)
			{
				_globalDelegates.Clear();
				_threadLocalDelegates.Value.Clear();
			}
		}

		/// <summary>
		/// Clears all global delegate registrations.
		/// </summary>
		public void ClearGlobalDelegateRegistrations()
		{
			lock (_lockObject)
			{
				_globalDelegates.Clear();
			}
		}

		/// <summary>
		/// Clears all delegate registrations on the current thread.
		/// </summary>
		public void ClearThreadLocalDelegateRegistrations()
		{
			_threadLocalDelegates.Value.Clear();
		}

		/// <summary>
		/// Raises the specified domain event. Domain event handlers will be located using <see cref="DomainEventHandlerFinder"/> and all registered
		/// delegates for the domain event of type <typeparamref name="TDomainEvent"/> will be invoked. After domain events are handled,
		/// global followed by thread-local delegates registered for <typeparamref name="TDomainEvent"/> are invoked.
		/// </summary>
		/// <param name="domainEvent">A domain event.</param>
		/// <typeparam name="TDomainEvent">A domain event type.</typeparam>
		public void Raise<TDomainEvent>(TDomainEvent domainEvent)
			where TDomainEvent : IDomainEvent
		{
			if (DomainEventHandlerFinder != null)
			{
				foreach (IDomainEventHandler<TDomainEvent> domainEventHandler in DomainEventHandlerFinder.Find<IDomainEventHandler<TDomainEvent>, TDomainEvent>())
				{
					domainEventHandler.Handle(domainEvent);
				}
			}

			InvokeGlobalDelegates(domainEvent);
			InvokeThreadLocalDelegates(domainEvent);
		}

		private void InvokeGlobalDelegates<TDomainEvent>(TDomainEvent domainEvent)
			where TDomainEvent : IDomainEvent
		{
			Action<TDomainEvent>[] delegates;

			lock (_lockObject)
			{
				delegates = _globalDelegates
					.OfType<Action<TDomainEvent>>()
					.ToArray();
			}
			foreach (Action<TDomainEvent> @delegate in delegates)
			{
				@delegate(domainEvent);
			}
		}

		private void InvokeThreadLocalDelegates<TDomainEvent>(TDomainEvent domainEvent)
			where TDomainEvent : IDomainEvent
		{
			IEnumerable<Action<TDomainEvent>> delegates = _threadLocalDelegates.Value.OfType<Action<TDomainEvent>>();

			foreach (Action<TDomainEvent> @delegate in delegates)
			{
				@delegate(domainEvent);
			}
		}
	}
}