using Avalonia.Controls;
using Avalonia.Input;
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
            ViewModel.ShowDayOverlay(day);
        }
    }

    private void OpenPageEditor_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.OpenEditorScreen(DateTime.Today);
    }

    private void PreviousMonth_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.MovePreviousMonth();
    }

    private void NextMonth_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.MoveNextMonth();
    }

    private void ShowMonthView_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.ShowMonthView();
    }

    private void ShowDayView_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.ShowDayView();
    }

    private void PreviousDay_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.MovePreviousDay();
    }

    private void NextDay_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.MoveNextDay();
    }

    private void TabButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Tag is Guid tabId)
        {
            ViewModel.SelectTab(tabId);
        }
    }

    private void SearchButton_Click(object? sender, RoutedEventArgs e)
    {
        var keyword = SearchKeywordTextBox.Text;

        var window = new SearchResultWindow(keyword ?? string.Empty);

        window.Show();
    }

    private void CloseDayOverlay_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.CloseDayOverlay();
    }

    private void OpenPageEditorFromOverlay_Click(object? sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedDayPageList == null)
        {
            return;
        }

        ViewModel.OpenEditorScreen(ViewModel.SelectedDayPageList.Date);
    }

    private void ShowDayViewFromOverlay_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.CloseDayOverlay();
        ViewModel.ShowDayView();
    }

    private void OpenPageDetailFromOverlay_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Tag is Guid pageId)
        {
            ViewModel.OpenDetailScreen(pageId);
        }
    }

    private void OpenPageDetailFromDayView_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Tag is Guid pageId)
        {
            ViewModel.OpenDetailScreen(pageId);
        }
    }

    private void BackToCalendar_Click(object? sender, RoutedEventArgs e)
    {
        ViewModel.ShowCalendarScreen();
    }

    private void SaveEditor_Click(object? sender, RoutedEventArgs e)
    {
        if (ViewModel.CurrentPageEditor == null)
        {
            return;
        }

        ViewModel.CurrentPageEditor.Save();

        ViewModel.ReloadCalendar();
        ViewModel.ShowCalendarScreen();
    }

    private void SaveDetail_Click(object? sender, RoutedEventArgs e)
    {
        if (ViewModel.CurrentPageDetail == null)
        {
            return;
        }

        ViewModel.CurrentPageDetail.Save();

        ViewModel.ReloadCalendar();
        ViewModel.ShowCalendarScreen();
    }

    private void DeleteDetail_Click(object? sender, RoutedEventArgs e)
    {
        if (ViewModel.CurrentPageDetail == null)
        {
            return;
        }

        ViewModel.CurrentPageDetail.Delete();

        ViewModel.ReloadCalendar();
        ViewModel.ShowCalendarScreen();
    }

    private void OverlayBackground_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        ViewModel.CloseDayOverlay();
    }

    private void Window_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape &&
            ViewModel.IsDayOverlayVisible)
        {
            ViewModel.CloseDayOverlay();
        }
    }
}