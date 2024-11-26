using bookflow.DbSettings;
using MongoDB.Driver;
using bookflow.Models;
using MongoDB.Bson;

namespace bookflow.Business
{
    public class BlBook
    {
        private readonly DbAccess _dbAccess;
        public BlBook(DbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public List<Book> GetBooks(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ValidationException("Insira um valor de entrada");


            if (ObjectId.TryParse(value, out var id))
            {
                var book = _dbAccess._bookRepository.GetById(value) ;
                return book == null ? new List<Book> { } : new List<Book> { book };
            }


            var cleanedValue = $"^{string.Join(@"\s*", value.Replace(" ", "").ToCharArray())}$";
            var queries = new List<FilterDefinition<Book>> {
                Builders<Book>.Filter.Regex("Author", new BsonRegularExpression(cleanedValue, "i")),
                Builders<Book>.Filter.Regex("Title", new BsonRegularExpression(cleanedValue, "i")),
                Builders<Book>.Filter.Regex("ISBN", new BsonRegularExpression(cleanedValue, "i"))
            };

            return _dbAccess._bookRepository.GetFiltered(Builders<Book>.Filter.Or(queries)).GetAwaiter().GetResult();                              
        }

        public async Task<bool> DeleteBook(string id)
        {
            var copies = await _dbAccess._copyRepository.GetFiltered(Builders<Copy>.Filter.Eq("BookId", id), new string[]{ "_id"});
            var book = _dbAccess._bookRepository.GetById(id, new string[] { "_id"});
            if (book == null) throw new ValidationException("Livro não encontrado!");

            if (copies?.Any() == true) throw new ValidationException("Não é possível excluir esse livro, porque há muitos exemplares vinculados a ele.");

            return await _dbAccess._bookRepository.DeleteOne(id);
        }

        public async Task<Book> CreateBook(Book book)
        {
            this.ValidateBook(book);
            await _dbAccess._bookRepository.InsertOne(book);
            return _dbAccess._bookRepository.GetById(book.Id);
        }

        public async Task<bool> UpdateBook(Book book)
        {
            this.ValidateBook(book);
            return await _dbAccess._bookRepository.Update(book);
        }
        private void ValidateBook(Book book)
        {
            if (book == null) throw new ValidationException("Verifique se os dados foram passados corretamente.");
            if (string.IsNullOrEmpty(book.ISBN)) throw new ValidationException("O campo ISBN é obrigatório.");
            if (string.IsNullOrEmpty(book.Author)) throw new ValidationException("O nome do autor é obrigatório.");
            if (string.IsNullOrEmpty(book.Title)) throw new ValidationException("O campo Título é obrigatório.");
            if (string.IsNullOrEmpty(book.Id) && this.GetBooks(book.ISBN)?.Any() == true) throw new ValidationException("Já existe um livro cadastrado com esse ISBN.");
        }



        public async Task<Reservation> MakeReservation(string id)
        {
            var newReservation = new Reservation
            {
                BookId = id,
                Status = ReservationStatus.Active,
                Date = DateTime.Now,
            };

            await _dbAccess._reservationRepository.InsertOne(newReservation);
            return newReservation;
        }

 
    }
}
