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


            #region DeleteRecord
            //var Record = Db.LoadRecordById<PersonModel>("user", new Guid("6a61aa54-fa91-4406-8ec0-a6a042ab8fd2"));

            //Db.DeleteRecord<PersonModel>("user", Record.Id);

            #endregion


            Console.ReadKey();
        }
    }










}