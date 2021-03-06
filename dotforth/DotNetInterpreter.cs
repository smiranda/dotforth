using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace DotForth
{
  public static class DotNetInterpreter
  {
    public static async Task Run(Forth forth, TextWriter output)
    {
      var script = forth.Stack.Pop().Token;
      var scriptOptions = ScriptOptions.Default;
      var mscorlib = typeof(object).GetTypeInfo().Assembly;
      var systemCore = typeof(System.Linq.Enumerable).GetTypeInfo().Assembly;
      var newton = typeof(Newtonsoft.Json.Linq.JObject).GetTypeInfo().Assembly;

      var references = new[] { mscorlib, systemCore, newton };
      scriptOptions = scriptOptions.AddReferences(references);

      using (var interactiveLoader = new InteractiveAssemblyLoader())
      {
        foreach (var reference in references)
        {
          interactiveLoader.RegisterDependency(reference);
        }

        scriptOptions = scriptOptions.AddImports("System");
        scriptOptions = scriptOptions.AddImports("System.Linq");
        scriptOptions = scriptOptions.AddImports("System.Collections.Generic");
        scriptOptions = scriptOptions.AddImports("Newtonsoft.Json");
        scriptOptions = scriptOptions.AddImports("Newtonsoft.Json.Linq");

        Script cscript = null;
        ScriptState state = null;
        cscript = CSharpScript.Create(@"", scriptOptions, typeof(InputData), interactiveLoader);
        state = cscript.RunAsync(new InputData() { Forth = forth }).Result;
        state = await state.ContinueWithAsync(script);
        //var output = state.ReturnValue;
      }
    }
  }

  public class InputData
  {
    public Forth Forth { get; set; }
  }
}
