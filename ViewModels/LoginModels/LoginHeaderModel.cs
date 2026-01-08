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
    private ViewModelBase _currentLoginContent;
    public ViewModelBase CurrentLoginContent
    {
        get => _currentLoginContent;
        set => this.RaiseAndSetIfChanged(ref _currentLoginContent,value);
    }
    public LoginHeaderModel(MainWindowViewModel Parent)
    {
        this.Parent = Parent;
        Login_Content = new LoginContentModel(Parent);
        Register_Content = new RegisterContentModel(Parent);
        _currentLoginContent = Login_Content;
    }
    public void Button_Action_Redirect_Login()
    {
        //Add functionality here
        Console.WriteLine("Button_Action_Redirect_Login and: "+Parent.User_Name);
        CurrentLoginContent = Login_Content;
        Parent.current_Content = Login_Content;
        
    }
    public void Button_Action_Redirect_Register()
    {
        //Add functionality here
        CurrentLoginContent = Register_Content;
        Parent.current_Content = Register_Content;
        Console.WriteLine("Button_Action_Redirect_Register");
    }
}