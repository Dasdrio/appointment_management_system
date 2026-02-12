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
using System.Globalization;
using System.Linq;

public class appointment_layout: IComparable
{
    public int CompareTo(object o)
    {
        appointment_layout comparing_partner = (appointment_layout)o;
        if (comparing_partner.appointment_time > this.appointment_time)
        {
            return -1;
        }else if(comparing_partner.appointment_time < this.appointment_time)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public DateTime appointment_time {get;set;}
    public string doctor_name {get;set;}="";
    public string appointment_id {get;set;}="";
    public string description {get;set;}="";


}
public class View_appointments_patient_content_model : View_model_base{
    //continue by making a view and showing all appointments. Then you can read them and if you want to you can delete them
    private bool _button_back_visable = true;
    private bool _button_for_visable = true;
    int position = 0;
    private MainWindow_view_model parent;
    private ObservableCollection<appointment_layout> _my_Appointments = new ObservableCollection<appointment_layout>();
    private List<appointment_layout> _all_my_Appointments = new List<appointment_layout>();
    private ObservableCollection<bool> _visable = new ObservableCollection<bool>{true,true,true};

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
    public ObservableCollection<appointment_layout> my_Appointments
    {
        get => _my_Appointments;
        set => this.RaiseAndSetIfChanged(ref _my_Appointments,value);
    }
    public ObservableCollection<bool> visable
    {
        get => _visable;
        set => this.RaiseAndSetIfChanged(ref _visable,value);
    }
    
    public View_appointments_patient_content_model(MainWindow_view_model parent)
    {
        this.parent = parent;
        //Now add funktion for back and for and make a more beautiful layout and make the buttons work
    }
    public void calculate_appointments()
    {
        my_Appointments = new ObservableCollection<appointment_layout>();
        _all_my_Appointments = new List<appointment_layout>();
        List<string[]> unconverted_appointments = Database_access.get_appointments_as_patient((int)parent.user_id,DateTime.Now);
        foreach(string[] u_appoint in unconverted_appointments)
        {
            DateTime dateTime_from_database = DateTime.Now;
            try
            {
                string[] date_and_time_temp = u_appoint[1].Split(' ');
                Console.WriteLine(u_appoint[0]);
                string[] time_temp = date_and_time_temp[1].Split(':');
                string[] date_temp = date_and_time_temp[0].Split('.');
                foreach(string hi in time_temp)
                {
                    Console.Write("Time: "+hi+"|");
                }
                Console.WriteLine();
                foreach(string hi in date_temp)
                {
                    Console.Write("Date: "+hi+"|");
                }
                Console.WriteLine();
                dateTime_from_database = new DateTime(Int32.Parse(date_temp[2]),Int32.Parse(date_temp[1]),Int32.Parse(date_temp[0]),Int32.Parse(time_temp[0]),Int32.Parse(time_temp[1]),Int32.Parse(time_temp[2]));
            }
            catch (System.Exception)
            {
                dateTime_from_database = DateTime.Now;
                Console.WriteLine("Eror at parse int date or time");
                continue;
            }
            Console.WriteLine(u_appoint);
            _all_my_Appointments.Add(new appointment_layout{doctor_name=u_appoint[2]+" "+u_appoint[3],appointment_id=u_appoint[0],description=u_appoint[4],appointment_time=dateTime_from_database});
            _all_my_Appointments.Sort();
        }
        int amount_appointments;
        if(_all_my_Appointments.Count < 3)
        {
            amount_appointments = _all_my_Appointments.Count;
        }
        else
        {
            amount_appointments = 3;
        }
        for(int i = 0; i < amount_appointments; i++)
        {
            my_Appointments.Add(_all_my_Appointments[i]);
        }
    }
    public void button_action_back()
    {
        //Next time add button back and forward (you added positon for that to determane the starting pos of _all_my_Appointments)
        //Add check logic and stuff and then replace the stuff in my_appointments
    }
    public void button_action_forward()
    {
        
    }
    public void button_action_delete(string appointment_id)
    {
        Console.WriteLine("delete: "+appointment_id);
    }
    public void button_action_update(string appointment_id)
    {
        Console.WriteLine("Update: "+appointment_id);
    }
}