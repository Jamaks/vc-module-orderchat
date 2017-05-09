using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Common;

namespace Jamak.OrderChatModule.Web.Model
{
    public class ChatMessage: AuditableEntity
    {
        [Required]
        public string CreaterUserId { get; set; }
        [StringLength(2048)]
        public string Text { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
