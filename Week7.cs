using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Exercise1
{
  class Program
  {
    public static T[] filter<T>(T[] array, Predicate<T> predicate)
    {
      List<T> list = new List<T>();
      foreach (T item in array)
      {
        if(predicate(item)) list.Add(item);
      }
      return list.ToArray();
    }

  }
}

namespace Exercise2
{
  class Program
  {
    public static T MaxBy<T, Key>(T[] array, Func<T, Key> f) where Key : IComparable<Key> 
    {
      if(array.Length == 0)
      {
        return default;
      }

      T max_item = array[0];
      Key maxKey = f(max_item);
      foreach (T item in array)
      {
        if (f(item).CompareTo(maxKey) > 0)
        {
          max_item = item;
          maxKey = f(max_item);
        }
      }

      return max_item;
    }


  }
}

namespace Exercise3
{
  class Program
  {
    void sort<T>(T[] a, Comparison<T> f)
    {
      List<T> list = new(a);
      for (int i = 0; i < a.Length; i++)
      {
        for (int j = i + 1; j < a.Length; j++)
        {
          if (f(list[i], list[j]) > 0)
          {
            (list[i], list[j]) = (list[j], list[i]);
          }
        }
      }
      a = list.ToArray();

    }


  }

}


namespace Exercise4
{
  class Program
  {
    static void Euler()
    {
      Console.WriteLine(Enumerable.Range(0, 1000).Where(i => i % 3 == 0 || i % 5 == 0).Sum());
    }
  }
}

namespace Exercise5
{
  class Program
  {
    static void subsets(int n)
    {
      Stack<int> stack = new Stack<int>();
      void go(int i)
      {
        if (i > n)
        {
          Console.WriteLine("{ " + string.Join(" ", stack.Reverse()) + " }");
        }
        else
        {
          stack.Push(i);
          go(i + 1);
          stack.Pop();
          go(i + 1);
        }
      }
      go(1);
    } 
  }
}


namespace Exercise6
{
  class Program
  {
    static void permutations(string s)
    {
      char[] chars = new char[s.Length];
      void go(int depth)
      {
        if(depth >= chars.Length)
        {
          Console.WriteLine(new string(chars));
        }
        else
        {
          for(int j = 0; j < chars.Length; j++)
          {
            if (chars[j] != 0)
            {
              continue;
            }
            chars[j] = s[depth];
            go(depth + 1);
            
            chars[j] = (char)0;
          }
        }
      }
      go(0);
      
    }

    
  }
}

namespace Exercise7
{
  class Program
  {
    static void combinations(int[] a, int m)
    {
      Stack<int> stack = new();
      int n = a.Length;
      void go(int i, int max)
      {
        if (i == m)
        {
          Console.WriteLine(string.Join(" ", stack.Reverse()));
        }
        else if (i < n)
        {
          foreach (var number in a)
          {
            if (number > max)
            {
              stack.Push(number);
              go(i + 1, number);
              stack.Pop();
            }
          }
        }
      }
      go(0, 0);
    }

   
  }
}



namespace Exercise8
{
  class Program
  {
    static void possibleStairs(int n)
    {
      int[] possibleCount = { 1, 2, 3 };
      Stack<int> stack = new();
      void go(int left)
      {
        if(left == 0)
        {
          Console.WriteLine(string.Join(" + ", stack.Reverse()));
        }
        else
        {
          foreach (var number in possibleCount)
          {
            if (left - number < 0)
            {
              continue;
            }
            stack.Push((int)number);
            go(left - number);
            stack.Pop();
          }
        }
      }
      go(n);
    }
 
  }
}


namespace Exercise9
{
  class Program
  {
    static void compositions(int number)
    {
      int[] possibleDivs = new int[number];
      for (int i = 0; i < number; i++)
      {
        possibleDivs[i] = i + 1;
      }
      Stack<int> comps = new Stack<int>();  
      void go(int left)
      {
        if (left == 0)
        {
          Console.WriteLine(string.Join(" + ", comps.Reverse()));
        }
        else
        {
          foreach(var num in possibleDivs)
          {
            if(left - num < 0)
            {
              continue;
            }
            comps.Push(num);
            go(left - num);
            comps.Pop();
          }
        }
      }

      go(number);

    }

  }
}


namespace Exercise10
{
  class Program
  { 
    static void partitions(int number)
    {
      int[] nums = new int[number];

      Stack<int> partition = new Stack<int>();
      for (int i = 0; i < number; ++i)
      {
        nums[i] = i + 1;
      }
      void go(int left, int max)
      {
        int[] used = new int[number];
        if(left == 0)
        {
          Console.WriteLine(string.Join(" + ", partition.Reverse()));
        }

        else
        {
          foreach(var k in nums)
          {
            if(left - k < 0 || k < max) { continue; }
            partition.Push(k);
            go(left - k, k);
            partition.Pop();
          }
        }
      }
      go(number, 0);
    }


  }
}

namespace Exercise11
{
  class Program
  {
    static void change(int n)
    {
      int[] possible = { 1, 2, 5, 10, 20, 50 };
      Stack<int> change = new Stack<int>();
      
      void go(int left, int max)
      {
        if (left == 0)
        {
          Console.WriteLine(string.Join(" + ", change.Reverse()));
        }
        else
        {
          foreach (var k in possible)
          {
            if(left - k < 0 || k < max)
            {
              continue;
            }
            change.Push(k);
            go(left - k, k);
            change.Pop();
          }
        }
      }
      go(n, 0);
    }


  }
}

