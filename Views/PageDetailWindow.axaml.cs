using Avalonia.Controls;
using Avalonia.Interactivity;
using DynaRealm.ViewModels;
using System;

namespace DynaRealm.Views
{

    public partial class PageDetailWindow : Window
    {
        private readonly Action? _onPageSaved;

        public PageDetailWindow()
        {
            InitializeComponent();
        }

        public PageDetailWindow(Guid pageId, Action? onPageSaved = null)
        {
            InitializeComponent();

            _onPageSaved = onPageSaved;
            DataContext = new PageDetailViewModel(pageId);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PageDetailViewModel viewModel)
            {
                viewModel.Save();

                _onPageSaved?.Invoke();

                Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PageDetailViewModel viewModel)
            {
                viewModel.Delete();

                _onPageSaved?.Invoke();

                Close();
            }
        }
    }
}