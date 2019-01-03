using System;
using System.Collections;
using System.Collections.Generic;

namespace DotNetRegexUtils
{
	class Program
	{
		static void Main(string[] args)
		{
			// --------------------------------------------------------------------------------
			// Build the Dictionary of values
			// --------------------------------------------------------------------------------
			
			Dictionary<string, string> values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
			{
				{ "title", "Mr" },
				{ "forename", "John" },
				{ "surname", "Smith" },
				{ "course_name", "C# programming"},
				{ "course_score", "87/100" }
			};

			// --------------------------------------------------------------------------------
			// Create the input template with parameters in the format ${<parameter-name>}
			// -----------------------------------------------------s---------------------------

			string input = "Hello ${title} ${surname}.\n\n${forename}, you scored ${course_score} on the course \'${course_name}\'.";

			Console.WriteLine($"\nInput:\n\n{input}\n");

			// --------------------------------------------------------------------------------
			// Show the Dictionary content
			// --------------------------------------------------------------------------------

			Console.WriteLine("Values:\n");

			foreach(KeyValuePair<string, string> item in values)
			{
				Console.WriteLine($"{item.Key} = {item.Value}");
			}

			// --------------------------------------------------------------------------------
			// Show the output
			// --------------------------------------------------------------------------------

			string output = RegexUtils.ReplaceValues(input, values);

			Console.WriteLine($"\nOutput:\n\n{output}\n");
		}
	}
}
