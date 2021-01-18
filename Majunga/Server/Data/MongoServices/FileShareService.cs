using Majunga.Shared.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Majunga.Server.Data.MongoServices
{
    public class FileShareService
    {
        private readonly IMongoCollection<File> _entities;
        private readonly GridFSBucket _gridFs;

        public FileShareService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _entities = database.GetCollection<File>("FileShare");
            _gridFs = new GridFSBucket(database);
            
        }

        public async Task<List<File>> Get() =>
            (await _entities.FindAsync(e => true)).ToList();

        public async Task<File> Get(string id) =>
            (await _entities.FindAsync(e => e.Id == id)).FirstOrDefault();

        public async Task<File> GetFile(File file) {
            file.FileBytes = (await _gridFs.DownloadAsBytesAsync(new ObjectId(file.FileId)));

            return file;
        }

        public async Task<File> Create(File file)
        {
            file.FileId = (await _gridFs.UploadFromBytesAsync(file.Name, file.FileBytes)).ToString();
            
            await _entities.InsertOneAsync(file);

            return file;
        }

        public async Task Update(string id, File file) =>
            await _entities.ReplaceOneAsync(e => e.Id == id, file);

        public async Task Remove(string id) {
            var file = (await _entities.FindAsync(e => e.Id == id)).FirstOrDefault();

            await _gridFs.DeleteAsync(new ObjectId(file.FileId));
            await _entities.DeleteOneAsync(file => file.Id == id);
        }
    }
}
