using System;
using System.ComponentModel.DataAnnotations;

namespace AuthApi.Dtos
{
    public class RegisterResponse{
        public bool Success{get;set;}
        public string AccessToken{get;set;}=string.Empty;
        public string Email{get;set;}=string.Empty;
        public string UserId{get;set;}=string.Empty;
        public string Message{get;set;}=string.Empty;
    }
}