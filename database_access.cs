using System;
using MySqlConnector;
using Tmds.DBus.Protocol;
namespace appointment_management_system;

public class database_access
{
    private static database_access instance;
    private String mysql_connection_string = "server = localost:8081; uid = access_client; pwd = accesspassword; database = appointment_managment";
    private static MySqlConnection mysql_connection;

    private database_access()
    {
        mysql_connection = new MySqlConnection();
         try
        {
            mysql_connection.ConnectionString = mysql_connection_string;
            mysql_connection.Open();
        }catch(MySqlException ex)
        {
            Console.WriteLine(ex);
        }

    }
    private static void close_database()
    {
        mysql_connection.Close();
    }

    public static database_access get_instance() //required to call for database connection
    {
        if(instance == null)
        {
            instance = new database_access();
            return instance;  
        }
        else
        {
            return instance;    
        }
        
        
    }
}