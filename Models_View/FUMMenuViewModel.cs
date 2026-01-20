using Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar;
using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Views.Farm;
using Efarming_Sustainability.App.Views.Farms;
using Efarming_Sustainability.App.Views.FUM;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Efarming_Sustainability.App.Models_View
{
    public class FUMMenuViewModel : INotifyPropertyChanged
    {
        private readonly FamilyUnitMembersRepository _familyUnitMembersRepository;
        private readonly FarmsRepository _farmsRepository;
        public Farm SelectedFarm { get; }
        public ObservableCollection<FamilyUnitMembers> FUMList { get; set; }= new ObservableCollection<FamilyUnitMembers>();
        private readonly FamilyUnitMembersRepositoryAPI _familyUnitMembersRepositoryAPI = new();
        private FamilyUnitMembers _selectedFUM;
        private readonly IAlert _alert;
        public bool CanEditFUM => SelectedFUM != null;
        public FamilyUnitMembers SelectedFUM
        {
            get => _selectedFUM;
            set
            {
                if (_selectedFUM != value)
                {
                    _selectedFUM = value;
                    OnPropertyChanged(nameof(SelectedFUM));
                    OnPropertyChanged(nameof(CanEditFUM));

                    ((Command)EditFUMCommand).ChangeCanExecute();
                }
            }
        }
        public Guid FarmId { get; }
        public ICommand EditFUMCommand { get; }
        public ICommand CreateFUMCommand { get; }
        public ICommand UploadFUMCommand { get; }
        public ICommand BackCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public FUMMenuViewModel(Guid farmId, IAlert alert)
        {
            _alert = alert;
            FarmId = farmId;
            _familyUnitMembersRepository = new FamilyUnitMembersRepository();
            _farmsRepository = new FarmsRepository();
            EditFUMCommand = new Command(
                async () => await EditFUM(),
                () => CanEditFUM);
            CreateFUMCommand = new Command(async () => await CreateFUM());
            UploadFUMCommand = new Command(async () => await UpdateFUMandSync());
            BackCommand = new Command(async () => await Back());

        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



        public async Task LoadFUMMembers()
        {
            try
            {
                var list = await _familyUnitMembersRepository.GetFUMLocalByFarmId(FarmId);

                FUMList.Clear();
                foreach (var item in list)
                    FUMList.Add(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error cargando personas: " + ex.Message);
            }
        }


        private async Task EditFUM()
        {
            if (SelectedFUM == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "No has seleccionado ninguna persona.",
                    "OK"
                );
                return;
            }

            await Application.Current.MainPage.Navigation.PushAsync(
                new EditFUM(SelectedFUM, _alert)
            );
        }




        private async Task CreateFUM()
        {
            await Application.Current.MainPage.Navigation.PushAsync(
                new CreateFUM(FarmId)
            );
        }

        private async Task Back()
        {
            var farm = await _farmsRepository.GetFarmbyId(FarmId);
            if (farm == null)
            {
                await _alert.ShowAlert("Error", "No se pudo cargar la finca.", "OK");
                return;
            }
            await Application.Current.MainPage.Navigation.PushAsync(new DashboardFarms(farm,_alert));
        }


        private async Task UploadFUMAsync()
        {
            try
            {
                if (SelectedFUM == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error", "No hay miembro seleccionado.", "OK");
                    return;
                }

                var success = await _familyUnitMembersRepositoryAPI.UpdateFUMAsync(SelectedFUM);

                if (success)
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Persona actualizada.", "OK");
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "Fallo al actualizar.", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task UpdateFUMandSync()
        {
            try
            {
                
                var connectivity = Connectivity.Current.NetworkAccess;

                if (connectivity != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Sin conexión",
                        "Por favor revisa tu conexión a internet antes de cargar la información.",
                        "OK");
                    return;
                }

                
                if (SelectedFUM == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "No hay Registro seleccionada.",
                        "OK");
                    return;
                }

                
                var result = await _familyUnitMembersRepositoryAPI.UpdateFUMAsync(SelectedFUM);

                if (result)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Éxito",
                        "Registro cargado correctamente.",
                        "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Ocurrió un problema al cargar el registro.",
                        "OK");
                }
            }

            catch (Exception ex)
            {
                await _alert.ShowAlert("Error", ex.Message, "OK");
            }

        }
    }
}