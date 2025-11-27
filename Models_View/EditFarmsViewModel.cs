using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Views.Farms;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using System.Runtime.CompilerServices;

namespace Efarming_Sustainability.App.Models_View
{
    public class EditFarmsViewModel : INotifyPropertyChanged
    {
        public Farm SelectedFarm { get; }

        public readonly DepartmentsRepository _departmentsRepository = new();
        public readonly MunicipalityRepository _municipalityRepository = new();
        public readonly VillageRepository _villageRepository = new();
        public readonly CooperativeRepository cooperativeRepository = new(); 
        public readonly FarmStatusRepository farmStatus = new();
        public readonly SupplyChainsRepository supplyChainsRepository = new();
        public readonly OwnershipTypesRepository ownershipTypesRepository = new();


        public ObservableCollection<Department> Departments { get; set; } = new();
        public ObservableCollection<Municipality> Municipalities { get; set; } = new();
        public ObservableCollection<Village> Villages { get; set; } = new();
        public ObservableCollection<Cooperative> Cooperatives { get; set; } = new();
        public ObservableCollection<FarmStatus> FarmStatuses { get; set; } = new();
        public ObservableCollection<SupplyChains> SupplyChains { get; set; } = new();
        public ObservableCollection<OwnershipTypes> OwnershipTypes { get; set; } = new();

        

        // flag para evitar cargas automáticas durante la inicialización
        private bool _isInitializing = false;

        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                if (_selectedDepartment == value) return;

                _selectedDepartment = value;
                OnPropertyChanged();

                // Solo cargar municipios si no estamos inicializando
                if (!_isInitializing && value != null)
                {
                    _ = LoadMunicipalitiesAsync(value.Id);
                }
            }
        }

        private Municipality _selectedMunicipality;
        public Municipality SelectedMunicipality
        {
            get => _selectedMunicipality;
            set
            {
                if (_selectedMunicipality == value) return;

                _selectedMunicipality = value;
                OnPropertyChanged();

                // Solo cargar veredas si no estamos inicializando
                if (!_isInitializing && value != null)
                {
                    _ = LoadVillagesAsync(value.Id);
                }
            }
        }

        private Village _selectedVillage;
        public Village SelectedVillage
        {
            get => _selectedVillage;
            set
            {
                if (_selectedVillage == value) return;

                _selectedVillage = value;
                OnPropertyChanged();
            }
        }

        private Cooperative _selectedCooperative;
        public Cooperative SelectedCooperative
        {
            get => _selectedCooperative;
            set
            {
                if (_selectedCooperative == value) return;
                _selectedCooperative = value;
                OnPropertyChanged();
            }
        }

        private FarmStatus _selectedFarmStatus;

        public FarmStatus SelectedFarmStatus
        {
            get => _selectedFarmStatus;
            set
            {
                if (_selectedFarmStatus == value) return;
                _selectedFarmStatus = value;
                OnPropertyChanged();
            }
        }

        public SupplyChains _selectedSupplyChain;
        public SupplyChains SelectedSupplyChain
        {
            get => _selectedSupplyChain;
            set
            {
                if (_selectedSupplyChain == value) return;
                _selectedSupplyChain = value;
                OnPropertyChanged();
            }
        }

        public OwnershipTypes _selectedOwnershipType;
        public OwnershipTypes SelectedOwnershipType
        {
            get => _selectedOwnershipType;
            set
            {
                if (_selectedOwnershipType == value) return;
                _selectedOwnershipType = value;
                OnPropertyChanged();
            }
        }


        public ICommand EditFincaCommand { get; }

        public ICommand GetLocationCommand { get; }

        public EditFarmsViewModel(Farm farm)
        {
            SelectedFarm = farm ?? throw new ArgumentNullException(nameof(farm));
            EditFincaCommand = new Command(async () => await SaveFarmAsync());
            GetLocationCommand = new Command(async () => await _getLocation());
            // Iniciamos la carga asincrónica sin bloquear el constructor
            _ = InitializeAsync();
        }

        

        

        private async Task InitializeAsync()
        {
            try
            {
                _isInitializing = true;


                Departments.Clear();
                var depts = await _departmentsRepository.GetAllDepartments();
                foreach (var d in depts)
                    Departments.Add(d);


                if (SelectedFarm?.VillageId == Guid.Empty)
                {

                    _isInitializing = false;
                    return;
                }

                var village = await _villageRepository.GetVillagesById(SelectedFarm.VillageId); // Ajusta si tu repo tiene otro nombre
                if (village == null)
                {
                    _isInitializing = false;
                    return;
                }


                var muni = await _municipalityRepository.GetMunicipalitiesById(village.MunicipalityId);
                var dept = await _departmentsRepository.GetDepartmentById(muni.DepartmentId);


                await LoadMunicipalitiesAsync(dept.Id);
                await LoadVillagesAsync(muni.Id);


                SelectedDepartment = Departments.FirstOrDefault(d => d.Id == dept.Id);
                SelectedMunicipality = Municipalities.FirstOrDefault(m => m.Id == muni.Id);
                SelectedVillage = Villages.FirstOrDefault(v => v.Id == village.Id);

                // Cargar cooperativas
                if (SelectedFarm.CooperativeId == Guid.Empty)
                {
                    _isInitializing = false;
                    return;
                }

                Cooperatives.Clear();
                var coops = await cooperativeRepository.GetLocalCooperatives();
                foreach (var c in coops)
                {
                    Cooperatives.Add(c);

                }

                var coop = await cooperativeRepository.GetCooperativesbyId(SelectedFarm.CooperativeId);

                SelectedCooperative = Cooperatives.FirstOrDefault(c => c.Id == coop.Id);

                

                //Cargar FarmStatuses

                if (SelectedFarm.FarmStatusId == Guid.Empty)
                {
                    _isInitializing = false;
                    return;
                }

                FarmStatuses.Clear();

                var statuses = await farmStatus.GetLocalFarmStatuses();
                foreach (var fs in statuses)
                {
                    FarmStatuses.Add(fs);
                }

                var status = await farmStatus.GetFarmstatusesById(SelectedFarm.FarmStatusId);
                SelectedFarmStatus = FarmStatuses.FirstOrDefault(fs => fs.Id == status.Id);

                //Cargar SupplyChains

                if (SelectedFarm.SupplyChainId == Guid.Empty)
                {
                    _isInitializing = false;
                    return;
                }
                SupplyChains.Clear();
                var supplyChains = await supplyChainsRepository.GetLocalSupplyChains();
                foreach (var sc in supplyChains)
                {
                    SupplyChains.Add(sc);
                }
                var supplyChain = await supplyChainsRepository.GetSupplyChainsById(SelectedFarm.SupplyChainId);
                SelectedSupplyChain = SupplyChains.FirstOrDefault(sc => sc.Id == supplyChain.Id);

                // Cargar OwnershipTypes

                if (SelectedFarm.OwnershipTypeId == Guid.Empty)
                {
                    _isInitializing = false;
                    return;
                }

                OwnershipTypes.Clear();

                var ownershipTypes = await ownershipTypesRepository.GetLocalOwnershipTypes();
                foreach (var ot in ownershipTypes)
                {
                    OwnershipTypes.Add(ot);
                }
                var ownershipType = await ownershipTypesRepository.GetOwnershipTypesById(SelectedFarm.OwnershipTypeId);
                SelectedOwnershipType = ownershipTypes.FirstOrDefault(ot => ot.Id == ownershipType.Id);
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine($"InitializeAsync error: {ex}");
            }
            finally
            {
                _isInitializing = false;
            }
        }

        private async Task _getLocation()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Permiso denegado",
                        "La app no tiene permiso para acceder a la ubicación.",
                        "OK");
                    return;
                }

                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Best,
                        Timeout = TimeSpan.FromSeconds(10)
                    });
                }

                if (location != null)
                {
                    // ► Actualizar el modelo de la finca
                    SelectedFarm.Latitude = location.Latitude.ToString();
                    SelectedFarm.Longitude = location.Longitude.ToString();

                    // ► Notificar cambios en UI
                    OnPropertyChanged(nameof(SelectedFarm));

                    await App.Current.MainPage.DisplayAlert(
                        "Ubicación",
                        $"Latitud: {location.Latitude}\nLongitud: {location.Longitude}",
                        "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Error",
                        "No se pudo obtener la ubicación.",
                        "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task LoadMunicipalitiesAsync(Guid deptId)
        {
            try
            {
                Municipalities.Clear();
                var list = await _municipalityRepository.GetMunicipalitiesByDepartment(deptId);
                foreach (var m in list) Municipalities.Add(m);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadMunicipalitiesAsync error: {ex}");
            }
        }

        private async Task LoadVillagesAsync(Guid muniId)
        {
            try
            {
                Villages.Clear();
                var list = await _villageRepository.GetVillagesByMunicipality(muniId);
                foreach (var v in list) Villages.Add(v);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadVillagesAsync error: {ex}");
            }
        }

        private async Task SaveFarmAsync()
        {
            if (SelectedFarm == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No hay finca seleccionada.", "OK");
                return;
            }

            if (SelectedVillage == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Seleccione una vereda.", "OK");
                return;
            }

            SelectedFarm.VillageId = SelectedVillage.Id;

            // Aquí iría la lógica para persistir la finca (repository, api, etc.)
            await Application.Current.MainPage.DisplayAlert("OK", "Finca guardada", "OK");
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
