using AutoMapper;
using PaymentMicroservice.Data.Interfaces;
using PaymentMicroservice.Entities;
using PaymentMicroservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentMicroservice.Models.Course;
using FluentValidation;
using CustomValidationException = PaymentMicroservice.Models.Exceptions.ValidationException;
using DocumentMicroservice.ServiceCalls;
using Microsoft.Extensions.Logging;

namespace PaymentMicroservice.Controllers
{
    [ApiController]
    [Route("api/Course")]
    [Produces("application/json", "application/xml")]
    //[Authorize] - kontroleru mogu pristupati samo autorizovani korisnici
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository courseRepository;
        private readonly LinkGenerator linkGeneration;
        private readonly IMapper mapper;
        private readonly IValidator<Course> Validator;
        private readonly ILoggerService logger;

        public CourseController(ICourseRepository courseRepository, LinkGenerator linkGeneration, IMapper mapper, IValidator<Course> Validator, ILoggerService logger)
        {
            this.courseRepository = courseRepository;
            this.linkGeneration = linkGeneration;
            this.mapper = mapper;
            this.Validator = Validator;
            this.logger = logger;
        }

        /// <summary>
        /// Vraca sve kurseve na osnovu prosledjenih filtera
        /// </summary>
        /// <param name="course">Kurs</param>
        /// <returns>Lista svih kurseva</returns>

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<CourseDto>>> GetCourse()
        {
            List<Course> CourseList = courseRepository.GetCourse();

            if (CourseList == null || CourseList.Count == 0)
            {
                await logger.LogMessage(LogLevel.Warning, "Course list is empty!", "Course microservice", "GetCourse");
                return NoContent();
            }

            await logger.LogMessage(LogLevel.Information, "Course list successfully returned!", "Course microservice", "GetCourse");
            return Ok(mapper.Map<List<CourseDto>>(CourseList));
        }

        /// <summary>
        /// Vraća traženi kurs po ID-ju
        /// </summary>
        /// <param name="CourseId">ID kursa</param>
        /// <returns>Traženi kurs</returns>
        /// <response code = "200">Vraća traženi kurs</response>
        /// <response code = "404">Nije pronađen traženi kurs</response>
        [HttpGet("{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CourseDto>> GetCourseById(Guid courseId)
        {
            Course Course = courseRepository.GetCourseById(courseId);
            await logger.LogMessage(LogLevel.Information, "Course successfully returned!", "Course microservice", "GetCourseById");
            return Ok(mapper.Map<CourseDto>(Course));
        }
        /// <summary>
        /// Kreira novi kurs
        /// </summary>
        /// <param name="course"> model kursa</param>
        /// <returns>Potvrda o kreiranom kursu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog kursa \
        /// POST /api/Course \
        /// { \
        ///"Currency" : "EUR",\
        ///"Value" : "118",\
        ///"CourseDate" : "23-03-2021",\
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiran kurs</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja kursa</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CourseConfirmationDto>> CreateCourseAsync([FromBody] CourseCreationDto CourseDto)
        {
            Course course = mapper.Map<Course>(CourseDto);

            var result = await Validator.ValidateAsync(course);
            if (!result.IsValid)
            {
                await logger.LogMessage(LogLevel.Warning, "Course validation failded!", "Course microservice", "CreateCourseAsync");
                throw new CustomValidationException(result.Errors);
            }

            CourseConfirmation confirmation = courseRepository.CreateCourse(course);
            courseRepository.SaveChanges();


            string uri = linkGeneration.GetPathByAction("GetCourseById", "Course", new { courseId = confirmation.CourseId });
            //LinkGenerator --> nalazi putanju resu (naziv akcije koja se radi, naziv kontrollera bez sufiksa kontroller, new-> nesto sto jedinstveno identifikuje nas resur koji trenutno trazimo)
            await logger.LogMessage(LogLevel.Information, "Course successfully created!", "Course microservice", "CreateCourseAsync");
            return Created(uri, mapper.Map<CourseConfirmationDto>(confirmation));
        }
        /// <summary>
        /// Ažurira jedan kurs
        /// </summary>
        /// <param name="course">Model kursa koja se ažurira</param>
        /// <returns>Potvrda o ažuriranom kursu</returns>
        /// <response code="200">Vraća ažuriran kurs</response>
        /// <response code="404">Nije pronađen kurs za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja kursa</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CourseDto>> UpdateCourseAsync(CourseUpdateDto courseDto)
        {
            Course existingCourse = courseRepository.GetCourseById(courseDto.CourseId);

            Course Course = mapper.Map<Course>(courseDto);

            var result = await Validator.ValidateAsync(Course);
            if (!result.IsValid)
            {
                await logger.LogMessage(LogLevel.Warning, "Course validation failded!", "Course microservice", "UpdateCourseAsync");
                throw new CustomValidationException(result.Errors);
            }

            mapper.Map(Course, existingCourse);
            courseRepository.SaveChanges();

            CourseConfirmation confirmation = mapper.Map<CourseConfirmation>(existingCourse);
            await logger.LogMessage(LogLevel.Information, "Course successfully updated!", "Course microservice", "UpdateCourseAsync");
            return Ok(mapper.Map<CourseConfirmationDto>(confirmation));
        }

        [HttpDelete("courseId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            Course Course = courseRepository.GetCourseById(courseId);

            courseRepository.DeleteCourse(Course);
            courseRepository.SaveChanges();

            await logger.LogMessage(LogLevel.Information, "Course successfully deleted!", "Course microservice", "DeleteCourse");
            return NoContent(); // Successful deletion -- sve je okej proslo ali ne vraca nikakav sadrzaj--> iz familije je 200
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetStateOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}