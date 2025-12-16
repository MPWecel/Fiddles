//  The task is to create a method that converts a numeric rating into a star rating representation.

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fiddles.StarRating;

public class Program
{
    public static List<Tuple<string,string>> inputOutputPairs = new()
    {
        new Tuple<string, string>("-0.1",  "empty empty empty empty empty"),
        new Tuple<string, string>("0.1",  "empty empty empty empty empty"),
        new Tuple<string, string>("0.24", "empty empty empty empty empty"),
        new Tuple<string, string>("0.26", "half empty empty empty empty"),
        new Tuple<string, string>("0.49", "half empty empty empty empty"),
        new Tuple<string, string>("0.5",  "half empty empty empty empty"),
        new Tuple<string, string>("0.51", "half empty empty empty empty"),
        new Tuple<string, string>("0.74", "half empty empty empty empty"),
        new Tuple<string, string>("0.75", "full empty empty empty empty"),
        new Tuple<string, string>("0.76", "full empty empty empty empty"),
        new Tuple<string, string>("0.9",  "full empty empty empty empty"),
        new Tuple<string, string>("1.0",  "full empty empty empty empty"),
        new Tuple<string, string>("1.1",  "full empty empty empty empty"),
        new Tuple<string, string>("1.23", "full empty empty empty empty"),
        new Tuple<string, string>("1.25", "full half empty empty empty"),
        new Tuple<string, string>("1.35", "full half empty empty empty"),
        new Tuple<string, string>("1.5",  "full half empty empty empty"),
        new Tuple<string, string>("1.75", "full full empty empty empty"),
        new Tuple<string, string>("2.15", "full full empty empty empty"),
        new Tuple<string, string>("2.35", "full full half empty empty"),
        new Tuple<string, string>("2.55", "full full half empty empty"),
        new Tuple<string, string>("2.65", "full full half empty empty"),
        new Tuple<string, string>("2.85", "full full full empty empty"),
        new Tuple<string, string>("3.15", "full full full empty empty"),
        new Tuple<string, string>("3.35", "full full full half empty"),
        new Tuple<string, string>("3.55", "full full full half empty"),
        new Tuple<string, string>("3.65", "full full full half empty"),
        new Tuple<string, string>("3.85", "full full full full empty"),
        new Tuple<string, string>("4.15", "full full full full empty"),
        new Tuple<string, string>("4.55", "full full full full half"),
        new Tuple<string, string>("4.65", "full full full full half"),
        new Tuple<string, string>("4.75", "full full full full full"),
        new Tuple<string, string>("4.95", "full full full full full"),
        new Tuple<string, string>("5.00", "full full full full full"),
        new Tuple<string, string>("5.10", "full full full full full")
    };

    public static bool TestMethod()
    {
        bool result = true;

        int iteration = 0;

        foreach (var pair in inputOutputPairs)
        {
            StringBuilder processingMessage = new();
            processingMessage.AppendFormat("Iteration:\t{0}\n\t", ++iteration);

            string processingOutput = StarRating(pair.Item1);
            string expectedOutput = pair.Item2;
            bool singleTestResult = processingOutput.Equals(expectedOutput);

            processingMessage.AppendFormat("Input received:\t{0}\n\tProcessingResult:\t{1}\n\tExpectedResult:\t{2}\n\tTestSuccessful:\t{3}\n", 
                                           pair.Item1, 
                                           processingOutput, 
                                           expectedOutput, 
                                           singleTestResult);
            Console.WriteLine(processingMessage.ToString());

            result &= singleTestResult;
        }

        Console.WriteLine($"FinalTestResult:\t{result}");

        return result;
    }

    public static decimal LimitValuesToRange(decimal input)
    {
        decimal result = input;

        if (result < 0.0M)
            result = 0.0M;

        if (result > 5.0M)
            result = 5.0M;

        return result;
    }

    public static decimal GetNumericRating(string input)
    {
        decimal result;

        bool isParsingSuccessful = Decimal.TryParse(input.Trim(), out result);

        if (!isParsingSuccessful)
            throw new Exception("Invalid input");

        result = LimitValuesToRange(result);


        return result;
    }

    public static decimal RoundDecimalToNearestHalves(decimal input) => Decimal.Multiply(Math.Round((Decimal.Multiply(input, (2.0M))), MidpointRounding.AwayFromZero), (0.5M));     //Rounding to nearest half achieved by doubling the input, rounding the result and halving it afterwards

    public static List<string> CalculateEachStar(decimal input)
    {
        const string full = "full";
        const string half = "half";
        const string empty = "empty";

        List<string> result = new();

        int elementsAdded = 0;
        int fullStarCount = Decimal.ToInt32(Math.Floor(input));
        decimal partialStars = input % 1.0M;
        bool hasHalfStar = partialStars > 0.0M;

        result.AddRange(Enumerable.Repeat(full, fullStarCount));
        elementsAdded += fullStarCount;

        if (hasHalfStar)
        {
            result.Add(half);
            elementsAdded++;
        }

        if (elementsAdded < 5)
        {
            int emptyCount = (5 - elementsAdded);
            result.AddRange(Enumerable.Repeat(empty, emptyCount));
            elementsAdded += emptyCount;
        }

        return result;
    }

    public static string StarRating(string input)
    {
        string result;

        decimal numericRating = GetNumericRating(input);
        decimal roundedNumericRating = RoundDecimalToNearestHalves(numericRating);
        List<string> eachStarSetting = CalculateEachStar(roundedNumericRating);
        result = String.Join(" ", eachStarSetting);

        return result;
    }

    public static void Main()
    {
        bool testFinalResult = TestMethod();

        Console.WriteLine($"FinalTestResult:\t{testFinalResult}");
    }
}