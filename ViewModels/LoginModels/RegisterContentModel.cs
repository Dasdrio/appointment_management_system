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
    private MainWindowViewModel parent;
    public RegisterContentModel(MainWindowViewModel parent)
    {
        this.parent = parent;
    }
    public string user_name
    {
        get
        {
          return parent.user_name;
        }
        set
        {
            parent.user_name = value;
        } 
    }
    public string password
    {
        get
        {
          return parent.password;  
        }
        set
        {
            parent.password = value;
        } 
    }
    public void Button_Action_Register()
    {
        //Add funktionality here
        Console.WriteLine("Button_Action_Register");
    }

}