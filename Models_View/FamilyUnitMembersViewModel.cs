using Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar;
using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class FamilyUnitMembersViewModel
    {

        private readonly FamilyUnitMembersRepositoryAPI _familyUnitMembersRepositoryAPI = new();
        private readonly FamilyUnitMembersRepository _familyUnitMembersRepository = new();
        public Farm SelectedFarm { get; }
        private readonly FarmsRepositoryAPI _farmsRepository = new();
        private readonly IAlert _alert;
        private ObservableCollection<FamilyUnitMembers> _items;
        private string _FarmId;

        public FamilyUnitMembersViewModel(FamilyUnitMembers familyUnitMembersRepository, 
                                          IAlert alert, string farmId)
        {

            
            _alert = alert;
            _FarmId = farmId;

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



        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Age { get; set; }
        public String? Identification { get; set; }
        public String? Education { get; set; }
        public String? PhoneNumber { get; set; }
        public String? Relationship { get; set; }
        public String? MaritalStatus { get; set; }
        public bool IsOwner { get; set; }
        public Guid FarmId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

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


        public async Task SaveFUMAsync(FamilyUnitMembers fum)
        {
            if (fum == null)
            {
                await _alert.ShowAlert("Error", "La lista de personas de la unidad familiar está vacía.", "OK");
                return;
            }

            try
            {
                await _familyUnitMembersRepository.SaveFUMLocally(new List<FamilyUnitMembers> { fum });

                await _alert.ShowAlert("Guardado", "Las personas de la unidad familiar se han guardado localmente.", "OK");
            }
            catch (Exception ex)
            {
                await _alert.ShowAlert("Error", $"Error al guardar las personas de la unidad familiar localmente: {ex.Message}", "OK");
            }
        }






    }
}
