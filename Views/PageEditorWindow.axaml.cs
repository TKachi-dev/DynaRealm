using Avalonia.Controls;
using DynaRealm.ViewModels;

namespace DynaRealm.Views
{
    public partial class PageEditorWindow : Window
    {
        private readonly PageEditorViewModel _viewModel;
        public PageEditorWindow()
        {
            InitializeComponent();

            _viewModel = new PageEditorViewModel();

            DataContext = _viewModel;
        }

        private void SaveButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _viewModel.Save();

            Close();
        }
    }
}