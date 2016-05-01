using System;
using System.Collections.Generic;
using System.Linq;
using FlightGates.DataStore.Entities;

namespace FlightGates.DataStore
{
    public interface IGatesRepository
    {
        IList<Gate> GetAll();
        Gate GetById(int id);
    }

    public class GatesRepository : IGatesRepository
    {
        private readonly IDataContext _dataContext;

        public GatesRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IList<Gate> GetAll()
        {
            return _dataContext.Gates;
        }

        public Gate GetById(int id)
        {
            return _dataContext.Gates.FirstOrDefault(g => g.Id == id);
        }
    }
}
