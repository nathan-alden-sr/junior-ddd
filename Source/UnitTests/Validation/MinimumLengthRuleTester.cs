using System;
using System.Linq;

using Junior.Ddd.Validation;

using NUnit.Framework;

namespace Junior.Ddd.UnitTests.Validation
{
	public static class MinimumLengthRuleTester
	{
		[TestFixture]
		public class When_testing_string_that_is_not_too_short
		{
			[Test]
			public void Must_not_return_validation_error()
			{
				Assert.That(new MinimumLengthRule<string>(1, "Error", "12", false).Validate(), Is.Empty);
			}
		}

		[TestFixture]
		public class When_testing_string_that_is_null_with_nulls_allowed
		{
			[Test]
			public void Must_not_return_validation_error()
			{
				Assert.That(new MinimumLengthRule<string>(1, "Error", null, true).Validate(), Is.Empty);
			}
		}

		[TestFixture]
		public class When_testing_string_that_is_null_with_nulls_disallowed
		{
			[Test]
			public void Must_throw_exception()
			{
				Assert.Throws<ArgumentNullException>(() => new MinimumLengthRule<string>(1, "Error", null, false).Validate());
			}
		}

		[TestFixture]
		public class When_testing_string_that_is_too_short
		{
			[Test]
			public void Must_return_validation_error()
			{
				Assert.That(new MinimumLengthRule<string>(1, "Error", "", false).Validate().ToArray(), Is.EquivalentTo(new[] { "Error" }));
			}
		}
	}
}