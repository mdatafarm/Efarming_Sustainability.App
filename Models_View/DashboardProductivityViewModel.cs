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
    public class DashboardProductivityViewModel : INotifyPropertyChanged
    {
        private readonly IAlert _alert;
        private readonly ProductivityRepository _productivityRepository;
        private readonly ProductivityRepositoryAPI _productivityRepositoryAPI;

        public ObservableCollection<Productivity> ProductivityList { get; set; } = new ObservableCollection<Productivity>();

        public Farm SelectedFarm { get; }

        public Guid FarmId { get; }

        public ICommand EditProductivityCommand { get; }
        public ICommand UploadProductivityCommand { get; }
        public ICommand BackCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DashboardProductivityViewModel(Guid farmId, IAlert alert)
        {
            _alert = alert;
            FarmId = farmId;
            _productivityRepository = new ProductivityRepository();
            _productivityRepositoryAPI = new ProductivityRepositoryAPI();


        }

        private void OnPropertyChanged(string propertyName) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public async Task LoadProductivityData()
        {
            try
            {
                var list = await _productivityRepository.GetProductivityByFarmId(FarmId);

                ProductivityList.Clear();
                foreach (var item in list)
                {
                    ProductivityList.Add(item);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar los datos de productividad: {ex.Message}");
            }
        }

        private async Task EditProductivity()
        {
            
        }








    }
}
