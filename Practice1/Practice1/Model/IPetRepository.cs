using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Practice1.Model
{
    public interface IPetRepository
    {
        void Add(PetModel petModel);
        void Edit(PetModel petModel);
        void Delete(int id);

        IEnumerable<PetModel> GetAll();
        IEnumerable<PetModel> GetByValue(string value); //Search
    }
}
