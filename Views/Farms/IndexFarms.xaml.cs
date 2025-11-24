using Efarming_Sustainability.App.Models_View;
using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;

using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Efarming_Sustainability.App.Views.Farm;

namespace Efarming_Sustainability.App.Views.Farms
{
    public partial class IndexFarms : ContentPage
    {
        private readonly DepartmentsViewModel _departmentsVM;
        private readonly MunicipalityViewModel _municipalityVM;
        private readonly VillageViewModel _villageVM;

        private List<Department> _departments = new();
        private List<Municipality> _municipalities = new();
        private List<Village> _villages = new();

        private FarmsViewModel _viewModel;

       


        public IndexFarms()
        {
            InitializeComponent();

            // Crear viewmodels (siguiendo la convención del proyecto)
            _departmentsVM = new DepartmentsViewModel(new DepartmentsRepository());
            _municipalityVM = new MunicipalityViewModel(new MunicipalityRepository());
            _villageVM = new VillageViewModel(new VillageRepository());

            // Mostrar la propiedad Name en los Pickers
            dptPicker.ItemDisplayBinding = new Binding("Name");
            mcppicker.ItemDisplayBinding = new Binding("Name");
            vilpicker.ItemDisplayBinding = new Binding("Name");

            // Eventos de selección para filtrar
            dptPicker.SelectedIndexChanged += DptPicker_SelectedIndexChanged;
            mcppicker.SelectedIndexChanged += McpPicker_SelectedIndexChanged;

            _viewModel = new FarmsViewModel(new FarmsRepository(), new AlertRepository());
            BindingContext = _viewModel;

           
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Cargar departamentos, municipios y veredas (pueden venir de sqlite o API según repositorio)
                await _departmentsVM.LoadDepartmentsAsync();
                _departments = _departmentsVM.Items ?? new List<Department>();
                dptPicker.ItemsSource = _departments;

                await _municipalityVM.LoadMunicipalitiesAsync();
                _municipalities = _municipalityVM.Items ?? new List<Municipality>();
                // Inicialmente mostramos todos los municipios o dejar vacío según preferencia:
                // mcppicker.ItemsSource = _municipalities;
                mcppicker.ItemsSource = new List<Municipality>();

                await _villageVM.LoadVillagesAsync();
                _villages = _villageVM.Items ?? new List<Village>();
                // Vilpicker vacío hasta seleccionar municipio
                vilpicker.ItemsSource = new List<Village>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando datos en la vista: {ex.Message}");
            }
        }

        private void DptPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var selected = dptPicker.SelectedItem as Department;
            if (selected == null)
            {
                // Si no hay departamento seleccionado, vaciar municipal y village (o mostrar todos)
                mcppicker.ItemsSource = new List<Municipality>();
                vilpicker.ItemsSource = new List<Village>();
                return;
            }

            // Filtrar municipios por DepartmentId
            var filteredMunicipalities = _municipalities
                .Where(m => m.DepartmentId == selected.Id)
                .ToList();

            mcppicker.ItemsSource = filteredMunicipalities;
            mcppicker.SelectedItem = null;

            // Limpiar villages hasta seleccionar municipio
            vilpicker.ItemsSource = new List<Village>();
            vilpicker.SelectedItem = null;
        }

        private void McpPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var selected = mcppicker.SelectedItem as Municipality;
            if (selected == null)
            {
                vilpicker.ItemsSource = new List<Village>();
                return;
            }

            // Filtrar villages por MunicipalityId
            var filteredVillages = _villages
                .Where(v => v.MunicipalityId == selected.Id)
                .ToList();

            vilpicker.ItemsSource = filteredVillages;
            vilpicker.SelectedItem = null;
        }

        private async void OnSearchFarmClicked(object sender, EventArgs e)
        {
            await BuscarFincaAsync();
        }

        private async Task BuscarFincaAsync()
        {


            try
            {

                string code = _viewModel.Code;
                var selectedVillage = vilpicker.SelectedItem as Village;
                Guid? villageId = selectedVillage?.Id;


                Guid userId = Guid.Parse("6a315709-eacb-4f9f-9136-f3fcb3fde4f0");


                await _viewModel.LoadFarmsAsync(userId, villageId, code);

                int itemCount = _viewModel.Items?.Count ?? 0;

                if (itemCount == 0)
                {
                    await DisplayAlert("Sin resultados", "No se encontraron fincas con esos criterios.", "OK");
                }
                else
                {

                    await DisplayAlert("Éxito", $"{itemCount} finca(s) encontrada(s).", "OK");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error de Búsqueda", $"Ocurrió un error al cargar las fincas. Detalles: {ex.Message}", "OK");
            }
            finally
            {

            }
        }

        private async void OnDownloadFarmClicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button btn && btn.CommandParameter is Efarming_Sustainability.Core.Models.Farm farm)
                {
                    await _viewModel.DownloadFarmLocallyAsync(farm);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnFarmListClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DownloadFarms());
        }

    }
}