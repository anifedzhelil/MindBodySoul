﻿namespace MindBodySoul.Models.DTO
{
    public class RegisterRequestDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }    
    }
}