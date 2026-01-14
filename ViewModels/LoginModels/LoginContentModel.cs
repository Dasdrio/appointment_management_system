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
    //I pass the MainWindowViewModel in the constructor so that I still have the user name and password in the main menu later on.
    private MainWindowViewModel Parent;
    public LoginContentModel(MainWindowViewModel Parent){
        this.Parent = Parent;
    }
    public string user_name{
        get => Parent.user_name;
        set => Parent.user_name = value;
    }
    public string password{
        get => Parent.password; 
        set => Parent.password = value;
    }
    public void Button_Action_Login(){
        //add login check

        //then continue
        Parent.current_header = Parent._main_header;
        Parent.current_content = Parent._blank_content;
        password = "";
        Console.WriteLine("Button_Action_Login");
        Console.WriteLine("UserName: "+Parent.user_name);
    }
}