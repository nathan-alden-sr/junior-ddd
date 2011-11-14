using System;
using System.Linq;

using Junior.Ddd.Validation;

using NUnit.Framework;

namespace Junior.Ddd.UnitTests.Validation
{
	public static class CharactersRuleTester
	{
		[TestFixture]
		public class When_testing_string_that_does_not_match_regex_expression
		{
			[Test]
			public void Must_return_validation_error()
			{
				Assert.That(new RegexRule<string>(@"^\d+$", "Error", "12a45", false).Validate().ToArray(), Is.EquivalentTo(new[] { "Error" }));
			}
		}

		[TestFixture]
		public class When_testing_string_that_is_null_with_nulls_allowed
		{
			[Test]
			public void Must_not_return_validation_error()
			{
				Assert.That(new RegexRule<string>(@"^\d+$", "Error", null, true).Validate(), Is.Empty);
			}
		}

		[TestFixture]
		public class When_testing_string_that_is_null_with_nulls_disallowed
		{
			[Test]
			public void Must_throw_exception()
			{
				Assert.Throws<ArgumentNullException>(() => new RegexRule<string>(@"^\d+$", "Error", null, false).Validate());
			}
		}

		[TestFixture]
		public class When_testing_string_that_matches_regex_expression
		{
			[Test]
			public void Must_return_validation_error()
			{
				Assert.That(new RegexRule<string>(@"^\d+$", "Error", "12345", false).Validate().ToArray(), Is.Empty);
			}
		}
	}
}