using Npgsql;
using System.Data;

namespace Data.Persistence
{
    public class ContextRead
    {
        private readonly string _stringConnection;
        private readonly IDbConnection _connection;

        public ContextRead(string stringConnection)
        {
            _stringConnection = stringConnection;
            _connection = new NpgsqlConnection(stringConnection);
        }
        public IDbConnection Connect() => _connection;
    }
}
