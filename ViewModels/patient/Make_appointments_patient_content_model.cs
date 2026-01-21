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
    private ObservableCollection<int> _day = new() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25};
    private ObservableCollection<bool> _day_visable = new() {true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true};
    private ObservableCollection<string> _weekdays = new() {"Monday","Tuesday","Wednsday","Thurday","Friday"};
    private int _month;
    private int _year;
    private DateTime currentDateTime = DateTime.Now;
    private string _button_back_text = "back";
    private string _button_for_text = "for";
    private bool _button_back_visable = true;
    private bool _button_for_visable = true;

    public ObservableCollection<int> day {
        get => _day;
        set=> this.RaiseAndSetIfChanged(ref _day,value);
    }
    public ObservableCollection<bool> day_visable {
        get => _day_visable;
        set=> this.RaiseAndSetIfChanged(ref _day_visable,value);
    }
    public ObservableCollection<string> weekdays {
        get => _weekdays;
        set=> this.RaiseAndSetIfChanged(ref _weekdays,value);
    }
    public int month {
        get => _month;
        set => this.RaiseAndSetIfChanged(ref _month,value);
    }
    public int year {
        get => _year;
        set => this.RaiseAndSetIfChanged(ref _year,value);
    }
    public string button_back_text {
        get => _button_back_text;
        set => this.RaiseAndSetIfChanged(ref _button_back_text,value);
    }
    public string button_for_text {
        get => _button_for_text;
        set => this.RaiseAndSetIfChanged(ref _button_for_text,value);
    }
    public bool button_back_visable {
        get => _button_back_visable;
        set => this.RaiseAndSetIfChanged(ref _button_back_visable,value);
    }
    public bool button_for_visable {
        get => _button_for_visable;
        set => this.RaiseAndSetIfChanged(ref _button_for_visable,value);
    }

    public Make_appointments_patient_content_model()
    {
        year = currentDateTime.Year;
        month = currentDateTime.Month;
        DateTime beginning_of_month = new DateTime(year,month,1);
        int days_in_month = DateTime.DaysInMonth(year,month);
        //Su 0 Mo 1 ...
        int weekday_at_beginning = (int)beginning_of_month.DayOfWeek;
        //Mo 0 Tu 1 ...
        weekday_at_beginning = (weekday_at_beginning+6)%7;
        int button_pos = 0;

        //do calculate wich day stand where at which date and which buttons are visable. Continute Here!!!!!
        if (weekday_at_beginning >= 4)
        {
            for(; button_pos < weekday_at_beginning; button_pos++)
            {
                day_visable[button_pos] = false;
            }
            for(int i = 1; i <= days_in_month || i<= )
            {
                
            }
        }
    }
    public void button_action_back(){
        Console.WriteLine("back");
    }
    public void button_action_forward(){
        Console.WriteLine("for");
    }
    public void button_action_day(int pos)
    {
        Console.WriteLine(pos);
    }

}