using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace blog_common.Utils
{
    public static class JwtUtil
    {
        /**
         * 生成jwt
         * 使用Hs256算法, 私匙使用固定秘钥
         *
         * @param secretKey jwt秘钥
         * @param expire jwt过期时间(毫秒)
         * @param claims    设置的信息
         * @return Claims
         */
        public static string CreateJwt(string secretKey, long expire, Dictionary<string, object> claims)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var expireTime = now.AddMilliseconds(expire);

            var claimList = new List<Claim>();
            foreach (var kv in claims)
            {
                claimList.Add(new Claim(kv.Key, kv.Value.ToString()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = creds,
                Expires = expireTime,
                Subject = new ClaimsIdentity(claimList)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        /**
        * Token解密
        *
        * @param secretKey jwt秘钥 此秘钥一定要保留好在服务端, 不能暴露出去, 否则sign就可以被伪造, 如果对接多个客户端建议改造成多个
        * @param token     加密后的token
        * @return Claims
        */

        public static Dictionary<string, object> ParseJwt(string secretKey, string token)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(keyBytes);

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            var handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(token, validationParams, out var validatedToken);
            var jwtToken = validatedToken as JwtSecurityToken;

            var result = new Dictionary<string, object>();
            foreach (var claim in jwtToken!.Claims)
            {
                result[claim.Type] = claim.Value;
            }
            return result;
        }
    }
}
