using Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar;
using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Views;

namespace Efarming_Sustainability.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var token = Preferences.Get("Token", null);

            Page startPage;

            if (!string.IsNullOrEmpty(token))
            {
                
                startPage = new AppShell();
            }
            else
            {
                var loginPage = new UserLoginPage(
                    new UserLoginRepository(),
                    new IndicatorsRepository(),
                    new IndicatorsRepositoryAPI(),
                    new MunicipalityRepository(),
                    new MunicipalityRepositoryAPI(),
                    new CooperativeRepository(),
                    new CooperativeRepositoryAPI(),
                    new DepartmentsRepository(),
                    new DepartmentsRepositoryAPI(),
                    new VillageRepository(),
                    new VillageRepositoryAPI(),
                    new OwnershipTypesRepository(),
                    new OwnershipTypesRepositoryAPI(),
                    new PlantationStatusesRepository(),
                    new PlantationStatusesRepositoryAPI(),
                    new PlantationTypesRepository(),
                    new PlantationTypesRepositoryAPI(),
                    new PlantationVarietiesRepository(),
                    new PlantationVarietiesRepositoryAPI(),
                    new SupplyChainsRepository(),
                    new SupplyChainsRepositoryAPI());

                startPage = new NavigationPage(loginPage);
            }

            return new Window(startPage);
        }
    }
}