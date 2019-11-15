using Windows.UI.Xaml.Controls;
using MBusBackend.Models;

namespace MBus.Controls
{
    public sealed partial class CardListTemplate : UserControl
    {
        public InfoCard InfoCard { get { return this.DataContext as InfoCard; } }

        public CardListTemplate()
        {
            this.InitializeComponent();

            this.DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}
