namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;
using System.Collections.Generic;

public class Login_content_model : View_model_base
{
    //I pass the MainWindow_view_model in the constructor so that I still have the user name and password in the main menu later on.
    private MainWindow_view_model parent;
    private string _message = "";
    public Login_content_model(MainWindow_view_model parent){
        this.parent = parent;
    }
    public string message
    {
        get =>_message;
        set => this.RaiseAndSetIfChanged(ref _message,value);
    }
    //I use user name here temporarily as the e-mail for login. After that it gets uses as the real user_name
    public string user_name{
        get => parent.user_name;
        set => parent.user_name = value;
    }
    public string password{
        get => parent.password; 
        set => parent.password = value;
    }
    public void button_action_login(){
        //login check
        parent.user_name = parent.user_name.Trim();
        if (!is_email(parent.user_name))
        {
            message = "Das ist kein gültiges e-mail format";
            return;
        }
        string? hash = Database_access.get_password_hash(parent.user_name);
        if(hash == null)
        {
            message = "Die E-Mail existiert nicht";
            return;
        }
        hash = hash.ToLower();
        if (!SHA256_hash_creator(parent.password).ToLower().Equals(hash))
        {
            Console.WriteLine("My hash: "+SHA256_hash_creator(parent.password)+" || The hash i got: "+hash);
            message = "Das Passwort ist falsch";
            return;
        }
        List<string[]> temp_user = Database_access.get_personal_information(parent.user_name);
        string[] user = temp_user[0];
        //Console.WriteLine("person_ID: "+user[0]+" Specialization:"+user[1]+" name:"+user[2]+" surname:"+user[3]+" e-mail:"+user[4]);
        //saving info
        try
        {
            parent.user_id = uint.Parse(user[0]);
        }
        catch (System.Exception)
        {
            
            message ="Fehler beim Login (unable to parse id)";
            return;
        }
        parent.first_name = user[2];
        parent.surname = user[3];
        parent.current_content = parent._blank_content;
        if (user[1].Trim().Equals("PATIENT"))
        {
             //i create thsi header only here to enshure that others dont have access to this info even if only in background (hopefully)
            parent._main_header_patient = new Main_header_model_patient(parent);
            parent.current_header = parent._main_header_patient;
            password = "";
            Console.WriteLine("Logged in as patient with id: "+user[0]);
        }
        else
        {
            //i create thsi header only here to enshure that patients dont have access to this info even if only in background (hopefully)
            parent._main_header_doctor = new Main_header_model_doctor(parent);
            parent.current_header = parent._main_header_doctor;
            password = "";
            Console.WriteLine("Logged in as doctor with id: "+user[0]);
        }
    }
}