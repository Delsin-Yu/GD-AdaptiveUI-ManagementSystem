using System;
using System.Runtime.CompilerServices;
using System.Text;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Test;

public static class TestHelpers
{
    public static void Run(Action call, [CallerArgumentExpression(nameof(call))] string name = null)
    {
        Log("Run Test: ");
        Log(name);
        call();
        Log("OK");
    }

    public static T Run<T>(Func<T> call, [CallerArgumentExpression(nameof(call))] string name = null)
    {
        Log("Run Test: ");
        Log(name);
        var result = call();
        Log($"Result: {result}"); 
        return result;
    }

    public static void Log(string message) => GD.Print($"[TestRunner] --> {message}");
    public static void LogWarning(string message) => GD.PushWarning($"[TestRunner] --> {message}");
    public static void LogError(string message) => GD.PrintErr($"[TestRunner] --> {message}");
}
