using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkiPass.User.DataAccess.MongoRepository
{
    public class MongoRepository<T> : IDisposable, IRepository<T> where T : class
    {
        #region Properties

        private IMongoDatabase _database;
        private IMongoCollection<T> _collection;
        private MongoClient _client;

        #endregion

        #region Ctor

        public MongoRepository()
        {
            GetDatabase();
            GetCollection();
        }

        #endregion

        #region Methods
        public void GetDatabase()
        {
            _client = new MongoClient("mongodb://root:root@127.0.0.1:18001");
            _database = _client.GetDatabase("skipassuser");
        }

        public void GetCollection()
        {
            if (_database.GetCollection<T>(typeof(T).Name) == null)
                _database.CreateCollection(typeof(T).Name);
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }

        public void Add(T entity)
        {
            this._collection.InsertOne(entity);
        }
        public void Update(T entity)
        {
            this._collection.ReplaceOne(null, entity);
        }
        public void Update(Expression<Func<T, bool>> expression, T entity)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(expression);
            this._collection.ReplaceOne(filter, entity);
        }
        public void Delete(Expression<Func<T, bool>> expression, bool forceDelete = false)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(expression);
            this._collection.DeleteOne(filter);
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> queryable = this._collection.AsQueryable();
            return queryable;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(expression);
            return this._collection.Find(filter).ToEnumerable().AsQueryable();
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(expression);
            return _collection.Find(filter).FirstOrDefault();
        }
        public int Count()
        {
            return (int)_collection.CountDocuments(null);
        }
        public int Count(Expression<Func<T, bool>> predicate)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(predicate);

            return (int)_collection.Find(filter).CountDocuments();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
