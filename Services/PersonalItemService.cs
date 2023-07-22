using System;
using MongoDB.Driver;
using System.Runtime.ConstrainedExecution;
using Microsoft.Extensions.Options;
using PractivaFinalMauricioRamirez.Models;

namespace PractivaFinalMauricioRamirez.Services
{
    public class PersonalItemService
	{
        private readonly IMongoCollection<PersonalItemModel> _carCollection;

        public PersonalItemService(IOptions<MongoConnection> mongoConnection)
        {
            var mongoClient = new MongoClient(mongoConnection.Value.Connection);
            var mongoDatabase = mongoClient.GetDatabase(mongoConnection.Value.DatabaseName);
            this._carCollection = mongoDatabase.GetCollection<PersonalItemModel>(mongoConnection.Value.CollectionName);
        }

        public async Task<List<PersonalItemModel>> Get()
        {
            return await this._carCollection.Find(_ => true).ToListAsync();
        }

        public async Task<PersonalItemModel?> GetById(string id)
        {
            return await this._carCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(PersonalItemModel carModel)
        {
            await this._carCollection.InsertOneAsync(carModel);
        }

        public async Task Patch(string id, PersonalItemModel updateCarModel)
        {
            await this._carCollection.ReplaceOneAsync(x => x.Id == id, updateCarModel);
        }

        public async Task DeleteById(string id)
        {
            await this._carCollection.DeleteOneAsync(x => x.Id == id);
        }

    }
}

