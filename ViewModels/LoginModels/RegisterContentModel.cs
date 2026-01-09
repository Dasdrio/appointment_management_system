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
    public string User_Name
    {
        get
        {
          return Parent.User_Name;
        }
        set
        {
            Parent.User_Name = value;
        } 
    }
    public string Password
    {
        get
        {
          return Parent.Password;  
        }
        set
        {
            Parent.Password = value;
        } 
    }
    public void Button_Action_Register()
    {
        //Add funktionality here
        Console.WriteLine("Button_Action_Register");
    }

}