namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;
using System.Collections.ObjectModel;

public class Make_appointments_patient_content_model : View_model_base{
    private ObservableCollection<string> _texte = new() {"hallo","Welt"};
    private ObservableCollection<bool> _visable = new() {false,false};

    public ObservableCollection<string> texte {
        get => _texte;
        set=> this.RaiseAndSetIfChanged(ref _texte,value);
    }
    public ObservableCollection<bool> visable {
        get => _visable;
        set=> this.RaiseAndSetIfChanged(ref _visable,value);
    }
    public void button_action_back(){
        string temp = texte[0];
        texte[0] = texte[1];
        texte[1] = temp;
        Console.WriteLine(texte[0]);
    }

}