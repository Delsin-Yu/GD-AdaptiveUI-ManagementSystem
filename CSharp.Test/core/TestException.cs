using System;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public class TestException : Exception
{
    public TestModule TestModule { get; }
    public TestException(TestModule testModule, Exception e) : base($"Error when running test module: {testModule.Name}", e) => TestModule = testModule;
}
