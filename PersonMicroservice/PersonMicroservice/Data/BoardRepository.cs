using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Data
{
    public class BoardRepository : IBoardRepository
    {
        private readonly PersonContext context;
        private readonly IMapper mapper;

        public BoardRepository(PersonContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        public async Task<BoardConfirmation> CreateBoard(Board board)
        {
            var createdEntity = await context.Boards.AddAsync(board);

            await context.SaveChangesAsync();

            return mapper.Map<BoardConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteBoard(Guid boardId)
        {
            var board = await GetBoardById(boardId);

            context.Boards.Remove(board);
            await context.SaveChangesAsync();
        }

        public async Task<List<Board>> GetAllBoards()
        {
            return await context.Boards
                .Include(p => p.President)
                .Include(m => m.Members)
                .ToListAsync();
        }

        public async Task<Board> GetBoardById(Guid boardId)
        {
            return await context.Boards
                .Include(p => p.President)
                .Include(m => m.Members)
             .FirstOrDefaultAsync(b => b.BoardId == boardId);
        }

        public async Task UpdateBoard(Board board)
        {
            await context.SaveChangesAsync();
        }
    }
}
