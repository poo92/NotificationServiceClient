using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServiceClient
{
    public class  NotificationRequest
    {
        
        public string Subject { get; set; } // Validate: Cannot be null
        
        public string PlainMessage { get; set; } // Validate: Cannot be null
        public string FormattedMessage { get; set; }
        public string ApplicationId { get; set; } // Considered to be removed: May not be needed since client credentials auth will tell api who the calling application is
        public NotificationMessageFormat? FormattedMessageType { get; set; }
      
        public Guid? RecipientId { get; set; }
        public Guid? SenderId { get; set; }
        public string OldEmailAddress { get; set; }

        //public static implicit operator NotificationRequest(NotificationRequest v)
        //{
        //    throw new NotImplementedException();
        //}
    }
    public enum NotificationMessageFormat
    {
        Html = 0
    }
}
