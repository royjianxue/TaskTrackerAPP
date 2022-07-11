using AutoMapper;
using Common.Contract.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerData.Domain;
using TaskTrackerData.Service;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {

        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PermissionController> _logger;
        const int maxProjectPageSize = 20;
        public PermissionController(IPermissionRepository permissionRepository,
                                IMapper mapper,
                                ILogger<PermissionController> logger)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{permissionId}")]
        public async Task<ActionResult<PermissionDto>> GetPermissionById(int permissionId)
        {
            if (!await _permissionRepository.PermissionExistAsync(permissionId))
            {
                return NotFound();
            }

            var permission = await _permissionRepository.GetPermissionByIdAsync(permissionId);

            return Ok(_mapper.Map<PermissionDto>(permission));

        }

        [HttpPost]

        public async Task<ActionResult<PermissionDto>> PostPermission(Permission permission)
        {
            try
            {

                var permissionDomain = await _permissionRepository.PostPermissionAsync(permission);

                return Created("~/api/permission/",
                                    _mapper.Map<PermissionDto>(permissionDomain));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }
        }




    }
}
