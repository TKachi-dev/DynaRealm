using Avalonia.Controls;
using Avalonia.Interactivity;
using DynaRealm.ViewModels;

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
}