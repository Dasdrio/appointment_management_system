namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;
using System.Collections.ObjectModel;
public class time_name_pair
{
    public string display_time { get; set; }="";
    public TimeOnly value { get; set; }
}
public class Make_day_appointment_patient_content_model : View_model_base{
    private int day;
    private time_name_pair _chosen_time;
    private Make_appointments_patient_content_model parent;
    //Continue here adding the doctor and stuff
    private ObservableCollection<time_name_pair> _times_appointmens = new ObservableCollection<time_name_pair>();
    public time_name_pair chosen_time
    {
        get =>_chosen_time;
        set {
            Console.WriteLine(value);
            this.RaiseAndSetIfChanged(ref _chosen_time,value);
        }
    }
    public id_name_pair doctor_name
    {
        get => parent.chosen_doctor;
        set => parent.chosen_doctor=value;
    }
    public ObservableCollection<time_name_pair> times_appointmens
    {
        get=>_times_appointmens;
        set=>this.RaiseAndSetIfChanged(ref _times_appointmens,value);
    }

    public Make_day_appointment_patient_content_model(Make_appointments_patient_content_model parent,int day)
    {
        for(int i = 0; i <= 16; i++)
        {
            TimeOnly time_to_add = new TimeOnly(8+i/2,i%2*30);
            _times_appointmens.Add(new time_name_pair{display_time=time_to_add.Hour+":"+time_to_add.Minute,value=time_to_add});
        }
        _chosen_time = _times_appointmens[0];
        this.parent =parent;
        this.day = day;
    }
    public void make_appointment()
    {
        //now add the logic to add an appointment and to recalculate the weekdays
        Console.WriteLine(chosen_time.value+" <-time name:"+doctor_name.display_name+" month: "+parent.month+" day:"+day);
        parent.describe_appointment.Close();
    }
}