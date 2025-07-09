using Microsoft.AspNetCore.Mvc;
using BlogApi.Models;
using BlogApi.Services;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_postService.GetAll());

        [HttpPost]
        public IActionResult Add(Post post)
        {
            _postService.Add(post);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _postService.Delete(id);
            return Ok();
        }
    }
}
