using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using Efarming_Sustainability.Core.ModelView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Spatial;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class FarmsViewModel
    {
        private readonly FarmsRepository _farmsRepository;
        
        private readonly IAlert _alert;


        private ObservableCollection<Farm> _items;
        private string? _code;



        public FarmsViewModel(FarmsRepository farmsRepository, IAlert alert)
        {

            _farmsRepository = farmsRepository;


            _items = new ObservableCollection<Farm>();

            _alert = alert;
        }

        public string? Code
        {
            get => _code;
            
            set => _code = value; 
        }

        
        public ObservableCollection<Farm> Items
        {
            get => _items;
            
            set => _items = value; 
        }

       
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid Id { get; set; }
        public Guid VillageId { get; set; }
        public Guid? FarmSubstatusId { get; set; }
        public Guid? CooperativeId { get; set; }
        public Guid? OwnershipTypeId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? SupplyChainId { get; set; }
        public Guid? FarmStatusId { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public async Task LoadFarmsAsync(Guid userId, Guid? villageId = null, string? code = null)
        {
            try
            {
                var farmsList = await _farmsRepository.GetFarms(userId, villageId, code);
                Items.Clear();
                foreach (var farm in farmsList)
                {
                    Items.Add(farm);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading farms: {ex.Message}");

                Items.Clear();
            }
        }

        public async Task DownloadFarmLocallyAsync(Farm farm)
        {
            if (farm == null)
                return;

            try
            {
                var farmLocal = new Farm   
                {
                    Id = farm.Id,
                    Name = farm.Name,
                    CreatedAt= farm.CreatedAt,
                    UpdatedAt=farm.UpdatedAt,
                    Code = farm.Code,
                    VillageId = farm.VillageId,
                    FarmSubstatusId= farm.FarmSubstatusId,
                    FarmStatusId=farm.FarmStatusId,
                    CooperativeId=farm.CooperativeId,
                    OwnershipTypeId=farm.OwnershipTypeId,
                    DeletedAt=farm.DeletedAt,
                    SupplyChainId= farm.SupplyChainId,
                    Latitude = farm.Latitude,
                    Longitude = farm.Longitude,

                };


                await _farmsRepository.SaveFarmsLocally(new List<Farm> { farm });




                await _alert.ShowAlert("Descarga", $"Finca '{farm.Name}' descargada.", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al descargar finca localmente: {ex.Message}");

                
                if (Shell.Current != null)
                {
                    await Shell.Current.DisplayAlert("Error de Descarga", $"No se pudo guardar la finca localmente: {ex.Message}", "OK");
                }
                else
                {
                   
                    Console.WriteLine($"ERROR: {ex.Message} (Fallo al mostrar DisplayAlert porque Shell.Current es null)");
                }
            }
        }

        public async Task GetLocalFarms()
        {
            _items.Clear();
            var farms = await _farmsRepository.GetLocalFarms();

            foreach(var farm in farms)
            {
                _items.Add(farm);
            }
        }


        public async Task<int> UpdateFarmLocalAsync(Farm farm)
        {
            try
            {
                var rows = await _farmsRepository.UpdateFarmLocalAsync(farm);
                return rows;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating farm local: {ex.Message}");
                return 0;
            }
        }

       
        public async Task<bool> UpdateFarmAndSyncAsync(Farm farm)
        {
            try
            {
                var result = await _farmsRepository.UpdateFarmAndSyncAsync(farm);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating and syncing farm: {ex.Message}");
                return false;
            }
        }
    }
}