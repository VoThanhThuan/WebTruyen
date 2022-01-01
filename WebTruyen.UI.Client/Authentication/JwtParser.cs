using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace WebTruyen.UI.Client.Authentication
{
    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJWT(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            ExtractRolesFromJWT(claims, keyValuePairs);

            claims.AddRange(keyValuePairs.Select(x => new Claim(x.Key, x.Value.ToString())));

            return claims;
        }

        private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyvaluePairs)
        {
            keyvaluePairs.TryGetValue(ClaimTypes.Role, out object roles);
            if (roles is not null) {
                var parseRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');
                if (parseRoles.Length > 1) {
                    foreach (var item in parseRoles) {
                        claims.Add(new Claim(ClaimTypes.Role, item.Trim('"')));
                    }
                } else {
                    claims.Add(new Claim(ClaimTypes.Role, parseRoles[0]));
                }

                keyvaluePairs.Remove(ClaimTypes.Role);
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4) {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
