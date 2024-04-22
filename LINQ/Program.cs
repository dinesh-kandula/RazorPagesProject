using System.Collections;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;

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
        
        static void Main2(string[] args)
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

            Dictionary<string, List<string>> constant = new Dictionary<string, List<string>>();


            constant["deletePerson"] = ["ADMIN", "SHIELD"];
            constant.Add("addPerson", ["ADMIN"]);

        }


        static void Main3(string[] args)
        {
            int[] arr = new int[4] { 1, 2, 3, 4 };

            var qr = from x in arr where x>2 select x;

            foreach (int i in qr) {
                Console.WriteLine(i);
            }

            string[] names = { "Adam", "Bridgette", "Carla", "Daniel",
                   "Ebenezer", "Francine", "George" };
            decimal[] hours = { 40, 6.667m, 40.39m, 82,
                    40.333m, 80, 16.75m };

            Console.WriteLine("{0,-20} {1,30}\n", "Name", "Hours");

            for (int counter = 0; counter < names.Length; counter++)
                Console.WriteLine("{0,-10} {1,30}", names[counter], hours[counter]);
        }

        static void Main(string[] args)
        {
            string urlTest = string.Format("https://jsonplaceholder.typicode.com/posts");
            WebRequest request = WebRequest.Create(urlTest);
            request.Method = "GET";
            HttpWebResponse response = null;
            response = (HttpWebResponse)request.GetResponse();

            string result = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }

            //Console.WriteLine(result);

            if (result != null)
            {
                List<DinnuObject> posts = JsonConvert.DeserializeObject<List<DinnuObject>>(result)!;

                foreach (var post in posts)
                {
                    Console.WriteLine($"Id: {post.Id}");
                }
            }
        }
    }

    public class DinnuObject
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
    }
}
