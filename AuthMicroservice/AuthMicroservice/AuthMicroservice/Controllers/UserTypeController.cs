namespace AuthMicroservice.Controllers
{
    using AuthMicroservice.Consts;
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using AuthMicroservice.Services.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    /// <summary>
    /// User type resource controller.
    /// </summary>
    [Route(RouteConsts.ROUTE_USER_TYPE_BASE)]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService userTypeService;

        /// <summary>
        /// Constructor for the user type controller.
        /// </summary>
        /// <param name="userTypeService">User type service.</param>
        public UserTypeController(IUserTypeService userTypeService)
        {
            this.userTypeService = userTypeService;
        }

        /// <summary>
        /// Endpoint for getting all user types.
        /// </summary>
        /// <returns>ActionResult&lt;List&lt;UserTypeResponseDTO&gt;&gt;</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<UserTypeResponseDTO>> HandleGetAllUserTypes()
        {
            return Ok(userTypeService.GetAll());
        }

        /// <summary>
        /// Endpoint for getting a single user type by user uid.
        /// </summary>
        /// <param name="uid">Uid of the user type.</param>
        /// <returns>ActionResult&lt;UserTypeResponseDTO&gt;</returns>
        [HttpGet(RouteConsts.ROUTE_USER_TYPE_GET_ONE_BY_UID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserTypeResponseDTO> HandleGetUserTypeByUid(string uid)
        {
            return Ok(userTypeService.GetByUid(uid));
        }

        /// <summary>
        /// Endpoint for getting options.
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult HandleGetUserTypeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        /// <summary>
        /// Endpoint for creating a new user type.
        /// </summary>
        /// <param name="requestDTO">Attributes of the user type.</param>
        /// <returns>ActionResult&lt;UserTypeResponseDTO&gt;</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<UserTypeResponseDTO> HandleCreateClient([FromBody] CreateUserTypeRequestDTO requestDTO)
        {
            return Ok(userTypeService.Create(requestDTO));
        }

        /// <summary>
        /// Endpoint for updating user type with the passed uid.
        /// </summary>
        /// <param name="uid">Uid of the user type.</param>
        /// <param name="requestDTO">Attributes of the user type.</param>
        /// <returns>ActionResult&lt;UserTypeResponseDTO&gt;</returns>
        [HttpPut(RouteConsts.ROUTE_USER_TYPE_GET_ONE_BY_UID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserTypeResponseDTO> HandleUpdateClient(string uid, [FromBody] UpdateUserTypeRequestDTO requestDTO)
        {
            return Ok(userTypeService.UpdateAsync(uid, requestDTO));
        }

        /// <summary>
        /// Endpoint for deleting user type with the passed uid.
        /// </summary>
        /// <param name="uid">Uid of the user type.</param>
        /// <returns>Void</returns>
        [HttpDelete(RouteConsts.ROUTE_USER_TYPE_GET_ONE_BY_UID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult HandleDeleteUserType(string uid)
        {
            userTypeService.DeleteAsync(uid);
            return Ok();
        }
    }
}
