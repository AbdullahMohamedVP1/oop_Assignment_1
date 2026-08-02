using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Threading.Channels;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    struct Shipment
    {
        private string trackingCode;
        private string description;
        private double weight;
        private decimal deliveryFee;

        public DeliveryAddress Destination { get; set; }

        //readonly from outside struct
        public string TrackingCode
        {
            get { return trackingCode; }
            private set
            {
                if(!string.IsNullOrWhiteSpace(value))
                {
                    trackingCode = value;
                }
            }
        }

        //read and write with validation  زي ما هو
        public string Description
        {
            get { return description; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    description = value;
            }
        }

        //نفس الكلام زي ما هو
        public double Weight
        {
            get { return weight; }
            set
            {
                if (value > 0)
                    weight = value;
            }
        }

        //public get and private set
        public decimal DeliveryFee
        {
            get { return deliveryFee; }
            private set
            {
                if (value > 0) { deliveryFee = value; }
            }
        }

        public decimal EstimatedCost
        {
            get { return DeliveryFee + ((decimal)Weight * 5); }    // هعمل كاستنج هحول من دبل الي ديسيمل
        }

        //اول كونستراكتور يستقبل trackingcode فقط
        public Shipment(string trackingCode)
        {
            this.trackingCode = trackingCode;
            description = "Unknown";
            weight = 1;
            deliveryFee = 50;

            Destination = new DeliveryAddress("Cairo", "Unknown", 0);
        }

        //Constructor ثاني يستقبل كل القيم
        public Shipment(string trackingCode, string description, double weight, decimal deliveryFee, DeliveryAddress destination)
        {
            this.trackingCode = trackingCode;
            this.description = description;
            this.weight = weight;
            this.deliveryFee = deliveryFee;
            Destination = destination;
        }

        // ميثود مطلوبه فالسؤال السادس
        //updates the fee only when newFee is greater than 0
        public void UpdateDeliveryFee(decimal newFee)
        {
            if (newFee > 0)
            {
                DeliveryFee = newFee;
            }
        }

        // ثاني ميثود مطلوبه تطبع كل البيانات 
        public void PrintShipment()
        {
            Console.WriteLine($"Tracking Code: {TrackingCode}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Weight: {Weight}");
            Console.WriteLine($"Delivery Fee: {DeliveryFee}");
            Console.WriteLine($"Destination: {Destination.GetFullAddress()}");
            Console.WriteLine($"Estimated Cost: {EstimatedCost}");
        }
    }

    struct DeliveryCenter
    {
        private Shipment[] shipments;

        public DeliveryCenter()
        {
            shipments = new Shipment[10];
        }


        // اندكسر نوعوا int
        public Shipment this[int index]
        {
            get
            {
                if (index >= 0 && index < shipments.Length) // لو رقم المستخدم صحيح وداخل جدود الاراي
                    return shipments[index];

                return default;
            }

            set
            {
                if (index >= 0 && index < shipments.Length)
                    shipments[index] = value;
            }
        }


        // String
        public Shipment this[string trackingCode]
        {
            get
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    if (shipments[i].TrackingCode == trackingCode)
                    {
                        return shipments[i];
                    }
                }

                return default;
            }
        }


        public bool AddShipment(Shipment shipment)
        {
            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i].TrackingCode == null)
                {
                    shipments[i] = shipment;
                    return true;
                }
            }

            return false;
        }
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

            //DeliveryAddress Dv1 = new DeliveryAddress("cairo" , "tahrir", 1);
            //DeliveryAddress Dv2 = Dv1;
            //Dv2.City = "Alex";
            //Dv2.Street = "Sea Road";
            //Dv2.BuildingNumber = 20;

            //Console.WriteLine("Address 1:");
            //Console.WriteLine(Dv1.GetFullAddress());
            //Console.WriteLine("Address 2:");
            //Console.WriteLine(Dv2.GetFullAddress());
            #endregion

            #region Question4
            //Apply proper encapsulation using public properties with the following validation rules:
            //TrackingCode cannot be null, empty, or whitespace.
            //Description cannot be null, empty, or whitespace.
            //Weight must be greater than 0.
            //DeliveryFee must be greater than 0.
            //If an invalid value is assigned, keep the previous valid value.
            #endregion

            #region Question5
            //Add the following properties:
            //TrackingCode: read - only from outside the struct.
            //Description: read/write property with validation.
            //Weight: read/write property with validation.
            //DeliveryFee: public getter and private setter.
            //Destination: public read/write property.
            //EstimatedCost: a calculated property that returns: DeliveryFee + (Weight × 5)
            //The EstimatedCost value must be calculated when requested and must not be stored in a separate field
            //طبعا الحل فوق في الاستركت زي السؤال اللي فات
            #endregion

            #region Question6
            //2. Add constructor overloading to Shipment
            //3. Add the following methods to Shipment:
            // UpdateDeliveryFee(decimal newFee): updates the fee only when newFee is greater than 0.
            // PrintShipment(): prints all shipment information, including the estimated cost.
            #endregion

            #region Question 7 : DeliveryCenter struct
            //السؤال 
            //5.Create a DeliveryCenter struct
            //Note : For this assignment, implement DeliveryCenter as a struct. In the next assignment, after
            //learning Classes and Inheritance, you will refactor it into a class.
            //The DeliveryCenter struct should store up to 10 shipments using a private Shipment[] array.
            //Add an integer indexer:
            //Returns the shipment at the given position.
            //Allows replacing a shipment.
            //If the index is invalid, the getter returns default.
            //If the index is invalid while setting, do nothing.
            //Add a string indexer:
            //Returns the first shipment with the matching tracking code.
            //Returns default if no matching shipment is found.
            //Add Method named AddShipment:
            //Adds the shipment to the first available position.
            //Returns true if the shipment was added successfully.
            //Returns false if the delivery center is full
            #endregion
        }
    }
}
