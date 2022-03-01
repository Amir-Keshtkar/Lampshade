using System.Linq.Expressions;

namespace _0_Framework.Domain {
    public interface IRepository<in TKey, T> where T : class {
        void Create (T entity);
        bool Exists (Expression<Func<T, bool>> expression);
        void SaveChanges ();
        T GetById (TKey id);
        List<T> GetAll ();
    }
}
