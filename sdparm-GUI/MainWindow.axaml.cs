using Avalonia.Controls;
using sdparm_GUI.ViewModels;

namespace sdparm_GUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}