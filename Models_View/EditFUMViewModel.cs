using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Interfaces;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Efarming_Sustainability.App.Models_View
{
    public class EditFUMViewModel : INotifyPropertyChanged
    {
        private readonly FamilyUnitMembersRepository _repository;
        private readonly IAlert _alert;

        private FamilyUnitMembers _familyUnitMembers;
        public FamilyUnitMembers FamilyUnitMembers
        {
            get => _familyUnitMembers;
            set
            {
                _familyUnitMembers = value;
                OnPropertyChanged();
            }
        }

        public ICommand EditFUMCommand { get; }

        public EditFUMViewModel(FamilyUnitMembers fum,IAlert alert)
        {
            _repository = new FamilyUnitMembersRepository();
            _alert = alert;
            

            FamilyUnitMembers = fum;

            EditFUMCommand = new Command(async () => await EditFumAsync());
        }

        private async Task EditFumAsync()
        {
            try
            {
                if (FamilyUnitMembers == null)
                    throw new Exception("FamilyUnitMembers es NULL");

                if (_repository == null)
                    throw new Exception("Repository es NULL");

                if (_alert == null)
                    throw new Exception("Alert es NULL");

                FamilyUnitMembers.UpdatedAt = DateTime.UtcNow;
                FamilyUnitMembers.DeletedAt= null;

                await _repository.UpdateFUMLocalAsync(FamilyUnitMembers);

                await _alert.ShowAlert(
                    "Actualizado",
                    "Persona actualizada correctamente",
                    "OK"
                );

                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error crítico",
                    ex.Message,
                    "OK"
                );
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
