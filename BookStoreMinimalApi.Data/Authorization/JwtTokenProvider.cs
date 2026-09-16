using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BookStoreMinimalApi.Application.Authorization;
using BookStoreMinimalApi.Application.Interfaces.Abstractions.Authorization;
using BookStoreMinimalApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreMinimalApi.Data.Services
{
      public class JwtTokenProvider(IOptions<JwtTokenSettings> jwtSettings) : ITokenProvider
      {
            public string GenerateToken(User user, List<Claim> claims)
            {
                  var secretKey = jwtSettings.Value.SecretKey;
                  var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                  var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


                  List<Claim> userClaims = new List<Claim>()
                 {
                        new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Nickname, user.UserName!),
                        new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email!)
                 };

                  userClaims.AddRange(claims);
                 

                  var descriptor = new SecurityTokenDescriptor()
                  {
                        Subject = new ClaimsIdentity(userClaims),
                        Issuer = jwtSettings.Value.Issuer,
                        Audience = jwtSettings.Value.Audience,
                        Expires = DateTime.UtcNow.AddMinutes(jwtSettings.Value.Expiration),
                        SigningCredentials = credentials
                  };

                  var handler = new JsonWebTokenHandler();

                  string accessToken = handler.CreateToken(descriptor);

                  return accessToken;
            }
      }
}