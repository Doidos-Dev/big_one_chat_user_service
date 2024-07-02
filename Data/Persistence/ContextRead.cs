using Npgsql;
using System.Data;

namespace Data.Persistence
{
    public class ContextRead(string stringConnection)
    {
        private readonly string _stringConnection = stringConnection;
        public IDbConnection Connection() => new NpgsqlConnection(_stringConnection);
    }
}
