
//  Questions Marks
//  
//  Have the function QuestionsMarks(str) take the str string parameter,
//  which will contain single digit numbers, letters, and question marks,
//  and check if there are exactly 3 question marks between every pair of two numbers that add up to 10.
//  If so, then your program should return the string true, otherwise it should return the string false.
//  If there aren't any two numbers that add up to 10 in the string, then your program should return false as well.
//  
//  For example: if str is "arrb6???4xxbl5???eee5" then your program should return true because there are exactly 3 question marks
//  between 6 and 4, and 3 question marks between 5 and 5 at the end of the string.


using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace Fiddles.QuestionMarks;


public static class Extensions
{
    //Extension method, because Mono returns true when calling All(predicate) on an empty collection
    public static bool AllOrFalseIfEmpty<T>(
      this IEnumerable<T> source,
      Predicate<T> predicate)
    {
        bool hasAny = false;
        foreach (var item in source)
        {
            hasAny = true;
            if (!predicate(item))
                return false;
        }
        return hasAny;
    }
}
internal class Program
{
    //Helper methods
    private static string BoolToString(bool input) => input ? "true" : "false";
    private static (int digit, int index) ConvertDigitCharactersToInt((char character, int index) input)
    => (digit: Int32.Parse(input.character.ToString()), index: input.index);
    private static int GetSumOfDigits((int digit, int index) first, (int digit, int index) second)
    => first.digit + second.digit;
    private static bool IsDigitPairEqualTo((int digit, int index) first, (int digit, int index) second, int expectedValue)
    => GetSumOfDigits(first, second) == expectedValue;
    private static bool IsPairEqualToTen((int digit, int index) first, (int digit, int index) second)
    => IsDigitPairEqualTo(first, second, 10);
    private static int CountCharacterOccurencesInStringSection(char desiredCharacter, string inputString, int startIndex, int endIndex)
    => inputString.Substring(startIndex, (endIndex - startIndex + 1)).AsEnumerable<char>().Count(x => x.Equals(desiredCharacter));
    private static int CountQuestionMarkOccurencesInStringSection(string inputString, int startIndex, int endIndex)
    => CountCharacterOccurencesInStringSection('?', inputString, startIndex, endIndex);
    private static bool IsQuestionMarkInStringThree(string inputString, int startIndex, int endIndex)
    => CountQuestionMarkOccurencesInStringSection(inputString, startIndex, endIndex).Equals(3);

    public static string QuestionsMarks(string inputString)
    {
        bool result = false;

        //Create collection of tuples representing values and positions of all the numbers in the string.
        //First Select creates tuples from all characters (as we need indexes later)
        //Where filters out all non-numbers
        //Second Select converts characters to ints, safe to use, as non-digit chars are already filtered out
        List<(int digit, int index)> digits = inputString.Select((c, i) => (character: c, index: i))
                                                     .Where(x => Char.IsDigit(x.character))
                                                                                   .Select(x => ConvertDigitCharactersToInt(x))
                                                                                   .ToList();

        //We need pairs of digits, so strings with 1 or no digits are a failure already
        //Then we create pairs of consecutive digits and their indices
        //Then filter out those that don't add up to 10
        //If the collection isn't empty - check whether the substring between the two numbers contains exactly three question marks. Return false on empty collection, or when any of substrings doesn't fit criteria.
        result = (digits.Count < 2) ?
                              false :
                              digits.Zip(digits.Skip(1), (first, second) => (first, second))
                                                .Where(pair => IsPairEqualToTen(pair.Item1, pair.Item2))
                                                .AllOrFalseIfEmpty<((int digit, int index) first, (int digit, int index) second)>(
                                      pair => IsQuestionMarkInStringThree(inputString, pair.Item1.Item2, pair.Item2.Item2));

        return BoolToString(result);
    }

    public static void Main() => Console.WriteLine(QuestionsMarks(Console.ReadLine()));
}


