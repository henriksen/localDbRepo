using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LocalDbRepo.Pipelines
{
    public class GetConversationByIdHandler
    {
        private readonly CommunicationContext communicationContext;

        public GetConversationByIdHandler(CommunicationContext communicationContext)
        {
            this.communicationContext = communicationContext;
        }

        public async Task<ConversationResponse> Handle(GetConversationByIdRequest request, CancellationToken cancellationToken)
        {
            var response = await communicationContext.Conversations
                                            .Select(c => new ConversationResponse
                                            {
                                                ConversationId = c.ConversationId,
                                                Customer = new ConversationResponse.Person
                                                {
                                                    FirstName = c.Customer.FirstName,
                                                    LastName = c.Customer.LastName
                                                }
                                            }).SingleOrDefaultAsync(c => c.ConversationId == request.ConversationId);
            if (response is null)
                throw new KeyNotFoundException($"Conversation {request.ConversationId} not found");
            return response;

        }

    }
}
