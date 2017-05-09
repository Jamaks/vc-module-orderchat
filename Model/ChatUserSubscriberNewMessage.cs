using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Common;

namespace Jamak.OrderChatModule.Web.Model
{
    public class ChatUserSubscriberNewMessage: Entity
    {
        public string MessageId { get; set; }
        public virtual ChatUserSubscriber ChatUserSubscriber { get; set; }
    }
}
