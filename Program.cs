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

            List<PersonModel> personModels = new List<PersonModel>()
            {
                new PersonModel{FirstName="ahmed",lastName="mohamed",AddressModel=new AddressModel{
                Street="11 st,",
                City="xcv",
                State="xcvv",
                ZipCode="1545"
                }},
                new PersonModel{FirstName="ali",lastName="ali",AddressModel=new AddressModel{
                Street="11 st,",
                City="xcv",
                State="xcvv",
                ZipCode="1545"
                }},
                new PersonModel{FirstName="zlatan",lastName="ibrahmovitch",AddressModel=new AddressModel{
                Street="11 st,",
                City="malmo",
                State="swidthc",
                ZipCode="1545"
                }},
                new PersonModel{FirstName="cristiano",lastName="ronaldo",AddressModel=new AddressModel{
                Street="14 st,",
                City="riad",
                State="gadah",
                ZipCode="6582"
                }},
            };

            Db.BulkInsert("user",personModels);


            Console.ReadLine();
        }
    }

    public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string  lastName { get; set; }

       public AddressModel AddressModel { set; get; }

    }

    public class AddressModel
    {
        public string  Street { get; set; }
        public string City { get; set; }

        public string  State { get; set; }

        public string ZipCode { get; set; }

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



    }










}