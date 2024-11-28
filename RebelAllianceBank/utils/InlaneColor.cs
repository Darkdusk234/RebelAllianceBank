using System;

namespace RebelAllianceBank.utils;

/// <summary>
/// Get the ANSI escape sequences for the given color
/// </summary>
public static class TextColor
{
    public static string NORMAL { get; } = "\x1b[39m";
    public static string Red { get; } = "\x1b[91m";
    public static string Green { get; } = "\x1b[92m";
    public static string Yellow { get; } = "\x1b[93m";
    public static string Blue { get; } = "\x1b[94m";
    public static string Magenta { get; } = "\x1b[95m";
    public static string Cyan { get; } = "\x1b[96m";
    public static string Grey { get; } = "\x1b[97m";
}
