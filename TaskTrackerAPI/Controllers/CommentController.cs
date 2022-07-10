using AutoMapper;
using Common.Contract.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TaskTrackerData.Domain;
using TaskTrackerData.Service;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/Comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentController> _logger;
        const int maxProjectPageSize = 20;
        public CommentController(ICommentRepository commentRepository,
                                IMapper mapper,
                                ILogger<CommentController> logger)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{commentId}")]
        
        public async Task<ActionResult<CommentDto>> GetCommentById(int commentId)
        {
            var comments = await _commentRepository.GetCommentsByIdAsync(commentId);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetProjectByStatus(string? query, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageSize > maxProjectPageSize)
                {
                    pageSize = maxProjectPageSize;
                }

                var (commentDomain, paginationMetadata) = await _commentRepository.GetCommentAsync(query, pageNumber, pageSize);

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

                return Ok(_mapper.Map<List<CommentDto>>(commentDomain));

            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> PostComment(Comment comment)
        {
            try
            {

                var commentDomain = await _commentRepository.PostProjectAsync(comment);

                await _commentRepository.SaveChangesAsync();

                return Created("~/api/Comment/",_mapper.Map<CommentDto>(commentDomain));

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }
        }


        [HttpPut("{commentId}")]
        public async Task<ActionResult> UpdateComment(int commentId, CommentForUpdateDto comment)
        {
            try
            {
                var commentDomain = await _commentRepository.GetCommentsByIdAsync(commentId);

                if (commentDomain == null)
                {
                    return NotFound();
                }

                _mapper.Map(comment, commentDomain);

                await _commentRepository.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception while getting users information", ex);

                return StatusCode(500, "A problem happened while handling your request.");
            }

        }

        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            var comment = await _commentRepository.GetCommentsByIdAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentRepository.DeleteCommentAsync(commentId);

            return NoContent();

        }

    }


}

