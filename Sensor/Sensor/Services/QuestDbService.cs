using Sensor.Sensor.Model;

namespace Sensor.Sensor.Services;
using Npgsql;
public class QuestDbService
{
    
    string username = "admin";
    string password = "quest";
    string database = "qdb";
    private string host = Environment.GetEnvironmentVariable("_QUEST_DB_HOST_");
    int port = 8812;

    public async Task<List<SensorData>> GetTemperatureData(string dataName)
    {
        var connectionString = $@"host={host};port={port};username={username};password={password};
database={database};ServerCompatibilityMode=NoTypeLoading;";

        await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        var sql = $"SELECT device,sensor,{dataName},timestamp FROM 'sensors_{dataName}' ORDER by timestamp DESC  LIMIT 1000;";

        List<SensorData> temperatureData = new List<SensorData>();
        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
        await using (var reader = await command.ExecuteReaderAsync()) {
            while (await reader.ReadAsync())
            {
                temperatureData.Add(new SensorData()
                {
                    device =  reader.GetString(0),
                    sensor =  reader.GetString(1),
                    data =  reader.GetDouble(2),
                    time =  reader.GetDateTime(3)
                });
            }
        }

        return temperatureData;
    }
}