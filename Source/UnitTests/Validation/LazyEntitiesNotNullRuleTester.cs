using System.Collections.Generic;
using System.Linq;

using Junior.Ddd.DomainModel;
using Junior.Ddd.Validation;

using NUnit.Framework;

namespace Junior.Ddd.UnitTests.Validation
{
	public static class LazyEntitiesNotNullRuleTester
	{
		[TestFixture]
		public class When_testing_lazy_entities_that_contains_null_value
		{
			[Test]
			public void Must_return_validation_error()
			{
				Assert.That(new LazyEntitiesNotNullRule<string, object>("Error", new LazyEntities<object>((IEnumerable<object>)null)).Validate().ToArray(), Is.EquivalentTo(new[] { "Error" }));
			}
		}

		[TestFixture]
		public class When_testing_null_lazy_entities_reference
		{
			[Test]
			public void Must_return_validation_error()
			{
				Assert.That(new LazyEntitiesNotNullRule<string, object>("Error", null).Validate().ToArray(), Is.EquivalentTo(new[] { "Error" }));
			}
		}

		[TestFixture]
		public class When_testing_uninitialized_non_null_lazy_entities_reference
		{
			[Test]
			public void Must_not_return_validation_error()
			{
				Assert.That(new LazyEntitiesNotNullRule<string, object>("Error", new LazyEntities<object>(() => new object[] { 1 })).Validate(), Is.Empty);
			}
		}
	}
}