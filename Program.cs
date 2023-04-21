using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MongoDBWithCsharpApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MongoCrud Db = new MongoCrud("AddressBook");


            #region InsertRecord

            // Insert FirstRecord
            //Db.InsertRecord<PersonModel>("user",
            //    new PersonModel { Id = Guid.NewGuid(), FirstName = "khaled", lastName = "ibrahim" });

            //// Second Record 
            //Db.InsertRecord("user", new PersonModel
            //{
            //    FirstName = "omar",
            //    lastName = "ahmed",
            //    AddressModel = new AddressModel
            //    {
            //        Street = "10 st",
            //        City = "ibrahimia",
            //        State = "zag",
            //        ZipCode = "100125"
            //    }
            //});

            #endregion


            #region BulkInsert
            //List<PersonModel> personModels = new List<PersonModel>()
            //{
            //    new PersonModel{FirstName="ahmed",lastName="mohamed",AddressModel=new AddressModel{
            //    Street="11 st,",
            //    City="xcv",
            //    State="xcvv",
            //    ZipCode="1545"
            //    }},
            //    new PersonModel{FirstName="ali",lastName="ali",AddressModel=new AddressModel{
            //    Street="11 st,",
            //    City="xcv",
            //    State="xcvv",
            //    ZipCode="1545"
            //    }},
            //    new PersonModel{FirstName="zlatan",lastName="ibrahmovitch",AddressModel=new AddressModel{
            //    Street="11 st,",
            //    City="malmo",
            //    State="swidthc",
            //    ZipCode="1545"
            //    }},
            //    new PersonModel{FirstName="cristiano",lastName="ronaldo",AddressModel=new AddressModel{
            //    Street="14 st,",
            //    City="riad",
            //    State="gadah",
            //    ZipCode="6582"
            //    }},
            //};

            //Db.BulkInsert("user",personModels); 
            #endregion


            #region Retrieve Data

            //// GetAllRecords Method

            //var userList = Db.GetAllRecords<PersonModel>("user");
            //foreach (var record in userList)
            //{
            //    Console.WriteLine($"{record.Id},{record.FirstName},{record.lastName}");

            //    if (record.AddressModel != null)
            //    {
            //        Console.WriteLine($"{record.AddressModel.ToString()}");
            //    }
            //    Console.WriteLine("----------------");
            //}



            //// RecordsWithProjectionColumns

            //var users = Db.RecordsWithProjectionColumns<PersonModel>("user");
            //foreach (var user in users)
            //{
            //    Console.WriteLine($"{user.FirstName},{user.lastName}");
            //    if (user.AddressModel != null)
            //    {
            //        Console.WriteLine($"{user.AddressModel.ToString()}");
            //    }
            //    else
            //    {
            //        Console.WriteLine("null");
            //    }
            //    Console.WriteLine("----------------");
            //}




            // LoadOneRecordMethod

            //   var Record = Db.LoadRecordById<PersonModel>("user", new Guid("6a61aa54-fa91-4406-8ec0-a6a042ab8fd2"));










            #endregion


            #region UpsertRecordFunction
            //// Update Or Insert UpsertRecordFunction

            //// ExistRecord
            //var Record = Db.LoadRecordById<PersonModel>("user", new Guid("6a61aa54-fa91-4406-8ec0-a6a042ab8fd2"));

            //Record.DateOfBirth = new DateTime(1996, 11, 23, 0, 0, 0, DateTimeKind.Utc);
            //Record.AddressModel = new AddressModel { Street = "1550 street", City = "Gotham", State = "NY", ZipCode = "546945" };

            //Db.UpsertRecord<PersonModel>("user", Record.Id, Record);

            #endregion


            var Record = Db.LoadRecordById<PersonModel>("user", new Guid("6a61aa54-fa91-4406-8ec0-a6a042ab8fd2"));

            Db.DeleteRecord<PersonModel>("user", Record.Id);



            Console.ReadKey();
        }
    }

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

    public class AddressModel
    {
        public string  Street { get; set; }
        public string City { get; set; }

        public string  State { get; set; }

        public string ZipCode { get; set; }

        public override string ToString()
        {
            return $" street:{Street} city: {City} state :{State} zipcode :{ZipCode} ";
        }

    }




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