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

    public static int login(String email, String password) //User login returns the user_ID back
    {
        int user_ID = 0;

        return user_ID;
    }

    public static void delete_person(int person_ID, String password) //deletion of the own AC
    {
        //Passwortabfrage einarbeiten
        string procedure = "sp_delete_person";
        command = new MySqlCommand(procedure, mysql_connection);
        command.CommandType = CommandType.StoredProcedure;
        
        command.Parameters.AddWithValue("p_person_ID", person_ID);
        command.ExecuteNonQuery();
    }

    public static String[] view_person(int person_ID) //get Infrmation abbaut a Person by their ID
    {
        try{
            string procedure = "sp_view_person_by_ID";
            MySqlCommand command = new MySqlCommand(procedure, mysql_connection);
            command.CommandType =CommandType.StoredProcedure;
            
            Console.Write("Start querry");
            command.Parameters.AddWithValue("p_person_ID", person_ID);
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                String[] temp = {(string)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5]};
                return temp;
            }
        }catch(Exception ex){
            Console.Write("Fehler Meldung: " + ex);
        }
        return null;
    }
}