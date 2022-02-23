using AddressMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Data.Interfaces
{
    public interface IStateRepository
    {
        List<State> GetState(string nameState = null);

        State GetStateById(Guid stateId);

        StateConfirmation CreateState(State state);

        void DeleteState(State state);

        void SaveChanges();
    }
}
