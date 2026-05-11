using Avalonia.Controls;
using Avalonia.Interactivity;
using DynaRealm.ViewModels;
using System;

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

        public PageEditorWindow(DateTime date)
        {
            InitializeComponent();

            _viewModel = new PageEditorViewModel(date);
            DataContext = _viewModel;
        }

        private void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            _viewModel.Save();
            Close();
        }
    }
}