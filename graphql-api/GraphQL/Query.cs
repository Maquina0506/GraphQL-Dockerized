using MongoDB.Driver;
using Libreria.GraphQL.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GraphQL_Api.GraphQL;
{
    public class Query
    {
        [Authorize]
        public async Task<IEnumerable<Note>> MyNotes([Service] IMongoCollection<Note> notes, ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var filter = Builders<Note>.Filter.Eq(x => x.OwnerEmail, email);
            var cursor = await notes.FindAsync(filter);
            return await cursor.ToListAsync();
        }
    }
}
