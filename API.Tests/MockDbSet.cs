using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using API.Data;

namespace API.Tests
{
    public class MockDbSet<T> : IDbSet<T>
        where T : BaseEntity
    {
        List<T> savedList;
        List<T> list;

        public MockDbSet()
        {
            list = new List<T>();
            savedList = list;
        }

        public MockDbSet(IEnumerable<T> initial)
        {
            list = initial.ToList();
            savedList = list;
        }

        public T Add(T entity)
        {
            list.Add(entity);
            return entity;
        }

        public T Find(params object[] keyValues)
        {
            var found = savedList.Where(_ => keyValues.Any(key => key.Equals(_.Id)));
            if (found.Count() == 0)
                return null;
            else
                return found.First();
        }

        public T Remove(T entity)
        {
            list.Remove(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return savedList.GetEnumerator();
        }

        public IQueryProvider Provider
        {
            get { return savedList.AsQueryable().Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return savedList.GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return savedList.AsQueryable().Expression; }
        }

        public void Save()
        {
            savedList = list;
        }

        #region NOT_IMPLEMENTED
        public T Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Local
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
    }
}