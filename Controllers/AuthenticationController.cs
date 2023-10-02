using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AuthApi.Dtos;
using AuthApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver.Linq;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/v1/authenticate")]
    public class AuthenticationController:ControllerBase{
        private readonly UserManager<User> _userManager;
        public AuthenticationController(UserManager<User>userManager){
            _userManager = userManager;
        }
        [HttpPost]
        [Route("login")]
        [ProducesResponseType((int)HttpStatusCode.OK,Type=typeof(LoginResponse))]
        public async Task<IActionResult>Login([FromBody]LoginRequest request){
            var result = await LoginAsync(request);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        private async Task<LoginResponse>LoginAsync(LoginRequest request){
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)return new LoginResponse{Message="Invalid email/password",Success=false};

            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x=>new Claim(ClaimTypes.Role,x));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lswek3u4uo2u4"));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expires =DateTime.Now.AddMinutes(30);

            var token = new JwtSecurityToken(
                issuer:"https://localhost:5001",
                audience:"https://localhost:5001",
                claims:claims,
                expires:expires,
                signingCredentials:creds
            );
            return new LoginResponse{
                AccessToken=new JwtSecurityTokenHandler().WriteToken(token),
                Message="Login Successful",
                Email=user?.Email,
                Success=true,
                UserId=user?.Id.ToString()
            };
        }
    }
}