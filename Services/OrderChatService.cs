using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamak.OrderChatModule.Web.Model;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Core.Common;

namespace Jamak.OrderChatModule.Web.Services
{
    public class OrderChatService: ServiceBase,IOrderChatService
    {
        public OrderChatService(Func<IOrderChatRepository> orderChatRepository)
        {
            RepositoryFactory = orderChatRepository;
        }
        protected Func<IOrderChatRepository> RepositoryFactory { get; private set; }

        public ChatMessage AddMessage(string OrderId, string Message, string UserCreaterId, string CreatedBy)
        {
            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = RepositoryFactory())
            {
                var chatRoom = repository.ChatRooms.FirstOrDefault(r => r.Id == OrderId);
                if (chatRoom != null)
                {
                    var newMess = new ChatMessage() { Id=Guid.NewGuid().ToString(),Text = Message ,CreaterUserId=UserCreaterId,CreatedDate=DateTime.Now, CreatedBy = CreatedBy };
                    chatRoom.ChatMessages.Add(newMess);

                    // add new messages to subscribe users
                    foreach (var subscriber in chatRoom.ChatUserSubscribers)
                    {
                        subscriber.ChatUserSubscriberNewMessages.Add(new ChatUserSubscriberNewMessage() { Id=Guid.NewGuid().ToString(),MessageId=newMess.Id});
                        repository.Attach(subscriber);
                    }
                    repository.Attach(chatRoom);

                    CommitChanges(repository);
                    pkMap.ResolvePrimaryKeys();
                    return newMess;
                }
            }
            throw new NotImplementedException();
        }

        public ChatRoom CreateRoom(string OrderId)
        {
            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = RepositoryFactory())
            {
                var chatRoom = repository.ChatRooms.FirstOrDefault(r => r.Id == OrderId);
                if (chatRoom == null)
                {
                    var newChatRoom = new ChatRoom() { Id = OrderId, CreatedDate = DateTime.Now};
                    
                    repository.Add(newChatRoom);

                    CommitChanges(repository);
                    pkMap.ResolvePrimaryKeys();
                    return newChatRoom;
                }
            }
            return null;
        }

        public void DeleteMessage(string OrderId, string MessageId)
        {
            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = RepositoryFactory())
            {
                var chatRoom = repository.ChatRooms.FirstOrDefault(r => r.Id == OrderId);
                if (chatRoom != null)
                {
                    var message = chatRoom.ChatMessages.FirstOrDefault(m => m.Id == MessageId);
                    if (message != null)
                    {
                        repository.Remove(message);
                        CommitChanges(repository);
                        pkMap.ResolvePrimaryKeys();
                    }
                    
                }
            }
        }

        public List<ChatMessage> GetRoomMessage(string OrderId, string UserId)
        {
            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = RepositoryFactory())
            {
                var chatRoom = repository.ChatRooms.FirstOrDefault(r => r.Id == OrderId);
                if (chatRoom != null)
                {
                    // remove new messages from subscribe users
                    var newUserMessages = chatRoom.ChatUserSubscribers.Where(s => s.UserId == UserId);
                    repository.Remove(newUserMessages);
                    CommitChanges(repository);

                    //TODO: return dynamic { isNew:true, message:{...}}

                    return chatRoom.ChatMessages.OrderBy(m => m.CreatedDate).ToList();
                }
                return null;
            }
        }

        public dynamic RoomInfo(string RoomId)
        {
            using (var repository = RepositoryFactory())
            {
                var chatRoom = repository.ChatRooms.FirstOrDefault(r => r.Id == RoomId);
                dynamic obj = new System.Dynamic.ExpandoObject();
                if (chatRoom != null)
                {
                    obj.Id = RoomId;
                    obj.LastMessage = chatRoom.ChatMessages.OrderByDescending(p => p.CreatedDate).FirstOrDefault();
                    obj.CountMessages = chatRoom.ChatMessages.Count();
                }
                else
                {
                    var room = CreateRoom(RoomId);
                    obj.Id = RoomId;
                    obj.LastMessage = null;
                    obj.CountMessages = 0;
                }
                return obj;
            }
        }

        public void SubscribeRoom(string OrderId, string UserId)
        {
            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = RepositoryFactory())
            {
                var chatRoom = repository.ChatRooms.FirstOrDefault(r => r.Id == OrderId);
                if (chatRoom != null)
                {
                    var subscriber = chatRoom.ChatUserSubscribers.FirstOrDefault(p => p.UserId == UserId);
                    if (subscriber == null)
                    {
                        var newSubscriber = new ChatUserSubscriber() { Id = Guid.NewGuid().ToString(), UserId = UserId };
                        chatRoom.ChatUserSubscribers.Add(newSubscriber);

                        repository.Attach(chatRoom);
                        CommitChanges(repository);
                        pkMap.ResolvePrimaryKeys();
                    }
                }
            }
        }

        public void UnSubscribeRoom(string OrderId, string UserId)
        {
            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = RepositoryFactory())
            {
                var chatRoom = repository.ChatRooms.FirstOrDefault(r => r.Id == OrderId);
                if (chatRoom != null)
                {
                    var subscriber = chatRoom.ChatUserSubscribers.FirstOrDefault(p => p.UserId == UserId);
                    if (subscriber != null)
                    {
                        chatRoom.ChatUserSubscribers.Remove(subscriber);

                        repository.Attach(chatRoom);
                        CommitChanges(repository);
                        pkMap.ResolvePrimaryKeys();
                    }
                }
            }
        }
    }
}
