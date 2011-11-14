using System.Linq;

using Junior.Ddd.DomainModel;
using Junior.Ddd.Validation;

using NUnit.Framework;

namespace Junior.Ddd.UnitTests.Validation
{
	public static class LazyEntityNotNullRuleTester
	{
		[TestFixture]
		public class When_testing_lazy_entity_that_contains_null_value
		{
			[Test]
			public void Must_return_validation_error()
			{
				Assert.That(new LazyEntityNotNullRule<string, object>("Error", new LazyEntity<object>((object)null)).Validate().ToArray(), Is.EquivalentTo(new[] { "Error" }));
			}
		}

		[TestFixture]
		public class When_testing_null_lazy_entity_reference
		{
			[Test]
			public void Must_return_validation_error()
			{
				Assert.That(new LazyEntityNotNullRule<string, object>("Error", null).Validate().ToArray(), Is.EquivalentTo(new[] { "Error" }));
			}
		}

		[TestFixture]
		public class When_testing_uninitialized_non_null_lazy_entity_reference
		{
			[Test]
			public void Must_not_return_validation_error()
			{
				Assert.That(new LazyEntityNotNullRule<string, object>("Error", new LazyEntity<object>(1)).Validate(), Is.Empty);
			}
		}
	}
}