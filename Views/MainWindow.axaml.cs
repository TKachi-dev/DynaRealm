using Avalonia.Controls;
using Avalonia.Interactivity;
using DynaRealm.ViewModels;
using System;

namespace DynaRealm.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel ViewModel =>
        (MainWindowViewModel)DataContext!;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void DayCell_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Tag is CalendarDayViewModel day)
        {
            var window = new DayPageListWindow(day, ViewModel.ReloadCalendar);
            window.Show();
        }
    }

    private void OpenPageEditor_Click(object? sender, RoutedEventArgs e)
    {
        var window = new PageEditorWindow();

        window.Closed += (_, _) =>
        {
            ViewModel.ReloadCalendar();
        };

        window.Show();
    }

    private void PreviousMonth_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.MovePreviousMonth();
    }

    private void NextMonth_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.MoveNextMonth();
    }

    private void TabButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Tag is Guid tabId)
        {
            ViewModel.SelectTab(tabId);
        }
    }

    private void SearchButton_Click(Object? sender, RoutedEventArgs e)
    {
        var keyword = SearchKeywordTextBox.Text;

        var window = new SearchResultWindow(keyword ?? string.Empty);

        window.Show();
    }
}