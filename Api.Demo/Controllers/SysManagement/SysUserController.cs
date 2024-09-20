using Api.Common.Model;
using Api.IService.SysManagement;
using Api.Model.SysManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Demo.Controllers.SysManagement
{
    [Route("system/user")]
    [ApiController]
    public class SysUserController : BaseController<SysUser, ISysUserService>
    {
        protected readonly IConfiguration _configuration;
        public SysUserController(ILogger<SysUser> logger, ISysUserService service, IConfiguration configuration) : base(logger, service)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="data">登录信息</param>
        /// <returns>返回登陆信息</returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("login")]
        public async Task<Response> Login([FromBody] dynamic data)
        {
            // 1.验证用户账号密码是否正确，
            SysUser d = JsonConvert.DeserializeObject<SysUser>(JsonConvert.SerializeObject(data));
            if (d.UserAccount == null || d.PassWord == null)
            {
                throw new Exception("账号跟密码不能为空！");
            }

            var user = await service.Login(d.UserAccount, d.PassWord);

            // 2.生成JWT
            // Header,选择签名算法
            var signingAlogorithm = SecurityAlgorithms.HmacSha256;
            //Payload,存放用户信息，下面我们放了一个用户id
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,"userId")
            };
            //Signature
            //取出私钥并以utf8编码字节输出
            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]);
            //使用非对称算法对私钥进行加密
            var signingKey = new SymmetricSecurityKey(secretByte);
            //使用HmacSha256来验证加密后的私钥生成数字签名
            var signingCredentials = new SigningCredentials(signingKey, signingAlogorithm);
            //生成Token
            var Token = new JwtSecurityToken(
                    issuer: _configuration["Authentication:Issuer"],        //发布者
                    audience: _configuration["Authentication:Audience"],    //接收者
                    claims: claims,                                         //存放的用户信息
                    notBefore: DateTime.UtcNow,                             //发布时间
                    expires: DateTime.UtcNow.AddHours(1),                   //有效期设置为1小时
                    signingCredentials                                      //数字签名
                );
            //生成字符串token
            var TokenStr = new JwtSecurityTokenHandler().WriteToken(Token);

            return new Response()
            {
                Data = new
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Token = "Bearer " + TokenStr    
                },
                Message = "登录成功！"
            };
        }

    }
}
