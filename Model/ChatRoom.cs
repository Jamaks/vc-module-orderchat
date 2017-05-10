using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Common;

namespace Jamak.OrderChatModule.Web.Model
{
    public class ChatRoom : AuditableEntity
    {
        public string OrderId { get; set; }
        public ICollection<ChatMessage> ChatMessages { get;set;}
        public ICollection<ChatUserSubscriber> ChatUserSubscribers { get; set; }
        public ChatRoom()
        {
            ChatMessages = new List<ChatMessage>();
            ChatUserSubscribers = new List<ChatUserSubscriber>();
        }
    }
}
