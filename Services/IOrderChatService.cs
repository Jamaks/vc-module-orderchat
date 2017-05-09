using Jamak.OrderChatModule.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamak.OrderChatModule.Web.Services
{
    public interface IOrderChatService
    {
        ChatRoom CreateRoom(string OrderId);
        ChatMessage AddMessage(string OrderId, string Message, string UserCreaterId, string CreatedBy);
        void DeleteMessage(string OrderId, string MessageId);
        List<ChatMessage> GetRoomMessage(string OrderId,string UserId);

        void SubscribeRoom(string OrderId, string UserId);
        void UnSubscribeRoom(string OrderId, string UserId);
    }
}
