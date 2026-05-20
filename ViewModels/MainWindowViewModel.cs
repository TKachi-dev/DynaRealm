using DynaRealm.Models;
using DynaRealm.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DynaRealm.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // タブ一覧
    public ObservableCollection<TabViewModel> Tabs { get; set; } = new();

    // カレンダー日付一覧
    public ObservableCollection<CalendarDayViewModel> CalendarDays { get; set; } = new();

    // 日付一覧オーバーレイ
    public DayPageListViewModel? SelectedDayPageList { get; set; }

    public PageEditorViewModel? CurrentPageEditor { get; set; }

    public PageDetailViewModel? CurrentPageDetail { get; set; }

    public bool IsDayOverlayVisible { get; set; }

    public string CurrentScreen { get; set; } = "Calendar";

    public bool IsCalendarScreen => CurrentScreen == "Calendar";

    public bool IsEditorScreen => CurrentScreen == "Editor";

    public bool IsDetailScreen => CurrentScreen == "Detail";

    public string CurrentScreenTitle =>
        CurrentScreen switch
        {
            "Calendar" => "カレンダー",
            "Editor" => "ページ登録",
            "Detail" => "ページ詳細",
            _ => string.Empty
        };

    private DateTime _currentDisplayDate = DateTime.Today;

    private Guid _selectedTabId;

    public Guid SelectedTabId => _selectedTabId;

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
            Tabs.Add(new TabViewModel
            {
                Id = tab.Id,
                Name = tab.Name
            });
        }

        if (Tabs.Any())
        {
            var firstTab = Tabs.First();

            _selectedTabId = firstTab.Id;
            firstTab.SetSelected(true);
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

    public void SelectTab(Guid tabId)
    {
        _selectedTabId = tabId;

        foreach (var tab in Tabs)
        {
            tab.SetSelected(tab.Id == tabId);
        }

        CreateCalendarDays(_currentDisplayDate);
    }

    public void ShowDayOverlay(CalendarDayViewModel day)
    {
        SelectedDayPageList = new DayPageListViewModel(day);
        IsDayOverlayVisible = true;

        OnPropertyChanged(nameof(SelectedDayPageList));
        OnPropertyChanged(nameof(IsDayOverlayVisible));
    }

    public void CloseDayOverlay()
    {
        IsDayOverlayVisible = false;

        OnPropertyChanged(nameof(IsDayOverlayVisible));
    }

    public void ShowCalendarScreen()
    {
        CurrentScreen = "Calendar";

        OnPropertyChanged(nameof(CurrentScreen));
        OnPropertyChanged(nameof(IsCalendarScreen));
        OnPropertyChanged(nameof(IsEditorScreen));
        OnPropertyChanged(nameof(IsDetailScreen));
        OnPropertyChanged(nameof(CurrentScreenTitle));
    }

    public void OpenEditorScreen(DateTime date)
    {
        CurrentPageEditor = new PageEditorViewModel(
            date,
            _selectedTabId);

        CloseDayOverlay();
        ShowEditorScreen();

        OnPropertyChanged(nameof(CurrentPageEditor));
    }

    public void ShowEditorScreen()
    {
        CurrentScreen = "Editor";

        OnPropertyChanged(nameof(CurrentScreen));
        OnPropertyChanged(nameof(IsCalendarScreen));
        OnPropertyChanged(nameof(IsEditorScreen));
        OnPropertyChanged(nameof(IsDetailScreen));
        OnPropertyChanged(nameof(CurrentScreenTitle));
    }

    public void OpenDetailScreen(Guid pageId)
    {
        CurrentPageDetail = new PageDetailViewModel(pageId);

        CloseDayOverlay();
        ShowDetailScreen();

        OnPropertyChanged(nameof(CurrentPageDetail));
    }

    public void ShowDetailScreen()
    {
        CurrentScreen = "Detail";

        OnPropertyChanged(nameof(CurrentScreen));
        OnPropertyChanged(nameof(IsCalendarScreen));
        OnPropertyChanged(nameof(IsEditorScreen));
        OnPropertyChanged(nameof(IsDetailScreen));
        OnPropertyChanged(nameof(CurrentScreenTitle));
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
            targetDate.Month,
            _selectedTabId);

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
                        PageId = p.Id,
                        Title = p.Title
                    })
                    .ToList()
            });
        }
    }
}
