using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBWithCsharpApp
{
    public class MongoCrud
    {

        private IMongoDatabase db;

        const string ConnectionUrl="mongodb://localhost:27017";

        //public MongoCrud( string DatabaseName)
        //{
        //    var client = new MongoClient();
        //    db = client.GetDatabase(DatabaseName);

        //}
        public MongoCrud(string DatabaseName)
        {
            var client = new MongoClient(ConnectionUrl);
            db = client.GetDatabase(DatabaseName);


        }

        public void InsertRecord<T>(string collection,T Record)
        {
            db.GetCollection<T>(collection).InsertOne(Record);


        }

        public void BulkInsert<T>(string collection, List<T> Records)
        {
            db.GetCollection<T>(collection).InsertMany(Records);
        }



        public List<T> GetAllRecords<T>(string collection)
        {
            var collectionToRetrieve = db.GetCollection<T>(collection);
            if (collectionToRetrieve != null)
            {
                // you want to retrieve all documents in a MongoDB collection without any filters, you can simply pass an empty filter object to the Find() method
                // an empty BsonDocument object is created and passed as the filter parameter to the Find() method. Since the filter is empty, the Find() method retrieves all documents in the collection.


                var filter = new BsonDocument();
                return collectionToRetrieve.Find(filter).ToList();
            }
            else
            {
                Console.WriteLine("Collection does not exist");
                return null;
            }

        }

        public List<T> RecordsWithProjectionColumns<T>(string collection)
        {
            var collectionToRetrieve = db.GetCollection<T>(collection);

            // Empty Filter
            var filter = new BsonDocument();

            // select a specific fields (Projection)

            var projection = Builders<T>.Projection.Include("FirstName").Include("lastName").Exclude("_id");

            if(collectionToRetrieve !=null)
            {

               return collectionToRetrieve.Find(filter).Project<T>(projection).ToList();
            }
            else
            {
                Console.WriteLine("Collection does not exist");
                return null;
            }




        }



        public T LoadRecordById<T>(string collection , Guid id)
        {


            var collectionToRetrieve = db.GetCollection<T>(collection);

            // Filter By Id
            var filter = Builders<T>.Filter.Eq("Id", id);

           return  collectionToRetrieve.Find(filter).FirstOrDefault();
        }


        public void UpsertRecord<T>(string collection,Guid id, T Record)
        {
            var collectionToRetrieve = db.GetCollection<T>(collection);

            var result = collectionToRetrieve.ReplaceOne(new BsonDocument("_id", id)
                                                                                  , Record
                                                                                  , new UpdateOptions() { IsUpsert = true }

                );


        }

        public void DeleteRecord<T>(string collection, Guid id)
        {
            var collectionToRetrieve = db.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq("Id", id);

            collectionToRetrieve.DeleteOne(filter);

        }



    }










}