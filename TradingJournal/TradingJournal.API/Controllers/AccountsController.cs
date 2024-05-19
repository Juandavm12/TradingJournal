using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using TradingJournal.API.Helpers;
using TradingJournal.Shared.DTOs;
using TradingJournal.Shared.Entities;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using TradingJournal.API.Data;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs.Models;


namespace TradingJournal.API.Controllers
{
   
    [ApiController]
     [Route("/api/accounts")]   
    public class AccountsController : ControllerBase
    {
            private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IFileStorage _fileStorage;
        private readonly IMailHelper _mailHelper;
        private readonly DataContext _context;
        private readonly string _container;

        public AccountsController(IUserHelper userHelper, IConfiguration configuration, IFileStorage fileStorage, IMailHelper mailHelper, DataContext context)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _fileStorage = fileStorage;
            _mailHelper = mailHelper;
            _context = context;
            _container = "users";
        }

        [HttpPost("RecoverPassword")]
        public async Task<ActionResult> RecoverPassword([FromBody] EmailDto model)
        {
            User user = await _userHelper.GetUserAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
            var tokenLink = Url.Action("ResetPassword", "accounts", new
            {
                userid = user.Id,
                token = myToken
            }, HttpContext.Request.Scheme, _configuration["UrlWEB"]);

            var response = _mailHelper.SendMail(user.FullName, user.Email!,
                $"Trading Journal - Password Recovery",
                $"<h1>Trading Journal - Password Recovery</h1>" +
                $"<p>To recover your password, please click 'Recover Password'':</p>" +
                $"<b><a href ={tokenLink}>Recover Password</a></b>");

            if (response.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.Message);
        }

       

        [HttpPost("changePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault()!.Description);
            }

            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Photo))
                {
                    var photoUser = Convert.FromBase64String(user.Photo);
                    user.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
                }

                var currentUser = await _userHelper.GetUserAsync(user.Email!);
                if (currentUser == null)
                {
                    return NotFound();
                }

                currentUser.Document = user.Document;
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.Address = user.Address;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Photo = !string.IsNullOrEmpty(user.Photo) && user.Photo != currentUser.Photo ? user.Photo : currentUser.Photo;
              


                var result = await _userHelper.UpdateUserAsync(currentUser);
                if (result.Succeeded)
                {
                    return Ok(BuildToken(currentUser));
                }

                return BadRequest(result.Errors.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("admin")]       
        public async Task<ActionResult> Putadmin(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Photo))
                {
                    var photoUser = Convert.FromBase64String(user.Photo);
                    user.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
                }

                var currentUser = await _userHelper.GetUserAsync(user.Email!);
                if (currentUser == null)
                {
                    return NotFound();
                }

                currentUser.Document = user.Document;
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.Address = user.Address;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Photo = !string.IsNullOrEmpty(user.Photo) && user.Photo != currentUser.Photo ? user.Photo : currentUser.Photo;
                currentUser.UserType = user.UserType;


                var result = await _userHelper.UpdateUserAsync(currentUser);
                if (result.Succeeded)
                {
                    return Ok(BuildToken(currentUser));
                }

                return BadRequest(result.Errors.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Get()
        {
            return Ok(await _userHelper.GetUserAsync(User.Identity!.Name!));
        }



       

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] UserDTO model)
        {
            
            User user = model;
            if (!string.IsNullOrEmpty(model.Photo))
            {
                var photoUser = Convert.FromBase64String(model.Photo);
                model.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
            }

            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());
               var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                var tokenLink = Url.Action("ConfirmEmail", "accounts", new
                {
                    userid = user.Id,
                    token = myToken
                }, HttpContext.Request.Scheme, _configuration["UrlWEB"]);

                var response = _mailHelper.SendMail(user.FullName, user.Email!,
                    $"Trading Journal - Account Confirmation",
                    $"<h1>Trading Journal - Account Confirmation</h1>" +
                    $"<p>To enable the user, please click 'Confirm Email'':</p>" +
                    $"<b><a href ={tokenLink}>Confirm Email</a></b>");

                if (response.IsSuccess)
                {
   
                    return NoContent();
                }

                return BadRequest(response.Message);
            }

            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("ResedToken")]
        public async Task<ActionResult> ResedToken([FromBody] EmailDto model)
        {
            User user = await _userHelper.GetUserAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            //TODO: Improve code 
            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            var tokenLink = Url.Action("ConfirmEmail", "accounts", new
            {
                userid = user.Id,
                token = myToken
            }, HttpContext.Request.Scheme, _configuration["UrlWEB"]);

            var response = _mailHelper.SendMail(user.FullName, user.Email!,
                   $"Trading Journal - Account Confirmation",
                   $"<h1>Trading Journal - Account Confirmation</h1>" +
                   $"<p>To enable the user, please click 'Confirm Email'':</p>" +
                   $"<b><a href ={tokenLink}>Confirm Email</a></b>");

            if (response.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.Message);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmailAsync(string userId, string token)
        {
            token = token.Replace(" ", "+");
            var user = await _userHelper.GetUserAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault());
            }

            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await _userHelper.LoginAsync(model);
            if (result.Succeeded)
            {
                var user = await _userHelper.GetUserAsync(model.Email);
                return Ok(BuildToken(user));
            }

            if (result.IsLockedOut)
            {
                return BadRequest("You have exceeded the maximum number of attempts, your account is locked, try again in 5 minutes.");
            }

            if (result.IsNotAllowed)
            {
                return BadRequest(" The user has not been enabled, you must follow the instructions in the email sent to you, to enable the user.<br><a class=\"bbtn btn-link\" href=\"/ResendToken\">Resend Account Activation Email</a>");
            }

            return BadRequest("Wrong email or password.<br><p><a class=\"bbtn btn-link\" href=\"/RecoverPassword\">Forgot your password?</a></p>");
        }

        private TokenDTO BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim("Document", user.Document),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Address", user.Address),
                new Claim("Photo", user.Photo ?? string.Empty)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        [HttpPost("ResetPassword")]

        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDTO model)

        {

            User user = await _userHelper.GetUserAsync(model.Email);

            if (user == null)

            {

                return NotFound();

            }

            var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)

            {

                return NoContent();

            }

            return BadRequest(result.Errors.FirstOrDefault()!.Description);

        }


    }
}