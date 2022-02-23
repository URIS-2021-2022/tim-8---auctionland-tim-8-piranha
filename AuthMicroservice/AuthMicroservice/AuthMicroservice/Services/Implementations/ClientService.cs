namespace AuthMicroservice.Services.Implementations
{
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using AuthMicroservice.Domain;
    using AuthMicroservice.Repositories.Abstractions;
    using AuthMicroservice.Services.Abstractions;
    using AutoMapper;
    using System.Collections.Generic;

    /// <summary>
    /// Client service interface implementation.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper autoMapper;

        /// <summary>
        /// Client service constructor.
        /// </summary>
        /// <param name="clientRepository">Auth repository.</param>
        public ClientService(IClientRepository clientRepository, IMapper autoMapper)
        {
            this.clientRepository = clientRepository;
            this.autoMapper = autoMapper;
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="requestDTO">Info about user that is to be created.</param>
        /// <returns>ClientResponseDTO</returns>
        public ClientResponseDTO Create(CreateClientRequestDTO requestDTO)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deletes a client.
        /// </summary>
        /// <param name="uid">Uid of the client that is to be deleted.</param>
        public void Delete(string uid)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>List&lt;ClientResponseDTO&gt;</returns>
        public List<ClientResponseDTO> GetAllClients()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets a single client by uid.
        /// </summary>
        /// <param name="uid">Uid of the client.</param>
        /// <returns>ClientResponseDTO</returns>
        public ClientResponseDTO GetOneByUid(string uid)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="uid">Uid of the client that is to be updated.</param>
        /// <param name="requestDTO">Info to update.</param>
        /// <returns>ClientResponseDTO</returns>
        public ClientResponseDTO Update(string uid, UpdateClientRequestDTO requestDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
