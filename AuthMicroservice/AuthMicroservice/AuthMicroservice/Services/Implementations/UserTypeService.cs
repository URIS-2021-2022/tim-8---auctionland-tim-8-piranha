namespace AuthMicroservice.Services.Implementations
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
    /// User type interface implementation.
    /// </summary>
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeRepository userTypeRepository;
        private readonly IMapper autoMapper;
        private readonly ILoggerService loggerService;

        /// <summary>
        /// User type service constructor.
        /// </summary>
        /// <param name="userTypeRepository">User type repository.</param>
        /// <param name="autoMapper">Auto model mapper.</param>
        /// <param name="loggerService">Logger service.</param>
        public UserTypeService(IUserTypeRepository userTypeRepository, IMapper autoMapper, ILoggerService loggerService)
        {
            this.userTypeRepository = userTypeRepository;
            this.autoMapper = autoMapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Creates a user type.
        /// </summary>
        /// <param name="requestDTO">User type info.</param>
        /// <returns>UserTypeResponseDto</returns>
        public async Task<UserTypeResponseDto> Create(CreateUserTypeRequestDto requestDTO)
        {
            ThrowExceptionIfUserTypeExists(requestDTO.Name);

            UserType userType = new UserType()
            {
                Uid = Guid.NewGuid().ToString(),
                Name = requestDTO.Name
            };

            userTypeRepository.Save(userType);

            await loggerService.LogMessage(
                    LogLevel.Information,
                    "Create successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "CreateAsync");

            return autoMapper.Map<UserTypeResponseDto>(userType);
        }

        private void ThrowExceptionIfUserTypeExists(string name)
        {
            if (FindOneByNameAsync(name) != null)
            {
                throw new EntityAlreadyExistsException($"User type with name {name} already exists!");
            }
        }

        /// <summary>
        /// Finds user type by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<UserType> FindOneByNameAsync(string name)
        {
            await loggerService.LogMessage(
                    LogLevel.Information,
                    "FindOneByNameAsync successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "FindOneByNameAsync");
            return userTypeRepository.FindOne(c => c.Name.Equals(name));
        }

        /// <summary>
        /// Deletes a user type.
        /// </summary>
        /// <param name="uid">Uid of the user type.</param>
        public async Task DeleteAsync(string uid)
        {
            UserType userType = userTypeRepository.FindOneByUid(uid);

            if (userType == null)
            {
                await loggerService.LogMessage(
                    LogLevel.Warning,
                    "Nothing to delete",
                    GeneralConsts.MICROSERVICE_NAME,
                    "DeleteAsync");

                return;
            }

            await loggerService.LogMessage(
                    LogLevel.Information,
                    "Delete successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "DeleteAsync");

            userTypeRepository.Delete(userType);
        }

        /// <summary>
        /// Gets all user types.
        /// </summary>
        /// <returns>List&lt;UserTypeResponseDto&gt;</returns>
        public async Task<List<UserTypeResponseDto>> GetAll()
        {
            await loggerService.LogMessage(
                    LogLevel.Information,
                    "GetAll successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "GetAll");

            return autoMapper.Map<List<UserTypeResponseDto>>(userTypeRepository.List(ut => true));
        }

        /// <summary>
        /// Gets a single user type by uid.
        /// </summary>
        /// <param name="uid">Uid of the user type.</param>
        /// <returns>UserTypeResponseDto</returns>
        public async Task<UserTypeResponseDto> GetByUid(string uid)
        {
            await loggerService.LogMessage(
                    LogLevel.Information,
                    "GetByUid successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "GetByUid");

            return autoMapper.Map<UserTypeResponseDto>(userTypeRepository.FindOneByUid(uid));
        }

        /// <summary>
        /// Updates a user type with the provided info.
        /// </summary>
        /// <param name="uid">Uid of the user type.</param>
        /// <param name="requestDTO">Info to be applied as a change.</param>
        /// <returns>UserTypeResponseDto</returns>
        public async Task<UserTypeResponseDto> UpdateAsync(string uid, UpdateUserTypeRequestDto requestDTO)
        {
            UserType userType = userTypeRepository.FindOneByUid(uid);

            if (userType == null)
            {
                await loggerService.LogMessage(
                    LogLevel.Error,
                    "Update failed",
                    GeneralConsts.MICROSERVICE_NAME,
                    "Update");
                throw new EntityNotFoundException($"User type with uid {uid} not found!");
            }

            userType.Name = requestDTO.Name;

            userTypeRepository.Save(userType);

            await loggerService.LogMessage(
                    LogLevel.Information,
                    "Update successful",
                    GeneralConsts.MICROSERVICE_NAME,
                    "UpdateAsync");

            return autoMapper.Map<UserTypeResponseDto>(userType);
        }
    }
}
