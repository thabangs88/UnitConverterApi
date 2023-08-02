﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using UnitConverterAPI.Options;

namespace UnitConverterAPI.Extensions
{
    public static class TokenOptionsExtensions
    {
        public static DateTime GetExpiration(this TokenOptions options) => DateTime.UtcNow.Add(options.ValidFor);

        public static SigningCredentials GetSigningCredentials(this TokenOptions options) => new SigningCredentials(options.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

        public static SymmetricSecurityKey GetSymmetricSecurityKey(this TokenOptions options) => new SymmetricSecurityKey(options.GetSigningKeyBytes());

        private static byte[] GetSigningKeyBytes(this TokenOptions options) => Encoding.ASCII.GetBytes(options.SigningKey);
    }
}
