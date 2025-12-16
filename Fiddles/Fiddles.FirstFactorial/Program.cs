//  First Factorial
//  
//  Have the function FirstFactorial(num) take the num parameter being passed and return the factorial of it.
//  For example: if num = 4, then your program should return (4 * 3 * 2 * 1) = 24.
//  For the test cases, the range will be between 1 and 18 and the input will always be an integer.

using System;

namespace FiddlesFirstFactorial;

public class MainClass
{
    public static Predicate<int> IsOneOrLess = x => x<=1;

    public static int FirstFactorial(int num) => IsOneOrLess(num) ? 1 : (num * FirstFactorial(num - 1));

    public static void Main() => Console.WriteLine(FirstFactorial(Console.ReadLine()));
}