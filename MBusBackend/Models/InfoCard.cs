using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MBusBackend.Models
{
    public class InfoCard : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Stop Stop { get; set; }

        public ObservableCollection<CombinedEta> Etas;

        public InfoCard(Stop stop, List<CombinedEta> etas)
        {
            Stop = stop;
            Etas = new ObservableCollection<CombinedEta>(etas);
        }

        private void RaiseOnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
