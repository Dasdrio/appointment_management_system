using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DynamicData;
using MySqlConnector;
using System.ComponentModel;
namespace appointment_management_system;


public enum Specialization{
        GENERAL_PRACTICE,
		PEDIATRICS,
		OPHTALMOLOGY,
		DERMATOLOGY,
		CARDIOLOGY,
		OTOLARYNGOLOGY,
    }
public class Database_access
{
    private static Database_access? instance;
    private String mysql_connection_string = "server=localhost;port=3306;uid=access_client;pwd=accesspassword;database=appointment_management";
    private static MySqlConnection mysql_connection;
    private static MySqlCommand? command;

    private Database_access(){
        mysql_connection = new MySqlConnection();
        try{
            mysql_connection.ConnectionString = mysql_connection_string;
            mysql_connection.Open();
        }catch(MySqlException ex){
            Console.WriteLine(ex.ToString());
        }
    }
    public static void close_database(){
        if(instance != null){
            mysql_connection.Close();
            instance = null;
        }
    }

    //required to call for database connection
    public static Database_access get_instance(){ 
        if(instance == null){
            instance = new Database_access();
            return instance;  
        }else{
            return instance;    
        }
    }
    //deletion of the own AC
    public static void delete_person(int person_ID){ 
    
        string procedure = "";
        command = new MySqlCommand(procedure, mysql_connection);
        command.CommandType = CommandType.StoredProcedure;
        
        command.Parameters.AddWithValue("", person_ID);
        command.ExecuteNonQuery();
    }

    public static String? get_password_hash(String email){
        String? password = null;

        try{
            string procedure = "sp_persons_get_password_hash_by_email";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("p_email", email);
            using(MySqlDataReader reader = command.ExecuteReader()){
                
                while (reader.Read()){
                    password = reader[0].ToString();    
                }    
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return password;
    }
    public static List<String[]> get_personal_information(String email){
        List<String[]> information_list = new List<String[]>();
        try{
            string procedure = "sp_persons_get_information_by_email";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("p_email", email);
            using(MySqlDataReader reader = command.ExecuteReader()){
                
                while (reader.Read()){
                    information_list.Add(new String[]{reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString()});   
                }                 
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return information_list;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="specialization"></param>
    /// <returns></returns>
    public static List<string[]> get_doctor(Specialization specialization){
        List<string[]> doctor_list = new List<string[]>();
        try{
            string procedure = "sp_persons_get_doctor_by_specialization";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("p_specialization", specialization.ToString());
            using(MySqlDataReader reader = command.ExecuteReader()){
                while (reader.Read()){
                    doctor_list.Add(new string[]{reader[0].ToString(),reader[1].ToString(),reader[2].ToString()});
                }
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return doctor_list;
    }
    /// <summary>
    /// Counts the ammount of appointments in a given month for each day
    /// </summary>
    /// <param name="doctor_ID">The person_ID of the doctor</param>
    /// <param name="date_and_time">The complete date and time for the wanted day</param>
    /// <example>For Example:
    /// <code> Database_acces.get_appointments_on_day("3", new DateTime(2017, 07, 25, 23, 45, 5));
    /// </code>
    /// </example>
    /// <returns>Returns a List of String Arrays containing the ammount of appointments and the day</returns>
    public static List<String[]> get_appointments_per_day(String doctor_ID, DateTime date_and_time){
        List<String[]> appointments_and_days_list = new List<string[]>();
        try{
            string procedure = "sp_appointments_find_day";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("p_doctor_ID", doctor_ID);
            command.Parameters.AddWithValue("p_date_and_time", date_and_time);
            using(MySqlDataReader reader = command.ExecuteReader()){
                
                while (reader.Read()){
                    appointments_and_days_list.Add(new string[]{reader[0].ToString(), reader[1].ToString()});
                }
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return appointments_and_days_list;
    }
    /// <summary>
    /// Selects each appointment for the given doctor on the given day from the table appointments.
    /// </summary>
    /// <param name="doctor_ID">The person_ID of the doctor</param>
    /// <param name="date_and_time">The complete date and time for the wanted day</param>
    /// <example>For Example:
    /// <code> Database_acces.get_appointments_on_day("3", new DateTime(2017, 07, 25, 23, 45, 5));
    /// </code>
    /// </example>
    /// <returns>An List with every appointment an doctor has</returns>
    public static  List<String[]> get_appointments_on_day(String doctor_ID, DateTime date_and_time){
        List<String[]> appointments_list = new List<string[]>();
        try{
            string procedure = "sp_appointments_get_appointments_on_day";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("p_doctor_ID", doctor_ID);
            command.Parameters.AddWithValue("p_date_and_time", date_and_time);
            using(MySqlDataReader reader = command.ExecuteReader()){
                
                while (reader.Read()){
                    appointments_list.Add(new String[]{reader[0].ToString()});
                }    
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return appointments_list;
    }
    /// <summary>
    /// Inserts the given params into the table appoiintments
    /// </summary>
    /// <param name="date_and_time">The day and time of the appointment</param>
    /// <param name="patient_ID">The person_ID of the patient</param>
    /// <param name="doctor_ID">The person_ID of the doctor</param>
    /// <param name="description">A short summary for the appointment</param>
    public static void insert_appointment(DateTime date_and_time, String patient_ID, String doctor_ID, String description){
        try{
            string procedure = "sp_insert_appointment";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;

            command.Parameters.AddWithValue("p_date_and_time", date_and_time);
            command.Parameters.AddWithValue("p_patient_ID", patient_ID);
            command.Parameters.AddWithValue("p_doctor_ID", doctor_ID);
            command.Parameters.AddWithValue("p_description", description);

            command.ExecuteNonQuery();
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
    }
    
    //TODO delete after Project Completion
    /*public static{
        try{
            string procedure = "";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;

            command.Parameters.AddWithValue("", );
            using(MySqlDataReader reader = command.ExecuteReader()){

                while (reader.Read()){
                }
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
    }*/
}