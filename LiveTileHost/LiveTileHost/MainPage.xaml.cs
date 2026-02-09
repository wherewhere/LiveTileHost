using LiveTileHost.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Notification = LiveTileHost.Models.Notification;

namespace LiveTileHost
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage() => InitializeComponent();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "Local", "Microsoft", "Windows", "Notifications", "wpndatabase.db");
            StorageFile file = await StorageFile.GetFileFromPathAsync(path);
            StorageFile db = await file.CopyAsync(ApplicationData.Current.TemporaryFolder, "wpndatabase.db", NameCollisionOption.ReplaceExisting);
            using NotificationContext context = NotificationContextFactory.CreateDbContext(db.Path);
            const string app = "18184wherewhere.CoolapkLite_4v4sx105x6y4r!App";
            var a = context.NotificationHandler.Select(x => new { x.RecordId, x.PrimaryId, x.ParentId }).FirstOrDefault(x => x.PrimaryId == app || x.ParentId == app);
            IQueryable<Notification> b = context.Notification.Where(x => x.HandlerId == a.RecordId);
            List<Notification> tiles = [.. b.Where(x => x.Type == "tile")];
            List<Notification> badges = [.. b.Where(x => x.Type == "badge")];
            foreach (Notification tile in tiles)
            {
                XmlDocument document = new();
                document.LoadXmlFromBuffer(tile.Payload.AsBuffer());
                Tile.CreateTileUpdater().Update(new TileNotification(document));
                await Task.Delay(2000);
            }
        }
    }
}
