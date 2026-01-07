namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private ViewModelBase _currentLoginContent = new LoginContentModel();

    public ViewModelBase CurrentLoginContent
    {
        get => _currentLoginContent;
        set => this.RaiseAndSetIfChanged(ref _currentLoginContent,value);
    }
    private string _User_Name = "Max MusterMusterMusterMannnnnn";
    private string _Password = "";
    public string Site_Name => "Our System";
    //Das was kurz für:
    //private string _Site_Name = "Our Appointment Management System";
    /*public string Site_Name
    {
        get
        {
          return _Site_Name;  
        }
        set => this.RaiseAndSetIfChanged(ref _Site_Name,value);
    }*/
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
    //Login Page (Only change the Content Grid to the aproriate content) exept for the login there you should create a new MainWindow with the User logged in
    public void Button_Action_Redirect_Login()
    {
        //Add funktionality here
        Console.WriteLine("Button_Action_Redirect_Login and: "+User_Name);
        
    }
    public void Button_Action_Redirect_Register()
    {
        //Add funktionality here
        Console.WriteLine("Button_Action_Redirect_Register");
    }
    public void Button_Action_Login()
    {
        //Add funktionality here
        Console.WriteLine("Button_Action_Login");
        CurrentLoginContent = new LoginContentModel();
    }
    
    //Main Page (Only change the Content Grid to the aproriate content) exept for the logout there you should create a new LoginWindow with the User logged out
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
        //Add funktionality here
    }
    //All Pages (Only change the Content Grid to the aproriate content)
    public void Button_Action_Impressum()
    {
        CurrentLoginContent = new ImpressumContentModel();
        Console.WriteLine("Button_Action_Impressum");
        //Add funktionality here
    }
    public void Button_Action_AGB()
    {
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
    //Template
    public void Button_Action_()
    {
        Console.WriteLine("Button_Action_");
        //Add funktionality here
    }
    //Functionality Funktions
}
