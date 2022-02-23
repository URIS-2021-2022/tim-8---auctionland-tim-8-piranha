namespace AuthMicroservice.Services.Implementations
{
    using AuthMicroservice.Consts;
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
        private IUserTypeRepository userTypeRepository;

        /// <summary>
        /// Client service constructor.
        /// </summary>
        /// <param name="clientRepository">Client repository.</param>
        /// <param name="autoMapper">Auto model mapper.</param>
        /// <param name="userTypeRepository">User type repository.</param>
        public ClientService(IClientRepository clientRepository, 
            IMapper autoMapper, 
            IUserTypeRepository userTypeRepository)
        {
            this.clientRepository = clientRepository;
            this.autoMapper = autoMapper;
            this.userTypeRepository = userTypeRepository;
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="requestDTO">Info about user that is to be created.</param>
        /// <returns>ClientResponseDTO</returns>
        public ClientResponseDTO Create(CreateClientRequestDTO requestDTO)
        {
            ThrowExceptionIfEmailExists(requestDTO.Username);

            Client client = new Client()
            {
                FirstName = requestDTO.FirstName,
                LastName = requestDTO.LastName,
                Username = requestDTO.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password)
            };

            client.UserType = userTypeRepository.FindOne(ut => ut.Name.Equals(requestDTO.UserType));

            clientRepository.Save(client);

            return autoMapper.Map<ClientResponseDTO>(client);
        }

        private void ThrowExceptionIfEmailExists(string email)
        {
            if (FindOneByEmailAddress(email) != null)
            {
                //throw new EntityAlreadyExistsException($"User with email {email} already exists!", GeneralConsts.MICROSERVICE_NAME);
            }
        }

        /// <summary>
        /// Method for finding a client with the specified email.
        /// </summary>
        /// <param name="email">Email of the user.</param>
        /// <returns>Client</returns>
        public Client FindOneByEmailAddress(string email)
        {
            return clientRepository.FindOne(c => c.Username.Equals(email));
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
            return autoMapper.Map<List<ClientResponseDTO>>(clientRepository.List(c => true));
        }

        /// <summary>
        /// Gets a single client by uid.
        /// </summary>
        /// <param name="uid">Uid of the client.</param>
        /// <returns>ClientResponseDTO</returns>
        public ClientResponseDTO GetOneByUid(string uid)
        {
            return autoMapper.Map<ClientResponseDTO>(clientRepository.FindOneByUid(uid));
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
