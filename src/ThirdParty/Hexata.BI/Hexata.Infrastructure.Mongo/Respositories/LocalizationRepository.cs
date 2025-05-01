using Hexata.BI.Application.Dtos;
using Hexata.BI.Application.Entities;
using Hexata.BI.Application.Extensions;
using Hexata.BI.Application.Repositories;
using Hexata.BI.Application.Services.Localizations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Hexata.Infrastructure.Mongo.Respositories
{
    internal class LocalizationRepository(IOptions<MongoDbSettings> optionSettings) : Repository<Localization>(optionSettings, "Localizations"), ILocalizationRepository
    {
        public async Task CleanDataAsync()
        {
            var name = "State";
            var filterEmpty = Builders<Localization>.Filter.Eq(name, null as object);
            var filterBsonNull = Builders<Localization>.Filter.Eq(name, BsonNull.Value);
            var filter = Builders<Localization>.Filter.Or(filterEmpty, filterBsonNull);

            var update = Builders<Localization>.Update.Set(name, "PR");

            var result = await _collection.UpdateManyAsync(filter, update);
            Console.WriteLine($"Matched: {result.MatchedCount}, Modified: {result.ModifiedCount}");
        }

        public Result<LocalizationProviderDto, string>? GetAddress(AddressDto addressDto)
        {
            var street = addressDto.Street?.RemoveAccents().RemoveCorrupted();
            var neighborhood = addressDto.Neighborhood?.RemoveAccents().RemoveCorrupted();
            var city = addressDto.City?.RemoveAccents().RemoveCorrupted();
            var state = addressDto.State?.RemoveAccents().RemoveCorrupted();
            var country = addressDto.Country?.RemoveAccents().RemoveCorrupted();
            var postal = addressDto.PostalCode?.RemoveAccents().RemoveCorrupted();

            var filter = Builders<Localization>.Filter.And(
                Builders<Localization>.Filter.Eq(e => e.Street, street),
                Builders<Localization>.Filter.Eq(e => e.Number, addressDto.Number),
                Builders<Localization>.Filter.Eq(e => e.Neighborhood, neighborhood),
                Builders<Localization>.Filter.Eq(e => e.City, city),
                Builders<Localization>.Filter.Eq(e => e.State, state),
                Builders<Localization>.Filter.Eq(e => e.Country, country),
                Builders<Localization>.Filter.Eq(e => e.PostalCode, postal)
            );


            var collation = new Collation("en", strength: CollationStrength.Primary);

            var result = _collection
                .Find(filter, new FindOptions()
                {
                    Collation = collation,
                })
                .FirstOrDefault();

            return result?.Result;
        }

        public void SaveAddress(AddressDto addressDto, Result<LocalizationProviderDto, string> address)
        {
            var entity = new Localization(addressDto, address);
            entity.Street = entity.Street?.RemoveAccents().RemoveCorrupted();
            entity.Neighborhood = entity.Neighborhood?.RemoveAccents().RemoveCorrupted();
            entity.City = entity.City?.RemoveAccents().RemoveCorrupted();
            entity.State = entity.State?.RemoveAccents().RemoveCorrupted();
            entity.Country = entity.Country.RemoveAccents().RemoveCorrupted();
            entity.PostalCode = entity.PostalCode?.RemoveAccents().RemoveCorrupted();


            _collection.InsertOne(entity);
        }
    }
}
