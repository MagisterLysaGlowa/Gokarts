using Gokarts.Database;
using System.Data.SQLite;
using Gokarts.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static Gokarts.Database.DataBase;

namespace Gokarts.Controllers;

public static class DataBaseController
{
    #region Initialize
    public static void Initialize() => DataBase.Initialize();

    #endregion

    #region SelectFunctions
    public static ObservableCollection<T> Select<T>(string query) where T : class
    {
        // Execute the query
        SQLiteDataReader reader = CreateCommand(query).ExecuteReader();
        ObservableCollection<T> results = new();

        // Iterate over the returned rows and convert them into `T`.
        while (reader.Read())
        {
            results.Add(Deserialize<T>(reader));
        }
        reader.Close();
        return results;
    }

    private static string ToPascalCase(string databaseName)
    {
        string returnValue = string.Empty;
        string[] strings = databaseName.Split('_');
        foreach (string s in strings)
        {
            returnValue += string.Concat(s[0].ToString().ToUpper(), s.AsSpan(1));
        }
        return returnValue;
    }

    private static T Deserialize<T>(SQLiteDataReader reader) where T : class
    {
        T instance = Activator.CreateInstance<T>();

        // Iterate over all columns and set their values in `instance`.
        for (int i = 0; i < reader.FieldCount; i++)
        {
            string columnName = ToPascalCase(reader.GetName(i));
            object value = reader.GetValue(i);

            // Convert the value into proper type and assign it in `instance`.
            var property = instance.GetType().GetProperty(columnName);
            if (property == null)
            {
                Trace.TraceWarning($"Class {typeof(T).FullName} does not contain key {columnName} present in the database.");
                continue;
            }
            object convertedValue;
            if (property.PropertyType.IsArray)
            {
                var valueArray = Convert.ToString(value)?.Split(';');
                convertedValue = Convert.ChangeType(valueArray, property.PropertyType)!;
            }
            else
            {
                convertedValue = Convert.ChangeType(value, property.PropertyType);
            }
            property.SetValue(instance, convertedValue);
        }
        return instance;
    }

    public static ObservableCollection<Team> SelectTeams() => Select<Team>("SELECT * FROM Teams");

    public static ObservableCollection<Player> SelectPlayers() => Select<Player>("SELECT * FROM Players");

    public static ObservableCollection<Tournament> SelectTournaments() => Select<Tournament>("SELECT * FROM Tournaments");

    public static ObservableCollection<Tournament> SelectTournament(int id) => Select<Tournament>($"SELECT * FROM Tournaments WHERE id={id}");

    public static ObservableCollection<Penalty> SelectPenalties() => Select<Penalty>("SELECT * FROM Penalties");

    public static ObservableCollection<Run> SelectRuns() => Select<Run>("SELECT * FROM Runs");

    public static ObservableCollection<TournamentOccurence> SelectTournamentOccurences() => Select<TournamentOccurence>("SELECT * FROM Tournament_Occurences");

    public static ObservableCollection<Lap> SelectLaps() => Select<Lap>("SELECT * FROM Laps");
    #endregion

    #region InsertFunctions
    public static int InsertTeam(Team team) => ExecuteNonQuery($"INSERT INTO Teams (name, img) VALUES ('{team.Name}', '{team.Img}')");

    public static int InsertPlayer(Player player) => ExecuteNonQuery($"INSERT INTO Players (first_name, last_name, date_of_birth, team_id, img) VALUES ('{player.FirstName}', '{player.LastName}', '{player.DateOfBirth}', {player.TeamId}, '{player.Img}')");

    public static int InsertPenalty(Penalty penalty) => ExecuteNonQuery($"INSERT INTO Penalties (name, value) VALUES ({penalty.RunId}, '{penalty.Name}', {penalty.Value})");

    public static int InsertTournament(Tournament tournament) => ExecuteNonQuery($"INSERT INTO Tournaments (name, img, is_active, description, type) VALUES ('{tournament.Name}', '{tournament.Img}', {tournament.IsActive}, '{tournament.Description}', '{tournament.Type}')");

    public static int InsertRun(Run run) => ExecuteNonQuery($"INSERT INTO Runs (player_id, tournament_id, time, penalties, description) VALUES ({run.PlayerId}, {run.TournamentId}, {run.Time}, '{run.Description}')");

    public static int InsertTournamentOccurence(TournamentOccurence occurence) => ExecuteNonQuery($"INSERT INTO Tournament_Occurences (tournament_id, date) VALUES ({occurence.TournamentId}, '{DateTime.Now.ToShortDateString()}')");

    public static int InsertLap(Lap lap) => ExecuteNonQuery($"INSERT INTO Laps (time, run_id) VALUES ({lap.Time}, {lap.RunId})");
    #endregion

    #region UpdateFunctions
    public static int UpdateTeam(Team team) => ExecuteNonQuery($"UPDATE Teams SET name='{team.Name}', img='{team.Img}' WHERE id={team.Id}");

    public static int UpdatePlayer(Player player) => ExecuteNonQuery($"UPDATE Players SET first_name='{player.FirstName}', last_name='{player.LastName}', date_of_birth='{player.DateOfBirth}', team_id={player.TeamId}, img='{player.Img}' WHERE id={player.Id}");

    public static int UpdatePenalty(Penalty penalty) => ExecuteNonQuery($"UPDATE Penalties SET run_id={penalty.RunId}, name='{penalty.Name}', value={penalty.Value} WHERE id={penalty.Id}");

    public static int UpdateTournament(Tournament tournament) => ExecuteNonQuery($"UPDATE Tournaments SET name='{tournament.Name}', img='{tournament.Img}', is_active={tournament.IsActive}, description='{tournament.Description}', type='{tournament.Type}' WHERE id={tournament.Id}");

    public static int UpdateRun(Run run) => ExecuteNonQuery($"UPDATE Runs SET player_id={run.PlayerId}, tournament_id={run.TournamentId}, time={run.Time}, description='{run.Description}' WHERE id={run.Id}");

    public static int UpdateTournamentOccurence(TournamentOccurence occurence) => ExecuteNonQuery($"UPDATE Tournament_Occurences SET tournament_id={occurence.TournamentId}, date='{occurence.Date}' WHERE id={occurence.Id}");

    public static int UpdateLap(Lap lap) => ExecuteNonQuery($"UPDATE Laps SET time={lap.Time}, run_id={lap.RunId} WHERE id={lap.Id}");
    #endregion

    #region DeleteFunctions
    private static int Delete(string tableName, int id) => ExecuteNonQuery($"DELETE FROM {tableName} WHERE id={id}");

    public static int DeleteTeam(int id) => Delete("Teams", id);

    public static int DeletePlayer(int id) => Delete("Players", id);

    public static int DeletePenalty(int id) => Delete("Penalties", id);

    public static int DeleteTournament(int id) => Delete("Tournaments", id);

    public static int DeleteRun(int id) => Delete("Runs", id);

    public static int DeleteTournamentOccurence(int id) => Delete("Tournament_Occurences", id);

    public static int DeleteLap(int id) => Delete("Laps", id);
    #endregion
}
