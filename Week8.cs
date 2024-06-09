using System;
using System.Linq;
using System.Collections.Generic;


namespace Exercise1
{
  class Program
  {
    static void coinSums(int n)
    {
      int[] coins = { 1, 2, 5, 10, 20, 50 };
      Stack<int> stack = new Stack<int>();
      int counter = 0;
      void go(int left)
      {
        if(left == 0)
        {
          counter++;
        }
        else
        {
          foreach(int x in coins)
          {
            if(left - x < 0)
            {
              continue;
            }
            stack.Push(x);
            go(left - x);
            stack.Pop();
          }
        }
      }
      go(n);
      Console.WriteLine(counter);
    }

  }
}


namespace Exercise2
{
  class Program
  {
    static void garden(int n)
    {
      Stack<char> s = new Stack<char>();
      void go(int left, bool allow_p)
      {
        if(left == 0)
        {
          Console.WriteLine(string.Join(" ", s.Reverse()));
        }
        else
        {
          s.Push('C');
          go(left - 1, true);
          s.Pop();
          if(allow_p)
          {
            s.Push('P');
            go(left - 1, false);
            s.Pop();
          }
        }
      }

      go(n, true);
    }

    static int Ways(int n)
    {
      int[] ways = new int[n + 1];

      ways[0] = 1;
      ways[1] = 2;

      for (int i = 2; i <= n; ++i)
      {
        int total = 0;
        total += ways[i - 1];
        total += ways[i - 2];
        ways[i] = total;
      }

      return ways[n];
    }


  }
}


namespace Exercise3
{ 
  class Program
  {
    static void smallestNumCoins(int[] coins, int sum)
    {
      Stack<int> stack = new Stack<int>();
      int w = int.MaxValue;
      List<int> list = stack.Reverse().ToList();
      void go(int left, int additions, Stack<int> stack)
      {
        if (left == 0)
        {
          if(additions < w)
          {
            w = additions;
            list = stack.Reverse().ToList();
          }
         
        }
        else
        {
          foreach (int i in coins)
          {
            if (left - i < 0)
            {
              continue;
            }
            stack.Push(i);
            go(left - i, additions + 1, stack);
            stack.Pop();
          }

        }
      }
      go(sum, 0, stack);
      Console.WriteLine(string.Join(" + ", list));
    }

  }
}

namespace Exercise4
{
  class Program
  {
    static void minJump(int[] array)
    {
      Stack<int> stack = new();
      int[] possible = new int[array[0]];
      int current_idx = 0;
      int min_depth = int.MaxValue;
      string best_stack = "";
      for (int i = 0;  i < possible.Length; i++)
      {
        possible[i] = i + 1;
      }
      void go(int curr, int depth)
      {
        if(curr >= array.Length)
        {
          if(depth < min_depth )
          {
            min_depth = depth;
            best_stack = string.Join(" + ", stack.Reverse());
          }

        }
        else
        {
          int[] possibles = new int[array[curr]];
          for (int i = 0; i < possibles.Length; i++)
          {
            possibles[i] = i + 1;
          }
          foreach(int n in possibles)
          {
            stack.Push((int)n);
            go(curr + n, depth + 1);
            stack.Pop();
          }
        }

      }

      go(current_idx, 0);
      Console.WriteLine(min_depth);
      Console.WriteLine(best_stack);

    }


  }

}
