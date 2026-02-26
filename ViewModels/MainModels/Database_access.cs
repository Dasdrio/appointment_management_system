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
    //Change in the finished product to a seperate account
    private String mysql_connection_string = "server=localhost;port=3306;uid=root;pwd=rootpassword;database=appointment_managment";
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
    /// <summary>
    /// Closes and destroys the instance of the database
    /// </summary>
    public static void close_database(){
        if(instance != null){
            mysql_connection.Close();
            instance = null;
        }
    }

    /// <summary>
    /// Creates an instance of the database or returns an existing one
    /// </summary>
    /// <returns>the instance for the database acces</returns>
    public static Database_access get_instance(){ 
        if(instance == null){
            instance = new Database_access();
            return instance;  
        }else{
            return instance;    
        }
    }
    /// <summary>
    /// Selects the person by the given email to return the passwordhash
    /// </summary>
    /// <param name="email">The email of the person thath wants to log in</param>
    /// <returns>The passwordhash as a String</returns>
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
    /// <summary>
    /// A Query to get every information about an client
    /// </summary>
    /// <param name="email">The email of the person thath is logged in</param>
    /// <returns>A List with an String Array Values ID, specialization, name and surname</returns>
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
    /// Selects the doctors working in the given specialization.
    /// </summary>
    /// <param name="specialization"></param>
    /// <returns>A List containing a String Arrays with the ID, name and surname of each doctor in the given specialization</returns>
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
    /// <returns>A List with every appointment a doctor has</returns>
    public static  List<String> get_appointments_on_day(String doctor_ID, DateTime date_and_time){
        List<String> appointments_list = new List<string>();
        try{
            string procedure = "sp_appointments_get_appointments_on_day";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("p_doctor_ID", doctor_ID);
            command.Parameters.AddWithValue("p_date_and_time", date_and_time);
            using(MySqlDataReader reader = command.ExecuteReader()){
                
                while (reader.Read()){
                    appointments_list.Add(reader[0].ToString());
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
            string procedure = "sp_appointments_insert_appointment";
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
    /// <summary>
    /// Inserts the data of a new person in the database
    /// </summary>
    /// <param name="p_name">The name of the Person as a String</param>
    /// <param name="p_surname">The surname of the Person as a String</param>
    /// <param name="p_email">The email of the Person as a String</param>
    /// <param name="p_password_hash">The hasched password of the Person as a String</param>
    public static void insert_new_person(String p_name, String p_surname, String p_email, String p_password_hash)
    {
        try{
            string procedure = "sp_persons_insert_person";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;

            command.Parameters.AddWithValue("p_name", p_name);
            command.Parameters.AddWithValue("p_surname", p_surname);
            command.Parameters.AddWithValue("p_email", p_email);
            command.Parameters.AddWithValue("p_password_hash", p_password_hash);

            command.ExecuteNonQuery();
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
    }
    /// <summary>
    /// Selects every appointment from the given date and time for the given patient
    /// </summary>
    /// <param name="p_patient_ID">The id of the selected Patient</param>
    /// <param name="p_date_and_time">The date and time as a startingpoint for the selection</param>
    /// <returns>Returns a List with String Arrays that contains the values 
    /// (appointment_ID, date_and_time, name of the doctor, surname of the doctor, description)
    /// </returns>
    public static List<String[]> get_appointments_as_patient(int p_patient_ID, DateTime p_date_and_time)
    {
        List<String[]> appointments = new List<string[]>();
        try{
            string procedure = "sp_appointments_get_appointments_from_patient";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;

            command.Parameters.AddWithValue("p_patient_ID", p_patient_ID);
            command.Parameters.AddWithValue("p_date_and_time", p_date_and_time);
            using(MySqlDataReader reader = command.ExecuteReader()){

                while (reader.Read())
                {
                    appointments.Add(new String[]{reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString()});
                }
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return appointments;
    }
    /// <summary>
    /// Selects every appointment from the given date and time for the given doctor
    /// </summary>
    /// <param name="p_doctor_ID">The id of the selected doctor</param>
    /// <param name="p_date_and_time">The date and time as a startingpoint for the selection</param>
    /// <returns>Returns a List with String Arrays that contains the values 
    /// (appointment_ID, date_and_time, name of the patient, surname of the patient, description)
    /// </returns>
    public static List<String[]> get_appointments_as_doctor(int p_doctor_ID, DateTime p_date_and_time)
    {
        List<String[]> appointments = new List<string[]>();
        try{
            string procedure = "sp_appointments_get_appointments_from_doctor";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;

            command.Parameters.AddWithValue("p_doctor_ID", p_doctor_ID);
            command.Parameters.AddWithValue("p_date_and_time", p_date_and_time);
            using(MySqlDataReader reader = command.ExecuteReader()){

                while (reader.Read())
                {
                    appointments.Add(new String[]{reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString()});
                }
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return appointments;
    }
    /// <summary>
    /// Updates the description of the appointment
    /// </summary>
    /// <param name="p_appointment_ID">The Id of the appointment to update</param>
    /// <param name="p_description">The new description</param>
    public static void update_description(int p_appointment_ID, String p_description)
    {
        try{
            string procedure = "sp_appointments_update_description";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;

            command.Parameters.AddWithValue("p_appointment_ID", p_appointment_ID);
            command.Parameters.AddWithValue("p_description", p_description);

            command.ExecuteNonQuery();
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
    }
    /// <summary>
    /// Deletes the appointment
    /// </summary>
    /// <param name="p_appointment_ID">The appointment to delete</param>
    public static void delete_appointment(int p_appointment_ID)
    {
        try{
            string procedure = "sp_appointments_delete_appointment";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;

            command.Parameters.AddWithValue("p_appointment_ID", p_appointment_ID);
            
            command.ExecuteNonQuery();
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
    }
    /// <summary>
    /// Gives the appointment with the given ID
    /// </summary>
    /// <param name="p_appointment_ID"></param>
    /// <returns>Returns a List with Strings with followling values (date_and_time, name, surname, description) </returns>
    public static List<String> get_appointment_by_id(int p_appointment_ID)
    {
        List<String> appointment = new List<string>();
        try{
            string procedure = "sp_appointments_get_appointment_by_ID";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;

            command.Parameters.AddWithValue("p_appointment_ID", p_appointment_ID);
            using(MySqlDataReader reader = command.ExecuteReader()){

                while (reader.Read())
                {
                    appointment.Add(reader[0].ToString());
                    appointment.Add(reader[1].ToString());
                    appointment.Add(reader[2].ToString());
                    appointment.Add(reader[3].ToString());
                }
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return appointment;
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