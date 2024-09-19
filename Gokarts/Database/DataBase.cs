using System.Data.SQLite;
using System.Diagnostics;

namespace Gokarts.Database;

public static class DataBase
{
    private static SQLiteConnection? _db = null;

    public static SQLiteConnection Db 
    { 
        get
        {
            if (_db == null)
            {
                throw new Exception("Not connected to the database.");
            }
            return _db;
        }
    }

    private static void ExecuteQuery(string sql)
    {
        try
        {
            SQLiteCommand com = Db.CreateCommand();
            com.CommandText = sql;
            com.ExecuteScalar();
            com.Dispose();
        }
        catch (Exception ex) 
        {
            Trace.WriteLine($"Exception {ex}, message: {ex.Message}");
        }

    }

    private static void InitializeDatabase()
    {
        try
        {
            ExecuteQuery($"PRAGMA foreign_keys = ON;");
            ExecuteQuery($"CREATE TABLE IF NOT EXISTS \"Teams\" (\"id\" INTEGER, \"name\" TEXT, \"img\" TEXT, PRIMARY KEY(\"id\" AUTOINCREMENT));");
            ExecuteQuery($"CREATE TABLE IF NOT EXISTS \"Players\" (\"id\" INTEGER, \"first_name\" TEXT, \"last_name\" TEXT, \"date_of_birth\" TEXT, \"team_id\" INTEGER, \"img\" TEXT, PRIMARY KEY(\"id\" AUTOINCREMENT), FOREIGN KEY(\"team_id\") REFERENCES \"Teams\"(\"id\"))");
            ExecuteQuery($"CREATE TABLE IF NOT EXISTS \"Penalties\" (\"id\" INTEGER, \"run_id\" INTEGER, \"name\" TEXT, \"value\" INTEGER, PRIMARY KEY(\"id\" AUTOINCREMENT), FOREIGN KEY(\"run_id\") REFERENCES \"Runs\"(\"id\"))");
            ExecuteQuery($"CREATE TABLE IF NOT EXISTS \"Tournaments\" (\"id\" INTEGER, \"name\" TEXT, \"img\" TEXT, \"is_active\" BOOLEAN, \"description\" TEXT, \"type\" TEXT, PRIMARY KEY(\"id\" AUTOINCREMENT))");
            ExecuteQuery($"CREATE TABLE IF NOT EXISTS \"Runs\" (\"id\" INTEGER, \"player_id\" INTEGER, \"tournament_id\" INTEGER, \"time\" INTEGER, \"penalties\" TEXT, \"description\" TEXT, PRIMARY KEY(\"id\" AUTOINCREMENT), FOREIGN KEY(\"tournament_id\") REFERENCES \"Tournament\"(\"id\"), FOREIGN KEY(\"player_id\") REFERENCES \"Players\"(\"id\"))");
            ExecuteQuery($"CREATE TABLE IF NOT EXISTS \"Tournament_Occurences\" (\"id\" INTEGER, \"tournament_id\" INTEGER, \"date\" TEXT, PRIMARY KEY(\"id\" AUTOINCREMENT), FOREIGN KEY(\"tournament_id\") REFERENCES \"Tournaments\"(\"id\"))");
            ExecuteQuery($"CREATE TABLE IF NOT EXISTS \"Laps\" (\"id\" INTEGER, \"time\" INTEGER, \"run_id\" INTEGER, PRIMARY KEY(\"id\" AUTOINCREMENT), FOREIGN KEY(\"run_id\") REFERENCES \"Runs\"(\"id\"))");
        }
        catch (Exception ex)
        {
            Trace.WriteLine($"Exception {ex}, message: {ex.Message}");
        }
    }

    public static void Initialize()
    {
        try
        {
            Trace.WriteLine("Current Directory: "+Environment.CurrentDirectory);

 
            string path = Environment.CurrentDirectory +"/Gokarts.db";

            string cs = @"URI=file:" + path + "; PRAGMA foreign_keys = 1;";

            _db = new SQLiteConnection(cs);

            //Opening connection between Database and Project
            _db.Open();

            InitializeDatabase();
        }
        catch (Exception ex)
        {
           Trace.WriteLine($"Exception {ex}, message: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new <c>SQLiteCommand</c> object using the default database connection.
    /// </summary>
    /// <param name="command">commandText for SQLiteCommand</param>
    /// <returns></returns>
    public static SQLiteCommand CreateCommand(string command)
    {
        return new SQLiteCommand(command, Db);
    }

    /// <summary>
    /// Creates and executes a new <c>SQLiteCommand</c> object using the default database connection.
    /// </summary>
    /// <param name="command">command to execute</param>
    /// <returns>Number of affected rows</returns>
    public static int ExecuteNonQuery(string command)
    {
        return ExecuteNonQuery(command, out _);
    }

    /// <summary>
    /// Creates and executes a new <c>SQLiteCommand</c> object using the default database connection.
    /// </summary>
    /// <param name="command">command to execute</param>
    /// <param name="sqliteCommand">executed SQLiteCommand object or null on failure</param>
    /// <returns>Number of affected rows</returns>
    public static int ExecuteNonQuery(string command, out SQLiteCommand sqliteCommand)
    {
        sqliteCommand = CreateCommand(command);
        return sqliteCommand.ExecuteNonQuery();
    }
}
