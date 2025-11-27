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
    public class FarmsMenuViewModel
    {
        public Farm SelectedFarm { get; }

        public ICommand EditFincaCommand { get; }

        public FarmsMenuViewModel(Farm farm)
        {
            SelectedFarm = farm;

            EditFincaCommand = new Command(async () => await EditFinca());
        }

        private async Task EditFinca()
        {
            Console.WriteLine("SelectedFarm es null? => " + (SelectedFarm == null));
            Console.WriteLine("Id => " + SelectedFarm?.Id);
            if (SelectedFarm == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No hay finca seleccionada.", "OK");
                return;
            }
            await Application.Current.MainPage.Navigation.PushAsync(new EditFarms(SelectedFarm));
        }
    }
}
