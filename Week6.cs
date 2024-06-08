using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exercise1
{
  class Program
  {
    static void genericSort<T>(ref T[] array) where T : IComparable<T>
    {
      for (int i = 0; i < array.Length; i++)
      {
        for(int j = i + 1; j < array.Length; j++) 
        {
          if (array[j].CompareTo(array[i]) < 0)
          {
            (array[i], array[j]) = (array[j], array[i]);
          }
        }
      }
    }

  }
}

namespace Exercise2
{
  class TreeSet<T> where T : IComparable<T>
  {
    class Node
    {
      public T value;
      public Node? left, right;

      public Node(T val)
      {
        value = val;
      }
    }

    Node? root;


    public bool Contains(T val)
    {
      Node? p = root;
      while (p != null)
      {
        int c = val.CompareTo(p.value);
        if(c == 0)
        {
          return true;
        }
        else if(c < 0)
        {
          p = p.left;
        }
        else
        {
          p = p.right;
        }
      }
      return false;
    }

    public void insert(T val)
    {
      if(root == null)
      {
        root = new Node(val);
        return;
      }
      if(Contains(val))
      {
        return;
      }
      Node? p = root;
      while (true)
      {
        int c = val.CompareTo(p.value);
        if(c < 0)
        {
          if(p.left != null)
          {
            p = p.left;
          }
          else
          {
            p.left = new Node(val);
            return;
          }
        }
        if (c > 0)
        {
          if (p.right != null)
          {
            p = p.right;
          }
          else
          {
            p.right = new Node(val);
            return;
          }
        }
      }
    }
    private List<T> array;

    public TreeSet(T[] a)
    {
      List<T> array = new(a);
      array.Sort();

      Node go(List<T> arr)
      {
        if(arr.Count == 1)
        {
          return new Node(arr[0]);
        }
        Node p = new Node(arr[arr.Count / 2]);
        p.left = go(arr[0..(arr.Count / 2)]);
        p.right = go(arr[(arr.Count / 2 + 1)..arr.Count]);
        return p;
      }

      root = go(array);
    }


    public T[] Range(T a, T b)
    {
      List<T> sortedArray = new();

      void go(Node? n)
      {
        if(n == null)
        {
          return;
        }

        if (n.value.CompareTo(a) <= 0 && n.value.CompareTo(b) >= 0)
        {
          sortedArray.Add(n.value);
        }
        else if (n.value.CompareTo(a) > 0)
        {
          go(n.left);
        }
        else if (n.value.CompareTo(b) < 0)
        {
          go(n.right);
        }

      }
      go(root);
      return sortedArray.ToArray();
    }

    public void Validate()
    {
      void go(Node? n, Node? low, Node? high)
      {
        if(n == null)
        {
          return;
        }
        bool isOK = (low == null || low.value.CompareTo(n.value) < 0) && (high == null || n.value.CompareTo(high.value) < 0);
        if(!isOK)
        {
          throw new Exception("Invalid structure");
        }
        go(n.left, low, n);
        go(n.right, n, high);
      }
      go(root, null, null);

    }



  }
}

namespace Exercise3
{
  class Dictionary<K, V> where K : IComparable<K>
  {
    class Node
    {
      public K key;
      public V value;
      public Node? next;
      public Node(K key, V value)
      {
        this.key = key;
        this.value = value;
      }
    }

    private Node?[] arr;
    private int size;
    public Dictionary(int capacity = 16)
    {
      size = 0;
      arr = new Node[size];
    }

    public V this[K key]
    {
      get => Get(key);
      set => Add(key, value);
    }

    private int Hash(K key)
    {
      return (key.GetHashCode() & 0x7fffffff) % arr.Length;
    }

    public void Add(K key, V value)
    {
      int idx = Hash(key);
      Node? p = arr[idx]; 
      while(p != null)
      {
        if (p.key.CompareTo(key) == 0)
        {
          p.value = value;
        }
        p = p.next;
      }
      Node newNode = new Node(key, value);
      newNode.next = arr[idx];
      arr[idx] = newNode;
      size++;

      if (size >= arr.Length * 0.75)
      {
        Resize();
      }

    }

    public V Get(K key)
    {
      int index = Hash(key);
      Node? node = arr[index];
      while (node != null)
      {
        if (node.key.CompareTo(key) == 0)
        {
          return node.value;
        }
        node = node.next;
      }
      throw new ArgumentException("Key not found");
    }

    public bool Remove(K key)
    {
      int index = Hash(key);
      Node? node = arr[index];
      Node? prev = null;

      while (node != null)
      {
        if (node.key.CompareTo(key) == 0)
        {
          if (prev == null)
          {
            arr[index] = node.next;
          }
          else
          {
            prev.next = node.next;
          }
          size--;
          return true;
        }
        prev = node;
        node = node.next;
      }
      return false;
    }

    public void Resize()
    {
      Node?[] oldArr = arr;
      arr = new Node[oldArr.Length * 2];
      size = 0;
      foreach(Node? head in oldArr)
      {
        Node? node = head;
        while (node != null)
        {
          Add(node.key, node.value);
          node = node.next;
        }
      }

    }


  }
}