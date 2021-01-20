using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Passageiro.Shared.Entities;

namespace Passageiro.Repository.Interfaces {
    public interface IRepositoryBase<T> where T : Entity {
        bool Create (T entity);
        bool Create (IList<T> entity);
        bool Update (int ID, T entity);
        bool Update (IList<T> entity);
        IList<T> GetAll ();
        IList<T> GetAll (Expression<Func<T, bool>> predicate);
        IList<T> GetAll (bool asNoTracking, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        T GetEntityById (int Id);
        T GetEntityById (int Id, params Expression<Func<T, object>>[] includeProperties);
        T GetEntityById (Expression<Func<T, bool>> predicate);
        bool DeleteEntity (int Id);
        bool Delete (int Id);
    }
}