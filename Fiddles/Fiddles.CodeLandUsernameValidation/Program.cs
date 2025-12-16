//  Have the function CodelandUsernameValidation(str) take the str parameter being passed and determine if the string is a valid username according to the following rules:
//  
//  1.The username is between 4 and 25 characters.
//  2. It must start with a letter.
//  3. It can only contain letters, numbers, and the underscore character.
//  4. It cannot end with an underscore character.
//  
//  If the username is valid then your program should return the string true, otherwise return the string false.

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Fiddles.CodeLandUsernameValidation;
public class MainClass
{

    private static readonly List<(string input, string expectedResult)> testCases = new()
    {
        ("admin"        ,   "true"),
        ("user"         ,   "true"),
        ("null"         ,   "true"),
        ("undefined"    ,   "true"),
        ("root"         ,   "true"),
        ("_admin"       ,   "false"),
        ("_user"        ,   "false"),
        ("_null"        ,   "false"),
        ("_undefined"   ,   "false"),
        ("_root"        ,   "false"),
        ("user_"        ,   "false"),
        ("admin_"       ,   "false"),
        ("us"           ,   "false"),
        ("a"            ,   "false"),
        ("valid_user123",   "true"),
        ("invalid-user!",   "false"),
        ("user name"    ,   "false"),
        ("thisisaverylongusernamethatexceedsthelimit", "false"),
    };
    private static bool isCharUnderscore(char input) => input.Equals('_');

    private static Predicate<char> isAlphaNumericOrUnderscore = x => Char.IsLetterOrDigit(x) || isCharUnderscore(x);
    private static Predicate<char[]> isLengthBetween4and25 = x => (x.Length >= 4) && (x.Length <=25);

    public static string CodelandUsernameValidation(string str)
    {
        const string resultSuccess = "true";
        const string resultFailure = "false";

        char[] strArray = str.ToArray<char>();
        bool isLengthCorrect = isLengthBetween4and25(strArray);
        bool isFirstCharLetter = Char.IsLetter(strArray[0]);
        bool isLastCharacterNotUnderScore = !isCharUnderscore(strArray.Last());
        bool isFullyAlphanumericOrUnderscore = strArray.All(x => isAlphaNumericOrUnderscore(x));

        bool result = isLengthCorrect && 
                      isFirstCharLetter && 
                      isLastCharacterNotUnderScore && 
                      isFullyAlphanumericOrUnderscore;

        return result ? resultSuccess : resultFailure;
    }

    public static void TestMethod() 
        => testCases.ForEach(
            testCase =>
            {
                string result = CodelandUsernameValidation(testCase.input);

                if (result != testCase.expectedResult)
                    Console.WriteLine($"Test failed for input: {testCase.input}. Expected: {testCase.expectedResult}, Got: {result}");
                else
                    Console.WriteLine($"Test passed for input: {testCase.input}. Result: {result} matches the expectations");
            });

    public static void Main() => TestMethod();
}