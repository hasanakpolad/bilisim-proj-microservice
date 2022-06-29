using Microsoft.IdentityModel.Tokens;
using SkiPass.Auth.Models;
using SkiPass.User.Data.Models;
using SkiPass.User.DataAccess.MongoRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkiPass.Auth.Helper
{
    public class AuthHelper : Attribute
    {
        public AuthModel Authenticate(string kullaniciAdi, string sifre)
        {
            _Appsettings appSettings = new _Appsettings();
            appSettings.Secret = Environment.GetEnvironmentVariable("SecurityKey");
            using (var repo = new MongoRepository<AuthModel>())
            {

                var user = repo.Get(x => x.KullaniciAdi.Equals(kullaniciAdi) && x.Sifre.Equals(sifre));

                // Kullanici bulunamadıysa null döner.
                if (user == null)
                    return null;

                // Authentication(Yetkilendirme) başarılı ise JWT token üretilir.
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                // Sifre null olarak gonderilir.
                user.Sifre = null;

                return user;
            }
        }
        class _Appsettings
        {
            public string Secret { get; set; }
        }
    }
}
