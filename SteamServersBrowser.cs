using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Playnite.SDK;
using Playnite.SDK.Plugins;

namespace SteamServersBrowser
{
    public class SteamServersBrowser : GenericPlugin
    {
        public override Guid Id => Guid.Parse("3C0C2C2E-1C7D-45F5-BA11-7D4C0A9D9D21");

        public SteamServersBrowser(IPlayniteAPI api) : base(api)
        {
        }

        public override IEnumerable<TopPanelItem> GetTopPanelItems()
        {
            var pluginPath = Path.GetDirectoryName(typeof(SteamServersBrowser).Assembly.Location);
            var iconPath = Path.Combine(pluginPath, "icon.png");

            yield return new TopPanelItem
            {
                Title = "Steam Servers",
                Icon = File.Exists(iconPath) ? iconPath : CreateFallbackIcon(),
                Activated = OpenSteamServers
            };
        }

        private void OpenSteamServers()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "steam://open/servers",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                PlayniteApi.Dialogs.ShowErrorMessage(
                    $"Could not open Steam Game Servers.\n\n{ex.Message}",
                    "Steam Servers");
            }
        }

        private object CreateFallbackIcon()
        {
            return new TextBlock
            {
                Text = "S",
                FontSize = 18,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
        }
    }
}
