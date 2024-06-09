using System;
using System.Linq;
using System.Collections.Generic;
using System.IO.Pipes;



namespace Exercise1
{
  class Program
  {

    static bool subsetsum(int[] a, int k)
    {
      int n = a.Length;
      bool[,] table = new bool[n + 1, k + 1];
      for(int t = 0 ; t <= n; t++)
      {
        table[t, 0] = true;
      }

      for (int i = 1; i <= n; i++)
      {
        for (int j = 1; j <= k; j++)
        {
          if(a[i - 1] > j)
          {
            table[i, j] = table[i - 1, j];
          }
          if(j >= a[i - 1])
          {
            table[i, j] = table[i - 1, j - a[i-1]] || table[i - 1, j];
          }
        }
      }

      return table[n, k];
    }



  }
}


namespace Exercise2
{
  class Program
  {
    static void squareSubMat(int[,] a)
    {
      int[,] dp = (int[,])a.Clone();
      int max_dim = 0;
      int x = -1;
      int y = -1;
      for (int i = 1; i < dp.GetLength(0); i++)
      {
        for (int j = 1; j < dp.GetLength(1); ++j)
        {
          if (dp[i-1, j-1] != 0 && dp[i-1, j] != 0 && dp[i, j-1] != 0 && dp[i,j] != 0)
          {
            dp[i, j] = 1 + Math.Min(dp[i - 1, j - 1], Math.Min(dp[i - 1, j], dp[i, j - 1]));
            if (dp[i,j] > max_dim)
            {
              max_dim = dp[i, j];
              x = i;
              y = j;
            }
          }
        }
      }

      Console.WriteLine( max_dim);
      Console.WriteLine($"{x}, {y}");
    }
  }
}

namespace Exercise3
{
  class Program
  {
    static bool segmentation(string[] words, string s)
    {
      bool[] t = new bool[s.Length + 1];
      int[] dp = new int[s.Length + 1];
      t[0] = true;
      dp[0] = 1;
      
      for (int i = 1; i <= s.Length; i++)
      {
        string part = s[..i];
        foreach(string word in words)
        {
          
          if (part.EndsWith(word) && t[i - word.Length])
          {
            t[i] = true;
            
          }
          if(part.EndsWith(word))
          {
            dp[i] += dp[i - word.Length];
          }
        }

      }
      Console.WriteLine(dp[s.Length]);
      return t[s.Length];

    }


  }
}

namespace Exercise4
{
  class Program
  {


    static string LCS(string s, string t)
    {
      int s_l = s.Length;
      int t_l = t.Length;
      string[,] dp = new string[s_l + 1, t_l + 1];
      for(int h = 0; h < s_l; h++)
      {
        dp[h, 0] = "";
      }
      for (int q = 1; q < t_l; q++)
      {
        dp[0, q] = "";
      }

      for(int i = 1; i <= s_l; ++i)
      {
        for (int j = 1; j <= t_l; ++j)
        {
          if (s[i - 1] == t[j - 1])
          {
            dp[i,j] = dp[i - 1, j - 1] + s[i - 1];
          }
          else
          {
            dp[i, j] = dp[i - 1, j].Length > dp[i, j - 1].Length ? dp[i - 1, j] : dp[i, j - 1];
          }
        }
      }

      
      return dp[s_l, t_l];
    }


  }
}


