using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerData.Domain;
using TaskTrackerData.Service;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/[controller]")]
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

        

        //public async Task<ActionResult<List<Project>>> GetProjectByStatus(bool status, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        //{
        //    if (pageSize > maxProjectPageSize)
        //    {
        //        pageSize = maxProjectPageSize;
        //    }


        //    var (userDomain, paginationMetadata) = await _projectRepository.GetProjectByStatus(status, searchQuery, pageNumber, pageSize);




        //}




    }
}
