using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
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
    public class EditProductivityViewModel : INotifyPropertyChanged
    {
        private readonly ProductivityRepository _repository;
        private readonly IAlert _alert;

        private Productivity _productivity;

        public Productivity Productivity
        {
            get => _productivity;
            set
            {
                _productivity = value;
                OnPropertyChanged();
            }
        }

        public ICommand EditProductivityCommand { get; }

        public EditProductivityViewModel(Productivity productivity, IAlert alert)
        {
            _productivity = productivity;
            
            _alert = alert;
            EditProductivityCommand = new Command(async () => await EditProductivityAsync());
        }

        private async Task EditProductivityAsync()
        {
            try
            {
                if (Productivity == null)
                    throw new Exception("Productivity es NULL");
                if (_repository == null)
                    throw new Exception("Repository es NULL");
                if (_alert == null)
                    throw new Exception("Alert es NULL");
                Productivity.UpdatedAt = DateTime.UtcNow;
                Productivity.DeletedAt = null;
                await _repository.UpdateProductivityLocalAsync(Productivity);
                await _alert.ShowAlert("Éxito", "Productividad actualizada correctamente.","Ok");
            }
            catch (Exception ex)
            {
                await _alert.ShowAlert("Error", $"Error al actualizar la productividad: {ex.Message}", "Ok");
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
