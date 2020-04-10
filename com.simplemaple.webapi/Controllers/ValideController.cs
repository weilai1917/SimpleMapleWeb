using com.simplemaple.shared;
using com.simplemaple.webapi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace com.simplemaple.webapi.Controllers
{
    public class ValideController : Controller
    {
        private JwtSettings _jwtSettings;
        private IHttpClientFactory _httpClient;

        public ValideController(IOptions<JwtSettings> jwtSetting,
            IHttpClientFactory httpClient)
        {
            this._jwtSettings = jwtSetting.Value;
            this._httpClient = httpClient;

        }

        public async Task<IActionResult> LoginByWechat(string wxloginCode)
        {
            var code2session = string.Format(ConstStr.wxjscode2session, "");
            var client = _httpClient.CreateClient();
            var connectReuslt = await client.GetAsync(code2session);
            if (connectReuslt == null)
            {
                return BadRequest();
            }

            var response = await connectReuslt.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(response))
            {
                return BadRequest();
            }

            var wxResult = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(response);
            var openid = Convert.ToString(wxResult["openid"]);
            if (string.IsNullOrEmpty(openid))
            {
                return BadRequest();
            }

            var userdto = new { UserId = "123123", OpenId = openid, Role = "admin" };//通过openid拿到用户的基本信息和userid

            var claim = new Claim[] {
                    new Claim(ClaimTypes.Name, userdto.UserId),
                    new Claim(ClaimTypes.Role, userdto.Role ),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claim, DateTime.Now, DateTime.Now.AddMinutes(30), creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }
    }
}