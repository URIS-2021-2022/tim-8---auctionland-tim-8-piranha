namespace AuthMicroservice.Repositories.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IBaseRepository<T> where T : class
    {
        List<T> List(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        T FindOneByUid(string uid, params Expression<Func<T, object>>[] includes);

        T FindOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        bool Any(Expression<Func<T, bool>> expression);

        T save(T entity);

        T Update(T entity);

        void Delete(T entity);
    }
}
