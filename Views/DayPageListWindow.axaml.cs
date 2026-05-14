using Avalonia.Controls;
using Avalonia.Interactivity;
using DynaRealm.ViewModels;
using System;

namespace DynaRealm.Views
{
    public partial class DayPageListWindow : Window
    {
        private readonly Action? _onPageSaved;

        public DayPageListWindow()
        {
            InitializeComponent();
        }

        public DayPageListWindow(CalendarDayViewModel day, Action? onPageSaved = null)
        {
            InitializeComponent();

            _onPageSaved = onPageSaved;
            DataContext = new DayPageListViewModel(day);
        }

        private void OpenPageEditor_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is DayPageListViewModel viewModel)
            {
                var window = new PageEditorWindow(viewModel.Date);

                window.Closed += (_, _) =>
                {
                    _onPageSaved?.Invoke();
                    Close();
                };

                window.Show();
            }
        }

        private void OpenPageDetail_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
                button.Tag is Guid pageId)
            {
                var window = new PageDetailWindow(pageId, _onPageSaved);

                window.Closed += (_, _) =>
                {
                    Close();
                };

                window.Show();
            }
        }
    }
}
