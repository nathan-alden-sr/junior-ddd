using Junior.Ddd.DomainModel;

using NUnit.Framework;

namespace Junior.Ddd.UnitTests.DomainModel
{
	public static class LazyEntityTester
	{
		[TestFixture]
		public class When_constructing_with_value_instead_of_func
		{
			[Test]
			public void Must_initialize_value_immediately()
			{
				var systemUnderTest = new LazyEntity<object>(new object());

				Assert.That(systemUnderTest.IsValueCreated, Is.True);
			}
		}

		[TestFixture]
		public class When_retrieving_lazy_entity_reference
		{
			[Test]
			public void Must_initialize_using_entity_supplied_in_constructor()
			{
				var entity = new object();
				LazyEntity<object> systemUnderTest = entity.Lazy();

				Assert.That(systemUnderTest.Value, Is.SameAs(entity));
			}

			[Test]
			public void Must_initialize_using_func_supplied_in_constructor()
			{
				var entity = new object();
				var systemUnderTest = new LazyEntity<object>(() => entity);

				Assert.That(systemUnderTest.Value, Is.SameAs(entity));
			}
		}
	}
}