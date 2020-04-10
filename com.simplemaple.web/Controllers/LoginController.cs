using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using com.simplemaple.service.contracts;
using com.simplemaple.service.contracts.Dtos;
using com.simplemaple.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace com.simplemaple.web.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private ILogger<LoginController> _logger;
        private JwtSettings _jwtSettings;
        private IUserService _userService;

        public LoginController(ILogger<LoginController> logger,
            IOptions<JwtSettings> jwtSetting,
            IUserService userService)
        {
            this._jwtSettings = jwtSetting.Value;
            this._userService = userService;
            this._logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login([FromBody]LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            UserDto userdto = new UserDto();
            userdto.UserName = "admin";
            userdto.PassWord = "admin";

            if (!_userService.ValidateUser(userdto))
            {
                return BadRequest();
            }

            var claim = new Claim[] {
                    new Claim(ClaimTypes.Name,viewModel.UserName),
                    new Claim(ClaimTypes.Role, "admin" ),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claim, DateTime.Now, DateTime.Now.AddMinutes(30), creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        /// <summary>
        /// 通过微信小程序第三方对接
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LoginByWechat()
        {
            

            
        }
    }
}