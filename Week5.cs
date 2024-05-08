using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Collections;
using System.Diagnostics;



class Exercises23{
  public Dictionary<string, string> flip(Dictionary<string, string> d) // 2 
  {
    Dictionary<string, string> returnDict = new Dictionary<string, string>();
    foreach (var k in d)
    {
      returnDict.Add(k.Value, k.Key);
    }
    return returnDict;
  }

  public string[] topWords(string filename, int n) // 3
  {
    Dictionary<string, int> d = new Dictionary<string, int>();
    using (StreamReader sr = new StreamReader(filename))
    {
      while (sr.ReadLine() is string line)
      {
        string[] words = line.Split(' ');
        foreach (string s in words)
        {
          if (d.ContainsKey(s))
          {
            d[s]++;
          }
          else
          {
            d[s] = 1;
          }
        }
      }
      sr.Close();
      return d.OrderByDescending(s => s.Value).Select(p => p.Key).Take(n).ToArray();

    }
  }

}

interface ICollection
{
  bool Contains(string key);
  void Clear();
  int Count { get; }

}

interface IStack : ICollection
{
  void Push(string key);
  string Pop();
}
interface IQueue : ICollection
{
  void Enqueue(string key);
  string Dequeue();
}
interface ISet : ICollection
{
  bool Add(string s);
  void Remove(string s);
}


class ArrayStack : IStack
{
  string[] array;
  int capacity;
  int elements;
  public ArrayStack()
  {
    array = new string[capacity];
    capacity = 10;
    elements = 0;
  }

  public void Clear()
  {
    capacity = 10;
    array = new string[capacity];
  }

  public int Count { get { return elements; } }

  public bool Contains(string s) => array.Contains(s);

  public void Push(string s)
  {
    if (elements == capacity - 1)
    {
      int newCapacity = capacity * 2;
      string[] newArray = new string[newCapacity];
      for (int i = 0; i < capacity - 1; i++)
      {
        newArray[i] = array[i];
      }
      newArray.Append(s);
      elements++;
      array = newArray;
      capacity = newCapacity;
      return;
    }
    else
    {
      array.Append(s);
      elements++;
      return;
    }

  }

  public string Pop()
  {
    string remove = array.Last();
    array = array.Where(s => s != remove).ToArray();
    elements--;
    return remove;
  }

}


class LinkedStack : IStack
{
  class Node
  {
    public Node? next;
    public string data;
    public Node (Node? next, string data)
    {
      this.next = next;
      this.data = data;
    }

  }

  private List<Node> stack;
  private Node? head;
  private Node? tail;

  public LinkedStack()
  {
    stack = new List<Node>();
    head = tail = null;

  }

  public int Count { get { return stack.Count; } }

  public void Clear() => stack = new List<Node>();

  public bool Contains(string s) => stack.Select(t => t.data).Contains(s);

  public void Push(string s)
  {
    if (head == null)
    {
      Node node = new Node(null, s);
      head = node;
      tail = node;
    }
    else
    {
      Node newNode = new Node(head, s);
      head = newNode;
    }
  }

  public string Pop()
  {
    if(tail == null || head == null)
    {
      throw new Exception("Stack is empty");
    }
    else
    {
      Node n = head.next;
      if (head.next == null)
      {
        tail = n;
      }
      string ret = head.data;
      head = n;
      return ret;
    }
  }


}


// Deque, ArrayQueue, LinkedQueue were implemented previously

class TreeNode
{
  public TreeNode? left;
  public TreeNode? right;
  public string data;

  public TreeNode(string data, TreeNode? left, TreeNode? right)
  {
    this.left = left;
    this.right = right;
    this.data = data;
  }
}


class Experiment
{
  public static void ExperimentA()
  {
    Random rng = new();

    for (int i = 1; i <= 40; i++)
    {
      int count = i * 10000;

      GC.Collect();
      Stopwatch sw = Stopwatch.StartNew();
      {
        HashSet<int> set1 = new();
        for (int j = 0; j < count; j++)
        {
          set1.Add(rng.Next());
        }
      }
      double set1time = sw.Elapsed.TotalMilliseconds;

      GC.Collect();
      sw.Restart();
      {
        SortedSet<int> set2 = new();
        for (int j = 0; j < count; j++)
        {
          set2.Add(rng.Next());
        }
      }
      double set2time = sw.Elapsed.TotalMilliseconds;

      Console.WriteLine($"{count}\t{set1time:0.00}\t{set2time:0.00}");
    }
  }
  public static void ExperimentB()
  {
    Random rng = new();

    for (int i = 1; i <= 40; i++)
    {
      int count = i * 10000;
      List<int> random = new();
      for (int j = 0; j < count; j++)
      {
        random.Add(rng.Next());
      }

      GC.Collect();
      Stopwatch sw = Stopwatch.StartNew();
      {
        HashSet<int> set1 = new();
        foreach (var val in random)
        {
          set1.Add(val);
        }
        foreach (var val in random)
        {
          set1.Remove(val);
        }
      }
      double set1time = sw.Elapsed.TotalMilliseconds;

      GC.Collect();
      sw.Restart();
      {
        SortedSet<int> set2 = new();
        foreach (var val in random)
        {
          set2.Add(val);
        }
        foreach (var val in random)
        {
          set2.Remove(val);
        }
      }
      double set2time = sw.Elapsed.TotalMilliseconds;

      Console.WriteLine($"{count}\t{set1time:0.00}\t{set2time:0.00}");
    }
  }

  public static void ExperimentC()
  {
    HashSet<int> set = new(1000000);

    const int steps = 300;
    const int insert = 20000;

    for (int i = 0; i < steps; i++)
    {
      Stopwatch sw = Stopwatch.StartNew();
      for (int j = 0; j < insert; j++)
      {
        set.Add(i * insert + j);
      }
      Console.WriteLine($"{set.Count}\t{sw.Elapsed.TotalMilliseconds:0.00}");
    }
  }

}



internal class Program
{
  static void Main(string[] args)
  { 
    Experiment.ExperimentA();
    Console.WriteLine("-------------------------------");
    Experiment.ExperimentB();
    Console.WriteLine("-------------------------------");
    Experiment.ExperimentC();
  }
}








