using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using DynaRealm.ViewModels;

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
    }
}
