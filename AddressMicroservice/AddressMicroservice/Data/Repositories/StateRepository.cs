using AutoMapper;
using AddressMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AddressMicroservice.Entities.Contexts;
using AddressMicroservice.Data.Interfaces;
using AddressMicroservice.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AddressMicroservice.Data.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly AddressContext Context;
        private readonly IMapper Mapper;

        public StateRepository(AddressContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public StateConfirmation CreateState(State State)
        {
            var createdEntity = Context.Add(State);

            return Mapper.Map<StateConfirmation>(createdEntity.Entity);
        }

        public void DeleteState(State state)
        {
            Context.Remove(state);
        }

        public List<State> GetState(string NameState = null)
        {
            return Context.State
                .AsNoTracking()
                .Where(o => o.NameState == NameState || NameState == null)
                .ToList();
        }

        public State GetStateById(Guid StateId)
        {
            var state = Context.State.FirstOrDefault(o => o.StateID == StateId);

            if (state == null)
            {
                throw new NotFoundException(nameof(Address), StateId);
            }

            return state;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
