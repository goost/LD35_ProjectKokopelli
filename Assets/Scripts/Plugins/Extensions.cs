using System;
using System.Diagnostics;

[DebuggerNonUserCode]
public static class Extensions
{
  public static int ToInt(this bool @this)
  {
    return @this ? 1 : 0;
  }

  public static bool ToBool(this int @this)
  {
    return @this != 0;
  }

  /// <summary>
  /// Extension for a nicer string.Format. Just use "".F()
  /// </summary>
  /// <param name="args">The arguments.</param>
  /// <returns>The formatted string</returns>
  [DebuggerNonUserCode]
  public static string F(this string s, params object[] args)
  {
    return string.Format(s, args);
  }
}

[DebuggerNonUserCode]
public static class FunctionalExtensions
{
  public static TResult Map<TSource, TResult>(
    this TSource @this,
    Func<TSource, TResult> fn)
  {
    return fn(@this);
  }

  public static T Tee<T>(
    this T @this,
    Action<T> act)
  {
    act(@this);
    return @this;
  }
}