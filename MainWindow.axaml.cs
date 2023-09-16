using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.Logging;

namespace GetStartedApp;

public partial class MainWindow : Window
{
    private ITestDependency _testDependency;
    private ILogger<MainWindow> _logger;

    public MainWindow(ILoggerFactory loggerFactory, ITestDependency testDependency)
    {
        _testDependency = testDependency;
        _logger = loggerFactory.CreateLogger<MainWindow>();
        InitializeComponent();
    }

    
    public void CalculateClicked(object source, RoutedEventArgs args)
    {
        _logger.LogInformation("Button is clicked at " + DateTime.Now.ToString("yyyy-MM-ddTHH_mm_ss"));
        _testDependency.SetText(Tb1);
    }
}