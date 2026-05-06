using System.Collections.ObjectModel;
using DynaRealm.Models;
using DynaRealm.Services;

namespace DynaRealm.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Tab> Tabs { get; set; } = new();

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
