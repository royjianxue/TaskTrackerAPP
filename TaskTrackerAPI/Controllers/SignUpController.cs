using AutoMapper;
using Common.Contract.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TaskTrackerData.Service;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/signup")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpRepository _signUpRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;
        public SignUpController(ISignUpRepository signUpRepository, IMapper mapper)
        {
            _signUpRepository = signUpRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(string? emailAddress, 
                                    string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }

            var(userDomain, paginationMetadata) = await _signUpRepository.GetUsersAsync(emailAddress, searchQuery, pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<UserDto>>(userDomain));
        }


    }
}
