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
    private ViewModelBase Login_Content;
    private ViewModelBase Register_Content;
    public ViewModelBase CurrentLoginContent
    {
        get => Parent.current_Content;
        set => Parent.current_Content = value;
    }
    public LoginHeaderModel(MainWindowViewModel Parent)
    {
        this.Parent = Parent;
        Login_Content = new LoginContentModel(Parent);
        Register_Content = new RegisterContentModel(Parent);
        Parent.current_Content = Login_Content;
    }
    public void Button_Action_Redirect_Login()
    {
        //Add functionality here
        Console.WriteLine("Button_Action_Redirect_Login and: "+Parent.User_Name);
        CurrentLoginContent = Login_Content;
        
    }
    public void Button_Action_Redirect_Register()
    {
        //Add functionality here
        CurrentLoginContent = Register_Content;
        Console.WriteLine("Button_Action_Redirect_Register");
    }
}