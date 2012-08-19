using System;
using System.Linq;

using Junior.Common;
using Junior.Ddd.Validation;

using NUnit.Framework;

namespace Junior.Ddd.UnitTests.Validation
{
	public static class DateTimeKindRuleTester
	{
		[TestFixture]
		public class When_testing_datetime_that_does_not_match_kind
		{
			[Test]
			public void Must_return_validation_error()
			{
				Assert.That(new DateTimeKindRule<string>(DateTimeKind.Utc, "Error", DateTime.Now.AsLocal()).Validate().ToArray(), Is.EquivalentTo(new[] { "Error" }));
			}
		}

		[TestFixture]
		public class When_testing_datetime_that_matches_match_kind
		{
			[Test]
			public void Must_not_return_validation_error()
			{
				Assert.That(new DateTimeKindRule<string>(DateTimeKind.Local, "Error", DateTime.Now.AsLocal()).Validate(), Is.Empty);
			}
		}
	}
}