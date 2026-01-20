using Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar;
using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
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
    public class FamilyUnitMembersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        private readonly FamilyUnitMembersRepositoryAPI _familyUnitMembersRepositoryAPI = new();
        private readonly FamilyUnitMembersRepository _familyUnitMembersRepository = new();

        public Farm SelectedFarm { get; }

        private FamilyUnitMembers _selectedFUM;
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

        public bool CanEditFUM => SelectedFUM != null;
        public FamilyUnitMembers familyUnitMembers { get; }

        private readonly FarmsRepositoryAPI _farmsRepository = new();
        private readonly IAlert _alert;
        private ObservableCollection<FamilyUnitMembers> _items;
        private string _FarmId;

        public ICommand EditFUMCommand { get; }
        public ICommand CreateFUMCommand { get; }

        public ICommand UploadFUMCommand { get; }

        public FamilyUnitMembersViewModel(string farmId, IAlert alert)
        {

            _FarmId = farmId;
            _alert = alert;
            Items = new ObservableCollection<FamilyUnitMembers>();
            CreateFUMCommand = new Command(async () => await SaveFUMAsync());
            EditFUMCommand = new Command(async () => await EditFumAsync(), () => CanEditFUM);

        }

        public Guid farmId
        {
            get => Guid.Parse(_FarmId);
            set => _FarmId = value.ToString();
        }

        public ObservableCollection<FamilyUnitMembers> Items
        {
            get => _items;
            set => _items = value;
        }




        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        private string _identification;
        public string Identification
        {
            get => _identification;
            set { _identification = value; OnPropertyChanged(nameof(Identification)); }
        }

        private DateTime _age = DateTime.Today;
        public DateTime Age
        {
            get => _age;
            set { _age = value; OnPropertyChanged(nameof(Age)); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        private bool _isOwner;
        public bool IsOwner
        {
            get => _isOwner;
            set { _isOwner = value; OnPropertyChanged(nameof(IsOwner)); }
        }

        private string _education;
        public string Education { get => _education; set { _education = value; OnPropertyChanged(nameof(Education)); } }

        private string _relationship;
        public string Relationship { get => _relationship; set { _relationship = value; OnPropertyChanged(nameof(Relationship)); } }

        private string _maritalStatus;
        public string MaritalStatus { get => _maritalStatus; set { _maritalStatus = value; OnPropertyChanged(nameof(MaritalStatus)); } }



        public async Task LoadFamilyUnitMembersAsync(Guid FarmId)
        {
            try
            {
                var fumList = await _familyUnitMembersRepositoryAPI.GetFUM(farmId);
                Items.Clear();

                foreach (var fum in fumList)
                {
                    Items.Add(fum);
                }
            }
            catch (Exception ex)
            {

            }
        }


        private async Task SaveFUMAsync()
        {
            FamilyUnitMembers fum = new FamilyUnitMembers
            {
                Id = Guid.NewGuid(),
                FirstName = FirstName,
                LastName = LastName,
                Identification = Identification,
                Education = Education,
                Relationship = Relationship,
                MaritalStatus = MaritalStatus,
                Age = Age,
                PhoneNumber = PhoneNumber,
                IsOwner = IsOwner,
                FarmId = Guid.Parse(_FarmId),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                DeletedAt = null


            };

            try
            {
                await _familyUnitMembersRepository.SaveFUMLocally(new List<FamilyUnitMembers> { fum });

                await _alert.ShowAlert("Guardado", "Persona creada correctamente", "OK");

                await Application.Current.MainPage.Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                await _alert.ShowAlert("Error", ex.Message, "OK");
            }
        }


        private async Task EditFumAsync()
        {
            try
            {
                if (SelectedFUM == null)
                {
                    await _alert.ShowAlert("Error", "No hay persona seleccionada", "OK");
                    return;
                }

                SelectedFUM.UpdatedAt = DateTime.UtcNow;

                await _familyUnitMembersRepository.UpdateFUMLocalAsync(SelectedFUM);

                await _alert.ShowAlert(
                    "Actualizado",
                    "Persona actualizada correctamente",
                    "OK"
                );

                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await _alert.ShowAlert("Error", ex.Message, "OK");
            }
        }


    }
}
