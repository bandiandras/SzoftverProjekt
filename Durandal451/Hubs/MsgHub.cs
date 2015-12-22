using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace gManagerNew.Hubs
{
    [HubName("msgHub")]
    public class MsgHub : Hub
    {
        public static void Send(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MsgHub>();
            context.Clients.All.newMessage(message);
        }

        public static void SendMessageSuccess(string regmessage)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MsgHub>();
            context.Clients.All.newMessageSuccess(regmessage);
        }

        public static void SendMessageToGroup(string message, string groupname)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MsgHub>();
            context.Clients.Group(groupname).newMessage(message);
        }

        //----------------------------------Groups-----------------------------------------------
        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }

    }
}