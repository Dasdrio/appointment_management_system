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
    private MainWindowViewModel Parent;
    private ViewModelBase login_content;
    private ViewModelBase Register_Content;
    public ViewModelBase CurrentLoginContent
    {
        get => Parent.current_content;
        set => Parent.current_content = value;
    }
    public LoginHeaderModel(MainWindowViewModel Parent)
    {
        this.Parent = Parent;
        login_content = new LoginContentModel(Parent);
        Register_Content = new RegisterContentModel(Parent);
        Parent.current_content = login_content;
    }
    public void Button_Action_Redirect_Login()
    {
        //Add functionality here
        Console.WriteLine("Button_Action_Redirect_Login and: "+Parent.user_name);
        CurrentLoginContent = login_content;
        
    }
    public void Button_Action_Redirect_Register()
    {
        //Add functionality here
        CurrentLoginContent = Register_Content;
        Console.WriteLine("Button_Action_Redirect_Register");
    }
}