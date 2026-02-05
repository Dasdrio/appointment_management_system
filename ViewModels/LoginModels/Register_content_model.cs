namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;
using Avalonia.Markup.Xaml.Styling;

public class Register_content_model : View_model_base
{
    //Ich übergebe im Constructer den MainWindow_view_model, um nachher im Main Menu immer noch User_Name und Passwort zu haben
    private MainWindow_view_model parent;
    public Register_content_model(MainWindow_view_model parent){
        this.parent = parent;
    }
    private string _user_name = "";
    private string _password = "";
    private string _repeat_password = "";
    private string _first_name = "";
    private string _surname = "";
    private string _message = "";
    public string user_name{
        get => _user_name;
        set => this.RaiseAndSetIfChanged(ref _user_name,value);
    }
    public string password{
        get => _password;  
        set => this.RaiseAndSetIfChanged(ref _password,value);
    }
    public string repeat_password{
        get => _repeat_password;
        set => this.RaiseAndSetIfChanged(ref _repeat_password,value);
    }
    public string first_name{
        get => _first_name;  
        set => this.RaiseAndSetIfChanged(ref _first_name,value);
    }
    public string surname{
        get => _surname;
        set => this.RaiseAndSetIfChanged(ref _surname,value);
    }
    public string message{
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message,value);
    }

    public void Button_Action_Register(){
        //Add funktionality here
        if(user_name=="")
        {
            message = "Es wurde keine E-mail Eingetragen!";
            return;
        }
        if (!is_email(user_name))
        {
            message = "Das ist kein gültiges E-Mail Format";
            return;
        }
        string? user = Database_access.get_password_hash(user_name);
        if(user != null)
        {
            message = "Diese E-Mail wurde berreits registriert. Bei Problemen wenden sie sich bitte an den Support!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
            return;
        }
        if(password =="")
        {
            message = "Es wurde kein Passwort eingetragen!";
            return;
        }
        if(first_name =="")
        {
            message = "Es wurde kein Vorname eingetragen!";
            return;
        }
        if(surname == "")
        {
            message = "Es wurde kein Nachname eingetragen!";
            return;
        }
        if(password.Equals(repeat_password))
        {
            message = "Die Passwörter stimmen nicht überein!";
        }
        password = SHA256_hash_creator(password);
        //Insert command to insert user here
        Database_access.inser_new_person(first_name,surname,user_name,password);
        message = "Sie wurden erfolgreich registriert. Bitte loggen sie sich im Login fenster ein";
        password = "";
        repeat_password = "";
        first_name = "";
        surname = "";
        user_name = "";
        Console.WriteLine("Button_Action_Register");
    }

}