using Avalonia.Controls;

namespace DynaRealm.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenPageEditor_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = new PageEditorWindow();
        window.Show();
    }
}