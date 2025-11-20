using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class DashboardFarmsViewModel
    {
        
            public Farm SelectedFarm { get; }

            public DashboardFarmsViewModel(Farm farm)
            {
                SelectedFarm = farm;
            }

    }
}
