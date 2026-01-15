using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DynamicData;
using MySqlConnector;
namespace appointment_management_system;

public class Database_access
{
    private static Database_access? instance;
    private String mysql_connection_string = "server=localhost;port=3306;uid=access_client;pwd=accesspassword;database=appointment_managment";
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
    private static void close_database(){
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
        String[] information = null;
        List<String> information_list = new List<String>();
        try{
            string procedure = "sp_persons_get_information_by_email";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("p_email", email);
            using(MySqlDataReader reader = command.ExecuteReader()){
                
                while (reader.Read()){
                    information_list.Add(reader[0].ToString());
                    information = information_list.ToArray();    
                }
                    
            } 
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return information;
    }
}