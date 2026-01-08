namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;

public class RegisterContentModel : ViewModelBase
{
    //Ich übergebe im Constructer den MainWindowViewModel, um nachher im Main Menu immer noch User_Name und Passwort zu haben
    private MainWindowViewModel Parrent;
    public RegisterContentModel(MainWindowViewModel Parrent)
    {
        this.Parrent = Parrent;
    }
    private string _User_Name = "";
    public string User_Name
    {
        get
        {
          return _User_Name;
        }
        set
        {
            Parrent.User_Name = value;
            _User_Name = value;
            this.RaiseAndSetIfChanged(ref _User_Name,value);
        } 
    }
    private string _Password = "";
    public string Password
    {
        get
        {
          return _Password;  
        }
        set
        {
            Parrent.Password = value;
            _Password = value;
            this.RaiseAndSetIfChanged(ref _Password,value);
        } 
    }
    public void Button_Action_Register()
    {
        //Add funktionality here
        Console.WriteLine("Button_Action_Register");
    }

}