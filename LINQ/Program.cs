using System.Collections;
namespace LINQ
{ 
    public delegate void MyDelegate();

    class A
    {
        public event MyDelegate Event;

        public void show()
        {
            Console.WriteLine("Show Method");
        }
    }

   class Program
    {

        public static event MyDelegate Event;

        public static void show()
        {
            Console.WriteLine("Show Method");
        }
        public static void Dshow()
        {
            Console.WriteLine("Dont Show Method");
        }
        
        static void Main(string[] args)
        {
           Console.WriteLine("Hello....");

          Event += new MyDelegate(show); 

          Event += delegate ()
          {
              Console.WriteLine("Anomynous Method");
          };

          Event += () =>
          {
              Console.WriteLine("Lambda Express");
          };

            Event();
        }
    }
}
