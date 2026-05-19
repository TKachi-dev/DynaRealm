using System;

namespace DynaRealm.ViewModels
{
    public class TabViewModel : ViewModelBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsSelected { get; set; }

        public string BackgroundColor =>
            IsSelected ? "#333333" : "#EEEEEE";

        public string ForegroundColor =>
            IsSelected ? "White" : "Black";

        public void SetSelected(bool isSelected)
        {
            IsSelected = isSelected;

            OnPropertyChanged(nameof(IsSelected));
            OnPropertyChanged(nameof(BackgroundColor));
            OnPropertyChanged(nameof(ForegroundColor));
        }
    }
}
