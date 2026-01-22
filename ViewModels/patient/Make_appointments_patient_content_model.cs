namespace appointment_management_system.ViewModels;

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ReactiveUI;
using Avalonia.Controls;
using appointment_management_system.Views;
using System.Collections.ObjectModel;
using Avalonia.Markup.Xaml.Templates;

public class Make_appointments_patient_content_model : View_model_base{

    public enum specilazations
    {
        Hausmeister,
        Teerputzer,
        Lungenentferner
    }
    private ObservableCollection<string> _specilazation= new();
    private string _chosen_specilazation;
    private ObservableCollection<int> _day = new() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25};
    private ObservableCollection<bool> _day_visable;
    private ObservableCollection<string> _weekdays = new() {"Monday","Tuesday","Wednsday","Thurday","Friday"};
    private int? _month = 1;
    private int? _year = 1;
    private DateTime currentDateTime = DateTime.Now;
    private string _button_back_text = "back";
    private string _button_for_text = "for";
    private bool _button_back_visable = false;
    private bool _button_for_visable = true;
    public ObservableCollection<string> specilazation
    {
        get => _specilazation;
        set=> this.RaiseAndSetIfChanged(ref _specilazation,value);
    }
    public string chosen_specilazation{
        get => _chosen_specilazation;
        set
        {
           Console.WriteLine(value);
           this.RaiseAndSetIfChanged(ref _chosen_specilazation,value); 
        } 
    }
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
    public int current_month
    {
        get => currentDateTime.Month;
    }
    public int current_year
    {
        get => currentDateTime.Year;
    }
    public int? month {
        get => _month;
        set
        {
            if(value != null)
            {
                this.RaiseAndSetIfChanged(ref _month,value);
            }
            else
            {
                month = currentDateTime.Month;
            }
        } 
    }
    public int? year {
        get => _year;
        set
        {
            
            if(value != null)
            {
                this.RaiseAndSetIfChanged(ref _year,value);
            }
            else
            {
                year = currentDateTime.Year;
            }
        }
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
        foreach(var specilazation_string in Enum.GetValues(typeof(specilazations)))
        {
            _specilazation.Add(specilazation_string.ToString());
        }
        _chosen_specilazation = _specilazation[0];
        calculate_weekdays();
    }
    private void calculate_weekdays()
    {
        
        day_visable = new() {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false};
        if(this.month == null)
        {
            this.month = currentDateTime.Month;
        }
        int month = (int)this.month;
        if(this.year == null)
        {
            this.year = currentDateTime.Year;
        }
        int year = (int)this.year;
        if(year <= currentDateTime.Year && month < currentDateTime.Month)
        {
            return;
        }
        DateTime beginning_of_month = new DateTime(year,month,1);
        int days_in_month = DateTime.DaysInMonth(year,month);
        //Su 0 Mo 1 ...
        int weekday_at_beginning = (int)beginning_of_month.DayOfWeek;
        //Mo 0 Tu 1 ...
        weekday_at_beginning = (weekday_at_beginning+6)%7;
        int button_pos = 0;

        //do calculate wich day stand where at which date and which buttons are visable
        int i = 1;
        int weekday = weekday_at_beginning;
        if (weekday_at_beginning <= 4)
        {
            button_pos+=weekday_at_beginning;
        }
        else
        {
            i += weekday_at_beginning-5;
            weekday = 0;
        }
        
        //button_pos++;
        while(i<=days_in_month){
            if(weekday <= 4){
                if (i < currentDateTime.Day && month <=currentDateTime.Month && year <=currentDateTime.Year)
                {
                    i++;
                    continue;
                }
                day[button_pos] = i;
                day_visable[button_pos] = true;
                button_pos++;
                weekday++;
                i++;
            }
            else{ 
                weekday = 0;
                 i+=2;
            }
        }
        
    }
    public void button_action_back(){
        if(month <= 1)
        {
            month = 12;
            if(year > 1)
            {
                year = year-1;
            }
        }
        else
        {
            month = month-1;
        }
        if(month <= currentDateTime.Month&&year<=currentDateTime.Year)
        {
            button_back_visable = false;
        }
        calculate_weekdays();
        //calculate_weekdays();
        Console.WriteLine("back");
    }
    public void button_action_forward(){
        if(month >= 12)
        {
            month = 1;
            if (year < 9999)
            {
                year = year+1;   
            }
        }
        else
        {
            month = month+1;
        }
        button_back_visable = true;
        calculate_weekdays();
        //calculate_weekdays();
        Console.WriteLine("for");
    }
    public void button_action_day(int pos)
    {
        Console.WriteLine(pos);
    }

}