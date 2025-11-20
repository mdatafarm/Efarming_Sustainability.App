using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Views.Farm;
using Efarming_Sustainability.App.Views.Farms;
using Efarming_Sustainability.Core.Models;
using Efarming_Sustainability.Core.ModelView;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Efarming_Sustainability.App.Models_View
{
    public class LocalFarmsViewModel : BaseViewModel
    {
        private readonly FarmsRepository _repository;

        public ObservableCollection<Farm> LocalFarms { get; set; } = new();

        public Command LoadFarmsCommand { get; }
        public ICommand EditFarmCommand { get; }

        public LocalFarmsViewModel(FarmsRepository repository)
        {
            _repository = repository;

            LoadFarmsCommand = new Command(async () => await LoadFarmsAsync());
            EditFarmCommand = new Command<Farm>(async farm => await EditFarm(farm));
        }

        public async Task LoadFarmsAsync()
        {


            var farms = await _repository.GetLocalFarms();

            Console.WriteLine($"Fincas leídas: {farms.Count}");
            LocalFarms.Clear();


            foreach (var farm in farms)
                LocalFarms.Add(farm);
        }

        public async Task EditFarmAsync(Farm farm)
        {
            if (farm == null)
                return;


            await Shell.Current.GoToAsync($"EditFarmPage?farmId={farm.Id}");
        }

        private async Task EditFarm(Farm farm)
        {
            if (farm == null) return;

            // Navegación usando Navigation.PushAsync (lo usas en tu proyecto)
            await Application.Current.MainPage.Navigation.PushAsync(new DashboardFarms(farm));
        }
    }
}
