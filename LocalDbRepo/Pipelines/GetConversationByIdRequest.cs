using System;

namespace LocalDbRepo.Pipelines
{
    public class GetConversationByIdRequest 
    {
        public Guid ConversationId { get; set; }

        public GetConversationByIdRequest(Guid conversationId)
        {
            ConversationId = conversationId;
        }

    }

}
