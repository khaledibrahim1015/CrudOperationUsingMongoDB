namespace MongoDBWithCsharpApp
{
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










}