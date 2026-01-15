namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;

public class Main_header_model : View_model_base
{
    private MainWindow_view_model parent;
    private View_model_base View_Appointments_Content;
    private View_model_base Make_Appointments_Content;
    private View_model_base _currentMainContent;
    public View_model_base CurrentMainContent
    {
        get => _currentMainContent;
        set => this.RaiseAndSetIfChanged(ref _currentMainContent,value);
    }
    public Main_header_model(MainWindow_view_model parent)
    {
        this.parent = parent;
        View_Appointments_Content = new Login_content_model(parent);
        Make_Appointments_Content = new Register_content_model(parent);
        _currentMainContent = Make_Appointments_Content;
    }
    private string _site_name = "Our Appointment Management System";
    public string site_name
    {
        get
        {
          return _site_name;
        }
        set => this.RaiseAndSetIfChanged(ref _site_name,value);
    }
    public string user_name
    {
        get
        {
          return parent.user_name;  
        }
        set => parent.user_name = value;
    }
    public string password
    {
        get
        {
          return parent.password;  
        }
        set => parent.password = value;
    }
    public void Button_Action_View_Appointments()
    {
        //Add funktionality here
        Console.WriteLine("Button_Action_View_Appointments");
    }
    public void Button_Action_Make_Appointments()
    {
        Console.WriteLine("Button_Action_Make_Appointments");
        //Add funktionality here
    }
    public void Button_Action_Logout()
    {
        Console.WriteLine("Button_Action_Logout");
        parent.current_header = parent._login_header;
        parent.current_content = parent._blank_content;
        //parent.user_name = "";
        //Add funktionality here
    }

}