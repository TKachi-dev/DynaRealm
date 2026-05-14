using Avalonia.Controls;
using Avalonia.Interactivity;
using DynaRealm.ViewModels;
using System;

namespace DynaRealm.Views
{

    public partial class SearchResultWindow : Window
    {
        public SearchResultWindow()
        {
            InitializeComponent();
        }

        public SearchResultWindow(string keyword)
        {
            InitializeComponent();

            DataContext = new SearchResultViewModel(keyword);
        }

        private void OpenPageDetail_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
                button.Tag is Guid pageId)
            {
                var window = new PageDetailWindow(pageId);
                window.Show();
            }
        }
    }
}