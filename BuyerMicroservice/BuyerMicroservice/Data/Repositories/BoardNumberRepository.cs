using AutoMapper;
using BuyerMicroservice.Data.Interfaces;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Data.Repositories
{
    public class BoardNumberRepository : IBoardNumberRepository
    {
        private readonly BuyerContext buyerContext;
        private readonly IMapper mapper;
        public BoardNumberRepository(BuyerContext buyerContext, IMapper mapper)
        {
            this.buyerContext = buyerContext;
            this.mapper = mapper;
        }

        public async Task<BoardNumberConfirmation> CreateBoardNumberAsync(BoardNumber boardNumber)
        {
            var createdBoardNumber = await buyerContext.AddAsync(boardNumber);
            return mapper.Map<BoardNumberConfirmation>(createdBoardNumber.Entity);
        }

       

        public async Task DeleteBoardNumberAsync(Guid boardNumberID)
        {
            var boardNumber = await GetBoardNumberByIdAsync(boardNumberID);
            buyerContext.Remove(boardNumber);
        }

        public async Task<List<BoardNumber>> GetBoardNumberAsync(int number = 0)
        {
            return await buyerContext.boardNumber.Where(o => (o.number == number || number == 0)).ToListAsync();
        }

        public async Task<BoardNumber> GetBoardNumberByIdAsync(Guid boardNumberID)
        {
            return buyerContext.boardNumber.FirstOrDefault(o => o.boardNumberID == boardNumberID);
        }

       

        public async Task<bool> SaveChangesAsync()
        {
            return await buyerContext.SaveChangesAsync() > 0;
        }

        public async Task UpdateBoardNumberAsync(BoardNumber boardNumber)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
              kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
