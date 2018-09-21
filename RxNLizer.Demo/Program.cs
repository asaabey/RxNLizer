using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxNLizer.Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            

            IEnumerable<string> rxTexts=  GetRxTextList().Take(10);

            foreach (var rxText in rxTexts)
            {
                RxNLizer rxNLizer = new RxNLizer();

                string rxClassObject = rxNLizer.GetRxClasses(rxText);

                Console.WriteLine("RxClasses-> {0}", arg0: rxClassObject);

                UpdateRxclassLookuptable(rxText, JsonConvert.DeserializeObject<RxClassReturnObject>(rxClassObject));

                rxNLizer=null; 


            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static void UpdateRxclassLookuptable(string rx_text, RxClassReturnObject rx_class)
        {
            
            string rx_class_json = JsonConvert.SerializeObject(rx_class);

            DbWrapper.UpdateRxClass(rx_text, rx_class_json);

        }

        private static IEnumerable<string> GetRxTextList()
        {
            
            return DbWrapper.GetRxTexts();

        }


    }
}
