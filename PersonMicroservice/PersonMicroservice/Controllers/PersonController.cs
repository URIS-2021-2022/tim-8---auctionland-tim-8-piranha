using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Controllers
{
    /// <summary>
    /// Kontroler za licnost
    /// </summary>
    [ApiController]
    [Route("api/person")]
    [Produces("application/json", "application/xml")]
    public class PersonController : ControllerBase
    {
    }
}
