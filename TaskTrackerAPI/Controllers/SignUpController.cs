using AutoMapper;
using Common.Contract.Model;
using Microsoft.AspNetCore.Http;
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
            _signUpRepository = signUpRepository ?? throw new ArgumentNullException(nameof(signUpRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
        [HttpPost]
        public async Task<ActionResult<UserDto>> SignUpUser(User user)
        {           
            var newUser = await _signUpRepository.PostUserAsync(user);

            return Ok(_mapper.Map<UserDto>(newUser));
        }

        [HttpPut("{userid}")]
        public async Task<ActionResult> UpdateUser(int userId, UserForUpdateDto user)
        {
            if (!await _signUpRepository.UserExistAsync(userId))
            {
                return NotFound();
            }
            var newUser = await _signUpRepository.GetUsersAsync(userId);
            if (newUser == null)
            {
                return NotFound();
            }
            _mapper.Map(user, newUser);

            await _signUpRepository.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch()]

    }
}
