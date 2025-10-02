using Microsoft.Data.Sqlite;
namespace SQL_CSHARP_HTTP_MESS;

public class Database
{
    public Database()
    {
        using (var connection = new SqliteConnection("Data Source=DataBaserrr.db")) // make commands connect and create DataBaserrr.db
        {
            connection.Open(); // connector

            var command = connection.CreateCommand(); // tell computer wut tu du
            command.CommandText =
            @"
PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS Owner (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  name TEXT NOT NULL,
  phone_number TEXT
);

CREATE TABLE IF NOT EXISTS Pet (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  pet_name TEXT NOT NULL,
  pet_type TEXT,
  owner_id INTEGER NOT NULL,
  FOREIGN KEY (owner_id) REFERENCES Owner(id)
);
            ";
            command.ExecuteNonQuery(); // execute teh cmd, not gib bak data
            connection.Close(); // close ze linkrrrr
        }
    }
    public int AddOwner(string name, string phone)
    {
        using (var connection = new SqliteConnection("Data Source=DataBaserrr.db"))
        {
            connection.Open(); // open the connection

            var command = connection.CreateCommand(); // tell computer wut tu du
            command.CommandText =
            @"
        INSERT INTO Owner(name, phone_number) VALUES(@name, @phone);
        ";

            command.Parameters.AddWithValue("@name", name);   // put the given name into @name
            command.Parameters.AddWithValue("@phone", phone); // put the given phone into @phone

            command.ExecuteNonQuery(); // run teh command, no gib bak data

            command.CommandText = "SELECT last_insert_rowid();";
            int ownerId = Convert.ToInt32(command.ExecuteScalar());

            connection.Close(); // close ze linkrrrr

            return ownerId; // return neu ownr idrssssssssssss
        }
    }
    public void AddPet(string petName, string petType, int ownerId)
    {
        using (var connection = new SqliteConnection("Data Source=DataBaserrr.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
        INSERT INTO Pet(pet_name, pet_type, owner_id) VALUES(@name, @type, @ownerId);
            ";

            command.Parameters.AddWithValue("@name", petName);
            command.Parameters.AddWithValue("@type", petType);
            command.Parameters.AddWithValue("@ownerId", ownerId);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void UpdateOwnerPhone(int ownerId, string newPhone)
    {
        using (var connection = new SqliteConnection("Data Source=DataBaserrr.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
        UPDATE Owner SET phone_number = @phone WHERE id = @id;
            ";

            command.Parameters.AddWithValue("@phone", newPhone);
            command.Parameters.AddWithValue("@id", ownerId);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public string? GetOwnerPhoneByPetName(string petName)
    {
        using (var connection = new SqliteConnection("Data Source=DataBaserrr.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                SELECT Owner.phone_number
                FROM Owner
                JOIN Pet ON Owner.id = Pet.owner_id
                WHERE Pet.pet_name = @petName;
            ";

            command.Parameters.AddWithValue("@petName", petName);
            var result = command.ExecuteScalar();
            return result?.ToString();
        }
    }
}