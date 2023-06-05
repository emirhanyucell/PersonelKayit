using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personel_Kayit
{
    public class PersonelDto
    {
        public int Id { get; set; }
        public string İsim { get; set; }
        public string Soyisim { get; set; }
        public string Şehir { get; set; }
        public int Maaş { get; set; }
        public string Meslek { get; set; }
        public bool MedeniHal { get; set; }
    }
}
