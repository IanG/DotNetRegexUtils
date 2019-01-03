# DotNetRegexUtils

## Introduction

This is a simple project to explore how classes within the System.Text.RegularExpressions namespace can be used to perform template-based substitutions on text.  

A use case for such functionality could be dynamic email body generation based upon a template containing placeholders and a supplied dictionary of Name/Value pairs.  This goes beyond typical .NET string interpolation of known variables at runtime.

## How Does It Work ?

Template text can define placeholders for values using the following format

```
${<field-name>}
```
An example template could be something like:

```
Hello ${title} ${surname}.\n\n${forename}, you scored ${course_score} on the course \'${course_name}\'."
```

A placeholder for a field can occur any number of times within the template.

The application makes use of a regular expression that performs a match to locate fields and a capturing group to obtain the field name within the match.

```
\$\{(\w+?)\}
```

When the Regex.Replace() method is called the application makes use of a System.Text.RegularExpressions.MatchEvaluator delegate.  The delegate attempts to locate the value from the capture group as a Key within the dictionary and, if found, replaces it with the Value of the dictionary item.

Doing this in .NET is somewhat more straight forward than using Java's RegEx packages as you can see here.

```
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
```

In Java you would have to do something like:

```
public static String replaceValues(String input, HashMap<String, String> values)
{        
    StringBuffer formattedMessage = new StringBuffer();
 
    Pattern pattern = Pattern.compile("\\$\\{(\\w+?)\\}");
    Matcher matcher = pattern.matcher(input);
           
    while(matcher.find())
    {
        String key = matcher.group(1);
        String replacement = values.containsKey(key) ? values.get(key) : "?{" + key + "}";
       
        matcher.appendReplacement(formattedMessage, Matcher.quoteReplacement(replacement));
    }
    matcher.appendTail(formattedMessage);
                      
    return formattedMessage.toString();
}
```
## The Code

### Getting The Code

You can get the code by cloning this repository with:

```
git clone https://github.com/IanG/DotNetRegexUtils.git
```

### Building The Application

From the base directory build the application with:

```
dotnet build
```

You will see build output for both the application and it's tests.

### Running The Tests

Tests for the project are housed within the DotNetRegexUtilsTests directory.  To run the tests switch to the directory and type:

```
dotnet test
```
### Running The Application

The application is housed wtihin the DotNetRegexUtils directory.  To run the application switch to the directory and type:

```
dotnet run
```

When you run the application you will see the following output

```
Input:

Hello ${title} ${surname}.

${forename}, you scored ${course_score} on the course '${course_name}'.

Values:

title = Mr
forename = John
surname = Smith
course_name = C# programming
course_score = 87/100

Output:

Hello Mr Smith.

John, you scored 87/100 on the course 'C# programming'.
```

- **Input** - shows the template string that will be used as the source of the RegEx replace.
- **Values** - shows the contents of the System.Collections.Generic.Dictionary<string, string>() containing Name/Value pairs which represent the fields and their values. 
- **Output** - shows the resulting string with the placeholders replaced by the values found within the dictionary.
