using PersonMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Data
{
    public interface IBoardRepository
    {
        Task<List<Board>> GetAllBoards();

        Task<Board> GetBoardById(Guid boardId);

        Task<BoardConfirmation> CreateBoard(Board board);

        Task DeleteBoard(Guid boardId);

        Task UpdateBoard(Board board);
    }
}
