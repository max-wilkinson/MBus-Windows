using System;
using Windows.UI.Notifications;
using MBusBackend.Models;

namespace MBus
{
    class LiveTileService
    {
        public void UpdateLiveTile(InfoCard card)
        {
            var timeString = GetTimeString(card);

            var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text02);

            var tileAttributes = tileXml.GetElementsByTagName("text");
            tileAttributes[0].AppendChild(tileXml.CreateTextNode("Next bus"));
            tileAttributes[1].AppendChild(tileXml.CreateTextNode(timeString));
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        private string GetTimeString(InfoCard card)
        {
            var nextBusEta = 999;
            foreach (var eta in card.Etas)
            {
                foreach (var minutesAway in eta.MinutesAwayCollection)
                {
                    if (minutesAway < nextBusEta)
                        nextBusEta = minutesAway;
                }
            }

            return DateTime.Now.AddMinutes(nextBusEta).ToString("h:mm tt");
        }
    }
}
