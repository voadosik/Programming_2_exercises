using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;


enum Suit { Clubs, Diamonds, Hearts, Spades }; // 1
class Card
{
  public int rank;    // 1 = Ace, 2 .. 10, 11 = Jack, 12 = Queen, 13 = King 
  public Suit suit;

  public Card(int rank, Suit suit)
  {
    this.rank = rank;
    this.suit = suit;
  }

  public string describe()
  {
    switch(rank)
    {
      case 1:
        return $"Ace of {suit}";
      case 11:
        return $"Jack of {suit}";
      case 12:
        return $"Queen of {suit}";
      case 13:
        return $"King of {suit}";
      default:
        return $"{rank} of {suit}";
    }
  }

}
class Deck 
{
  Card[] deckOfCards;
  public Deck()
  {
    deckOfCards = new Card[52];
    GenerateCards();
  }
  private void GenerateCards()
  {
    foreach (Suit s in (Suit[])Enum.GetValues(typeof(Suit)))
    {
      for (int i = 1; i < 15; ++i)
      {
        deckOfCards.Append(new Card(i, s));
      }
    }
  }

  public Card[]? deal(int n)
  {
    Card[] dealer = new Card[n];
    int size = 52;
    Random rand = new Random();
    if (n > size)
    {
      return null;
    }
    for (int i = 0; i < n; ++i)
    {
      int remove = rand.Next(n);
      while (dealer[remove] == null)
      {
        remove = rand.Next(n);
      }
      dealer.Append(deckOfCards[remove]);
      deckOfCards[remove] = null;
      size--;
    }
    return dealer;
  }
}


class Polynomial // 2
{
  public double[] coefficients;
  public Polynomial(params double[] coefficients)
  {
    int start;
    for (start = 0; start < coefficients.Length; ++start)
    {
      if (coefficients[start] != 0)
      {
        break;
      }
    }
    this.coefficients = new double[coefficients.Length - start];
    for (int i = 0; i < this.coefficients.Length; i++)
    {
      this.coefficients[i] = coefficients[i + start];
    }

  }

  public int degree
  {
    get
    {
      return coefficients.Length - 1;
    }
  }

  public static Polynomial operator *(Polynomial p1, Polynomial p2)
  {
    if (p1.degree == -1 || p2.degree == -1)
    {
      return new();
    }
    int newDegree = p1.degree + p2.degree;
    double[] newCoefs = new double[newDegree + 1];
    for (int i = 1; i <= p1.coefficients.Length; ++i)
    {
      for (int j = 1; j <= p2.coefficients.Length; j++)
      {
        newCoefs[^(i + j - 1)] = p1.coefficients[^i] * p2.coefficients[^j];
      }
    }

    return new Polynomial(newCoefs);
  }

  public static Polynomial operator +(Polynomial pol1, Polynomial pol2)
  {
    if (pol1.degree < pol2.degree)
    {
      (pol1, pol2) = (pol2, pol1);
    }

    double[] newCoefs = pol1.coefficients.ToArray();

    for (int i = 1; i <= pol2.degree + 1; i++)
    {
      newCoefs[^i] += pol2.coefficients[^i];
    }

    return new(newCoefs);

  }
  public static Polynomial operator -(Polynomial pol1, Polynomial pol2) => pol1 + new Polynomial(-1) * pol2;

  public double eval(double x)
  {
    double result = 0;
    for (int i = 0; i < coefficients.Length; i++)
    {
      result *= x;
      result += coefficients[i];
    }
    return result;
  }

  public Polynomial derivative()
  {
    if (degree < 1)
    {
      return new();
    }
    double[] coefs = new double[degree];
    for (int i = 0; i < degree; i++)
    {
      coefs[i] = coefficients[i] * (degree - i);
    }
    return new Polynomial(coefficients);
  }

  public string AsString()
  {
    List<string> result = new();
    for (int i = 0; i < coefficients.Length; i++)
    {
      double coef = coefficients[i];
      if (coef == 0)
      {
        continue;
      }
      int exponent = degree - i;
      if (exponent == 0)
      {
        result.Add($"{coefficients[i]}");
      }
      else
      {
        string coefstr = coef == 1 ? "" : $"{coef}";
        string expstr = exponent == 1 ? "" : $"^{exponent}";
        result.Add(coefstr + "x" + expstr);
      }
    }
    return string.Join(" + ", result);
  }

  public Polynomial Parse(string input)
  {
    List<string> tokens = input.Split(" + ").ToList();
    List<(double Coef, int Exp)> factors = new();
    foreach (string token in tokens)
    {
      string[] s = token.Split('x');
      if (s.Length == 1) 
      {
        factors.Add((double.Parse(s[0]), 0));
      }
      else
      {
        double coef = s[0] == "" ? 1 : double.Parse(s[0]);
        int exp = s[1] == "" ? 1 : int.Parse(s[1]);
        factors.Add((coef, exp));
      }
    }

    int deg = factors.Select(p => p.Exp).Max();
    double[] newCoefs = new double[deg + 1];
    foreach(var (c, e) in  factors)
    {
      newCoefs[^(e + 1)] = c;
    }  
    return new Polynomial(newCoefs);
  }
}


class Node // 3
{
  public Node? next;
  public Node? prev;

  public int data;

  public Node(int data, Node? prev, Node? next)
  {
    this.next = next;
    this.prev = prev;
    this.data = data;
  }
}

class Dequeue // 3
{
  public Node? head;
  public Node? tail;
  List<Node> nodes;
  public Dequeue()
  {
    this.head = null;
    this.tail = null;
    this.nodes = new List<Node>();
  }

  public bool isEmpty() => nodes.Count == 0;

  public void enqueueFirst(int i)
  {
    if (head == null && tail == null)
    {
      Node n = new Node(i, null, null);
      nodes.Add(n);
      head = n; tail = n;
      return;
    }

    Node newNode = new Node(i, null, head);
    Node prevhead = head!;
    prevhead.prev = newNode;
    head = newNode;
    return;
  }

  public int degueueFirst()
  {
    if (isEmpty()) return int.MinValue;
    if (head == tail)
    {
      int d = head.data;
      nodes.Remove(head!);
      head = tail = null;
      return d;
    }
    else
    {
      int returndata = head!.data;
      Node oldhead = head;
      Node newhead = head.next!;
      nodes.Remove(oldhead);
      newhead.prev = null;
      head = newhead;
      return returndata;
    }

  }

  public void enqueueLast(int i)
  {
    if (head == null && tail == null)
    {
      Node n = new Node(i, null, null);
      nodes.Add(n);
      head = n; tail = n;
      return;
    }

    Node newNode = new Node(i, tail, null);
    Node prevtail = tail!;
    prevtail.next = newNode;
    tail = newNode;
    return;
  }

  public int degueueLast()
  {
    if (isEmpty()) return int.MinValue;
    if (head == tail)
    {
      int d = tail!.data;
      nodes.Remove(tail!);
      head = tail = null;
      return d;
    }
    else
    {
      int returndata = tail!.data;
      Node oldtail = tail;
      Node newtail = tail.prev!;
      nodes.Remove(oldtail);
      newtail.next = null;
      tail = newtail;
      return returndata;
    }

  }

}

class DictNode // 4
{
  public string key;
  public int value;
  public DictNode? next;
  public DictNode(string key, int value, DictNode? next)
  {
    this.key = key;
    this.value = value;
    this.next = next;
  }
}
class SIDict // 4
{
  List<DictNode> dict;


  public SIDict()
  {
    dict = new List<DictNode>();
  }

  //indexer
  public int this[string s]
  {
    get
    {
      if (dict.Count == 0)
      {
        return 0;
      }
      DictNode node = dict[0]; 
      while (node != null)
      {
        if (node.key == s)
        {
          return node.value;
        }
        if (!(node.next == null))
          node = node.next;
        else
        {
          break;
        }
      }
      return 0;

    }
    set
    {
      if (dict.Count == 0)
      {
        DictNode n = new DictNode(s, value, null);
        dict.Add(n);
      }
      DictNode node = dict[0];
      while (node.next != null)
      {
        if (node.key == s)
        {
          DictNode n = node; 
          node = new DictNode(s, value, n.next);
        }
        if (!(node.next == null))
          node = node.next;
        else
        {
          break;
        }
      }
      if (node.key == s)
      {
        DictNode n = node;
        node = new DictNode(s, value, n.next);
      }
      else
      {
        DictNode newNode = new DictNode(s, value, null); 
        dict.Add(newNode);
        node = new DictNode(node.key, node.value, newNode);
      }

    }
  }
}


