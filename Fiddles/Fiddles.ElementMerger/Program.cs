//  The task is to create a method that processes an array of integers by repeatedly merging adjacent elements until only one element remains.
//  The merging process involves replacing each pair of adjacent elements with the absolute difference of those elements.
//  The final remaining element is the output of the method.
//  The code also includes a testing framework to validate the method against predefined input-output pairs.

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fiddles.ElementMerger;

public class Program
{
    public static List<Tuple<int[], int>> inputOutputPairs = new()
    {
        new(new int[] {5, 7, 16, 1, 2}, 7),
        new(new int[] {1, 1, 1, 2}, 1)
    };

    public static bool TestMethod()
    {
        bool result = true;

        int iteration = 0;

        foreach (var pair in inputOutputPairs)
        {
            StringBuilder processingMessage = new();
            processingMessage.AppendFormat("Iteration:\t{0}\n\t", ++iteration);

            int processingOutput = ElementMergerShort(pair.Item1);
            int expectedOutput = pair.Item2;
            bool singleTestResult = processingOutput.Equals(expectedOutput);

            processingMessage.AppendFormat("Input received:\t{0}\n\tProcessingResult:\t{1}\n\tExpectedResult:\t{2}\n\tTestSuccessful:\t{3}\n", 
                                           $"[{String.Join(", ", pair.Item1)}]", 
                                           processingOutput, 
                                           expectedOutput, 
                                           singleTestResult);
            Console.WriteLine(processingMessage.ToString());

            result &= singleTestResult;
        }

        Console.WriteLine($"FinalTestResult:\t{result}");

        return result;
    }

    public static List<int> MergeStepZip(List<int> input) => input.Zip(input.Skip(1), (first, second) => Math.Abs(first - second)).ToList();

    public static int ElementMergerShort(int[] input) => (input.Length > 0) ? MergeUntilSingle(input.ToList()).Single() : throw new Exception("Invalid input!");

    public static List<int> MergeUntilSingle(List<int> input) => (input.Count == 1) ? (input) : (MergeUntilSingle(MergeStepZip(input)));

    public static void Main() => TestMethod();
}

