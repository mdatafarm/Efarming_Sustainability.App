using Efarming_Sustainability.App.Views.Farms;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Efarming_Sustainability.App.Models_View
{
    public class DashboardFarmsViewModel
    {

        public Farm SelectedFarm { get; }

        public ICommand OpenFincaCommand { get; }

        public DashboardFarmsViewModel(Farm farm)
        {
            SelectedFarm = farm;

            OpenFincaCommand = new Command(async () => await OpenFinca());
        }

        private async Task OpenFinca()
        {
            Console.WriteLine("SelectedFarm es null? => " + (SelectedFarm == null));
            Console.WriteLine("Id => " + SelectedFarm?.Id);

            if (SelectedFarm == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No hay finca seleccionada.", "OK");
                return;
            }

            await Application.Current.MainPage.Navigation.PushAsync(new FarmsMenu(SelectedFarm));
        }
    }
}
