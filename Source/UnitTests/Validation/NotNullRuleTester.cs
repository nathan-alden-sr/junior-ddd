using System.Linq;

using Junior.Ddd.Validation;

using NUnit.Framework;

namespace Junior.Ddd.UnitTests.Validation
{
	public static class NotNullRuleTester
	{
		[TestFixture]
		public class When_testing_not_null_value
		{
			[Test]
			public void Must_not_return_validation_error()
			{
				Assert.That(new NotNullRule<string, string>("Error", "").Validate(), Is.Empty);
			}
		}

		[TestFixture]
		public class When_testing_null_value
		{
			[Test]
			public void Must_return_validation_error()
			{
				Assert.That(new NotNullRule<string, string>("Error", null).Validate().ToArray(), Is.EquivalentTo(new[] { "Error" }));
			}
		}
	}
}