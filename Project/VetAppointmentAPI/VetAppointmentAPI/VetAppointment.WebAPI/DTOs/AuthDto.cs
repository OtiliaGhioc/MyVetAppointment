namespace VetAppointment.WebAPI.DTOs
{
    namespace AuthDtos
    {
        public class UserAuthDto
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
            public bool IsMedic { get; set; } = false;
        }

        public class AuthResponseDto
        {
            public AuthResponseDto(IEnumerable<string> errors) 
            { 
                Errors = errors;
            }

            public AuthResponseDto(string accessToken, string refreshToken) 
            { 
                AccessToken= accessToken;
                RefreshToken= refreshToken;
            }

            public IEnumerable<string>? Errors { get; private set; } = new List<string>();
            public string? AccessToken { get; private set; }
            public string? RefreshToken { get; set; }
        }

        public class RefreshTokenDto
        {
            public string? RefreshToken { get; set; }
        }

        public class RefreshResponseDto
        {
            public RefreshResponseDto(IEnumerable<string> errors)
            {
                Errors = errors;
            }

            public RefreshResponseDto(string accessToken)
            {
                AccessToken = accessToken;
            }

            public IEnumerable<string> Errors { get; private set; } = new List<string>();
            public string? AccessToken { get; private set; }
        }
    }
}
