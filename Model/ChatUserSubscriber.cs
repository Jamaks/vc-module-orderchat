using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Common;

namespace Jamak.OrderChatModule.Web.Model
{
    public class ChatUserSubscriber: Entity
    {
        [Required]
        public string UserId { get; set; }
        public ICollection<ChatUserSubscriberNewMessage> ChatUserSubscriberNewMessages { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
