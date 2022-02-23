﻿namespace AuthMicroservice.Services.Implementations
{
    using AuthMicroservice.Consts;
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using AuthMicroservice.Domain;
    using AuthMicroservice.Repositories.Abstractions;
    using AuthMicroservice.Services.Abstractions;
    using AuthMicroservice.Utils.LoggerService;
    using AutoMapper;
    using Common.ExceptionHandling.Exceptions;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Client service interface implementation.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper autoMapper;
        private IUserTypeRepository userTypeRepository;
        private readonly ILoggerService loggerService;

        /// <summary>
        /// Client service constructor.
        /// </summary>
        /// <param name="clientRepository">Client repository.</param>
        /// <param name="autoMapper">Auto model mapper.</param>
        /// <param name="userTypeRepository">User type repository.</param>
        /// <param name="loggerService">Logger service.</param>
        public ClientService(IClientRepository clientRepository, 
            IMapper autoMapper, 
            IUserTypeRepository userTypeRepository,
            ILoggerService loggerService)
        {
            this.clientRepository = clientRepository;
            this.autoMapper = autoMapper;
            this.userTypeRepository = userTypeRepository;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="requestDTO">Info about user that is to be created.</param>
        /// <returns>ClientResponseDTO</returns>
        public async Task<ClientResponseDTO> Create(CreateClientRequestDTO requestDTO)
        {
            ThrowExceptionIfEmailExists(requestDTO.Username);

            Client client = new Client()
            {
                Uid = Guid.NewGuid().ToString(),
                FirstName = requestDTO.FirstName,
                LastName = requestDTO.LastName,
                Username = requestDTO.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password)
            };

            client.UserType = userTypeRepository.FindOne(ut => ut.Name.Equals(requestDTO.UserType));

            clientRepository.Save(client);

            await loggerService.LogMessage(
                    LogLevel.Information,
                    "Create successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "Create");

            return autoMapper.Map<ClientResponseDTO>(client);
        }

        private void ThrowExceptionIfEmailExists(string email)
        {
            if (FindOneByEmailAddress(email) != null)
            {
                throw new EntityAlreadyExistsException($"User with email {email} already exists!");
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
        public async Task DeleteAsync(string uid)
        {
            Client client = clientRepository.FindOneByUid(uid);

            if (client == null)
            {
                await loggerService.LogMessage(
                   LogLevel.Warning,
                   "No client to delete",
                   GeneralConsts.MICROSERVICE_NAME,
                   "Delete");

                return;
            }

            await loggerService.LogMessage(
                   LogLevel.Information,
                   "Client deleted",
                   GeneralConsts.MICROSERVICE_NAME,
                   "DeleteAsync");

            clientRepository.Delete(client);
        }

        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>List&lt;ClientResponseDTO&gt;</returns>
        public async Task<List<ClientResponseDTO>> GetAllClients()
        {
            await loggerService.LogMessage(
                    LogLevel.Information,
                    "GetAllClients successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "GetAllClients");
            List<Client> clients = clientRepository.List(c => true);

            if (clients.Count == 0)
            {
                return new List<ClientResponseDTO>();
            }

            return autoMapper.Map<List<Client>, List<ClientResponseDTO>>(clients);
        }

        /// <summary>
        /// Gets a single client by uid.
        /// </summary>
        /// <param name="uid">Uid of the client.</param>
        /// <returns>ClientResponseDTO</returns>
        public async Task<ClientResponseDTO> GetOneByUid(string uid)
        {
            await loggerService.LogMessage(
                    LogLevel.Information,
                    "GetOneByUid successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "GetOneByUid");

            return autoMapper.Map<Client, ClientResponseDTO>(clientRepository.FindOneByUid(uid));
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="uid">Uid of the client that is to be updated.</param>
        /// <param name="requestDTO">Info to update.</param>
        /// <returns>ClientResponseDTO</returns>
        public async Task<ClientResponseDTO> Update(string uid, UpdateClientRequestDTO requestDTO)
        {
            Client client = clientRepository.FindOneByUid(uid);

            if (client == null)
            {
                await loggerService.LogMessage(
                   LogLevel.Error,
                   "Update failed",
                   GeneralConsts.MICROSERVICE_NAME,
                   "Update");

                throw new EntityNotFoundException($"Client with uid {uid} not found!");
            }

            client.FirstName = requestDTO.FirstName;
            client.LastName = requestDTO.LastName;
            client.Username = requestDTO.Username;
            client.Password = requestDTO.Password;

            clientRepository.Save(client);

            await loggerService.LogMessage(
                    LogLevel.Information,
                    "Update successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "Update");

            return autoMapper.Map<ClientResponseDTO>(client);
        }
    }
}
