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
using System.Collections.Generic;
using Avalonia.Dialogs.Internal;

public class specilasation_name_pair{
    public string display_name { get; set; }
    public Specialization value { get; set; }
}
public class id_name_pair{
    public string display_name { get; set; }
    public string value { get; set; }
}

public class Make_appointments_patient_content_model : View_model_base{

    public Window describe_appointment;
    private MainWindow_view_model parent;
    private ObservableCollection<specilasation_name_pair> _specilazation= new();
    private specilasation_name_pair _chosen_specilazation;
    private ObservableCollection<id_name_pair> _doctors = new();
    private id_name_pair _chosen_doctor;
    private ObservableCollection<int> _day = new() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25};
    private ObservableCollection<bool> _day_visible;
    private ObservableCollection<string> _weekdays = new() {"Monday","Tuesday","Wednsday","Thurday","Friday"};
    private int? _month = 1;
    private int? _year = 1;
    private DateTime currentDateTime = DateTime.Now;
    private string _button_back_text = "back";
    private string _button_for_text = "for";
    private bool _button_back_visible = false;
    private bool _button_for_visible = true;
    public ObservableCollection<specilasation_name_pair> specilazation{
        get => _specilazation;
        set=> this.RaiseAndSetIfChanged(ref _specilazation,value);
    }
    public ObservableCollection<id_name_pair> doctors{
        get => _doctors;
        set=> this.RaiseAndSetIfChanged(ref _doctors,value);
    }
    public specilasation_name_pair chosen_specilazation{
        get => _chosen_specilazation;
        set{
           change_doctors(value.value);
           this.RaiseAndSetIfChanged(ref _chosen_specilazation,value); 
        } 
    }
    public id_name_pair chosen_doctor{
        get => _chosen_doctor;
        set{
            //this is very stupid but it works because i calculate all the weekdays to enable the buttons that should be enabeled but the alternative is creating a whole new array and change a funktion so i don't do it
            calculate_weekdays();
            //I get the appointments of the current months and disable the days that have no appointments left
            calculate_appointment_days(value.value);
            this.RaiseAndSetIfChanged(ref _chosen_doctor,value); 
        } 
    }
    
    public ObservableCollection<int> day {
        get => _day;
        set => this.RaiseAndSetIfChanged(ref _day,value);
    }
    public ObservableCollection<bool> day_visible {
        get => _day_visible;
        set => this.RaiseAndSetIfChanged(ref _day_visible,value);
    }
    public ObservableCollection<string> weekdays {
        get => _weekdays;
        set => this.RaiseAndSetIfChanged(ref _weekdays,value);
    }
    public int current_month{
        get => currentDateTime.Month;
    }
    public int current_year{
        get => currentDateTime.Year;
    }
    public int? month {
        get => _month;
        set{
            if(value != null){
                this.RaiseAndSetIfChanged(ref _month,value);
            }
            else{
                month = currentDateTime.Month;
            }
        } 
    }
    public int? year {
        get => _year;
        set{     
            if(value != null){
                this.RaiseAndSetIfChanged(ref _year,value);
            }
            else{
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
    public bool button_back_visible {
        get => _button_back_visible;
        set => this.RaiseAndSetIfChanged(ref _button_back_visible,value);
    }
    public bool button_for_visible {
        get => _button_for_visible;
        set => this.RaiseAndSetIfChanged(ref _button_for_visible,value);
    }
    public Make_appointments_patient_content_model(MainWindow_view_model parent){
        this.parent = parent;
        year = currentDateTime.Year;
        month = currentDateTime.Month;
        day_visible = new() {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false};
        _specilazation.Add(new specilasation_name_pair{display_name ="Allgemeinmedizin", value= Specialization.GENERAL_PRACTICE});
        _specilazation.Add(new specilasation_name_pair{display_name ="Kinderheilkunde", value= Specialization.PEDIATRICS});
        _specilazation.Add(new specilasation_name_pair{display_name ="Augenheilkunde", value= Specialization.OPHTALMOLOGY});
        _specilazation.Add(new specilasation_name_pair{display_name ="Dermatologie", value= Specialization.DERMATOLOGY});
        _specilazation.Add(new specilasation_name_pair{display_name ="Kardiologie", value= Specialization.CARDIOLOGY});
        _specilazation.Add(new specilasation_name_pair{display_name ="Hals-Nasen-Ohren-Heilkunde", value= Specialization.OTOLARYNGOLOGY});
        _chosen_specilazation = _specilazation[0];
        change_doctors(Specialization.GENERAL_PRACTICE);
        chosen_doctor = doctors[0];
        calculate_weekdays();
        //calculate_appointment_days(chosen_doctor.value);
        
    }
    private void change_doctors(Specialization chosen_special){
        List<string[]> doctor = Database_access.get_doctor(chosen_special);
        doctors = new();
        //Now add the doctors to doctors
        foreach(string[] info in doctor){
            doctors.Add(new id_name_pair{display_name =info[1]+" "+info[2], value=info[0]});
        }
        
        //Temp stuff get the real doctors then use calculate weekdays and make the ones that are not available not visible at first and every tima a doctor changes
        if(doctors.Count >= 0){
            chosen_doctor = doctors[0];
        }
        //Next step is to create a view for the day to choose a time in dropdown and make a description and send it to the database
    }
    public void calculate_appointment_days(string doctor_ID){
        List<string[]> appointments_day = Database_access.get_appointments_per_day(doctor_ID,new DateTime((int)year,(int)month,1));
        //Now check if works
        foreach(string[] day_info in appointments_day)
        {
            //Parsing info
            int day;
            try{
                day = int.Parse(day_info[1].Split('.')[0]);
            }
            catch (System.Exception){
                Console.WriteLine("Error parsing day: " + day_info[1].Split('.')[0]);
                continue;
            }
            int amount_appointments;
            try{
                amount_appointments = int.Parse(day_info[0]);
                Console.WriteLine("Anzahl appointments: " + amount_appointments);
            }
            catch (System.Exception){
                Console.WriteLine("Error parsing amount: " + day_info[0]);
                continue;
            }
            
            //Then you check if the amount of appintments is greater or equals to 16 to determane if the button should be visible or not
            Console.WriteLine("day: " + day + "amount_appointments:" + amount_appointments);
            int occupied = 0;
            int possible_available;
            if(new DateTime((int)year, (int)month, day).Day == DateTime.Now.Day){
                //Replace the value with the one from the Database connection
                possible_available = 16-((DateTime.Now.Hour - 8) * 2 +  DateTime.Now.Minute / 30);
                occupied = Database_access.get_todays_appointments_from_now(int.Parse(doctor_ID));
                Console.WriteLine("available: " + possible_available + " occupied: " + occupied + "doctor_ID " + doctor_ID);
                if (possible_available > occupied){
                    Console.WriteLine("Day "+ day+" is enabled");
                    continue;
                }else{
                    //determane the pos of the bool if the button is visible
                    int pos_day = this.day.IndexOf(day);
                    //Failsafe if something goes wrong
                    if(pos_day == -1){
                        Console.WriteLine("Day "+day+"Skipped. Somewhere is a mistake (It could also be that an appointment somehow got on the Weekend!");
                        continue;
                    }//then set so false
                    _day_visible[pos_day] = false;
                    Console.WriteLine("Day "+ day+" is disabled");
                }
            }else{
                if(amount_appointments < 16){
                    Console.WriteLine("Day "+ day+" is enabled");
                    continue;
                }
                else{
                    //determane the pos of the bool if the button is visible
                    int pos_day = this.day.IndexOf(day);
                    //Failsafe if something goes wrong
                    if(pos_day == -1){
                        Console.WriteLine("Day "+day+"Skipped. Somewhere is a mistake (It could also be that an appointment somehow got on the Weekend!");
                        continue;
                    }//then set so false
                    _day_visible[pos_day] = false;
                    Console.WriteLine("Day "+ day+" is disabled");
                }
            }
            
            
        }
    }
    public void calculate_weekdays(){
        for(int pos = 0; pos < day_visible.Count; pos++){
            day_visible[pos] = false;
        }
        if(this.month == null){
            this.month = currentDateTime.Month;
        }
        int month = (int)this.month;
        if(this.year == null){
            this.year = currentDateTime.Year;
        }
        int year = (int)this.year;
        if(year <= currentDateTime.Year && month < currentDateTime.Month){
            Console.WriteLine("Hi");
            return;
        }
        DateTime beginning_of_month = new DateTime(year,month,1);
        int days_in_month = DateTime.DaysInMonth(year,month);
        //Su 0 Mo 1 ...
        int weekday_at_beginning = (int)beginning_of_month.DayOfWeek;
        //Mo 0 Tu 1 ...
        weekday_at_beginning = (weekday_at_beginning+6)%7;
        int button_pos = 0;

        //do calculate wich day stand where at which date and which buttons are visible
        int i = 1;
        int weekday = weekday_at_beginning;
        if (weekday_at_beginning <= 4){
            button_pos+=weekday_at_beginning;
        }
        else{
            i += weekday_at_beginning-5;
            weekday = 0;
        }
        Console.WriteLine(days_in_month);
        //button_pos++;
        while(i<=days_in_month){
            if(weekday <= 4){
                day[button_pos] = i;
                if (i < currentDateTime.Day && month <= currentDateTime.Month && year <= currentDateTime.Year){
                    day_visible[button_pos] = false;
                }
                else{
                    day_visible[button_pos] = true;
                }
                button_pos++;
                weekday++;
                i++;
            }
            else{ 
                weekday = 0;
                 i+=2;
            }
        }
        //If there are no appointments in the current month you imedeatly skip into the next
        if (!_day_visible.Contains(true)){
            button_action_forward();
        } 
    }
    public void button_action_back(){
        if(month <= 1){
            month = 12;
            if(year > 1){
                year = year-1;
            }
        }
        else{
            month = month-1;
        }
        if(month <= currentDateTime.Month&&year<=currentDateTime.Year){
            button_back_visible = false;
        }
        calculate_weekdays();
        calculate_appointment_days(chosen_doctor.value);
        //calculate_weekdays();
        Console.WriteLine("back");
    }
    public void button_action_forward(){
        if(month >= 12){
            month = 1;
            if (year < 9999){
                year = year+1;   
            }
        }
        else{
            month = month+1;
        }
        button_back_visible = true;
        calculate_weekdays();
        calculate_appointment_days(chosen_doctor.value);
        //calculate_weekdays();
        Console.WriteLine("for");
    }
    public void button_action_day(int pos){
        describe_appointment = new appointment_management_system.Views.Make_day_appointment_patient_content_model(){
            DataContext = new Make_day_appointment_patient_content_model(this,day[pos],parent.user_id),
        };
        Console.WriteLine(pos);
        describe_appointment.ShowDialog(parent.desktop.MainWindow);     
    }
}