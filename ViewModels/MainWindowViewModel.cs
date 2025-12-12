namespace appointment_management_system.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public partial class MainWindowViewModel : ViewModelBase,INotifyPropertyChanged
{
    string _Site_Name = "Our Appointment Management System";
    string _User_Name = "Max MusterMusterMusterMannnnnn";
    public string Site_Name {
        get
        {
            return _Site_Name;
        }
        set
        {
            _Site_Name = value;
            OnPropertyChanged(nameof(Site_Name));
        }
    }
    public string User_Name {
        get
        {
            return _User_Name;
        }
        set
        {
            _User_Name = value;
            OnPropertyChanged(nameof(User_Name));
        }
    }
    //Als nächstes Buttons funktionen hinzufügen und binden
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
