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
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Dialogs.Internal;

public class appointment_info : IComparable{
    public string id {get; set;} = "";
    public DateTime date_and_time {get; set;} = DateTime.Now;
    public string patient_name {get; set;} = "";
    public string description {get; set;} = "";
    public bool visible {get;set;} = false;
    public int CompareTo(object o){
        appointment_info comparing_partner = (appointment_info)o;
        if (comparing_partner.date_and_time > this.date_and_time){
            return -1;
        }else if(comparing_partner.date_and_time < this.date_and_time){
            return 1;
        }
        else{
            return 0;
        }
    }
    public appointment_info(){}
    public appointment_info(string id, DateTime date_and_time, string patient_name, string description){
        this.id = id;
        this.date_and_time = date_and_time;
        this.patient_name = patient_name;
        this.description = description;
    }
}
public class View_appointment_doctor_content_model : View_model_base{
    private MainWindow_view_model parent;
    private int _year;
    private byte _month;
    private byte _calendar_week;
    private ObservableCollection<byte> _days;
    private ObservableCollection<bool> _visible;
    private List<appointment_info> appointments;
    private ObservableCollection<appointment_info> _appointments_of_week;
    //cause of ISO 8601. To easily calculate the first week of the year.
    private DateTime _thursday;
    
    public int year{
        get => _year;
        set => this.RaiseAndSetIfChanged(ref _year, value);
    }
    public byte month{
        get => _month;
        set => this.RaiseAndSetIfChanged(ref _month, value);
    }
    public byte calendar_week{
        get => _calendar_week;
        set => this.RaiseAndSetIfChanged(ref _calendar_week, value);
    }
    public ObservableCollection<byte> days{
        get => _days;
        set => this.RaiseAndSetIfChanged(ref _days, value);
    }
    public ObservableCollection<bool> visible{
        get => _visible;
        set => this.RaiseAndSetIfChanged(ref _visible, value);
    }
    public ObservableCollection<appointment_info> appointments_of_week{
        get => _appointments_of_week;
        set => this.RaiseAndSetIfChanged(ref _appointments_of_week, value);
    }
    public DateTime thursday{
        get => _thursday;
        set => this.RaiseAndSetIfChanged(ref _thursday, value);
    }

    public View_appointment_doctor_content_model(MainWindow_view_model parent){
        Console.WriteLine("Doctor Contentent Created");
        thursday = DateTime.Now;
        this.parent = parent;
        initilise_appointments();
    }
    public void initilise_appointments()
    {
        DateTime thursday_temp = thursday;
        thursday = DateTime.Now; 
        thursday = thursday.AddDays((((double)(thursday.DayOfWeek + 6) % 7) * - 1) + 3);
        _days = new ObservableCollection<byte>{0,0,0,0,0};
        _appointments_of_week = new ObservableCollection<appointment_info>();
        _visible = new ObservableCollection<bool>();
        for(int i = 0; i < 80; i++){
            visible.Add(false);
            appointments_of_week.Add(new appointment_info());
        }
        appointments = new List<appointment_info>();

        update_time();

        List<string[]> appointments_str = Database_access.get_appointments_as_doctor((int)parent.user_id, thursday.AddDays(-3).Date);
        foreach(string[] appointment in appointments_str){
            string date_time_str = appointment[1];
            date_time_str.Trim();
            string[] date_time_arr = date_time_str.Split(' ');
            string[] date_arr = date_time_arr[0].Split('.');
            string[] time_arr = date_time_arr[1].Split(':');
            try{ 
                Console.WriteLine(date_arr[0]+ " "+date_arr[1]+" "+date_arr[2]+ " | "+time_arr[0]+" "+time_arr[1]+" "+time_arr[2]);
                appointments.Add(new appointment_info(
                        appointment[0],
                        new DateTime(int.Parse(date_arr[2]),
                            int.Parse(date_arr[1]),
                            int.Parse(date_arr[0]),
                            int.Parse(time_arr[0]),
                            int.Parse(time_arr[1]),
                            int.Parse(time_arr[2])
                        ),
                        appointment[2],
                        appointment[3]
                    )
                );
            }
            catch (Exception e){
                Console.WriteLine(e);
            }
        }
        appointments.Sort();
        thursday = thursday_temp;
        update_time();
        
    }
    public void button_previous_week(){
        if(thursday.AddDays(4) > DateTime.Now.AddDays(6))
        thursday = thursday.AddDays(-7);
        update_time();
    }
    public void button_next_week(){
        thursday = thursday.AddDays(7);
        update_time();
    }

    private void update_time(){
        year = thursday.Year;
        month = (byte)thursday.Month;
        calendar_week = (byte)((thursday.DayOfYear + 6) / 7);
        for(int i = 0; i < 5; i++){
            days[i]=(byte)thursday.AddDays(i-3).Day;
        }
        for(int i = 0; i < 80; i++) visible[i] = false;
        foreach(appointment_info appointment in appointments){
            if (appointment.date_and_time < thursday.AddDays(-3).Date) continue;
            else if (appointment.date_and_time > thursday.AddDays(2).Date) break;
            //the standard is that Sunday is 0 but we want Monday to be 0. If we subtract 1 we would have Sunday -1 but since there are no appointments everything is fine.
            int position = ((int)appointment.date_and_time.DayOfWeek-1)*16 +    //day
                (appointment.date_and_time.Hour - 8) * 2 +                      //hour
                appointment.date_and_time.Minute / 30;                          //minutes
            appointments_of_week[position] = appointment;
            appointments_of_week[position].visible = true;
            visible[position] = true;
        }
    }
    Window describe_appointment = new Views.View_appointment_doctor_window_model();
    public void button_action_show_appointment(string id){
        describe_appointment = new Views.View_appointment_doctor_window_model();
        describe_appointment.DataContext = new View_appointment_doctor_window_model(parent,id,describe_appointment,this);
        describe_appointment.ShowDialog(parent.desktop.MainWindow);
    }
}