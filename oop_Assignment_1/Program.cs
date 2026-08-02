using System.Security.Cryptography;
using System.Threading.Channels;

namespace oop_Assignment_1
{
    struct DeliveryAddress
    {
        public string City;
        public string Street;
        public int BuildingNumber;

        public DeliveryAddress(string city, string street, int buldingNumber)
        {
            City = city;
            Street = street;
            BuildingNumber = buldingNumber;
        }
        public string GetFullAddress()
        {
            return $"city: {City}, street: {Street}, building: {BuildingNumber}";
        }
    }
    public class Customer
    {
        public string Name;
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            #region Question1
            //a) What happens when a DeliveryAddress variable is copied into another variable and the copy is modified?

            //DeliveryAddress Add1 = new DeliveryAddress();
            //Add1.City = "Cairo";
            //Add1.Street = "Tahrir";

            //DeliveryAddress Add2 = Add1;
            //Add2.City = "Alex";

            //solution: a new copy of data is created also modifying the copy does not affect the original var



            //b) What happens when a Customer variable is copied into another variable and one variable modifies the object
            //Customer c1 = new Customer();
            //c1.Name = "Ahmed";

            //Customer c2 = c1;
            //c2.Name = "Abdo";

            //Console.WriteLine(c1.Name); // Abdo
            //Console.WriteLine(c2.Name); // Abdo

            // Answer: both variables reference the same object
            // modifying variable affects the other
            #endregion

            #region Question2
            //a) Identify at least three problems with this design from an encapsulation perspective

            //solution:fields are public so they can be modified directly
            // there isn't validation for the fields
            //Invalid data can be assigned such as negative values

            //b) How can private fields and public properties improve this design?

            //sol: private fields protect data from direct access
            // public properties allow validation
            // make properties to keep objects in valid state
            #endregion

            #region DeliveryAddress struct
            //Create one DeliveryAddress value, copy it into a second variable, modify the copy, and print both values to prove that the original did not change

            DeliveryAddress Dv1 = new DeliveryAddress("cairo" , "tahrir", 1);
            DeliveryAddress Dv2 = Dv1;
            Dv2.City = "Alex";
            Dv2.Street = "Sea Road";
            Dv2.BuildingNumber = 20;

            Console.WriteLine("Address 1:");
            Console.WriteLine(Dv1.GetFullAddress());
            Console.WriteLine("Address 2:");
            Console.WriteLine(Dv2.GetFullAddress());
            #endregion
        }
    }
}
