using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections;
using System.Collections.Generic;

using DotNetRegexUtils;

namespace DotNetRegexUtilsTests
{
	[TestClass]
	public class RegexUtilsTests
	{
		[TestMethod]
		[TestCategory("Regex")]
		public void TestReplaceWithoutValues()
		{
			string input = "There are no replacements";
			string expectedOutput = input;

			string output = RegexUtils.ReplaceValues(input, null);

			Assert.AreEqual(expectedOutput, output, "Should match expected output with no replacements");
		}

		[TestMethod]
		[TestCategory("Regex")]
		public void TestReplaceWithValues()
		{
			string input = "Replace the values ${1}, ${2} and ${3}";
			string expectedOutput = "Replace the values One, Two and Three";

			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
			{
				{ "1", "One" },
				{ "2", "Two" },
				{ "3", "Three" }
			};

			string output = RegexUtils.ReplaceValues(input, dictionary);

			Assert.AreEqual(expectedOutput, output, "Should match expected output with replacements.");
		}

		[TestMethod]
		[TestCategory("Regex")]
		public void TestReplaceWithMissingValue()
		{
			string input = "Replace the values ${1}, ${2}, ${3} and ${4}";
			string expectedUnknownValue = "?4?";
			string expectedOutput = "Replace the values One, Two, Three and " + expectedUnknownValue;

			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
			{
				{ "1", "One" },
				{ "2", "Two" },
				{ "3", "Three" }
			};

			string output = RegexUtils.ReplaceValues(input, dictionary);

			Assert.AreEqual(expectedOutput, output, "Should match expected output with one non-substituted value.");
			Assert.IsTrue(output.Contains(expectedUnknownValue), "Expected output should have expected unknown value");
		}

		[TestMethod]
		[TestCategory("Regex")]
		public void TestReplaceWithMultiReplacementOfValue()
		{
			string input = "Replace the values ${1}, ${2}, ${2} and ${3}";
			string expectedOutput = "Replace the values One, Two, Two and Three";

			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
			{
				{ "1", "One" },
				{ "2", "Two" },
				{ "3", "Three" }
			};

			string output = RegexUtils.ReplaceValues(input, dictionary);

			Assert.AreEqual(expectedOutput, output, "Should match expected output with replacements.");
		}

		[TestMethod]
		[TestCategory("Regex")]
		public void TestReplaceWithMalformedPlaceholder()
		{
			string input = "Replace the values ${1}, ${2) and ${3}";
			string expectedOutput = "Replace the values One, ${2) and Three";

			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
			{
				{ "1", "One" },
				{ "2", "Two" },
				{ "3", "Three" }
			};

			string output = RegexUtils.ReplaceValues(input, dictionary);

			Assert.AreEqual(expectedOutput, output, "Should match expected output containing malformed placeholder.");
		}
	}
}
