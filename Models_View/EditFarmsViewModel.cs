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


        public ObservableCollection<Department> Departments { get; set; } = new();
        public ObservableCollection<Municipality> Municipalities { get; set; } = new();
        public ObservableCollection<Village> Villages { get; set; } = new();

        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged();

                if (value != null)
                    LoadMunicipalities(value.Id);
            }
        }

        private Municipality _selectedMunicipality;
        public Municipality SelectedMunicipality
        {
            get => _selectedMunicipality;
            set
            {
                _selectedMunicipality = value;
                OnPropertyChanged();

                if (value != null)
                    LoadVillages(value.Id);
            }
        }

        private Village _selectedVillage;
        public Village SelectedVillage
        {
            get => _selectedVillage;
            set
            {
                _selectedVillage = value;
                OnPropertyChanged();
            }
        }
        public ICommand EditFincaCommand { get; }

        public EditFarmsViewModel(Farm farm)
        {
            SelectedFarm = farm;
            EditFincaCommand = new Command(async () => await SaveFarm());

            LoadInitialData();
        }

        private async void LoadInitialData()
        {
            // 1. Load Departments
            var depts = await _departmentsRepository.GetAllDepartments();
            foreach (var d in depts)
                Departments.Add(d);

            // 2. Load Village
            var village = await _villageRepository.GetVillagesById(SelectedFarm.VillageId);
            SelectedVillage = village;

            // 3. Load Municipality
            var muni = await _municipalityRepository.GetMunicipalitiesById(village.MunicipalityId);
            SelectedMunicipality = muni;

            // 4. Load Department
            var dept = await _departmentsRepository.GetDepartmentById(muni.DepartmentId);
            SelectedDepartment = dept;

            // Fill dependent lists
            await LoadMunicipalities(dept.Id);
            await LoadVillages(muni.Id);
        }

        private async Task LoadMunicipalities(Guid deptId)
        {
            Municipalities.Clear();

            var list = await _municipalityRepository.GetMunicipalitiesByDepartment(deptId);

            foreach (var m in list)
                Municipalities.Add(m);
        }

        private async Task LoadVillages(Guid muniId)
        {
            Villages.Clear();

            var list = await _villageRepository.GetVillagesByMunicipality(muniId);

            foreach (var v in list)
                Villages.Add(v);
        }


        private async Task SaveFarm()
        {
            Console.WriteLine("SelectedFarm es null? => " + (SelectedFarm == null));
            Console.WriteLine("Id => " + SelectedFarm?.Id);

            SelectedFarm.VillageId = SelectedVillage.Id;

            if (SelectedFarm == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No hay finca seleccionada.", "OK");
                return;
            }


            await Application.Current.MainPage.DisplayAlert("OK", "Finca guardada", "OK");
        }

        //private async Task EditFinca()
        //{
        //    Console.WriteLine("SelectedFarm es null? => " + (SelectedFarm == null));
        //    Console.WriteLine("Id => " + SelectedFarm?.Id);
        //    if (SelectedFarm == null)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", "No hay finca seleccionada.", "OK");
        //        return;
        //    }
        //    // Navegar a la página de edición de la finca
        //    await Application.Current.MainPage.Navigation.PushAsync(new EditFarms(SelectedFarm.Id));
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
