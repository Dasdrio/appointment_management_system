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
    private Window window;
    View_appointment_doctor_content_model parent_direct;
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
    public View_appointment_doctor_window_model(MainWindow_view_model parent, string id,Window window,View_appointment_doctor_content_model parent_direct){
        this.parent = parent;
        this.parent_direct = parent_direct;
        this._id = id;
        Console.WriteLine(id);
        this.window = window;
        //Returns a List with Strings with followling values (date_and_time, name, surname, description)
        List<string> appointment_info = Database_access.get_appointment_by_id(int.Parse(id));
        _patient_name = appointment_info[1]+" "+appointment_info[2];
        _description = appointment_info[3];
        string date_time_str = appointment_info[0];
        date_time_str.Trim();
        string[] date_time_arr = date_time_str.Split(' ');
        string[] date_arr = date_time_arr[0].Split('.');
        string[] time_arr = date_time_arr[1].Split(':');
        try{
            date = new DateOnly(int.Parse(date_arr[2]),int.Parse(date_arr[1]),int.Parse(date_arr[0]));
            time = new TimeOnly(int.Parse(time_arr[0]), int.Parse(time_arr[1]), int.Parse(time_arr[2]));
        }catch(Exception e){
            Console.WriteLine(e);
            Console.WriteLine("Error Parsing date or then time");
        }
    }
    public void cancel_appointment() {
        Database_access.delete_appointment(int.Parse(id));
        parent_direct.set_appointments_from_database();
        this.window.Close();
    }
}