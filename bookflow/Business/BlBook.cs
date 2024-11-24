using bookflow.DbSettings;
using MongoDB.Driver;
using bookflow.Models;

namespace bookflow.Business
{
    public class BlBook
    {
        private readonly DbAccess _dbAccess;
        public BlBook(DbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public void teste()
        {
            _dbAccess._reservationRepository.InsertOne(
                new Models.Reservation
                {
                    Date = DateTime.Now,
                    BookId = "123",
                    Status = Models.ReservationStatus.Active,
                    UserId = "123",
                }
                );
        }

        public async Task<string> BorrowCopy(string id)
        {
            var book = _dbAccess._bookRepository.GetById(id);

            if (book == null) return "Livro não encontrado";
            var queries = new List<FilterDefinition<Copy>> {
                Builders<Copy>.Filter.Eq("BookId", id),
                Builders<Copy>.Filter.Eq("Available",true),
                Builders<Copy>.Filter.Ne("Condition",CopyCondition.Lost),
                Builders<Copy>.Filter.Ne("Condition",CopyCondition.Damaged)
            };
            var availableCopies = await _dbAccess._copyRepository.GetFiltered(Builders<Copy>.Filter.And(queries));


            return "";
        }
    }
}
