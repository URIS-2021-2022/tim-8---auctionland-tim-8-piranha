﻿namespace AuthMicroservice.Controllers
{
    using AuthMicroservice.Consts;
    using AuthMicroservice.Controllers.DTOs.Request;
    using AuthMicroservice.Controllers.DTOs.Response;
    using AuthMicroservice.Services.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    /// <summary>
    /// Controller for client resource.
    /// </summary>
    [Route(RouteConsts.ROUTE_CLIENT_BASE)]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;

        /// <summary>
        /// Constructor for the client controller.
        /// </summary>
        /// <param name="clientService">Client service.</param>
        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        /// <summary>
        /// Endpoint for getting all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ClientResponseDto>> HandleGetAllClients()
        {
            return Ok(clientService.GetAllClients());
        }


        /// <summary>
        /// Endpoint for getting a single user by user uid.
        /// </summary>
        /// <param name="uid">Uid of the user.</param>
        /// <returns>ActionResult&lt;ClientResponseDto&gt;</returns>
        [HttpGet(RouteConsts.ROUTE_CLIENT_GET_ONE_BY_UID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ClientResponseDto> HandleGetClientByUid(string uid)
        {
            return Ok(clientService.GetOneByUid(uid));
        }

        /// <summary>
        /// Endpoint for getting options.
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult HandleGetClientOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        /// <summary>
        /// Endpoint for creating a new client.
        /// </summary>
        /// <param name="requestDTO">Attributes of the client.</param>
        /// <returns>ActionResult&lt;ClientResponseDto&gt;</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<ClientResponseDto> HandleCreateClient([FromBody] CreateClientRequestDto requestDTO)
        {
            return Ok(clientService.Create(requestDTO));
        }

        /// <summary>
        /// Endpoint for updating user with the passed uid.
        /// </summary>
        /// <param name="uid">Uid of the user.</param>
        /// <param name="requestDTO">Attributes of the user.</param>
        /// <returns>ActionResult&lt;ClientResponseDto&gt;</returns>
        [HttpPut(RouteConsts.ROUTE_CLIENT_GET_ONE_BY_UID)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ClientResponseDto> HandleUpdateClient(string uid, [FromBody] UpdateClientRequestDto requestDTO)
        {
            return Ok(clientService.Update(uid, requestDTO));
        }

        /// <summary>
        /// Endpoint for deleting user with the passed uid.
        /// </summary>
        /// <param name="uid">Uid of the user.</param>
        /// <returns>Void</returns>
        [HttpDelete(RouteConsts.ROUTE_CLIENT_GET_ONE_BY_UID)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult HandleDeleteClient(string uid)
        {
            clientService.DeleteAsync(uid);
            return Ok();
        }
    }
}
