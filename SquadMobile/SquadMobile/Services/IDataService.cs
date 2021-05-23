using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SquadMobile.Services
{
    public interface IDataService
    {
        Task<O> Post<O, I>(string api, string method, I input);
        Task<O> Post<O, I>(string api, string method, IList<I> input);
        Task<O> GetAsync<O, I>(string api, string method, I input) where I : class; 
        Task<O> Get<O, I>(string api, string method, IList<I> input);
    }
}
