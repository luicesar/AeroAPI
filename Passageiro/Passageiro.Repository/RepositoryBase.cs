using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Passageiro.Repository.Data;
using Passageiro.Repository.Interfaces;
using Passageiro.Shared.Entities;

namespace Passageiro.Repository {
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : Entity {
        private readonly PassageiroContext context;

        public RepositoryBase (PassageiroContext context) {
            this.context = context;
        }
        public virtual IList<T> GetAll () {
            return GetAll (null);
        }

        public virtual IList<T> GetAll (Expression<Func<T, bool>> predicate) {
            return GetAll (true, predicate, null);
        }

        public virtual IList<T> GetAll (bool asNoTracking, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties) {
            IQueryable<T> query = context.Set<T> ();
            if (predicate != null)
                query = query.Where (predicate);
            if (includeProperties != null)
                query = includeProperties.Aggregate (query, (current, include) => current.Include (include));

            if (asNoTracking)
                return query.AsNoTracking ().ToList ();
            else
                return query.ToList ();
        }

        public virtual T GetEntityById (int id) {
            return GetEntityById (id, null);
        }

        public virtual T GetEntityById (int id, params Expression<Func<T, object>>[] includeProperties) {
            IQueryable<T> query = context.Set<T> ();
            if (includeProperties != null)
                query = includeProperties.Aggregate (query, (current, include) => current.Include (include));
            return query.Where (x => x.Id == id)
                .AsNoTracking ()
                .FirstOrDefault ();
        }

        public T GetEntityById (Expression<Func<T, bool>> predicate) {
            IQueryable<T> query = context.Set<T> ();
            query = query.Where (predicate);
            return query.AsNoTracking ().FirstOrDefault ();
        }

        public bool DeleteEntity (int id) {
            try {
                T entity = GetEntityById (id);
                if (entity != null) {
                    Delete (id);
                    return true;
                } else {
                    return false;
                }
            } catch (Exception) {
                return false;
            }
        }

        public bool Delete (int id) {
            try {
                T entity = context.Set<T> ().Where (x => x.Id == id).FirstOrDefault ();
                context.Entry (entity).State = EntityState.Deleted;
                context.SaveChanges ();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public virtual bool Update (int id, T entity) {
            try {
                entity.SetId (id);
                context.Update (entity);
                context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                var err = ex;
                return false;
            }
        }

        public virtual bool Create (T entity) {
            try {
                context.Add<Entity> (entity);
                context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                var err = ex;
                return false;
            }
        }

        public virtual bool Create (IList<T> entity) {
            try {
                foreach (var item in entity) {
                    context.Add<Entity> (item);
                }
                context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                var err = ex;
                return false;
            }
        }

        public virtual bool Update (IList<T> entity) {
            try {

                foreach (var item in entity) {
                    context.Attach (item);
                }

                context.SaveChanges ();
                return true;
            } catch (Exception ex) {
                var err = ex;
                return false;
            }
        }

        private IQueryable<T> Include (IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties) {
            foreach (var property in includeProperties)
                query = query.Include (property);
            return query;
        }
    }
}