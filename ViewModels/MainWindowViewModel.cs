using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DynaRealm.Models;
using DynaRealm.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DynaRealm.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // タブ一覧
    public ObservableCollection<Tab> Tabs { get; set; } = new();

    // カレンダー日付一覧
    public ObservableCollection<CalendarDayViewModel> CalendarDays { get; set; } = new();

    private DateTime _currentDisplayDate = DateTime.Today;

    // 月表示
    public string CurrentMonthText { get; set; } = string.Empty;

    // 曜日
    public string[] WeekDays { get; set; } =
    {
        "日", "月", "火", "水", "木", "金", "土"
    };

    public MainWindowViewModel()
    {
        var tabService = new TabService();
        var tabs = tabService.GetTabs();

        foreach (Tab tab in tabs)
        {
            Tabs.Add(tab);
        }

        // カレンダー日付生成
        UpdateCurrentMonthText();
        CreateCalendarDays(DateTime.Today);
    }

    public void ReloadCalendar()
    {
        CreateCalendarDays(_currentDisplayDate);
    }

    public void MovePreviousMonth()
    {
        _currentDisplayDate = _currentDisplayDate.AddMonths(-1);
        UpdateCurrentMonthText();
        CreateCalendarDays(_currentDisplayDate);
    }

    public void MoveNextMonth()
    {
        _currentDisplayDate = _currentDisplayDate.AddMonths(1);
        UpdateCurrentMonthText();
        CreateCalendarDays(_currentDisplayDate);
    }

    private void UpdateCurrentMonthText()
    {
        CurrentMonthText = $"{_currentDisplayDate.Year}年{_currentDisplayDate.Month}月";
        OnPropertyChanged(nameof(CurrentMonthText));
    }

    // カレンダー日付生成処理
    private void CreateCalendarDays(DateTime targetDate)
    {
        CalendarDays.Clear();

        var pageService = new PageService();

        var pages = pageService.GetPagesByMonth(
            targetDate.Year,
            targetDate.Month);

        var firstDayOfMonth = new DateTime(targetDate.Year, targetDate.Month, 1);
        var startDate = firstDayOfMonth.AddDays(-(int)firstDayOfMonth.DayOfWeek);

        for (int i = 0; i < 42; i++)
        {
            var date = startDate.AddDays(i);

            CalendarDays.Add(new CalendarDayViewModel
            {
                Date = date,
                IsCurrentMonth = date.Month == targetDate.Month,

                Pages = pages
                    .Where(p => p.StartDate.Date == date.Date)
                    .Select(p => new CalendarPageItemViewModel
                    {
                        Title = p.Title
                    })
                    .ToList()
            });
        }
    }
}
