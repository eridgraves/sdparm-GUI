namespace sdparm_GUI.Models;

public record SdparmOption(
    string LongOption,
    string ShortOption,
    string Description,
    bool RequiresValue,
    string? ValueHint = null)
{
    public string DisplayName => ValueHint is null
        ? $"--{LongOption} (-{ShortOption})"
        : $"--{LongOption}=<{ValueHint}> (-{ShortOption} {ValueHint})";

    public string BuildArgument(string value) =>
        RequiresValue ? $"--{LongOption}={value}" : $"--{LongOption}";
}
