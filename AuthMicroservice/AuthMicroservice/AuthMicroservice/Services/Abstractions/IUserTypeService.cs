using AuthMicroservice.Controllers.DTOs.Request;
using AuthMicroservice.Controllers.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthMicroservice.Services.Abstractions
{
    /// <summary>
    /// Interface for user type service.
    /// </summary>
    public interface IUserTypeService
    {
        /// <summary>
        /// Gets all user types.
        /// </summary>
        /// <returns></returns>
        public Task<List<UserTypeResponseDto>> GetAll();

        /// <summary>
        /// Gets a single user type by uid.
        /// </summary>
        /// <param name="uid">Uid of the user type.</param>
        /// <returns>UserTypeResponseDto</returns>
        public Task<UserTypeResponseDto> GetByUid(string uid);

        /// <summary>
        /// Updates a user type entity.
        /// </summary>
        /// <param name="uid">Uid of the user type.</param>
        /// <param name="requestDTO">Info about user type to be changed.</param>
        /// <returns>UserTypeResponseDto</returns>
        public Task<UserTypeResponseDto> UpdateAsync(string uid, UpdateUserTypeRequestDto requestDTO);

        /// <summary>
        /// Creates a new user type entity.
        /// </summary>
        /// <param name="requestDTO">Info about user type.</param>
        /// <returns>UserTypeResponseDto</returns>
        public Task<UserTypeResponseDto> Create(CreateUserTypeRequestDto requestDTO);

        /// <summary>
        /// Deletes a user type.
        /// </summary>
        /// <param name="uid">Uid of the user type.</param>
        public Task DeleteAsync(string uid);
    }
}
