using System;
using System.Threading.Tasks;

namespace DotForth
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var forth = new Forth();
      var line = "";
      Console.Write("$ ");
      while ((line = Console.ReadLine()) != null)
      {
        await forth.Run(line, Console.Out);
        Console.Write("\n");
        Console.Write("$ ");
      }
    }
  }
}
