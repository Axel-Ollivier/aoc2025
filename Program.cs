using System;
using System.Linq;
using aoc2025.Days;
using Terminal.Gui;

namespace aoc2025;

internal static class Program
{
    public static void Main()
    {
        Application.Init();
        Theme.SetupGruvboxTheme();

        var top = Application.Top;
        var mainWindow = BuildMainWindow();

        top.Add(mainWindow);

        top.KeyPress += e =>
        {
            if (e.KeyEvent.Key == Key.Q)
            {
                Application.RequestStop();
                e.Handled = true;
            }
        };

        Application.Run();
        Application.Shutdown();
    }

    private static Window BuildMainWindow()
    {
        var window = new Window("Advent of Code - C#")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = Colors.Base
        };

        var listView = new ListView(DayRegistry.Days.Select(d => d.Label).ToList())
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill() - 2,
            ColorScheme = Colors.Base
        };

        listView.OpenSelectedItem += args =>
        {
            var index = args.Item;
            var day = DayRegistry.Days[index];
            OpenDayWindow(day);
        };

        var quitButton = new Button("Exit")
        {
            X = 1,
            Y = Pos.AnchorEnd(1),
            ColorScheme = Colors.Base,
            IsDefault = false
        };
        quitButton.Clicked += () => Application.RequestStop();

        window.Add(listView, quitButton);
        return window;
    }

    private static void OpenDayWindow(DayDefinition day)
    {
        var dayWin = new Window(day.Label)
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = Colors.Dialog
        };

        var output = new TextView
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill() - 1,
            ReadOnly = true,
            ColorScheme = Colors.Dialog
        };

        var backButton = new Button("Return")
        {
            X = 1,
            Y = Pos.AnchorEnd(1),
            ColorScheme = Colors.Base,
        };
        backButton.Clicked += () => Application.RequestStop(dayWin);

        dayWin.Add(output, backButton);

        try
        {
            var result = day.Run();
            output.Text = result;
        }
        catch (Exception ex)
        {
            output.Text = ex.ToString();
        }

        Application.Run(dayWin);
    }
}
