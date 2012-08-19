using System.Collections.Generic;
using System.Linq;

using Junior.Ddd.DomainModel;

using NUnit.Framework;

namespace Junior.Ddd.UnitTests.DomainModel
{
	public static class LazyEntitiesTester
	{
		[TestFixture]
		public class When_constructing_with_value_instead_of_func
		{
			[Test]
			public void Must_initialize_value_immediately()
			{
				var systemUnderTest = new LazyEntities<object>(Enumerable.Empty<object>());

				Assert.That(systemUnderTest.IsValueCreated, Is.True);
			}
		}

		[TestFixture]
		public class When_retrieving_lazy_entity_references
		{
			[Test]
			public void Must_initialize_using_entities_supplied_in_constructor()
			{
				IEnumerable<object> entities = new[]
					{
						new object(),
						new object()
					};
				LazyEntities<object> systemUnderTest = entities.Lazy();

				Assert.That(systemUnderTest.Value, Is.EquivalentTo(entities));
			}

			[Test]
			public void Must_initialize_using_func_supplied_in_constructor()
			{
				var entities = new[]
					{
						new object(),
						new object()
					};
				var systemUnderTest = new LazyEntities<object>(() => entities);

				Assert.That(systemUnderTest.Value, Is.SameAs(entities));
			}
		}
	}
}