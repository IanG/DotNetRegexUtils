using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DotNetRegexUtils
{
    public class RegexUtils
    {
        public static string ReplaceValues(string input, Dictionary<string,string> values)
        {
            const string pattern = @"\$\{(\w+?)\}";

            MatchEvaluator evaluator = match =>
            {
                string key = match.Groups[1].Value;
                return values.ContainsKey(key) ? values[key] : $"?{key}?";
            };

            return Regex.Replace(input, pattern, evaluator);
        }
    }
}