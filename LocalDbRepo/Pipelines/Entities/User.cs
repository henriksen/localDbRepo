using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocalDbRepo.Pipelines.Entities
{
    public class User
    {
        [Key]
        public Guid SubjectId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string Issuer { get; set; }

        public ICollection<Conversation> ConversationsAsCustomer { get; set; }
        public ICollection<Conversation> AssignedConversations { get; set; }

    }
}
