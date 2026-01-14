namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;

public class MainHeaderModel : ViewModelBase
{
    private MainWindowViewModel Parent;
    private ViewModelBase View_Appointments_Content;
    private ViewModelBase Make_Appointments_Content;
    private ViewModelBase _currentMainContent;
    public ViewModelBase CurrentMainContent
    {
        get => _currentMainContent;
        set => this.RaiseAndSetIfChanged(ref _currentMainContent,value);
    }
    public MainHeaderModel(MainWindowViewModel Parent)
    {
        this.Parent = Parent;
        View_Appointments_Content = new LoginContentModel(Parent);
        Make_Appointments_Content = new RegisterContentModel(Parent);
        _currentMainContent = Make_Appointments_Content;
    }

    private string _user_name = "";
    private string _password = "";
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
          return _user_name;  
        }
        set
        {
            _user_name = value;
            this.RaiseAndSetIfChanged(ref _user_name,value);
        }
    }
    public string password
    {
        get
        {
          return _password;  
        }
        set => this.RaiseAndSetIfChanged(ref _password,value);
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
        Parent.current_header = Parent._login_header;
        Parent.current_content = Parent._blank_content;
        Parent.user_name = "";
        //Add funktionality here
    }

}