using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBWithCsharpApp
{
    public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string  lastName { get; set; }

       public AddressModel AddressModel { set; get; }

        [BsonElement(elementName:"DOB")]
        public DateTime DateOfBirth { get; set; }

    }










}