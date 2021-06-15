using System;

namespace DotForth
{
  public class CompiledWord
  {
    public CompiledWord(string script)
    {
      Script = script;
    }
    public CompiledWord(Action<Forth> function)
    {
      Function = function;
    }
    public void Execute(Forth forth)
    {
      if (Function != null)
      {
        // Default function
        Function(forth);
      }
      else
      {
        // Custom script
        Forth.Run(Script, forth);
      }
    }
    public string Script { get; set; }
    public Action<Forth> Function { get; set; }
  }
}