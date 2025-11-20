using MongoDB.Driver;
using Libreria.GraphQL.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Libreria.GraphQL.Auth;
using Microsoft.Extensions.Configuration;

namespace GraphQL_Api.GraphQL;
{
    public class Mutation
    {
        // Ya no pedimos "secret" por argumento; lo leemos del entorno
        public string Login(string email, [Service] IConfiguration config)
        {
            var secret = config["JWT_SECRET"] ?? "dev_secret";
            return JwtService.Issue(email, secret);
        }

        [Authorize]
        public async Task<Note> CreateNote(string title, string? content, [Service] IMongoCollection<Note> notes, ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email)?.Value ?? "anon@example.com";
            var note = new Note { Title = title, Content = content, OwnerEmail = email, CreatedAt = DateTime.UtcNow };
            await notes.InsertOneAsync(note);
            return note;
        }
    }
}
