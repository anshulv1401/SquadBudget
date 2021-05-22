using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Data
{
    public class DataAccessObject : IDataAccessObject
    {
        public List<D> Create<D>(List<D> key, ApplicationDbContext _Context) where D : new()
        {
            //_Context.
            throw new NotImplementedException();
        }

        public D CreateEntity<D>(D model, IDbContextTransaction _Context) where D : new()
        {
            throw new NotImplementedException();
        }

        public D CreateEntity<D>(D model, ApplicationDbContext _Context) where D : new()
        {
            throw new NotImplementedException();
        }

        public List<D> Read<D>(D key, IDbContextTransaction _Context) where D : new()
        {
            throw new NotImplementedException();
        }

        public D ReadEntity<D>(D key, IDbContextTransaction _Context) where D : new()
        {
            throw new NotImplementedException();
        }
    }
}
