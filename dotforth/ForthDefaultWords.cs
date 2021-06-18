using System.IO;

namespace DotForth
{
  public static class ForthDefaultWords
  {
    public static void Sum(Forth forth, TextWriter output)
    {
      var op1 = forth.Stack.Pop();
      var op2 = forth.Stack.Pop();
      var op1l = op1.AsLong();
      var op2l = op2.AsLong();
      var r = op1l + op2l;
      forth.Stack.Push(new StackEntry(r.ToString()));
    }
    public static void Subtract(Forth forth, TextWriter output)
    {
      var op1 = forth.Stack.Pop();
      var op2 = forth.Stack.Pop();
      var op1l = op1.AsLong();
      var op2l = op2.AsLong();
      var r = op1l - op2l;
      forth.Stack.Push(new StackEntry(r.ToString()));
    }
    public static void Multiply(Forth forth, TextWriter output)
    {
      var op1 = forth.Stack.Pop();
      var op2 = forth.Stack.Pop();
      var op1l = op1.AsLong();
      var op2l = op2.AsLong();
      var r = op1l * op2l;
      forth.Stack.Push(new StackEntry(r.ToString()));
    }
    public static void Divide(Forth forth, TextWriter output)
    {
      var op1 = forth.Stack.Pop();
      var op2 = forth.Stack.Pop();
      var op1l = op1.AsLong();
      var op2l = op2.AsLong();
      var r = op1l / op2l;
      forth.Stack.Push(new StackEntry(r.ToString()));
    }
    public static void Print(Forth forth, TextWriter output)
    {
      var op = forth.Stack.Pop();
      output.Write($"> {op.Token}");
    }
  }
}