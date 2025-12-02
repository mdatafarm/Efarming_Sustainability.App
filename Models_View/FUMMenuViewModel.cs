using Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar;
using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Efarming_Sustainability.App.Models_View
{
    public class FUMMenuViewModel
    {
        private readonly FamilyUnitMembersRepositoryAPI _familyUnitMembersRepository = new();

        public FamilyUnitMembers SelectedFUM { get; }

        public Farm SelectedFarm { get; }
        public ICommand EditFUMCommand { get; }
        public ICommand CreateFUMCommand { get; }

        public ICommand UploadFUMCommand { get; }

        public FUMMenuViewModel(FamilyUnitMembersRepositoryAPI familyUnitMembersRepository, FamilyUnitMembers fum, Farm selectedFarm)
        {
            SelectedFUM = fum;
            SelectedFarm = selectedFarm;
            _familyUnitMembersRepository = familyUnitMembersRepository;
            EditFUMCommand = new Command(async () => await EditFUM());
            CreateFUMCommand = new Command(async () => await CreateFUM(selectedFarm.Id));
            UploadFUMCommand = new Command(async () => await UploadFUMAsync());
            SelectedFarm = selectedFarm;
        }

        private async Task EditFUM()
        {
            Console.WriteLine("SelectedFUM es null? => " + (SelectedFUM == null));
            Console.WriteLine("Id => " + SelectedFUM?.Id);
            if (SelectedFUM == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No hay miembro de unidad familiar seleccionado.", "OK");
                return;
            }
            // Aquí iría la navegación a la página de edición, similar al ejemplo anterior.
        }

        private async Task CreateFUM(Guid farmId)
        {
            Console.WriteLine("ID de la finca recibida => " + farmId);


            //await Shell.Current.Navigation.PushAsync(new CreateFUMPage(farmId));
        }

        private async Task UploadFUMAsync()
        {
            try
            {
                // 1. Validar conexión a internet
                var connectivity = Connectivity.Current.NetworkAccess;
                if (connectivity != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Sin conexión",
                        "Por favor revisa tu conexión a internet antes de cargar la información.",
                        "OK");
                    return;
                }
                // 2. Validar FUM
                if (SelectedFUM == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "No hay miembro de unidad familiar seleccionado.",
                        "OK");
                    return;
                }
                // 3. Llamar al API
                var success = await _familyUnitMembersRepository.UpdateFUMAsync(SelectedFUM);
                if (success)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Éxito",
                        "Miembro de unidad familiar actualizado correctamente.",
                        "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "No se pudo actualizar el miembro de unidad familiar.",
                        "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el miembro de unidad familiar: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    $"Ocurrió un error al cargar el miembro de unidad familiar: {ex.Message}",
                    "OK");
            }
        }

    }
}
