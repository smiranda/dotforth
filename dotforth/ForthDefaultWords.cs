using System.IO;
using System.Threading.Tasks;

namespace DotForth
{
  public static class ForthDefaultWords
  {
    public static async Task Sum(Forth forth, TextWriter output)
    {
      await Task.Run(() =>
      {
        var op1 = forth.Stack.Pop();
        var op2 = forth.Stack.Pop();
        var op1l = op1.AsLong();
        var op2l = op2.AsLong();
        var r = op1l + op2l;
        forth.Stack.Push(new StackEntry(r.ToString()));
      });
    }
    public static async Task Subtract(Forth forth, TextWriter output)
    {
      await Task.Run(() =>
      {
        var op1 = forth.Stack.Pop();
        var op2 = forth.Stack.Pop();
        var op1l = op1.AsLong();
        var op2l = op2.AsLong();
        var r = op1l - op2l;
        forth.Stack.Push(new StackEntry(r.ToString()));
      });
    }
    public static async Task Multiply(Forth forth, TextWriter output)
    {
      await Task.Run(() =>
      {
        var op1 = forth.Stack.Pop();
        var op2 = forth.Stack.Pop();
        var op1l = op1.AsLong();
        var op2l = op2.AsLong();
        var r = op1l * op2l;
        forth.Stack.Push(new StackEntry(r.ToString()));
      });
    }
    public static async Task Divide(Forth forth, TextWriter output)
    {
      await Task.Run(() =>
      {
        var op1 = forth.Stack.Pop();
        var op2 = forth.Stack.Pop();
        var op1l = op1.AsLong();
        var op2l = op2.AsLong();
        var r = op1l / op2l;
        forth.Stack.Push(new StackEntry(r.ToString()));
      });
    }
    public static async Task Print(Forth forth, TextWriter output)
    {
      var op = forth.Stack.Pop();
      await output.WriteAsync($"> {op.Token}");
    }
  }
}