using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Realtime.Infrastructure.Hubs
{
    public interface IFileNotificationClient
    {
        Task SendFileExportedNotification( int fileId, string fileName);
    }
    [Authorize]
    public class FileNotificationHub(): Hub<IFileNotificationClient>
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        //public async Task SendFileExportedNotification(string fileId, string fileName)
        //{
        //    await Clients.All.SendAsync("ReceiveFileExportedNotification", fileId, fileName);
        //}
    }
}
