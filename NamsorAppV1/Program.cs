using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NamsorAppV1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Validate parameters.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: NamsorAppV1 input_file_path output_file_path");
                return;
            }
            string inputFilePath = args[0];
            string outputFilePath = args[1];

            // Read Input file.
            IList<User> users = new List<User>();
            using (var reader = new StreamReader(inputFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    users.Add(new User(line));
                }
            }

            // Update gender.
            foreach (User user in users)
            {
                RetrieveGenderFromNamsorApi(user);
            }

            // Write output file.
            using (var w = new StreamWriter(outputFilePath))
            {
                foreach (User user in users)
                {
                    w.WriteLine(user);
                }
            }

            Console.ReadLine();
        }

        private static void RetrieveGenderFromNamsorApi(User user)
        {
            string url = String.Format("http://api.namsor.com/onomastics/api/json/gender/{0}/{1}", user.FirstName, user.LastName);
            string responseBody = HttpGet(url);
            Dictionary<string, dynamic> values = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseBody);
            string keyForGender = "gender";
            if (values.ContainsKey(keyForGender))
            {
                user.Gender = values[keyForGender];
            }
        }

        private static string HttpGet(string url)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }
    }
}
