using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TaskManager.Domain.DTOs;
using TaskManager.Tests.Fixures;

namespace TaskManager.Tests.Integrations.Controllers
{
    public class AuthControllerTests : IClassFixture<WebAppFactory>
    {
        private readonly HttpClient _client;

        public AuthControllerTests(WebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_ShouldReturn200_WhenValidData()
        {
            // Arrange
            var dto = new RegisterDTO("John Doe", "john@example.com", "Password1!");

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", dto);
            var result = await response.Content.ReadFromJsonAsync<DefaultResponseDTO<string>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result!.Success);
            Assert.Equal("User registered successfully", result?.Message);
        }

        [Fact]
        public async Task Register_ShouldReturn400_WhenEmailAlreadyExists()
        {
            // Arrange
            var dto = new RegisterDTO("John Doe", "duplicate@example.com", "Password1!");
            await _client.PostAsJsonAsync("/api/auth/register", dto); // first register

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/register", dto); // duplicate
            var result = await response.Content.ReadFromJsonAsync<DefaultResponseDTO<string>>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(result!.Success);
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenValidCredentials()
        {
            // Arrange — register first
            var register = new RegisterDTO("Jane Doe", "jane@example.com", "Password1!");
            await _client.PostAsJsonAsync("/api/auth/register", register);

            var login = new LoginDTO("jane@example.com", "Password1!");

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", login);
            var result = await response.Content.ReadFromJsonAsync<DefaultResponseDTO<string>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result!.Success);
            Assert.NotNull(result.Content);
            Assert.NotEmpty(result.Content!);
        }

        [Fact]
        public async Task Login_ShouldReturn401_WhenWrongPassword()
        {
            // Arrange
            var register = new RegisterDTO("Bob", "bob@example.com", "Password1!");
            await _client.PostAsJsonAsync("/api/auth/register", register);

            var login = new LoginDTO("bob@example.com", "WrongPassword1!");

            // Act
            var response = await _client.PostAsJsonAsync("/api/auth/login", login);
            var result = await response.Content.ReadFromJsonAsync<DefaultResponseDTO<string>>();

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized);
            Assert.False(result!.Success);
            Assert.Equal("Invalid credentials", result?.Message);
        }
    }
}