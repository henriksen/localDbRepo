using System;
using System.ComponentModel.DataAnnotations;

namespace LocalDbRepo.Pipelines.Entities
{
    public class Conversation
    {
        [Key]
        public Guid ConversationId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        public User Customer { get; set; }

    }
}
