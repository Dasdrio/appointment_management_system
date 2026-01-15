namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;

public class Register_content_model : View_model_base
{
    //Ich übergebe im Constructer den MainWindow_view_model, um nachher im Main Menu immer noch User_Name und Passwort zu haben
    private MainWindow_view_model parent;
    public Register_content_model(MainWindow_view_model parent){
        this.parent = parent;
    }
    public string user_name{
        get => parent.user_name;
        set => parent.user_name = value;
    }
    public string password{
        get => parent.password;  
        set => parent.password = value;
    }
    public void Button_Action_Register(){
        //Add funktionality here
        Console.WriteLine("Button_Action_Register");
    }

}