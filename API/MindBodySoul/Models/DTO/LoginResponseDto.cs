namespace MindBodySoul.Models.DTO
{
    public class LoginResponseDto
    {
        public  required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string Token { get; set; }   
        public required List<string> Roles { get; set; }
    }
}
