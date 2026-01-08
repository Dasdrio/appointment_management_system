namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;

public class LoginContentModel : ViewModelBase
{
    private string _Text = "Login";
    public string Text
    {
        get
        {
          return _Text;  
        }
        set => this.RaiseAndSetIfChanged(ref _Text,value);
    }
}