using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recomendation.DTOs;
using Recomendation.Services.Abstractions;
using System.Text.Json;
using Recomendation.Controllers;

namespace Recomendation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IPostRecomendationsService _postService;
        public HomeController(IPostRecomendationsService post)
        {
            _postService = post;
        }
        [HttpGet("sim")]
        public string Post(int user_id, string password, int limit)
        {
            return "asgfasdg";
        }

        [HttpGet("posts")]
        public string Posts(int user_id, string password, int limit)
        {
            try
            {
                return JsonSerializer.Serialize(new PostRecomendationOutDTO() { message = "success", posts = _postService.GetLimitRecomendation(user_id, limit) });
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new PostRecomendationOutDTO() { message = ex.Message, posts = _postService.GetLimitRecomendation(user_id, limit) });
            }
        }
    }
}
