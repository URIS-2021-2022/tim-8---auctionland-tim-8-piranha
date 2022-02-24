using BuyerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Data.Interfaces
{
    public interface IBoardNumberRepository
    {
        Task<List<BoardNumber>> GetBoardNumberAsync(int number = 0);

        Task<BoardNumber> GetBoardNumberByIdAsync(Guid boardNumberID);

        Task<BoardNumberConfirmation> CreateBoardNumberAsync(BoardNumber boardNumber);

        Task UpdateBoardNumberAsync(BoardNumber boardNumber);

        Task DeleteBoardNumberAsync(Guid boardNumberID);

        Task<bool> SaveChangesAsync();
    }
}
