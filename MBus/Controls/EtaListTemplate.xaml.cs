using Windows.UI.Xaml.Controls;
using MBusBackend.Models;

namespace MBus.Controls
{
    public sealed partial class EtaListTemplate : UserControl
    {
        public CombinedEta CombinedEta { get { return this.DataContext as CombinedEta; } }

        public EtaListTemplate()
        {
            this.InitializeComponent();

            this.DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}
