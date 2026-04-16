  using System;
using System.Linq;
using System.Text.Json;

namespace UserService.Core.Helpers
{
    public static class JwtHelper
    {
        public static Guid GetUserIdFromToken(string token)
        {
            try
            {
                if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = token.Substring(7);
                }

                var parts = token.Split('.');
                if (parts.Length != 3)
                    return Guid.Empty;

                var payload = parts[1];
                var json = DecodeBase64Url(payload);
                
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                
                var claimNames = new[] { 
                    "nameid", 
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                    "sub"
                };
                
                foreach (var claimName in claimNames)
                {
                    if (root.TryGetProperty(claimName, out var element))
                    {
                        var userId = element.GetString();
                        if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var guid))
                            return guid;
                    }
                }
                
                return Guid.Empty;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        private static string DecodeBase64Url(string base64Url)
        {
            string base64 = base64Url.Replace('-', '+').Replace('_', '/');
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            var bytes = Convert.FromBase64String(base64);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}