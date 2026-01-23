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

    public static String[]? get_personal_information(String email){
        String[]? information = null;
        List<String> information_list = new List<String>();
        try{
            string procedure = "sp_persons_get_information_by_email";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("p_email", email);
            using(MySqlDataReader reader = command.ExecuteReader()){
                
                while (reader.Read()){
                    information_list.Add(reader[0].ToString());
                    information_list.Add(reader[1].ToString());
                    information_list.Add(reader[2].ToString());
                    information_list.Add(reader[3].ToString());
                    information_list.Add(reader[4].ToString());    
                }
                information = information_list.ToArray();                    
            } 
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return information;
    }

    /*Returns an Array of every doctor in the given specialization
    every doctor has 3 values id, name and surname*/
    public static List<string[]>? get_doctor(Specialization specialization){
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
    
    public static String[]? get_appointments_per_day(String doctor_ID, DateTime date_and_time){
        String[]? appointments_and_days = null;
        List<String> appointments_and_days_list = new List<string>();
        try{
            string procedure = "sp_appointments_find_day";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("p_doctor_ID", doctor_ID);
            command.Parameters.AddWithValue("p_date_and_time", date_and_time);
            using(MySqlDataReader reader = command.ExecuteReader()){
                
                while (reader.Read()){      
                }    
            }
              
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return appointments_and_days;
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