namespace AuthMicroservice.Services.Abstractions
{
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using System.Collections.Generic;

    /// <summary>
    /// Client service interface.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>List&lt;ClientResponseDTO&gt;</returns>
        public List<ClientResponseDTO> GetAllClients();

        /// <summary>
        /// Gets a single client by uid.
        /// </summary>
        /// <param name="uid">Uid of the client.</param>
        /// <returns>ClientResponseDTO</returns>
        public ClientResponseDTO GetOneByUid(string uid);

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="requestDTO">Info about user that is to be created.</param>
        /// <returns>ClientResponseDTO</returns>
        public ClientResponseDTO Create(CreateClientRequestDTO requestDTO);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="uid">Uid of the client that is to be updated.</param>
        /// <param name="requestDTO">Info to update.</param>
        /// <returns>ClientResponseDTO</returns>
        public ClientResponseDTO Update(string uid, UpdateClientRequestDTO requestDTO);

        /// <summary>
        /// Deletes a client.
        /// </summary>
        /// <param name="uid">Uid of the client that is to be deleted.</param>
        public void Delete(string uid);
    }
}
