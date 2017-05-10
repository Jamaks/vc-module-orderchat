using Jamak.OrderChatModule.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using VirtoCommerce.Platform.Core.Security;

namespace Jamak.OrderChatModule.Web.Controllers.Api
{
    [RoutePrefix("api/order/chat")]
    public class OrderChatController : ApiController
    {
        private readonly IOrderChatService _orderChatService;
        private readonly ISecurityService _securityService;
        public OrderChatController(IOrderChatService orderChatService, ISecurityService securityService)
        {
            _orderChatService = orderChatService;
            _securityService = securityService;
        }
        #region Room methods
        /// <summary>
        /// Get all messages from room
        /// </summary>
        /// <param name="RoomId">RoomId or OrderId</param>
        /// <returns></returns>
        [HttpPost]
        [Route("room/messages/{RoomId}")]
        public IHttpActionResult GetRoomMessages(string RoomId)
        {
            var ident = (ClaimsIdentity)User.Identity;
            var UserId = ident.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);
            return Ok(_orderChatService.GetRoomMessage(RoomId, UserId == null ? null : UserId.Value));
        }
        /// <summary>
        /// Create room
        /// </summary>
        /// <param name="RoomId">RoomId or OrderId</param>
        /// <returns></returns>
        [HttpPost]
        [Route("room/create/{RoomId}")]
        public IHttpActionResult CreateRoom(string RoomId)
        {
            return Ok(_orderChatService.CreateRoom(RoomId));
        }

        /// <summary>
        /// Get room info
        /// </summary>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("room/info/{RoomId}")]
        public IHttpActionResult RoomInfo(string RoomId)
        {
            return Ok(_orderChatService.RoomInfo(RoomId));
        } 
        #endregion

        #region Subscribe methods
        [HttpPost]
        [Route("room/Subscribe/{RoomId}")]
        public IHttpActionResult RoomSubscribe(string RoomId)
        {
            var ident = (ClaimsIdentity)User.Identity;
            var UserId = ident.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);
            _orderChatService.SubscribeRoom(RoomId, UserId == null ? null : UserId.Value);
            return Ok();
        }
        [HttpPost]
        [Route("room/UnSubscribe/{RoomId}")]
        public IHttpActionResult RoomUnSubscribe(string RoomId)
        {
            var ident = (ClaimsIdentity)User.Identity;
            var UserId = ident.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);
            _orderChatService.UnSubscribeRoom(RoomId, UserId == null ? null : UserId.Value);
            return Ok();
        }
        #endregion

        #region Message methods
        /// <summary>
        /// Add new message to room
        /// </summary>
        /// <param name="RoomId"></param>
        /// <param name="Text"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("room/message/add")]
        public IHttpActionResult AddMessage(string RoomId, string Text)
        {
            var ident = (ClaimsIdentity)User.Identity;
            var UserId = ident.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);

            return Ok(_orderChatService.AddMessage(RoomId, Text, (UserId == null ? null : UserId.Value), User.Identity.Name));
        }
        /// <summary>
        /// Delete message from room
        /// </summary>
        /// <param name="MessageId"></param>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("room/{RoomId}/message/delete/{MessageId}")]
        public IHttpActionResult DeleteMessage(string RoomId, string MessageId)
        {
            _orderChatService.DeleteMessage(RoomId, MessageId);
            return Ok();
        } 
        #endregion
    }
}
