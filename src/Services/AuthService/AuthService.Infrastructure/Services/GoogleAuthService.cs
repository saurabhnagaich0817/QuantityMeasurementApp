using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AuthService.Core.Interfaces;

namespace AuthService.Infrastructure.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly HttpClient _httpClient;

        public GoogleAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GoogleUserInfo?> VerifyGoogleTokenAsync(string idToken)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"https://oauth2.googleapis.com/tokeninfo?id_token={idToken}");
                
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<GoogleUserInfoResponse>(json);

                if (userInfo == null)
                    return null;

                return new GoogleUserInfo
                {
                    Email = userInfo.Email,
                    Name = userInfo.Name,
                    GoogleId = userInfo.Sub
                };
            }
            catch
            {
                return null;
            }
        }

        private class GoogleUserInfoResponse
        {
            public string Email { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Sub { get; set; } = string.Empty;
        }
    }
}