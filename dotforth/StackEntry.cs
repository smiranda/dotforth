using System;

namespace DotForth
{
  public class StackEntry
  {
    public StackEntry(string token)
    {
      Token = token;
    }
    public long AsLong()
    {
      return Convert.ToInt32(Token);
    }
    public string Token { get; set; }
  }
}