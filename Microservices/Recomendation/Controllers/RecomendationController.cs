using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recomendation.DTOs;
using Recomendation.Services.Abstractions;
using System;
using System.Text.Json;

namespace Recomendation.Controllers
{
    [Route("recomendation")]
    [Authorize]
    [ApiController]
    public class RecomendationController : ControllerBase
    {
        private readonly IPostRecomendationsService _postService;
        public RecomendationController(IPostRecomendationsService postService)
        {
            _postService = postService;
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
