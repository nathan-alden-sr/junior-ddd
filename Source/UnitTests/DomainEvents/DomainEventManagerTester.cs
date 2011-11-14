using Junior.Ddd.DomainEvents;

using NUnit.Framework;

using Rhino.Mocks;

namespace Junior.Ddd.UnitTests.DomainEvents
{
	public static class DomainEventManagerTester
	{
		public class DomainEvent : IDomainEvent
		{
		}

		[TestFixture]
		public class When_clearing_all_registered_delegates
		{
			[TearDown]
			public void TearDown()
			{
				DomainEventManager.Instance.ClearAllDelegateRegistrations();
				DomainEventManager.Instance.DomainEventHandlerFinder = new NullDomainEventHandlerFinder();
			}

			[Test]
			public void Must_clear_all_registered_delegates()
			{
				int globalCount = 0;
				int threadLocalCount = 0;

				DomainEventManager.Instance.RegisterGlobalDelegate<DomainEvent>(arg => globalCount++);
				DomainEventManager.Instance.RegisterThreadLocalDelegate<DomainEvent>(arg => threadLocalCount++);
				DomainEventManager.Instance.Raise(new DomainEvent());

				Assert.That(globalCount, Is.EqualTo(1));
				Assert.That(threadLocalCount, Is.EqualTo(1));

				DomainEventManager.Instance.ClearAllDelegateRegistrations();
				DomainEventManager.Instance.Raise(new DomainEvent());

				Assert.That(globalCount, Is.EqualTo(1));
				Assert.That(threadLocalCount, Is.EqualTo(1));
			}
		}

		[TestFixture]
		public class When_clearing_registered_global_delegates
		{
			[TearDown]
			public void TearDown()
			{
				DomainEventManager.Instance.ClearAllDelegateRegistrations();
				DomainEventManager.Instance.DomainEventHandlerFinder = new NullDomainEventHandlerFinder();
			}

			[Test]
			public void Must_clear_all_registered_delegates()
			{
				int globalCount = 0;
				int threadLocalCount = 0;

				DomainEventManager.Instance.RegisterGlobalDelegate<DomainEvent>(arg => globalCount++);
				DomainEventManager.Instance.RegisterThreadLocalDelegate<DomainEvent>(arg => threadLocalCount++);
				DomainEventManager.Instance.Raise(new DomainEvent());

				Assert.That(globalCount, Is.EqualTo(1));
				Assert.That(threadLocalCount, Is.EqualTo(1));

				DomainEventManager.Instance.ClearGlobalDelegateRegistrations();
				DomainEventManager.Instance.Raise(new DomainEvent());

				Assert.That(globalCount, Is.EqualTo(1));
				Assert.That(threadLocalCount, Is.EqualTo(2));
			}
		}

		[TestFixture]
		public class When_clearing_registered_threadlocal_delegates
		{
			[TearDown]
			public void TearDown()
			{
				DomainEventManager.Instance.ClearAllDelegateRegistrations();
				DomainEventManager.Instance.DomainEventHandlerFinder = new NullDomainEventHandlerFinder();
			}

			[Test]
			public void Must_clear_all_registered_delegates()
			{
				int globalCount = 0;
				int threadLocalCount = 0;

				DomainEventManager.Instance.RegisterGlobalDelegate<DomainEvent>(arg => globalCount++);
				DomainEventManager.Instance.RegisterThreadLocalDelegate<DomainEvent>(arg => threadLocalCount++);
				DomainEventManager.Instance.Raise(new DomainEvent());

				Assert.That(globalCount, Is.EqualTo(1));
				Assert.That(threadLocalCount, Is.EqualTo(1));

				DomainEventManager.Instance.ClearThreadLocalDelegateRegistrations();
				DomainEventManager.Instance.Raise(new DomainEvent());

				Assert.That(globalCount, Is.EqualTo(2));
				Assert.That(threadLocalCount, Is.EqualTo(1));
			}
		}

		[TestFixture]
		public class When_raising_an_event_with_a_domain_event_handler_finder_provided
		{
			[TearDown]
			public void TearDown()
			{
				DomainEventManager.Instance.ClearAllDelegateRegistrations();
				DomainEventManager.Instance.DomainEventHandlerFinder = new NullDomainEventHandlerFinder();
			}

			[Test]
			public void Must_resolve_domain_event_handler_using_domain_event_handler_finder()
			{
				var domainEventHandlerFinder = MockRepository.GenerateMock<IDomainEventHandlerFinder>();
				var domainEventHandler = MockRepository.GenerateMock<IDomainEventHandler<DomainEvent>>();

				domainEventHandlerFinder.Stub(arg => arg.Find<IDomainEventHandler<DomainEvent>, DomainEvent>()).Return(
					new[]
						{
							domainEventHandler
						});

				DomainEventManager.Instance.DomainEventHandlerFinder = domainEventHandlerFinder;
				DomainEventManager.Instance.Raise(new DomainEvent());

				domainEventHandlerFinder.AssertWasCalled(arg => arg.Find<IDomainEventHandler<DomainEvent>, DomainEvent>());
			}
		}

		[TestFixture]
		public class When_raising_an_event_with_a_global_delegate_registered
		{
			[TearDown]
			public void TearDown()
			{
				DomainEventManager.Instance.ClearAllDelegateRegistrations();
				DomainEventManager.Instance.DomainEventHandlerFinder = new NullDomainEventHandlerFinder();
			}

			[Test]
			public void Must_call_delegate_when_handling_domain_event()
			{
				int count = 0;

				DomainEventManager.Instance.RegisterGlobalDelegate<DomainEvent>(arg => count++);
				DomainEventManager.Instance.Raise(new DomainEvent());

				Assert.That(count, Is.EqualTo(1));
			}
		}

		[TestFixture]
		public class When_raising_an_event_with_a_threadlocal_delegate_registered
		{
			[TearDown]
			public void TearDown()
			{
				DomainEventManager.Instance.ClearAllDelegateRegistrations();
				DomainEventManager.Instance.DomainEventHandlerFinder = new NullDomainEventHandlerFinder();
			}

			[Test]
			public void Must_call_delegate_when_handling_domain_event()
			{
				int count = 0;

				DomainEventManager.Instance.RegisterThreadLocalDelegate<DomainEvent>(arg => count++);
				DomainEventManager.Instance.Raise(new DomainEvent());

				Assert.That(count, Is.EqualTo(1));
			}
		}
	}
}