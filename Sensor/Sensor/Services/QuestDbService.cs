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

    public async Task<List<SensorData>> GetLast24hChartData(string dataName)
    {
        var connectionString = $@"host={host};port={port};username={username};password={password};
database={database};ServerCompatibilityMode=NoTypeLoading;";

        await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        var sql = $"SELECT avg({dataName}),timestamp FROM 'sensors_{dataName}' WHERE timestamp  BETWEEN now() AND dateadd('d', -1, now()) SAMPLE BY 15m;";

        List<SensorData> temperatureData = new List<SensorData>();
        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
        await using (var reader = await command.ExecuteReaderAsync()) {
            while (await reader.ReadAsync())
            {
                temperatureData.Add(new SensorData()
                {
                    data =  reader.GetDouble(0),
                    time =  reader.GetDateTime(1)
                });
            }
        }

        return temperatureData;
    }
}