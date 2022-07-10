using AutoMapper;
using Common.Contract.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TaskTrackerData.Domain;
using TaskTrackerData.Service;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/signup")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpRepository _signUpRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SignUpController> _logger;
        const int maxCitiesPageSize = 20;
        public SignUpController(ISignUpRepository signUpRepository, IMapper mapper, ILogger<SignUpController> logger)
        {
            _signUpRepository = signUpRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(string? emailAddress,
                                    string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageSize > maxCitiesPageSize)
                {
                    pageSize = maxCitiesPageSize;
                }

                var (userDomain, paginationMetadata) = await _signUpRepository.GetUsersAsync(emailAddress, searchQuery, pageNumber, pageSize);

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

                return Ok(_mapper.Map<IEnumerable<UserDto>>(userDomain));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }

        }
        [HttpGet("{id}")]
        [ActionName("GetUser")]

        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _signUpRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }


        [HttpPost]
        public async Task<ActionResult<UserDto>>SignUpUser(User user)
        {
            try
            {
                var userDomain = await _signUpRepository.PostUserAsync(user);
                
                return CreatedAtAction("GetUser", 
                                    new {id = user.UserId}, 
                                    _mapper.Map<UserDto>(userDomain));
            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }

        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser(int userId, UserForUpdateDto user)
        {
            try
            {

                var userDomain = await _signUpRepository.GetUserByIdAsync(userId);
                if (userDomain == null)
                {
                    return NotFound();
                }

                if (userDomain == null)
                {
                    return NotFound();
                }
                _mapper.Map(user, userDomain);

                await _signUpRepository.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");

            }
            
        }
        [HttpPatch("{userId}")]

        public async Task<ActionResult> PartialUpdateUserAccount(int userId,
                            JsonPatchDocument<UserForUpdateDto> patchDocument)
        {
            try
            {
                var oldUser = await _signUpRepository.GetUserByIdAsync(userId);

                if (oldUser == null)
                {
                    return NotFound();
                }

                var userToPatch = _mapper.Map<UserForUpdateDto>(oldUser);
                
                patchDocument.ApplyTo(userToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!TryValidateModel(patchDocument))
                {
                    return BadRequest(ModelState);
                }
                _mapper.Map(userToPatch, oldUser);

                await _signUpRepository.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }

        }
    }
}
