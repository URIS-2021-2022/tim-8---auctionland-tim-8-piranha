namespace AuthMicroservice.Services.Abstractions
{
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Client service interface.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>List&lt;ClientResponseDto&gt;</returns>
        public Task<List<ClientResponseDto>> GetAllClients();

        /// <summary>
        /// Gets a single client by uid.
        /// </summary>
        /// <param name="uid">Uid of the client.</param>
        /// <returns>ClientResponseDto</returns>
        public Task<ClientResponseDto> GetOneByUid(string uid);

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="requestDTO">Info about user that is to be created.</param>
        /// <returns>ClientResponseDto</returns>
        public Task<ClientResponseDto> Create(CreateClientRequestDto requestDTO);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="uid">Uid of the client that is to be updated.</param>
        /// <param name="requestDTO">Info to update.</param>
        /// <returns>ClientResponseDto</returns>
        public Task<ClientResponseDto> Update(string uid, UpdateClientRequestDto requestDTO);

        /// <summary>
        /// Deletes a client.
        /// </summary>
        /// <param name="uid">Uid of the client that is to be deleted.</param>
        public Task DeleteAsync(string uid);
    }
}
