using System;
using System.IO;

namespace DotForth
{
  public class CompiledWord
  {
    public CompiledWord(string script)
    {
      Script = script;
    }
    public CompiledWord(Action<Forth, TextWriter> function)
    {
      Function = function;
    }
    public void Execute(Forth forth, TextWriter output)
    {
      if (Function != null)
      {
        // Default function
        Function(forth, output);
      }
      else
      {
        // Custom script
        Forth.Run(Script, forth, output);
      }
    }
    public string Script { get; set; }
    public Action<Forth, TextWriter> Function { get; set; }
  }
}