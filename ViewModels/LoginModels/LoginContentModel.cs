namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;

public class LoginContentModel : ViewModelBase
{
    //Ich übergebe im Constructer den MainWindowViewModel, um nachher im Main Menu immer noch User_Name und Passwort zu haben
    private MainWindowViewModel Parent;
    public LoginContentModel(MainWindowViewModel Parent)
    {
        this.Parent = Parent;
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
            Parent.User_Name = value;
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
            Parent.Password = value;
            _Password = value;
            this.RaiseAndSetIfChanged(ref _Password,value);
        } 
    }
    public void Button_Action_Login()
    {
        //Hier Login überprüfung einfügen

        //Dann weiter
        Parent.Current_Top = Parent._Main_Header;
        Password = "";
        User_Name = "";
        Parent.Password ="";
        Parent.current_Content = Parent._Blank_Content;
        Console.WriteLine("Button_Action_Login");
    }

}