using Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar;
using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
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

        private readonly FarmsRepositoryAPI _farmsRepository = new();
        public Farm SelectedFarm { get; }

        public ICommand EditFincaCommand { get; }

        public ICommand UploadFarmCommand { get; }

        public FarmsMenuViewModel(Farm farm)
        {
            SelectedFarm = farm;

            EditFincaCommand = new Command(async () => await EditFinca());

            UploadFarmCommand = new Command(async () => await UploadFarmAsync());
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


        private async Task UploadFarmAsync()
        {
            try
            {
                // 1. Validar conexión a internet
                var connectivity = Connectivity.Current.NetworkAccess;

                if (connectivity != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Sin conexión",
                        "Por favor revisa tu conexión a internet antes de cargar la información.",
                        "OK");
                    return;
                }

                // 2. Validar finca
                if (SelectedFarm == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "No hay finca seleccionada.",
                        "OK");
                    return;
                }

                // 3. Llamar al API
                var result = await _farmsRepository.UpdateFarmAsync(SelectedFarm);

                if (result)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Éxito",
                        "La finca se cargó correctamente al servidor.",
                        "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Ocurrió un problema al cargar la finca.",
                        "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error inesperado",
                    ex.Message,
                    "OK");
            }
        }


    }
}
