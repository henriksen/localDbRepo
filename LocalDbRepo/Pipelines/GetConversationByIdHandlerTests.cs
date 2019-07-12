using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using LocalDbRepo.Pipelines.Entities;
using Xunit;
using Xunit.Abstractions;

namespace LocalDbRepo.Pipelines {
    public class GetConversationByIdHandlerTests : TestBase {
        private readonly ITestOutputHelper _output;

        public GetConversationByIdHandlerTests (ITestOutputHelper output) {
            _output = output;
        }

        [Fact]
        public async Task Handle_GivenTwoConversations_SelectsJustTheOne () {
            Randomizer.Seed = new Random (54698335);

            using (var database = await LocalDb ()) {
                var testUser = new Faker<User> ()
                    .RuleFor(u => u.SubjectId, f => Guid.NewGuid())
                    .RuleFor (u => u.FirstName, f => f.Name.FirstName ())
                    .RuleFor (u => u.LastName, f => f.Name.LastName ())
                    .RuleFor (u => u.Issuer, f => f.Random.String2 (15))
                    .Generate ();
                var testConversations = new Faker<Conversation> ()
                    .RuleFor (u => u.ConversationId, f => Guid.NewGuid ())
                    .RuleFor (c => c.Customer, f => testUser)
                    .Generate (2);
                await database.AddData (testConversations);

                var handler = new GetConversationByIdHandler (database.Context);
                var response = await handler.Handle (new GetConversationByIdRequest (testConversations[1].ConversationId), CancellationToken.None);

                response.Customer.FirstName.Should ().Be (testUser.FirstName);
            }
        }


        [Fact]
        public async Task Handle_GivenInvalidConversationId_ThrowKeyNotFoundException () {
            using (var database = await LocalDb ()) {
                var testUsers = new Faker<User> ()
                    .RuleFor (u => u.SubjectId, f => Guid.NewGuid ())
                    .RuleFor (u => u.FirstName, f => f.Name.FirstName ())
                    .RuleFor (u => u.LastName, f => f.Name.LastName ())
                    .RuleFor (u => u.Issuer, f => f.Random.String2 (15))
                    .Generate (2);
                var testConversations = new Faker<Conversation> ()
                    .RuleFor (c => c.ConversationId, f => Guid.NewGuid ())
                    .RuleFor (c => c.Customer, f => testUsers[0])
                    .Generate (2);
                await database.AddData (testConversations);

                var handler = new GetConversationByIdHandler (database.Context);
                await Assert.ThrowsAsync<KeyNotFoundException> (async () =>
                    await handler.Handle (new GetConversationByIdRequest (Guid.NewGuid ()), CancellationToken.None));
            }

        }
    }
}