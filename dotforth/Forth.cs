using System;
using System.Collections.Generic;
using System.IO;

namespace DotForth
{

  public class Forth
  {
    public Forth() : this(Console.Out)
    {

    }
    public Forth(TextWriter outputConsole)
    {
      OutputConsole = outputConsole;
      Stack = new Stack<StackEntry>();
      Words = new Dictionary<string, CompiledWord>();
      ScratchPad = new ScratchPad();
      LoadDefaultWords();
    }
    internal TextWriter OutputConsole { get; private set; }
    public Stack<StackEntry> Stack { get; private set; }
    public Dictionary<string, CompiledWord> Words { get; private set; }
    public ScratchPad ScratchPad { get; private set; }
    public void Run(string script)
    {
      Run(script, this);
    }

    public void Push(string value)
    {
      Stack.Push(new StackEntry(value));
    }
    public void Push(long value)
    {
      Stack.Push(new StackEntry(value.ToString()));
    }
    public string Pop()
    {
      return Stack.Pop().Token;
    }
    public void LoadWord(string word, CompiledWord compiledWord)
    {
      Words.Add(word, new CompiledWord(ForthDefaultWords.Divide));
    }
    private void LoadDefaultWords()
    {
      Words.Add("+", new CompiledWord(ForthDefaultWords.Sum));
      Words.Add("-", new CompiledWord(ForthDefaultWords.Divide));
      Words.Add("*", new CompiledWord(ForthDefaultWords.Multiply));
      Words.Add("/", new CompiledWord(ForthDefaultWords.Divide));
      Words.Add(".", new CompiledWord(ForthDefaultWords.Print));
      Words.Add("dotnet", new CompiledWord(DotNetInterpreter.Run));
      Words.Add("net", new CompiledWord(DotNetInterpreter.Run));
    }

    internal static void Run(string script, Forth forth)
    {
      var definition = "";
      var definitionName = "";
      var scriptParseState = ScriptParseState.Interpret;
      var token = "";
      var stringBody = "";


      foreach (var c in (script + ' '))
      {
        if (scriptParseState == ScriptParseState.StringBody)
        {
          if (c == '`')
          {
            // End the string
            forth.Stack.Push(new StackEntry(stringBody));
            stringBody = "";
            scriptParseState = ScriptParseState.Interpret;
            continue;
          }
          else
          {
            stringBody += c;
            continue;
          }
        }

        if (scriptParseState == ScriptParseState.DefinitionNameStart)
        {
          if (c == ' ')
          {
            continue;
          }
          scriptParseState = ScriptParseState.DefinitionName;
          definitionName += c;
          continue;
        }
        if (scriptParseState == ScriptParseState.DefinitionName)
        {
          if (c == ' ')
          {
            scriptParseState = ScriptParseState.DefinitionBody;
            continue;
          }
          definitionName += c;
          continue;
        }
        else if (scriptParseState == ScriptParseState.DefinitionBody)
        {
          if (c == ';')
          {
            // End a definition
            forth.OutputConsole.WriteLineAsync($"> {definitionName} word defined");
            forth.Words[definitionName] = new CompiledWord(definition);
            definition = "";
            definitionName = "";
          }
          definition += c;
          continue;
        }

        if (c == ':')
        {
          // Start a definition
          scriptParseState = ScriptParseState.DefinitionNameStart;
          continue;
        }
        else if (c == '`')
        {
          // Start a string
          scriptParseState = ScriptParseState.StringBody;
          continue;
        }
        else if (c == ' ')
        {
          if (token == "")
            continue;

          CompiledWord word;
          if (forth.Words.TryGetValue(token, out word))
          {
            word.Execute(forth);
          }
          else
          {
            forth.Stack.Push(new StackEntry(token));
          }
          token = "";
        }
        else
        {
          token += c;
        }
      }
    }
  }
}