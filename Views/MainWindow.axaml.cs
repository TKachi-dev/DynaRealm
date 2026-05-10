using Avalonia.Controls;
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

    private void OpenPageEditor_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = new PageEditorWindow();

        window.Closed += (_, _) =>
        {
            ViewModel.ReloadCalendar();
        };

        window.Show();
    }

    private void PreviousMonth_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ViewModel.MovePreviousMonth();
    }

    private void NextMonth_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ViewModel.MoveNextMonth();
    }
}