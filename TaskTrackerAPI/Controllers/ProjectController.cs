using AutoMapper;
using Common.Contract.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TaskTrackerData.Domain;
using TaskTrackerData.Service;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/Project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectController> _logger;
        const int maxProjectPageSize = 20;
        public ProjectController(IProjectRepository projectRepository,
                                IMapper mapper,
                                ILogger<ProjectController> logger)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("projectId")]
        [ActionName("GetProject")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int projectId)
        {
            if (!await _projectRepository.ProjectExistAsync(projectId))
            {
                return NotFound();
            }
            var projects = await _projectRepository.GetProjectByIdAsync(projectId);

            return Ok(_mapper.Map<ProjectDto>(projects));
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetProjectByStatus(bool status, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageSize > maxProjectPageSize)
                {
                    pageSize = maxProjectPageSize;
                }

                var (projectDomain, paginationMetadata) = await _projectRepository.GetProjectByStatusAsync(status, pageNumber, pageSize);

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

                return Ok(_mapper.Map<IEnumerable<ProjectDto>>(projectDomain));

            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> PostProject(Project project)
        {
            try
            {
                
                var projectDomain = await _projectRepository.PostProjectAsync(project);

                return CreatedAtAction("GetProject", 
                                    new { id = project.ProjectId }, 
                                    _mapper.Map<ProjectDto>(projectDomain));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }

        }

        [HttpPut("{projectId}")]
        public async Task<ActionResult> UpdateProject(int projectId, ProjectForUpdateDto projectForUpdate)
        {
            try
            {
                if (!await _projectRepository.ProjectExistAsync(projectId))
                {
                    return NotFound();

                }
                var projectDomain = await _projectRepository.GetProjectByIdAsync(projectId);

                if (projectDomain == null)
                {
                    return NotFound();
                }

                _mapper.Map(projectForUpdate, projectDomain);

                await _projectRepository.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");

            }

        }

        [HttpPatch("{projectId}")]
        public async Task<ActionResult> PartialUpdateProject(int projectId,
                       JsonPatchDocument<ProjectForUpdateDto> patchDocument)
        {
            try
            {
                if (!await _projectRepository.ProjectExistAsync(projectId))
                {
                    return NotFound();
                }
                var oldProject = await _projectRepository.GetProjectByIdAsync(projectId);

                if (oldProject == null)
                {
                    return NotFound();
                }
                var projectToPatch = _mapper.Map<ProjectForUpdateDto>(oldProject);

                patchDocument.ApplyTo(projectToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!TryValidateModel(patchDocument))
                {
                    return BadRequest(ModelState);
                }
                _mapper.Map(projectToPatch, oldProject);
                await _projectRepository.SaveChangesAsync();
                return NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }
        }


        [HttpDelete("projectId")]

        public async Task<ActionResult> DeleteProjectById(int projectId)
        {
            if (!await _projectRepository.ProjectExistAsync(projectId))
            {
                return NotFound();
            }
            await _projectRepository.DeleteProjectAsync(projectId);
            return NoContent();
        }

    }
}
