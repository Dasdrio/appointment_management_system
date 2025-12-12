namespace appointment_management_system.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public partial class MainWindowViewModel : ViewModelBase,INotifyPropertyChanged
{
    public string Greeting { get; } = "Welcome to Avalonia!";
}
