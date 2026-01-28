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
    public string display_time { get; set; }
    public TimeOnly value { get; set; }
}
public class View_appointments_patient_content_model : View_model_base{
    private TimeOnly _chosen_time;
    private id_name_pair _doctor_name;
    //Continue here adding the doctor and stuff

    private ObservableCollection<time_name_pair> _times_appointmens;
    public TimeOnly chosen_time
    {
        get =>_chosen_time;
        set => this.RaiseAndSetIfChanged(ref _chosen_time,value);
    }
    public ObservableCollection<time_name_pair> times_appointmens
    {
        get=>_times_appointmens;
        set=>this.RaiseAndSetIfChanged(ref _times_appointmens,value);
    }


}