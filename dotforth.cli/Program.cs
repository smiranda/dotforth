using System;

namespace DotForth
{
  class Program
  {
    static void Main(string[] args)
    {
      var forth = new Forth();
      var line = "";
      Console.Write("$ ");
      while ((line = Console.ReadLine()) != null)
      {
        forth.Run(line);
        Console.Write("$ ");
      }
    }
  }
}
