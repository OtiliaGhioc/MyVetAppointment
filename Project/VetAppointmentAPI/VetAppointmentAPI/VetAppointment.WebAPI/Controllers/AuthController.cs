using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.Domain.Helpers;
using VetAppointment.WebAPI.Auth;
using VetAppointment.WebAPI.DTOs.AuthDtos;

namespace VetAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly JWTUtil jwtUtil;
        private readonly string? PasswordHasherSecret;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.PasswordHasherSecret = (string?)configuration.GetValue(typeof(string), "PasswordHasher:Secret", null);

            string jwtSecret = (string?)configuration.GetValue(typeof(string), "JWT:Secret", null) ??
                throw new ArgumentNullException("JWT:Secret");
            string jwtIssuer = (string?)configuration.GetValue(typeof(string), "JWT:Issuer", null) ??
                throw new ArgumentNullException("JWT:Issuer");
            string jwtAudience = (string?)configuration.GetValue(typeof(string), "JWT:Audience", null) ??
                throw new ArgumentNullException("JWT:Audience");
            int jwtAccessTokenValidity = (int?)configuration.GetValue(typeof(int), "JWT:AccessTokenValidityInMinutes", null) ??
                throw new ArgumentNullException("JWT:AccessTokenValidityInMinutes");
            int jwtRefreshTokenValidity = (int?)configuration.GetValue(typeof(int), "JWT:RefreshTokenValidityInDays", null) ??
                throw new ArgumentNullException("JWT:RefreshTokenValidityInDays");

            this.jwtUtil = new JWTUtil(jwtSecret, jwtIssuer, jwtAudience, jwtAccessTokenValidity, jwtRefreshTokenValidity);
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLoginAsync([FromBody] UserAuthDto authDto)
        {
            if (authDto.Username == null || authDto.Password == null)
                return BadRequest("Invalid request data");

            User? user = (await userRepository.Find(item => item.Username == authDto.Username)).FirstOrDefault();

            if (user == null)
                return NotFound();

            if (!user.IsPasswordValid(authDto.Password, PasswordHasherSecret))
            {
                return Unauthorized("Invalid credentials");
            }

            Tuple<string?, string?> tokens = jwtUtil.GetTokensForUser(user);
            if (tokens.Item1 == null || tokens.Item2 == null)
                return StatusCode(500, "Could not generate access and/or refresh tokens for user");

            return Ok(new AuthResponseDto(tokens.Item1, tokens.Item2));
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegister([FromBody] UserAuthDto authDto)
        {
            if (authDto.Username == null || authDto.Password == null)
                return BadRequest("Invalid request data");

            User? user = (await userRepository.Find(item => item.Username == authDto.Username)).FirstOrDefault();

            if (user != null)
                return BadRequest("Username already registered");

            string hashedPassword = PasswordHasher.GetHashedPassword(authDto.Password, PasswordHasherSecret);

            User createUser = new User(authDto.Username, hashedPassword, authDto.IsMedic);
            User createdUser = await userRepository.Add(createUser);
            await userRepository.SaveChanges();

            Tuple<string?, string?> tokens = jwtUtil.GetTokensForUser(createdUser);
            if (tokens.Item1 == null || tokens.Item2 == null)
                return StatusCode(500, "Could not generate access and/or refresh tokens for user");

            return Ok(new AuthResponseDto(tokens.Item1, tokens.Item2));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> TokenRefresh([FromBody] RefreshTokenDto refreshDto)
        {
            if (refreshDto.RefreshToken == null)
                return BadRequest("Invalid request body");

            if (!jwtUtil.IsTokenValid(refreshDto.RefreshToken))
                return Unauthorized("Invalid token provided");
            string? accessToken = jwtUtil.GenerateNewAccessToken(refreshDto.RefreshToken);

            if (accessToken == null)
                return StatusCode(500, "Could not generate access token from refresh token");

            return Ok(new RefreshResponseDto(accessToken));
        }
    }
}
