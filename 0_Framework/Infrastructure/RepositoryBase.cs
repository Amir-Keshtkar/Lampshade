using System.Linq.Expressions;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Infrastructure {
    public class RepositoryBase<TKey, T>: IRepository<TKey, T> where T : class {
        private readonly DbContext _Context;

        public RepositoryBase(DbContext context) {
            _Context = context;
        }

        public void Create (T entity) {
            _Context.Add<T>(entity);
        }

        public bool Exists (Expression<Func<T, bool>> expression) {
            return _Context.Set<T>().Any(expression);
        }

        public List<T> GetAll() {
            return _Context.Set<T>().ToList();
        }

        public T GetById(TKey id) {
            return _Context.Find<T>(id);
        }

        public void SaveChanges () {
            _Context.SaveChanges();
        }
    }
}
