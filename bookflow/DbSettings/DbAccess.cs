using MongoDB.Driver;
using bookflow.Models;

namespace bookflow.DbSettings
{
    public class DbAccess
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;

        public BaseRepository<User> _userRepository { get; set; }
        public BaseRepository<Copy> _copyRepository { get; set; }
        public BaseRepository<Reservation> _reservationRepository { get; set; }
        public BaseRepository<Loan> _loanRepository { get; set; }
        public BaseRepository<Book> _bookRepository { get; set; }
        public BaseRepository<Fine> _fineRepository { get; set; }
        public DbAccess()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = _configuration["Database:ConnectionString"];
            var databaseName = _configuration["Database:Name"];
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(databaseName);


            _userRepository = new BaseRepository<User>(_database);
            _bookRepository = new BaseRepository<Book>(_database);
            _loanRepository = new BaseRepository<Loan>(_database);
            _fineRepository = new BaseRepository<Fine>(_database);
            _copyRepository = new BaseRepository<Copy>(_database);
            _reservationRepository = new BaseRepository<Reservation>(_database);
        }
    }
}
