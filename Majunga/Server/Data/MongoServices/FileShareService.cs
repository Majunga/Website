using Majunga.Shared.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Majunga.Server.Data.MongoServices
{
    public class FileShareService
    {
        private readonly IMongoCollection<File> _entities;

        public FileShareService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _entities = database.GetCollection<File>("FileShare");
        }

        public List<File> Get() =>
            _entities.Find(e => true).ToList();

        public File Get(string id) =>
            _entities.Find(e => e.Id == id).FirstOrDefault();

        public File Create(File file)
        {
            _entities.InsertOne(file);
            return file;
        }

        public void Update(string id, File file) =>
            _entities.ReplaceOne(e => e.Id == id, file);

        public void Remove(File bookIn) =>
            _entities.DeleteOne(e => e.Id == bookIn.Id);

        public void Remove(string id) =>
            _entities.DeleteOne(book => book.Id == id);
    }
}
