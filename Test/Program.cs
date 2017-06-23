using AdamOneilSoftware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
			/*var doc = SampleDoc.Load<SampleDoc>(@"C:\Users\Adam\Desktop\hello.xml");
            Console.WriteLine(doc.FirstName);
            Console.WriteLine(doc.LastName);
            Console.WriteLine(doc.BirthDate);
            Console.ReadLine();*/

			/*var doc = new SampleDoc();
            doc.PropertyChanged += Doc_PropertyChanged;
            doc.FirstName = "whatever";
            doc.LastName = "this thing";
            doc.BirthDate = DateTime.Today;*/

			var settings = UserSettings.Load<Settings>("Adam O'Neil Software", "XmlSerializerHelper");
			//settings.Greeting = "hello";
			//settings.Farewell = "good-bye";

			Console.Write($"greeting = {settings.Greeting}, farewell = {settings.Farewell}");

            Console.ReadLine();
        }

        private static void Doc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine($"{e.PropertyName} changed to");
        }
    }
}
