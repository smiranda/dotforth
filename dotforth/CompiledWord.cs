using System;
using System.IO;
using System.Threading.Tasks;

namespace DotForth
{
  public class CompiledWord
  {
    public CompiledWord(string script)
    {
      Script = script;
    }
    public CompiledWord(Func<Forth, TextWriter, Task> function)
    {
      Function = function;
    }
    public async Task Execute(Forth forth, TextWriter output)
    {
      if (Function != null)
      {
        // Default function
        await Function(forth, output);
      }
      else
      {
        // Custom script
        await Forth.Run(Script, forth, output);
      }
    }
    public string Script { get; set; }
    public Func<Forth, TextWriter, Task> Function { get; set; }
  }
}