using System;

namespace MvcClient.Models
{
    public class UserProxyView
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public string AccountStatusId { get; set; }

        public DateTime AccountCreatedDate { get; set; }

        public DateTime? LastLogonDate { get; set; }

        public string UserName { get; set; }

        public string ProxyUserRelationId { get; set; }

        public string ProxyUserRelation { get; set; }

        public string PatId { get; set; }

        public int AccessLevel { get; set; }

        public string Mrn { get; set; }

        public string PatientName { get; set; }
    }
}
