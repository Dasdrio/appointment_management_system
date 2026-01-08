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

    private string _User_Name = "";
    private string _Password = "";
    private string _Site_Name = "Our Appointment Management System";
    public string Site_Name
    {
        get
        {
          return _Site_Name;
        }
        set => this.RaiseAndSetIfChanged(ref _Site_Name,value);
    }
    public string User_Name
    {
        get
        {
          return _User_Name;  
        }
        set
        {
            _User_Name = value;
            this.RaiseAndSetIfChanged(ref _User_Name,value);
        }
    }
    public string Password
    {
        get
        {
          return _Password;  
        }
        set => this.RaiseAndSetIfChanged(ref _Password,value);
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
        Parent.Current_Top = Parent._Login_Header;
        Parent.current_Content = Parent._Blank_Content;
        Parent.User_Name = "";
        //Add funktionality here
    }

}