using Master.Models.HomePage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Master.ViewModels.HomePage
{
    internal class HomeViewModel
    {
        public ObservableCollection<Device> Devices { get; set; }

        public HomeViewModel()
        {
            // Default Data (Mocking your original image list)
            Devices = new ObservableCollection<Device>
            {
                new Device { Name = "Scanner 1", StatusBrush = Brushes.LightGreen },
                new Device { Name = "Scanner 2", StatusBrush = Brushes.LightGreen },
                new Device { Name = "Transverse_Y1", StatusBrush = Brushes.LightGray },
                new Device { Name = "Transverse_Y2", StatusBrush = Brushes.LightGray },
                new Device { Name = "Advantech Motion", StatusBrush = Brushes.LightGreen },
                new Device { Name = "Vision Controller", StatusBrush = Brushes.Orange },
                new Device { Name = "Barcode_Scanner", StatusBrush = Brushes.LightGreen }
            };
        }

    }
}
