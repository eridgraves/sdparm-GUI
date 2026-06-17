using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using sdparm_GUI.Models;
using sdparm_GUI.Services;

namespace sdparm_GUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly SdparmProcessRunner _runner = new();
    private readonly StringBuilder _output = new();

    private string _executablePath = "sdparm";
    private string _devicePath = string.Empty;
    private string _outputText = string.Empty;
    private string _commandPreview = "sdparm";
    private bool _isRunning;
    private CancellationTokenSource? _runCts;

    public MainWindowViewModel()
    {
        Options = new ObservableCollection<OptionViewModel>(
            SdparmOptionCatalog.All.Select(o => new OptionViewModel(o)));

        foreach (var option in Options)
            option.PropertyChanged += (_, _) => UpdateCommandPreview();

        RunCommand = new RelayCommand(RunAsync, () => !IsRunning);
        CancelCommand = new RelayCommand(Cancel, () => IsRunning);
        ClearOutputCommand = new RelayCommand(ClearOutput);

        UpdateCommandPreview();
    }

    public ObservableCollection<OptionViewModel> Options { get; }

    public RelayCommand RunCommand { get; }
    public RelayCommand CancelCommand { get; }
    public RelayCommand ClearOutputCommand { get; }

    public string ExecutablePath
    {
        get => _executablePath;
        set
        {
            if (SetProperty(ref _executablePath, value))
                UpdateCommandPreview();
        }
    }

    public string DevicePath
    {
        get => _devicePath;
        set
        {
            if (SetProperty(ref _devicePath, value))
                UpdateCommandPreview();
        }
    }

    public string CommandPreview
    {
        get => _commandPreview;
        private set => SetProperty(ref _commandPreview, value);
    }

    public string OutputText
    {
        get => _outputText;
        private set => SetProperty(ref _outputText, value);
    }

    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            if (SetProperty(ref _isRunning, value))
            {
                RunCommand.RaiseCanExecuteChanged();
                CancelCommand.RaiseCanExecuteChanged();
            }
        }
    }

    private string BuildArguments()
    {
        var args = Options
            .Select(o => o.BuildArgument())
            .Where(a => a is not null);

        var parts = args.Concat(string.IsNullOrWhiteSpace(DevicePath) ? Enumerable.Empty<string>() : new[] { DevicePath });
        return string.Join(' ', parts);
    }

    private void UpdateCommandPreview()
    {
        var arguments = BuildArguments();
        CommandPreview = string.IsNullOrEmpty(arguments)
            ? ExecutablePath
            : $"{ExecutablePath} {arguments}";
    }

    private async Task RunAsync()
    {
        _output.Clear();
        OutputText = string.Empty;
        AppendOutput($"$ {CommandPreview}");

        _runCts = new CancellationTokenSource();
        IsRunning = true;
        try
        {
            var exitCode = await _runner.RunAsync(ExecutablePath, BuildArguments(), AppendOutput, _runCts.Token);
            AppendOutput($"[exited with code {exitCode}]");
        }
        catch (OperationCanceledException)
        {
            AppendOutput("[cancelled]");
        }
        finally
        {
            IsRunning = false;
            _runCts?.Dispose();
            _runCts = null;
        }
    }

    private void Cancel() => _runCts?.Cancel();

    private void ClearOutput()
    {
        _output.Clear();
        OutputText = string.Empty;
    }

    private void AppendOutput(string line)
    {
        Dispatcher.UIThread.Post(() =>
        {
            _output.AppendLine(line);
            OutputText = _output.ToString();
        });
    }
}
