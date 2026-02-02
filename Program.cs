using System.Data;
using Dapper;
using Npgsql;

await using var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("PG_CONNECTION_STRING")
    ?? "Host=localhost;Database=postgres;Username=postgres;Password=postgres");

await connection.OpenAsync();

var parameters = new
{
    Ids = new PostgresArray<string>(["a"])
};

var command = new CommandDefinition("SELECT unnest(@Ids) AS Value", parameters, cancellationToken: CancellationToken.None);
var rows = await connection.QueryAsync<string>(command); // <--- Segfault happens here

foreach (var value in rows) // <--- This line is never reached
{
    Console.WriteLine(value);
}

internal readonly struct PostgresArray<T>(T[] value) : SqlMapper.ICustomQueryParameter
{
    public void AddParameter(IDbCommand command, string name)
    {
        var param = new NpgsqlParameter(name, value);
        command.Parameters.Add(param);
    }
}
