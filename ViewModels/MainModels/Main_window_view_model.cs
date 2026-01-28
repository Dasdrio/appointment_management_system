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
using System.Runtime.Intrinsics.Arm;

public partial class MainWindow_view_model : View_model_base
{
    private View_model_base _login_content;
    private View_model_base _impressum_content;
    private View_model_base _AGB_content;
    private View_model_base _data_security_information_content;
    private View_model_base _contact_content;
    public View_model_base _login_header;
    public View_model_base _main_header_patient;
    public View_model_base _main_header_doctor;
    public View_model_base _blank_content;
    //the current content of the window. Can be swaped out with other View_model_bases (use the property to access the field)
    private View_model_base _current_content;
    private View_model_base _current_header;
    private IClassicDesktopStyleApplicationLifetime desktop;
    private string _user_name = "";
    private string _password = "";
    private string _surname = "";
    private string _first_name = "";
    private uint _user_id = 0; //default value user ids are starting at 1
    private string _site_name = "Our Appointment Management System";
    //Überprüfen ob das funktioniert. Der Top Content sollte mit dem LoginTop Content ausgetauscht werden und darüber dann auch den Content dieser Wiederum kann auch Impressum durch die MainWindowView Anzeigen. Mal sehen
    public MainWindow_view_model(IClassicDesktopStyleApplicationLifetime desktop){
        _impressum_content = new Impressum_content_model();
        _AGB_content = new AGB_content_model();
        _contact_content = new Contact_content_model();
        _data_security_information_content = new Data_security_information_content_model();
        _login_content = new Login_content_model(this);
        _blank_content = new Blank_content_model();
        _login_header = new Login_header_model(this);
        _current_header = _login_header;
        _current_content = _impressum_content;
        this.desktop = desktop;
    }
    public View_model_base current_content{
        get => _current_content;
        set => this.RaiseAndSetIfChanged(ref _current_content,value);
    }
    public View_model_base current_header{
        get => _current_header;
        set => this.RaiseAndSetIfChanged(ref _current_header,value);
    }
    
    public string site_name{
        get => _site_name;
        set => this.RaiseAndSetIfChanged(ref _site_name,value);
    }
    public string user_name{
        get => _user_name;  
        set =>  this.RaiseAndSetIfChanged(ref _user_name,value);
    }
    public string password{
        get => _password;  
        set => this.RaiseAndSetIfChanged(ref _password,value);
    }
        public string surname{
        get => _surname;
        set => this.RaiseAndSetIfChanged(ref _surname,value);
    }
    public string first_name{
        get => _first_name;
        set => this.RaiseAndSetIfChanged(ref _first_name,value);
    }
    public uint user_id{
        get => _user_id;
        set => this.RaiseAndSetIfChanged(ref _user_id,value);
    }
    //Button functions    
    
    //All Pages (Only change the Content Grid to the apropriate content)
    public void button_action_impressum(){
        current_content = _impressum_content;
        Console.WriteLine("button_action_impressum");
        //add functionality here
    }
    public void button_action_AGB(){
        current_content = _AGB_content;
        Console.WriteLine("button_action_AGB");
        //add functionality here
    }
    public void button_action_contact(){
        current_content = _contact_content;
        Console.WriteLine("button_action_contact");
        //add functionality here
    }
    public void button_action_data_security_information(){
        current_content = _data_security_information_content;
        Console.WriteLine("button_action_data_security_information");
        //add functionality here
    }
    /*Template
    public void Button_Action_(){
        Console.WriteLine("Button_Action_");
        //add functionality here
    }*/
    
}