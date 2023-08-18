using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recomendations.Data.Abstractions;
using Recomendations.DTOs;
using Recomendations.Services.Abstractions;
using System;
using System.Text.Json;

namespace Recomendations.Controllers
{
    [Route("recomendation")]
    [ApiController]
    public class RecomendationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IStartupRepository _startupRepository;
        private readonly IPostRecomendationsService _postService;
        public RecomendationController(IPostRecomendationsService postService, IStartupRepository startupRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _postService = postService;
            _startupRepository = startupRepository;
        }

        [HttpGet("postsGet")]
        public string Posts(int user_id, string password, int limit)
        {
            try
            {
                if (!_userRepository.Authorize(user_id, password))
                    throw new Exception("User isnt correct");
                return JsonSerializer.Serialize(new PostRecomendationOutDTO() { message = "success", posts = _postService.GetLimitRecomendation(user_id, limit) });
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new PostRecomendationOutDTO() { message = ex.Message});
            }
        }
        [HttpGet("startupsForUserGet")]
        public string Startups(int user_id, string password, int limit)
        {
            try
            {
                if (!_userRepository.Authorize(user_id, password))
                    throw new Exception("User isnt correct");
                return JsonSerializer.Serialize(new StartupRecomendationOutDTO() { message = "success", startups = _startupRepository.GetStartupIDsRecomandationByUserID(user_id, limit) });
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new StartupRecomendationOutDTO() { message = ex.Message });
            }
        }
        [HttpGet("allUsersForStartupsGet")]
        public string Startups(int user_id, string password, int startup_id, int limit)
        {
            try
            {
                if (!_userRepository.Authorize(user_id, password))
                    throw new Exception("User isnt correct");
                var startups = _startupRepository.GetAllUserIDsRecomandationByStartupID(startup_id, limit);
                return JsonSerializer.Serialize(new StartupRecomendationOutDTO() { message = "success", startups = startups});
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new StartupRecomendationOutDTO() { message = ex.Message });
            }
        }
        [HttpGet("usersForStartupsBySpecialistIDGet")]
        public string Startups(int user_id, string password, int startup_id, int specialistID, int limit)
        {
            try
            {
                if (!_userRepository.Authorize(user_id, password))
                    throw new Exception("User isnt correct");
                return JsonSerializer.Serialize(new StartupRecomendationOutDTO() { message = "success", startups = _startupRepository.GeUserIDsRecomandationByStartupIDSpecialIDs(startup_id, specialistID, limit) });
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new StartupRecomendationOutDTO() { message = ex.Message });
            }
        }
    }
}
