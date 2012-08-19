using System.Linq;

using Junior.Ddd.Validation;

using NUnit.Framework;

namespace Junior.Ddd.UnitTests.Validation
{
	public static class NullableMaximumValueRuleTester
	{
		[TestFixture]
		public class When_testing_value_that_is_not_too_large
		{
			[Test]
			public void Must_not_return_validation_error()
			{
				Assert.That(new NullableMaximumValueRule<string, int>(1, "Error", 0).Validate(), Is.Empty);
			}
		}

		[TestFixture]
		public class When_testing_value_that_is_too_large
		{
			[Test]
			public void Must_return_validation_error()
			{
				Assert.That(new NullableMaximumValueRule<string, int>(1, "Error", 2).Validate().ToArray(), Is.EquivalentTo(new[] { "Error" }));
			}
		}
	}
}