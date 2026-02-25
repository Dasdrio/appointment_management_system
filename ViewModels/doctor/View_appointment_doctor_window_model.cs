namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using DynamicData;
public class View_appointment_doctor_window_model : View_model_base{
    private MainWindow_view_model parent;
    private DateOnly _date;
    private TimeOnly _time;
    private string _id;
    private string _patient_name;
    private string _description;
    public DateOnly date{
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }
    public TimeOnly time{
        get => _time;
        set => this.RaiseAndSetIfChanged(ref _time, value);
    }
    public string id{
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }
    public string patient_name{
        get => _patient_name;
        set => this.RaiseAndSetIfChanged(ref _patient_name, value);
    }
    public string description{
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }
    public View_appointment_doctor_window_model(MainWindow_view_model parent, string id){
        this.parent = parent;
        this.id = id;
        Console.WriteLine(id);
    }
    public void cancel_appointment() {
        Database_access.delete_appointment(int.Parse(id));
    }
}