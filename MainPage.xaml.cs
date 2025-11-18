using Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar;
using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;

namespace Efarming_Sustainability.App
{
    public partial class MainPage : ContentPage
    {


        int count = 0;
        private readonly UserLoginRepository _userLoginRepository;
        private readonly IndicatorsRepository _indicatorsRepository;
        private readonly IndicatorsRepositoryAPI _indicatorsRepositoryAPI;
        private readonly CooperativeRepository _cooperativeRepository;
        private readonly CooperativeRepositoryAPI _cooperativeRepositoryAPI;
        private readonly DepartmentsRepository _departmentsRepository;
        private readonly DepartmentsRepositoryAPI _departmentsRepositoryAPI;
        private readonly MunicipalityRepository _municipalitiesRepository;
        private readonly MunicipalityRepositoryAPI _municipalitiesRepositoryAPI;
        private readonly VillageRepository _villageRepository;
        private readonly VillageRepositoryAPI _villageRepositoryAPI;
        private readonly OwnershipTypesRepository _ownershipTypesRepository;
        private readonly OwnershipTypesRepositoryAPI _ownershipTypesRepositoryAPI;
        private readonly PlantationStatusesRepository _plantationStatusesRepository;
        private readonly PlantationStatusesRepositoryAPI _plantationStatusesRepositoryAPI;
        private readonly PlantationTypesRepository _plantationTypesRepository;
        private readonly PlantationTypesRepositoryAPI _plantationTypesRepositoryAPI;
        private readonly PlantationVarietiesRepository _plantationVarietiesRepository;
        private readonly PlantationVarietiesRepositoryAPI _plantationVarietiesRepositoryAPI;
        private readonly SupplyChainsRepository _supplyChainsRepository;
        private readonly SupplyChainsRepositoryAPI _supplyChainsRepositoryAPI;


        public MainPage(UserLoginRepository userLoginRepository, 
            IndicatorsRepository indicatorsRepository, 
            IndicatorsRepositoryAPI indicatorsRepositoryAPI,
            CooperativeRepository cooperativeRepository,
            CooperativeRepositoryAPI cooperativeRepositoryAPI,
            DepartmentsRepository departmentsRepository,
            DepartmentsRepositoryAPI departmentsRepositoryAPI,
            MunicipalityRepository municipalitiesRepository,
            MunicipalityRepositoryAPI municipalitiesRepositoryAPI,
            VillageRepository villageRepository,
            VillageRepositoryAPI villageRepositoryAPI,
            OwnershipTypesRepository ownershipTypesRepository,
            OwnershipTypesRepositoryAPI ownershipTypesRepositoryAPI,
            PlantationStatusesRepository plantationStatusesRepository,
            PlantationStatusesRepositoryAPI plantationStatusesRepositoryAPI,
            PlantationTypesRepository plantationTypesRepository,
            PlantationTypesRepositoryAPI plantationTypesRepositoryAPI,
            PlantationVarietiesRepository plantationVarietiesRepository,
            PlantationVarietiesRepositoryAPI plantationVarietiesRepositoryAPI,
            SupplyChainsRepository supplyChainsRepository,
            SupplyChainsRepositoryAPI supplyChainsRepositoryAPI)
        {
            _userLoginRepository = userLoginRepository;
            _indicatorsRepository = indicatorsRepository;
            _indicatorsRepositoryAPI = indicatorsRepositoryAPI;
            _cooperativeRepository = cooperativeRepository;
            _cooperativeRepositoryAPI = cooperativeRepositoryAPI;
            _departmentsRepository = departmentsRepository;
            _departmentsRepositoryAPI = departmentsRepositoryAPI;
            _municipalitiesRepository = municipalitiesRepository;
            _municipalitiesRepositoryAPI = municipalitiesRepositoryAPI;
            _villageRepository = villageRepository;
            _villageRepositoryAPI = villageRepositoryAPI;
            _ownershipTypesRepository = ownershipTypesRepository;
            _ownershipTypesRepositoryAPI = ownershipTypesRepositoryAPI;
            _plantationStatusesRepository = plantationStatusesRepository;
            _plantationStatusesRepositoryAPI = plantationStatusesRepositoryAPI;
            _plantationTypesRepository = plantationTypesRepository;
            _plantationTypesRepositoryAPI = plantationTypesRepositoryAPI;
            _plantationVarietiesRepository = plantationVarietiesRepository;
            _plantationVarietiesRepositoryAPI = plantationVarietiesRepositoryAPI;
            _supplyChainsRepository = supplyChainsRepository;
            _supplyChainsRepositoryAPI = supplyChainsRepositoryAPI;

            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
