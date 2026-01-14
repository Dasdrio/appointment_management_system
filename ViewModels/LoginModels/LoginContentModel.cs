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
    private MainWindowViewModel parent;
    public LoginContentModel(MainWindowViewModel parent){
        this.parent = parent;
    }
    public string user_name{
        get => parent.user_name;
        set => parent.user_name = value;
    }
    public string password{
        get => parent.password; 
        set => parent.password = value;
    }
    public void button_action_login(){
        //add login check

        //then continue
        parent.current_header = parent._main_header;
        parent.current_content = parent._blank_content;
        password = "";
        Console.WriteLine("button_action_login");
        Console.WriteLine("UserName: "+parent.user_name);
    }
}