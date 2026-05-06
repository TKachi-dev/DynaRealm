using System;
using System.Collections.ObjectModel;
using DynaRealm.Models;
using DynaRealm.Services;

namespace DynaRealm.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // タブ一覧
    public ObservableCollection<Tab> Tabs { get; set; } = new();

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
    }
}
