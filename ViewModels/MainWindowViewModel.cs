using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynaRealm.Models;
using DynaRealm.Services;

namespace DynaRealm.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // タブ一覧
    public ObservableCollection<Tab> Tabs { get; set; } = new();

    // カレンダー日付一覧
    public ObservableCollection<CalendarDayViewModel> CalendarDays { get; set; } = new();

    // 月表示
    public string CurrentMonthText { get; set; } =
        $"{DateTime.Today.Year}年{DateTime.Today.Month}月";

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
        CreateCalendarDays(DateTime.Today);
    }

    // カレンダー日付生成処理
    private void CreateCalendarDays(DateTime targetDate)
    {
        CalendarDays.Clear();

        var firstDayOfMonth = new DateTime(targetDate.Year, targetDate.Month, 1);
        var startDate = firstDayOfMonth.AddDays(-(int)firstDayOfMonth.DayOfWeek);

        for (int i = 0; i < 42; i++)
        {
            var date = startDate.AddDays(i);

            CalendarDays.Add(new CalendarDayViewModel
            {
                Date = date,
                IsCurrentMonth = date.Month == targetDate.Month,

                Pages = new List<CalendarPageItemViewModel>
                {
                    new CalendarPageItemViewModel
                    {
                        Title = "Java勉強"
                    },
                    new CalendarPageItemViewModel
                    {
                        Title = "買い物"
                    }
                }
            });
        }
    }
}
