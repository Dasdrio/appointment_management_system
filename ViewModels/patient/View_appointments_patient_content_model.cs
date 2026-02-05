namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;
using System.Collections.ObjectModel;

public class appointments
{
    public DateTime appointment_time;
    public string doctor_name = "";
    public string appointment_id = "";
    public string description = "";

}
public class View_appointments_patient_content_model : View_model_base{
    //continue by making a view and showing all appointments. Then you can read them and if you want to you can delete them
    private bool _button_back_visable = true;
    private bool _button_for_visable = true;
    ObservableCollection<appointments> _my_Appointments;

    public bool button_back_visable
    {
        get => _button_back_visable;
        set => this.RaiseAndSetIfChanged(ref _button_back_visable,value);
    }
    public bool button_for_visable
    {
        get => _button_for_visable;
        set => this.RaiseAndSetIfChanged(ref _button_for_visable,value);
    }
    public ObservableCollection<appointments> my_Appointments
    {
        get => _my_Appointments;
        set => this.RaiseAndSetIfChanged(ref _my_Appointments,value);
    }
    
    View_appointments_patient_content_model()
    {
        //Insert command that  loads all Appointments of this person
        my_Appointments.Add(new appointments{doctor_name="test",appointment_id="-1",description="Hi",appointment_time=DateTime.Now});
    }
    public void button_action_back()
    {
        
    }
    public void button_action_forward()
    {
        
    }
}