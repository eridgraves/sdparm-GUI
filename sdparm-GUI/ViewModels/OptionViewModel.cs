using sdparm_GUI.Models;

namespace sdparm_GUI.ViewModels;

public class OptionViewModel : ViewModelBase
{
    private bool _isSelected;
    private string _value = string.Empty;

    public OptionViewModel(SdparmOption option)
    {
        Option = option;
    }

    public SdparmOption Option { get; }

    public string DisplayName => Option.DisplayName;
    public string Description => Option.Description;
    public bool RequiresValue => Option.RequiresValue;

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public string Value
    {
        get => _value;
        set => SetProperty(ref _value, value);
    }

    public string? BuildArgument()
    {
        if (!IsSelected)
            return null;

        if (RequiresValue && string.IsNullOrWhiteSpace(Value))
            return null;

        return RequiresValue ? Option.BuildArgument(Value) : Option.BuildArgument(string.Empty);
    }
}
