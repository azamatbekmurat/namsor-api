using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamsorAppV1
{
    class User
    {
        public User(string fullName)
        {
            string[] parts = fullName.Split(',');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Unexpected entry: {0}", fullName);
            }
            FirstName = parts[0].Trim();
            LastName = parts[1].Trim();
        }

        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Gender { set; get; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", FirstName, LastName, Gender);
        }

    }
}
