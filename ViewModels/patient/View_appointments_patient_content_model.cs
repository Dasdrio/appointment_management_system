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
    ObservableCollection<appointments> my_Appointments;
    View_appointments_patient_content_model()
    {
        //Insert command that  loads all Appointments of this person
    }
}