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

public class time_name_pair
{
    public string display_time { get; set; }="";
    public TimeOnly value { get; set; }
}
public class Make_day_appointment_patient_content_model : View_model_base{
    private int day;
    private uint user_id;
    private string _description;
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
    public string doctor_name
    {
        get=> parent.chosen_doctor.display_name;
        set=> parent.chosen_doctor.display_name = value;
    }
    public string description
    {
        get=>_description;
        set=>this.RaiseAndSetIfChanged(ref _description,value);
    }
    public ObservableCollection<time_name_pair> times_appointmens
    {
        get=>_times_appointmens;
        set=>this.RaiseAndSetIfChanged(ref _times_appointmens,value);
    }

    public Make_day_appointment_patient_content_model(Make_appointments_patient_content_model parent,int day,uint user_id)
    {
        List<string> times_at_day = Database_access.get_appointments_on_day(parent.chosen_doctor.value,new DateTime((int)parent.year,(int)parent.month,day));
        TimeOnly[] times = new TimeOnly[times_at_day.Count];
        int i = 0;
        foreach(string hallo in times_at_day)
        {
            
            string[] time_as_string = hallo.Split(' ')[1].Split(':');
            int hour = 0;
            int minute = 0;
            try
            {
                hour = Int32.Parse(time_as_string[0]);
                minute = Int32.Parse(time_as_string[1]);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Error parsing hour and minute");
            }
            times[i] = new TimeOnly(hour,minute);
            i++;
            Console.WriteLine(hallo);   
        }
        for(i = 0; i <= 15; i++)
        {
            TimeOnly time_to_add = new TimeOnly(8+i/2,i%2*30);
            if (!times.Contains(time_to_add))
            {
                _times_appointmens.Add(new time_name_pair{display_time=time_to_add.Hour+":"+time_to_add.Minute,value=time_to_add});
            }
        }
        this.parent =parent;
        this.day = day;
        this.user_id = user_id;
    }
    public void make_appointment()
    {
        //now add the logic to add an appointment and to recalculate the weekdays
        if(parent.year == null || parent.month == null)
        {
            Console.WriteLine("No year or no month or both");
            parent.describe_appointment.Close();
        }
        if(chosen_time == null)
        {
            chosen_time = times_appointmens[0];
        }
        DateTime appointment_time = new DateTime((int)parent.year,(int)parent.month,day,chosen_time.value.Hour,chosen_time.value.Minute,0);
        //add button for text and so
        Database_access.insert_appointment(appointment_time,""+user_id,parent.chosen_doctor.value,description);
        Console.WriteLine("appointment time: "+appointment_time+" userid: "+user_id+" doctor: "+parent.chosen_doctor.value+"");
        Console.WriteLine(chosen_time.value+" <-time name:"+parent.chosen_doctor.display_name+" month: "+parent.month+" day:"+day);
        //This is to check if a day has no other appointments left
        parent.calculate_weekdays();
        parent.calculate_appointment_days(parent.chosen_doctor.value);
        parent.describe_appointment.Close();
    }
}