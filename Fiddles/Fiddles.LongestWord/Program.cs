//  Longest Word
//  
//  Have the function LongestWord(sen) take the sen parameter being passed and return the longest word in the string.
//  If there are two or more words that are the same length, return the first word from the string with that length.
//  Ignore punctuation and assume sen will not be empty. Words may also contain numbers, for example "Hello world123 567"


using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Fiddles.LongestWord;

public static class StringExtensions
{
    public static string GetLongestWordInArray(this string[] inputArray) => inputArray.OrderByDescending(x => x.Length).First();
}

public class Program
{
    private static readonly List<(string input, string expectedResult)> _testCases = new()
    {
        ("fun&!! time", "time"),
        ("I love dogs", "love"),
        ("Hello world123 567", "world123"),
        ("A b c d e f g", "A"),
        ("This is a test-case.", "This"),
        ("Longer words are better!", "Longer"),
        ("Punctuation, should be ignored.", "Punctuation"),
        ("123 4567 89", "4567"),
        ("Equal size words here", "Equal"),
    };

    private static Func<char, char> NonLettersToSpaces = x => Char.IsLetterOrDigit(x) ? x : ' ';
    public static string CleanseNonLetters(string input) => new String(input.ToArray<char>().Select(NonLettersToSpaces).ToArray<char>());

    private static char[] _separators = new char[] { ' ' };
    public static string LongestWord(string sen) => CleanseNonLetters(sen).Split(_separators, StringSplitOptions.RemoveEmptyEntries).GetLongestWordInArray();

    public static void TestMethod()
    {
        foreach (var (input, expectedResult) in _testCases)
        {
            var result = LongestWord(input);
            Console.WriteLine($"Input: \"{input}\" | Expected: \"{expectedResult}\" | Result: \"{result}\" | Passed: {result == expectedResult}");
        }
    }

    public static void Main() => TestMethod();
}
