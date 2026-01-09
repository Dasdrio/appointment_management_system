using System;
using System.Data;
using MySqlConnector;
namespace appointment_management_system;

public class Database_access
{
    private static Database_access? instance;
    private String mysql_connection_string = "server=localhost;port=3306;uid=access_client;pwd=accesspassword;database=appointment_managment";
    private static MySqlConnection mysql_connection;
    private static MySqlCommand? command;

    private Database_access()
    {
        mysql_connection = new MySqlConnection();
        try{
            mysql_connection.ConnectionString = mysql_connection_string;
            mysql_connection.Open();
        }catch(MySqlException ex){
            Console.WriteLine(ex.ToString());
        }

    }
    private static void close_database()
    {
        if(instance != null){
            mysql_connection.Close();
            instance = null;
        }
    }

    public static Database_access get_instance() //required to call for database connection
    {
        if(instance == null){
            instance = new Database_access();
            return instance;  
        }else{
            return instance;    
        }
    }

    public static void delete_person(int person_ID) //deletion of the own AC
    {
        //Passwortabfrage einarbeiten
        string procedure = "sp_delete_person";
        command = new MySqlCommand(procedure, mysql_connection);
        command.CommandType = CommandType.StoredProcedure;
        
        command.Parameters.AddWithValue("p_person_ID", person_ID);
        command.ExecuteNonQuery();
    }

    public static String? get_password(String email)
    {
        String? password = null;

        try{
            string procedure = "sp_view_person_by_email";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("p_email", email);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                return reader[5].ToString();    
            }
            
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }

        return password;
    }
}