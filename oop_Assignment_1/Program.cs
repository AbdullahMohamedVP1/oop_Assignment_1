namespace oop_Assignment_1
{
    struct DeliveryAddress
    {
        public string City;
        public string Street;
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
        }
    }
}
