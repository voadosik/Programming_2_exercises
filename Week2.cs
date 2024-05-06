using System;
using System.Collections.Generic;

internal class Week2
{

  static bool startWith(string s, string t) // 2
  {
    if (t.Length > s.Length)
    {
      return false;
    }

    for (int i = 0; i < t.Length; i++)
    {
      if (s[i] != t[i])
      {
        return false;
      }
    }
    return true;
  }


  static string numWithCommas(string number) // 3
  {
    string output = "";
    for (int i = number.Length - 1; i >= 0; i--)
    {
      output = number[i] + output;
      if ((i+1) % 3 == 0)
      {
        output = "," + output;
      }
    }
    return output;
  }


  static List<int> sortingTwoValues(List<int> numbers) // 4
  {
    int first_number = numbers[0];
    int second_number = int.MinValue;
    int count_fir = 1;
    int count_sec = 0;
    for (int i = 1; i < numbers.Count; i++)
    {
      if (numbers[i] != first_number)
      {
        second_number = numbers[i];
        count_sec++;
      }
      else
      {
        count_fir++;
      }
    }
    List<int> result = new List<int>();
    if (first_number < second_number)
    {
      for (int i = 0; i < count_fir; i++)
      {
        result.Add(first_number);
      }
      for (int i = 0; i < count_sec; i++)
      {
        result.Add(second_number);
      }
    }
    else
    {
      for (int i = 0; i < count_sec; i++)
      {
        result.Add(second_number);
      }
      for (int i = 0; i < count_fir; i++)
      {
        result.Add(first_number);
      }
    }
    return result;
    
  }


  static void matrixRotate(int[][] matrix) // 8
  {

    for (int i = 0; i < matrix[0].Length ; ++i)
    {
      for (int j = matrix[0].Length - 1; j >= 0; --j)
      {
        Console.Write($"{matrix[j][i]} ");
      }
      Console.Write("\n");
    }

  }

  static long integerSqrt(long number) // 10
  {
    long lo = 0;
    long hi = number;
    
    while (lo <= hi)
    {
      long mid = lo + (hi - lo) / 2;
      if (mid * mid < number)
      {
        lo = mid + 1;
      }
      else if (mid * mid > number) 
      {
        hi = mid - 1;
      }
      else if (mid * mid == number)
      {
        return mid;
      }
    }
    return hi;
  }
  

}

