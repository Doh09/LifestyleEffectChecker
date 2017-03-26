using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifestyleEffectChecker.Repository
{
    public interface IRepository<T>
    {
        Task<T> Create(T obj);

        Task<T> Read(int id);

        Task<IEnumerable<T>> ReadAll();

        Task<T> Update(T obj);

        Task<bool> Delete(int id);
    }

}
