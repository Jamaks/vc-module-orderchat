using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamak.OrderChatModule.Web.Model;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;
using VirtoCommerce.Platform.Data.Infrastructure;
using System.Data.Entity;

namespace Jamak.OrderChatModule.Web
{
    public class OrderChatRepository : EFRepositoryBase, IOrderChatRepository
    {
        public OrderChatRepository()
        {

        }
        public OrderChatRepository(string nameOrConnectionString, params IInterceptor[] interceptors)
             : base(nameOrConnectionString, null, interceptors)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ChatRoom>().HasKey(x => x.Id)
                .Property(x => x.Id);
            modelBuilder.Entity<ChatRoom>().ToTable("ChatRoom");

            modelBuilder.Entity<ChatMessage>().HasKey(x => x.Id)
                 .HasRequired(c => c.ChatRoom)
               .WithMany(c => c.ChatMessages);
            modelBuilder.Entity<ChatMessage>().ToTable("ChatMessage");

            modelBuilder.Entity<ChatUserSubscriber>().HasKey(x => x.Id)
                .HasRequired(c => c.ChatRoom)
                .WithMany(c => c.ChatUserSubscribers);
            modelBuilder.Entity<ChatUserSubscriber>().ToTable("ChatUserSubscriber");

            modelBuilder.Entity<ChatUserSubscriberNewMessage>().HasKey(x => x.Id)
                .HasRequired(s => s.ChatUserSubscriber)
                .WithMany(s => s.ChatUserSubscriberNewMessages);
            modelBuilder.Entity<ChatUserSubscriberNewMessage>().ToTable("ChatUserSubscriberNewMessage");

            base.OnModelCreating(modelBuilder);
        }
        public IQueryable<ChatMessage> ChatMessages
        {
            get { return GetAsQueryable<ChatMessage>(); }
        }

        public IQueryable<ChatRoom> ChatRooms
        {
            get { return GetAsQueryable<ChatRoom>(); }
        }

        public IQueryable<ChatUserSubscriber> ChatUserSubscribers
        {
            get { return GetAsQueryable<ChatUserSubscriber>(); }
        }
    }
}
