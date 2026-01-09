namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using System.Threading;
using appointment_management_system.Views;

public partial class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase Login_Content;
    private ViewModelBase Impressum_Content;
    private ViewModelBase _current_Content;
    private ViewModelBase _Current_Top;
    public ViewModelBase _Login_Header;
    public ViewModelBase _Main_Header;
    public ViewModelBase _Blank_Content;
    private IClassicDesktopStyleApplicationLifetime desktop;
    //Überprüfen ob das funktioniert. Der Top Content sollte mit dem LoginTop Content ausgetauscht werden und darüber dann auch den Content dieser Wiederum kann auch Impressum durch die MainWindowView Anzeigen. Mal sehen
    public MainWindowViewModel(IClassicDesktopStyleApplicationLifetime desktop)
    {
        Login_Content = new LoginContentModel(this);
        Impressum_Content = new ImpressumContentModel();
        _Blank_Content = new blankContentModel();
        _Main_Header = new MainHeaderModel(this);
        _Login_Header = new LoginHeaderModel(this);
        _Current_Top = _Login_Header;
        _current_Content = Impressum_Content;
        this.desktop = desktop;
    }
    public ViewModelBase current_Content
    {
        get => _current_Content;
        set => this.RaiseAndSetIfChanged(ref _current_Content,value);
    }
    public ViewModelBase Current_Top
    {
        get => _Current_Top;
        set => this.RaiseAndSetIfChanged(ref _Current_Top,value);
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
    //Button functions    
    //Main Page (Only change the Content Grid to the aproriate content) exept for the logout there you should create a new LoginWindow with the User logged out
    
    //All Pages (Only change the Content Grid to the aproriate content)
    public void Button_Action_Impressum()
    {
        current_Content = Impressum_Content;
        Console.WriteLine("Button_Action_Impressum");
        //Add funktionality here
    }
    public void Button_Action_AGB()
    {
        desktop.MainWindow.Hide();
        desktop.MainWindow.Show();
        Console.WriteLine("Button_Action_AGB");
        //Add funktionality here
    }
    public void Button_Action_Contact()
    {
        Console.WriteLine("Button_Action_Contact");
        //Add funktionality here
    }
    public void Button_Action_Data_Security_Information()
    {
        Console.WriteLine("Button_Action_Data_Security_Information");
        //Add funktionality here
    }
    /*Template
    public void Button_Action_()
    {
        Console.WriteLine("Button_Action_");
        //Add funktionality here
    }*/
    //Functionality Funktions
}
