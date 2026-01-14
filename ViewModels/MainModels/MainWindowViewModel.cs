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
    private ViewModelBase login_content;
    private ViewModelBase impressum_content;
    private ViewModelBase AGB_content;
    private ViewModelBase data_security_information_content;
    private ViewModelBase contact_content;
    //the current content of the window. Can be swaped out with other ViewModelBases (use the property to access the field)
    private ViewModelBase _current_content;
    private ViewModelBase _current_header;
    public ViewModelBase _login_header;
    public ViewModelBase _main_header;
    public ViewModelBase _blank_content;
    private IClassicDesktopStyleApplicationLifetime desktop;
    private string _user_name = "";
    private string _password = "";
    private string _site_name = "Our Appointment Management System";
    //Überprüfen ob das funktioniert. Der Top Content sollte mit dem LoginTop Content ausgetauscht werden und darüber dann auch den Content dieser Wiederum kann auch Impressum durch die MainWindowView Anzeigen. Mal sehen
    public MainWindowViewModel(IClassicDesktopStyleApplicationLifetime desktop){
        impressum_content = new ImpressumContentModel();
        AGB_content = new AGB_content_model();
        contact_content = new contact_content_model();
        data_security_information_content = new data_security_information_content_model();
        login_content = new LoginContentModel(this);
        _blank_content = new blankContentModel();
        _main_header = new MainHeaderModel(this);
        _login_header = new LoginHeaderModel(this);
        _current_header = _login_header;
        _current_content = impressum_content;
        this.desktop = desktop;
    }
    public ViewModelBase current_content{
        get => _current_content;
        set => this.RaiseAndSetIfChanged(ref _current_content,value);
    }
    public ViewModelBase current_header{
        get => _current_header;
        set => this.RaiseAndSetIfChanged(ref _current_header,value);
    }
    
    public string site_name{
        get => _site_name;
        set => this.RaiseAndSetIfChanged(ref _site_name,value);
    }
    public string user_name{
        get => _user_name;  
        set{
            _user_name = value;
            this.RaiseAndSetIfChanged(ref _user_name,value);
        }
    }
    public string password{
        get => _password;  
        set => this.RaiseAndSetIfChanged(ref _password,value);
    }
    //Button functions    
    
    //All Pages (Only change the Content Grid to the apropriate content)
    public void button_action_impressum(){
        current_content = impressum_content;
        Console.WriteLine("button_action_impressum");
        //add functionality here
    }
    public void button_action_AGB(){
        current_content = AGB_content;
        Console.WriteLine("button_action_AGB");
        //add functionality here
    }
    public void button_action_contact(){
        current_content = contact_content;
        Console.WriteLine("button_action_contact");
        //add functionality here
    }
    public void button_action_data_security_information(){
        current_content = data_security_information_content;
        Console.WriteLine("button_action_data_security_information");
        //add functionality here
    }
    /*Template
    public void Button_Action_(){
        Console.WriteLine("Button_Action_");
        //add functionality here
    }*/
    //functionality functions
}