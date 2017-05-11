using Jamak.OrderChatModule.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Common;

namespace Jamak.OrderChatModule.Web
{
    public interface IOrderChatRepository: IRepository, IDisposable
    {
        IQueryable<ChatMessage> ChatMessages { get; }
        IQueryable<ChatRoom> ChatRooms { get; }
        IQueryable<ChatUserSubscriber> ChatUserSubscribers { get; }
        IQueryable<ChatUserSubscriberNewMessage> ChatUserSubscriberNewMessages { get; }

    }
}
