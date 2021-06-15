namespace DotForth
{
  public enum ScriptParseState
  {
    Interpret,
    DefinitionNameStart,
    DefinitionName,
    DefinitionBody,
    StringBody,
  }
}