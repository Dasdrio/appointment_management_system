namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;

public class LoginHeaderModel : ViewModelBase
{
    private MainWindowViewModel parent;
    private ViewModelBase _login_content;
    private ViewModelBase Register_Content;
    public ViewModelBase CurrentLoginContent
    {
        get => parent.current_content;
        set => parent.current_content = value;
    }
    public LoginHeaderModel(MainWindowViewModel parent)
    {
        this.parent = parent;
        _login_content = new LoginContentModel(parent);
        Register_Content = new RegisterContentModel(parent);
        parent.current_content = _login_content;
    }
    public void Button_Action_Redirect_Login()
    {
        //Add functionality here
        Console.WriteLine("Button_Action_Redirect_Login and: "+parent.user_name);
        CurrentLoginContent = _login_content;
        
    }
    public void Button_Action_Redirect_Register()
    {
        //Add functionality here
        CurrentLoginContent = Register_Content;
        Console.WriteLine("Button_Action_Redirect_Register");
    }
}