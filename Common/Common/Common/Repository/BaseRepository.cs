namespace Common.Repository
{
    using Commons.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Common.Domain;
    using Common.ExceptionHandling.Exceptions;

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

        private DbSet<T> _entity { get; }
        protected DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
            this._entity = context.Set<T>();
        }

        public virtual List<T> List(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var result = _entity.Where(expression);

            result = includes
                   .Aggregate(result, (current, property) => current.Include(property));

            return result.ToList();
        }


        public T FindOneByUid(string uid, params Expression<Func<T, object>>[] includes)
        {
            var result = _entity.Where(e => e.Uid == uid);

            result = includes
                   .Aggregate(result, (current, property) => current.Include(property));

            return result.FirstOrDefault();
        }

        public T FindOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var result = _entity.Where(expression);

            result = includes
                   .Aggregate(result, (current, property) => current.Include(property));

            return result.FirstOrDefault();
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _entity.Any(expression);
        }

        public T Save(T entity)
        {
            try
            {
                _entity.Add(entity);
                context.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {
                throw new EntityCannotBeSavedException(e.InnerException.Message);
            }
        }

        public T Update(T entity)
        {
            try
            {
                _entity.Update(entity);
                context.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {
                throw new EntityCannotBeSavedException(e.InnerException.Message);
            }
        }

        public void Delete(T entity)
        {
            _entity.Remove(entity);
            context.SaveChanges();
        }
    }
}
