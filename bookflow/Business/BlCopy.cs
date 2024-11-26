using bookflow.DbSettings;
using bookflow.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace bookflow.Business
{
    public class BlCopy
    {
        private readonly DbAccess _dbAccess;
        public BlCopy(DbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public async Task<Loan> BorrowCopy(string id, DateTime returnDate)
        {
            if (DateTime.Now > returnDate) throw new ValidationException("Data de devolução inválida!");
            if ((returnDate - DateTime.Now).Days > 10) throw new ValidationException("Você pode alugar livro por no máximo 10 dias!");

            var book = _dbAccess._bookRepository.GetById(id);

            if (book == null) throw new ValidationException("Livro não encontrado");
            var queries = new List<FilterDefinition<Copy>> {
                Builders<Copy>.Filter.Eq("BookId", id),
                Builders<Copy>.Filter.Eq("Available",true),
                Builders<Copy>.Filter.Ne("Condition",CopyCondition.Lost),
                Builders<Copy>.Filter.Ne("Condition",CopyCondition.Damaged)
            };
            var availableCopies = await _dbAccess._copyRepository.GetFiltered(Builders<Copy>.Filter.And(queries), new string[] { "_id" });
            var copyId = availableCopies.FirstOrDefault()?.Id;
            if (string.IsNullOrEmpty(copyId)) throw new ValidationException("Não há um exemplar desse livro disponível no momento!");


            await _dbAccess._copyRepository.Update(
                Builders<Copy>.Filter.Eq("_id", new ObjectId(copyId)),
                Builders<Copy>.Update.Set(x => x.Available, false)
                );

            var loan = new Loan()
            {
                CopyId = availableCopies.First().Id,
                Status = LoanStatus.Active,
                Date = DateTime.Now,
                ReturnDate = DateTime.Now,
            };

            await _dbAccess._loanRepository.InsertOne(loan);

            return loan;
        }

        public async Task<string> ReturnCopy(string id)
        {

            var filter = Builders<Loan>.Filter.Eq("_id", new ObjectId(id));

            var loan = await _dbAccess._loanRepository.GetFiltered(filter, new string[] { "CopyId", "UserId" });

            if (loan.FirstOrDefault() == null) throw new ValidationException("Nenhum empréstimo foi encontrado");

            await _dbAccess._copyRepository.Update(
                Builders<Copy>.Filter.Eq("_id", new ObjectId(loan.FirstOrDefault()?.CopyId)),
                Builders<Copy>.Update.Set(x => x.Available, true)
            );

            await _dbAccess._loanRepository.Update(
                filter,
                Builders<Loan>.Update.Set(x => x.Status, LoanStatus.Returned)
                );

            return "";
        }

    }
}
