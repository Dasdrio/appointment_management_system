namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _Site_Name = "Our Appointment Management System";
    private string _User_Name = "Max MusterMusterMusterMannnnnn";
    
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
    
    public void Button_Action_View_Appointments()
    {
        //Hier Funktionen einfügen
        Console.WriteLine("Button_Action_View_Appointments");
    }
    public void Button_Action_Make_Appointments()
    {
        Console.WriteLine("Button_Action_Make_Appointments");
        //Hier Funktionen einfügen
    }
    public void Button_Action_Logout()
    {
        Console.WriteLine("Button_Action_Logout");
        //Hier Funktionen einfügen
    }
    public void Button_Action_Impressum()
    {
        Console.WriteLine("Button_Action_Impressum");
        //Hier Funktionen einfügen
    }
    public void Button_Action_AGB()
    {
        Console.WriteLine("Button_Action_AGB");
        //Hier Funktionen einfügen
    }
    public void Button_Action_Contact()
    {
        Console.WriteLine("Button_Action_Contact");
        //Hier Funktionen einfügen
    }
    public void Button_Action_Data_Security_Information()
    {
        Console.WriteLine("Button_Action_Data_Security_Information");
        //Hier Funktionen einfügen
    }
    //Template
    public void Button_Action_()
    {
        Console.WriteLine("Button_Action_");
        //Hier Funktionen einfügen
    }
    
}
