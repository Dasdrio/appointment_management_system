namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;

public class Login_header_model : View_model_base
{
    private MainWindow_view_model parent;
    private View_model_base _login_content;
    private View_model_base Register_Content;
    public View_model_base CurrentLoginContent
    {
        get => parent.current_content;
        set => parent.current_content = value;
    }
    public Login_header_model(MainWindow_view_model parent)
    {
        this.parent = parent;
        _login_content = new Login_content_model(parent);
        Register_Content = new Register_content_model(parent);
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