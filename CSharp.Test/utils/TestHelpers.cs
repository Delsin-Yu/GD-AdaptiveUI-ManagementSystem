using System;
using System.Runtime.CompilerServices;
using System.Text;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public static class TestHelpers
{
    private static bool s_IsInTest;

    public static void Run(Action call, [CallerArgumentExpression(nameof(call))] string name = null)
    {
        s_IsInTest = true;
        GD.Print("┌┈┈┈┈ Test ┈┈┈┈");
        Log(name);
        call();
        GD.Print("└┈┈┈┈┈ OK ┈┈┈┈┈");
        s_IsInTest = false;
    }

    public static T Run<T>(Func<T> call, [CallerArgumentExpression(nameof(call))] string name = null)
    {
        s_IsInTest = true;
        GD.Print($"┌┈┈┈┈ Test ┈┈┈┈");
        Log(name);
        var result = call();
        GD.Print($"├┈┈┈┈┈┈┈┈┈┈┈┈┈┈");
        GD.Print($"│ Result({result})");
        GD.Print($"└┈┈┈┈┈ OK ┈┈┈┈┈");
        s_IsInTest = false;
        return result;
    }

    public static async GDTask RunAsync(Func<GDTask> call, [CallerArgumentExpression(nameof(call))] string name = null)
    {
        s_IsInTest = true;
        GD.Print("┌┈┈┈┈ Test ┈┈┈┈");
        Log(name);
        await call();
        GD.Print("└┈┈┈┈┈ OK ┈┈┈┈┈");
        s_IsInTest = false;
    }

    public static async GDTask<T> RunAsync<T>(Func<GDTask<T>> call, [CallerArgumentExpression(nameof(call))] string name = null)
    {
        s_IsInTest = true;
        GD.Print($"┌┈┈┈┈ Test ┈┈┈┈");
        Log(name);
        var result = await call();
        GD.Print($"├┈┈┈┈┈┈┈┈┈┈┈┈┈┈");
        GD.Print($"│ Result({result})");
        GD.Print($"└┈┈┈┈┈ OK ┈┈┈┈┈");
        s_IsInTest = false;
        return result;
    }

    public static async GDTask Delay2Seconds()
    {
        Log("Start next action after 2.0");
        await GDTask.Delay(TimeSpan.FromSeconds(0.5f));
        Log("Start next action after 1.5");
        await GDTask.Delay(TimeSpan.FromSeconds(0.5f));
        Log("Start next action after 1.0");
        await GDTask.Delay(TimeSpan.FromSeconds(0.5f));
        Log("Start next action after 0.5");
        await GDTask.Delay(TimeSpan.FromSeconds(0.5f));
    }

    public static void Log(string message) => LogCore(message, GD.Print);
    public static void LogWarning(string message) => LogCore(message, GD.PushWarning);
    public static void LogError(string message) => LogCore(message, GD.PrintErr);

    private static void LogCore(string message, Action<string> logHandler) => 
        logHandler(s_IsInTest ? $"│ {message.Replace("\n", "\n│ ")}" : message);
}
