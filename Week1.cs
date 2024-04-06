namespace Week1
{
  internal class Program
  {
    static int Multiples(int n)
    {
      int sum = 0;
      for (int i = 1; i < n; ++i)
      {
        if (i % 3 == 0 || i % 5 == 0)
        {
          sum += i;

        }
      }
      return sum;
    }

    static int FibSum(int n)
    {
      int sum = 0;
      int a = 1;
      int b = 1;
      while (b < n)
      {
        (a, b) = (b, b + a);
        if (b % 2 == 0)
        {
          sum += b;
        }
      }
      return sum;
    }

    static int PrimeFactor(long n)
    {
      bool[] sieve(int n)
      {
        bool[] sieve = new bool[n+1];
        for(int i = 0; i < sieve.Length; ++i)
        {
          sieve[i] = true;
        }
        sieve[0] = sieve[1] = false;
        for(int i = 2; i < (int) Math.Sqrt(n) + 1; ++i)
        {
          if (sieve[i])
          {
            for (int j = i * i; j < n + 1; j += i)
            {
              sieve[j] = false;
            }
          }
        }
        return sieve;
      }
      int l_fac = 1;
      bool[] our_sieve = sieve((int)Math.Sqrt(n) + 1);
      for (int i = 1; i < (int) Math.Sqrt(n) + 1; ++i)
      {
        if (n % i == 0 && our_sieve[i] == true)
        {
          l_fac = i;
        }
      }
      return l_fac;
    }


    static int largestPalindrome()
    {
      int Reverse(int num)
      {
        int rev_num = 0;
        while (num > 0)
        {
          rev_num = rev_num * 10 + num % 10;
          num = num / 10;
        }
        return rev_num;
      }


      int result = 0;
      int palindrome = 0;
      int j = 1;
      for (int i = 99; i > 0; i--)
      {
        for (int k = 99; k > 0; k--)
        {
          result = i * k;
          if (result == Reverse(result))
          {
            if (result > palindrome)
            {
              palindrome = result;
            }
          }
        }
      }

      return palindrome;
    }


    static int nth_prime_number(int n)
    {
      bool is_prime(int n)
      {
        bool[] sieve = new bool[n + 1];
        for (int i = 0; i < sieve.Length; ++i)
        {
          sieve[i] = true;
        }
        sieve[0] = sieve[1] = false;
        for (int i = 2; i < (int)Math.Sqrt(n) + 1; ++i)
        {
          if (sieve[i])
          {
            for (int j = i * i; j < n + 1; j += i)
            {
              sieve[j] = false;
            }
          }
        }
        return sieve[n];
      }
      int n_p = 0;
      int prime = 2;
      while (n_p < n)
      {
        if (is_prime(prime))
        {
          ++n_p;
        }
        ++prime;
      }
      return prime;
    }

    static int lastDigits(int pow)
    {
      int dig = 1;
      for (int i = 0; i < pow; ++i)
      {
        dig = (dig * 2) % 100000;
      }
      return dig;
    }



    static void Main(string[] args)
    {
      Console.WriteLine(Multiples(1000)); // 1
      Console.WriteLine(FibSum(4_000_000)); // 2
      Console.WriteLine(PrimeFactor(600851475143)); // 3
      Console.WriteLine(largestPalindrome()); // 4
     /* Console.WriteLine(nth_prime_number(10001));   5 (slow but who cares) */
      Console.WriteLine(lastDigits(1000)); // 6
    }
  }
}
