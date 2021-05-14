using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Data
{
    interface IDataAccessObject
    {
        D CreateEntity<D>(D model, ApplicationDbContext _Context) where D : new();

        //List<D> Create<D>(List<D> key, IDbContextTransaction _Context) where D : new();

        //D ReadEntity<D>(D key, IDbContextTransaction _Context) where D : new();

        //List<D> Read<D>(D key, IDbContextTransaction _Context) where D : new();
    }
}
