using Avalonia.Controls;

namespace GetStartedApp
{
    public interface ITestDependency
    {
        void SetText(TextBlock tb1);
        void TestSomething();
    }
}