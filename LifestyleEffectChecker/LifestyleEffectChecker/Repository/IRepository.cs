using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleEffectChecker.Repository
{
    interface IRepository<T>
    {
        T Create(T obj);

        T Read(int id);

        IEnumerable<T> ReadAll();

        T Update(T obj);

        bool Delete(int id);
    }

}
