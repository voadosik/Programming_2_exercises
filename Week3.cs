using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

internal class Week3
{


  class DynArray // 1
  {
    private int[] array;
    private int non_zero;
    private int max_length;

    public DynArray()
    {
      max_length = 10;
      array = new int[max_length];
      non_zero = 0;
    }

    private void upArray()
    {
      int[] temp_array = new int[max_length * 2];
      for (int i = 0; i < length(); i++)
      {
        temp_array[i] = array[i];
      }
      array = temp_array;
      max_length *= 2;
    }

    public int length() => array.Length;


    public void append(int i)
    {
      if (non_zero == max_length - 1)
      {
        upArray();
      }
      array[++non_zero] = i;

    }
    public int get(int index) => array[index];

    public void set(int index, int value)
    {
      Debug.Assert(index >= 0 && index < length());
      array[index] = value;
    }
  }

  class Queue // 2
  {
    private List<int> queue;

    public Queue()
    {
      queue = new List<int>();
    }

    public bool isEmpty() => this.queue.Count == 0;

    public void enqueue(int i) => queue.Add(i);

    public int dequeue()
    {
      if (isEmpty())
      {
        throw new Exception("Empty queue");
      }
      int res = queue[0];
      this.queue.RemoveAt(0);
      return res;
    }

    public bool tryDequeue(out int i)
    {
      if (isEmpty())
      {
        i = int.MaxValue;
        return false;
      }
      i = this.queue[0];
      this.queue.RemoveAt(0);
      return true;
    }
  }

  class Node
  {
    public Node? left;
    public Node? right;
    public int value;

    public Node(Node? left, Node? right, int value)
    {
      this.left = left;
      this.right = right;
      this.value = value;
    }
  }

  class BinaryTree // 3
  {
   

    protected Node? root;
    private int size;
    public BinaryTree()
    {
      root = null;
      size = 0;
    }
    public void insert(int i)
    {
      if (root == null)
      {
        root = new Node(null, null, i);
        size++;
        return;
      }

      Node node = root;
      while (true)
      {
        if (node.value < i)
        {
          if (node.right != null)
          {
            node = node.right;
          }
          else
          {
            node.right = new Node(null, null, i);
            size++;
            return;
          }
        }
        else if (node.value > i)
        {
          if (node.left != null)
          {
            node = node.left;
          }
          else
          {
            node.left = new Node(null, null, i);
            size++;
            return;
          }
        }
        else
        {
          return;
        }
      }

    }

    public bool contains(int i)
    {
      if (root == null)
      {
        return false;
      }
      Node node = root;
      while (true)
      {
        if (node.value == i)
        {
          return true;
        }
        else if (node.value > i)
        {
          if (node.left != null)
          {
            node = node.left;
          }
          else
          {
            return false;
          }
        }
        else
        {
          if (node.right != null)
          {
            node = node.right;
          }
          else
          {
            return false;
          }
        }
      }


    }

    public int[] toArray()
    {
      int[] array = new int[size];
      int index = 0;

      void Go(Node? n)
      {
        if (n == null)
        {
          return;
        }
        else
        {
          Go(n.left);
          array[index++] = n.value;
          Go(n.right);
        }
      }
      Go(root);
      return array;
    }

    static public BinaryTree FromArray(int[] array) // 4
    {
      Node? Go(int from, int to)
      {
        if (from > to)
        {
          return null;
        }
        else
        {
          int mid = from + (to - from) / 2;
          return new(Go(from, mid - 1), Go(mid + 1, to), array[mid]);
        }
      }

      BinaryTree tree = new();
      tree.root = Go(0, array.Length - 1);
      tree.size = array.Length;
      return tree;
    }


    
  
  }


 
  

}

