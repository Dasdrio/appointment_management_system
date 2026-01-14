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
    private MainWindowViewModel Parent;
    public RegisterContentModel(MainWindowViewModel Parent)
    {
        this.Parent = Parent;
    }
    public string user_name
    {
        get
        {
          return Parent.user_name;
        }
        set
        {
            Parent.user_name = value;
        } 
    }
    public string password
    {
        get
        {
          return Parent.password;  
        }
        set
        {
            Parent.password = value;
        } 
    }
    public void Button_Action_Register()
    {
        //Add funktionality here
        Console.WriteLine("Button_Action_Register");
    }

}