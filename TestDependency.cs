using System;
using Avalonia.Controls;
using Microsoft.Extensions.Logging;

namespace GetStartedApp
{
    public class TestDependency : ITestDependency
    {
        private ILogger<TestDependency> _logger;

        public TestDependency(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TestDependency>();

        }

        public void SetText(TextBlock tb1)
        {
            tb1.Text = "Value is set by the test dependency";
        }

        public void TestSomething()
        {
            _logger.LogInformation("Test dependency is called");
        }
    }
}

