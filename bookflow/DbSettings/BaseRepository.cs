using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository;
using System.Reflection;
using bookflow.Models;

namespace bookflow.DbSettings
{

    public class BaseRepository<T> 
    {
        public IMongoCollection<T> collection { get; set; }

        public BaseRepository(IMongoDatabase database)
        {
            var collectionName = typeof(T).Name;
            collection = database.GetCollection<T>(collectionName);
        }

        #region GET

        public  T GetById(string id, string[] fields = null)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));

            var projectionBuilder = Builders<T>.Projection;
            ProjectionDefinition<T> projection = null;

            if (fields != null && fields.Length > 0)
            {
                projection = projectionBuilder.Include(fields[0]);
                for (int i = 1; i < fields.Length; i++)
                    projection = projectionBuilder.Combine(projection, projectionBuilder.Include(fields[i]));
            }

            return projection != null ?
                collection.Find(filter).Project<T>(projection).FirstOrDefault() :
                collection.Find(filter).FirstOrDefault();
        }

        public  async Task<List<T>> GetFiltered(FilterDefinition<T> filter, string[] fields = null)
        {
            var projectionBuilder = Builders<T>.Projection;
            ProjectionDefinition<T> projection = null;

            if (fields != null && fields.Length > 0)
            {
                projection = projectionBuilder.Include(fields[0]);
                for (int i = 1; i < fields.Length; i++)
                    projection = projectionBuilder.Combine(projection, projectionBuilder.Include(fields[i]));
            }

            return projection != null ?
                await collection.Find(filter).Project<T>(projection).ToListAsync() :
                await collection.Find(filter).ToListAsync();
        }

        #endregion

        #region CREATE
        public  async Task<bool> InsertOne(T data)
        {
            try
            {

                await collection.InsertOneAsync(data);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DELETE
        public  async Task<bool> DeleteOne(string id)
        {
            try
            {
                await collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public  async Task<bool> DeleteMany(FilterDefinition<T> filter)
        {
            try
            {

                await collection.DeleteManyAsync(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region UPDATE
        public  async Task<bool> Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            try
            {
                await collection.UpdateManyAsync(filter, update);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public  async Task<bool> Update(BaseModel data)
        {
            try
            {
                var updateDefinitions = new List<UpdateDefinition<T>>();
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var property in properties)
                {
                    var value = property.GetValue(data);
                    if (value != null && property.Name != nameof(BaseModel.Id))
                    {
                        var fieldName = property.Name;
                        var update = Builders<T>.Update.Set(fieldName, value);
                        updateDefinitions.Add(update);
                    }
                }
                await collection.UpdateOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(data.Id)), Builders<T>.Update.Combine(updateDefinitions));
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}
