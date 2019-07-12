using System;

namespace LocalDbRepo.Pipelines
{
    public class ConversationResponse
    {
        public Guid ConversationId { get; set; }
        public Person Customer { get; set; }
        public Person AssignedEmployee { get; set; }

        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

    }
}
