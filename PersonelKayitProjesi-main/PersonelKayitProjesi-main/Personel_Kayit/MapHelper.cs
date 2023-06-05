using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personel_Kayit
{
    public class MapHelper
    {

        public List<PersonelDto> MapPersonelDto(List<Personel> personels,List<City> cities)
        {
            var result = new List<PersonelDto>();

            foreach (var item in personels)
            {
                var personel = new PersonelDto
                {
                    Id = item.Id,
                    Maaş = item.Salary,
                    MedeniHal = item.MartialStatus,
                    Meslek = item.Job,
                    Soyisim = item.LastName,
                    Şehir = (from s in cities where s.Id == item.CityId select s.name).FirstOrDefault().ToString(),
                    İsim = item.Name

                };
                result.Add(personel);

                
            }

            return result;


        }



        public Personel MapPersonel(PersonelDto personeldto, List<City> cities)
        {
            var result = new Personel()
            {
                Id = personeldto.Id,
                Name=personeldto.İsim,
                LastName= personeldto.Soyisim,
                CityId= (from s in cities where s.name == personeldto.Şehir select s.Id).FirstOrDefault(),
                Job=personeldto.Meslek,
                MartialStatus= personeldto.MedeniHal,
                Salary= personeldto.Maaş
            };

            return result;

        }


    }
}
