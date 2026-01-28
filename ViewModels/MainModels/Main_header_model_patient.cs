namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;

public class Main_header_model_patient : View_model_base{
    private MainWindow_view_model parent;
    private View_model_base View_Appointments_Content;
    private View_model_base Make_Appointments_Content;
    public Main_header_model_patient(MainWindow_view_model parent){
        this.parent = parent;
        //View_Appointments_Content = new some_other_model(parent);
        Make_Appointments_Content = new Make_appointments_patient_content_model();
    }
    private string _site_name = "Our Appointment Management System";
    public string site_name{
        get => _site_name;
        set => this.RaiseAndSetIfChanged(ref _site_name,value);
    }
    public string user_name{
        get => parent.first_name+" "+parent.surname;
    }
    public string password{
        get => parent.password;  
        set => parent.password = value;
    }
    public void Button_Action_View_Appointments(){
        //Add funktionality here
        Console.WriteLine("Button_Action_View_Appointments");
    }
    public void Button_Action_Make_Appointments(){
        Console.WriteLine("Button_Action_Make_Appointments");
        parent.current_content = Make_Appointments_Content;
        //Add funktionality here
    }
    public void Button_Action_Logout(){
        Console.WriteLine("Button_Action_Logout");
        parent.current_header = parent._login_header;
        parent.current_content = parent._blank_content;
        //I delete the header so no patient can get info about the doctor if they search in ram ot something
        parent._main_header_patient = null;
        parent.user_name = "";
        parent.first_name = "";
        parent.surname = "";
        parent.user_id = 0;
        //Add funktionality here
    }

}