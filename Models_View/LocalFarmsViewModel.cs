using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class LocalFarmsViewModel : BaseViewModel
    {
        private readonly FarmsRepository _repository;

        public ObservableCollection<Farms> LocalFarms { get; set; } = new();

        public Command LoadFarmsCommand { get; }
        public Command<Farms> EditFarmCommand { get; }

        public LocalFarmsViewModel(FarmsRepository repository)
        {
            _repository = repository;

            LoadFarmsCommand = new Command(async () => await LoadFarmsAsync());
            EditFarmCommand = new Command<Farms>(async (farm) => await EditFarmAsync(farm));
        }

        public async Task LoadFarmsAsync()
        {
          

            var farms = await _repository.GetLocalFarms();

            Console.WriteLine($"Fincas leídas: {farms.Count}");
            LocalFarms.Clear();


            foreach (var farm in farms)
                LocalFarms.Add(farm);
        }

        private async Task EditFarmAsync(Farms farm)
        {
            if (farm == null)
                return;

            // Navegar a página de edición
            await Shell.Current.GoToAsync($"EditFarmPage?farmId={farm.Id}");
        }
    }
}
