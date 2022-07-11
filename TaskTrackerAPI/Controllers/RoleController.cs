using AutoMapper;
using Common.Contract.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerData.Domain;
using TaskTrackerData.Service;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleController> _logger;
        const int maxProjectPageSize = 20;
        public RoleController(IRoleRepository roleRepository,
                                IMapper mapper,
                                ILogger<RoleController> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("{roleId}")]
        [ActionName("GetRole")]
        public async Task<ActionResult<RoleDto>> GetRoleById(int roleId)
        {
            if (!await _roleRepository.RoleExistAsync(roleId))
            {
                return NotFound();
            }

            var role = await _roleRepository.GetRoleByIdAsync(roleId);

            return Ok(_mapper.Map<RoleDto>(role));

        }

        [HttpPost]

        public async Task<ActionResult<RoleDto>> PostRole(Role role)
        {
            try
            {

                var roleDomain = await _roleRepository.PostRolesAsync(role);

                return CreatedAtAction("GetRole", new {roleId = role.RoleId},
                                    _mapper.Map<RoleDto>(roleDomain));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

    }
}
