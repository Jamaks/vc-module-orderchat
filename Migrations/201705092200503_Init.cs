namespace Jamak.OrderChatModule.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatRoom",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrderId = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 64),
                        ModifiedBy = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChatMessage",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreaterUserId = c.String(nullable: false),
                        Text = c.String(maxLength: 2048),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 64),
                        ModifiedBy = c.String(maxLength: 64),
                        ChatRoom_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatRoom", t => t.ChatRoom_Id, cascadeDelete: true)
                .Index(t => t.ChatRoom_Id);
            
            CreateTable(
                "dbo.ChatUserSubscriber",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false),
                        ChatRoom_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatRoom", t => t.ChatRoom_Id, cascadeDelete: true)
                .Index(t => t.ChatRoom_Id);
            
            CreateTable(
                "dbo.ChatUserSubscriberNewMessage",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MessageId = c.String(),
                        ChatUserSubscriber_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatUserSubscriber", t => t.ChatUserSubscriber_Id, cascadeDelete: true)
                .Index(t => t.ChatUserSubscriber_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatUserSubscriberNewMessage", "ChatUserSubscriber_Id", "dbo.ChatUserSubscriber");
            DropForeignKey("dbo.ChatUserSubscriber", "ChatRoom_Id", "dbo.ChatRoom");
            DropForeignKey("dbo.ChatMessage", "ChatRoom_Id", "dbo.ChatRoom");
            DropIndex("dbo.ChatUserSubscriberNewMessage", new[] { "ChatUserSubscriber_Id" });
            DropIndex("dbo.ChatUserSubscriber", new[] { "ChatRoom_Id" });
            DropIndex("dbo.ChatMessage", new[] { "ChatRoom_Id" });
            DropTable("dbo.ChatUserSubscriberNewMessage");
            DropTable("dbo.ChatUserSubscriber");
            DropTable("dbo.ChatMessage");
            DropTable("dbo.ChatRoom");
        }
    }
}
