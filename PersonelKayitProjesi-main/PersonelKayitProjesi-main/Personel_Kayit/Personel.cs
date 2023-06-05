
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personel_Kayit
{
    public  class Personel
    {
      

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int CityId { get; set; }
        public int Salary { get; set; }
        public string Job { get; set; }
        public bool MartialStatus { get; set; }

    }

   
}
