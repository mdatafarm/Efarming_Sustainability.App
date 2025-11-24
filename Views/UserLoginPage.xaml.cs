

using Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar;
using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Views;

public partial class UserLoginPage : ContentPage
{
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
    public UserLoginPage(UserLoginRepository userLoginRepository, 
        IndicatorsRepository indicatorsRepository,
        IndicatorsRepositoryAPI indicatorsRepositoryAPI, 
        MunicipalityRepository municipalitiesRepository,
        MunicipalityRepositoryAPI municipalitiesRepositoryAPI,
        CooperativeRepository cooperativeRepository,
        CooperativeRepositoryAPI cooperativeRepositoryAPI,
        DepartmentsRepository departmentsRepository,
        DepartmentsRepositoryAPI departmentsRepositoryAPI,
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

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            LoginResultLabel.Text = "Por favor ingresa usuario y contraseña.";
            return;
        }

        try
        {
            var loginResponse = await _userLoginRepository.LoginUsuario(username, password);

            if (loginResponse != null)
            {
                var IndicatorsReponse = await _indicatorsRepositoryAPI.GetIndicatorsAsync(loginResponse.UserId);
                await _indicatorsRepository.SaveIndicatorsLocally(IndicatorsReponse);

                var CooperativesResponse = await _cooperativeRepositoryAPI.GetCooperatives();
                await _cooperativeRepository.SaveCooperativesLocally(CooperativesResponse);

                var DepartmentsResponse = await _departmentsRepositoryAPI.GetDepartments();
                await _departmentsRepository.SaveDepartamentsLocally(DepartmentsResponse);

                var MunicipalitiesResponse = await _municipalitiesRepositoryAPI.GetMunicipalities();
                await _municipalitiesRepository.SaveMunicipalityLocally(MunicipalitiesResponse);

                var VillagesResponse = await _villageRepositoryAPI.GetVillages();
                await _villageRepository.SaveVillagesLocally(VillagesResponse);

                var OwnershipTypesResponse = await _ownershipTypesRepositoryAPI.GetOwnershipTypes();
                await _ownershipTypesRepository.SaveOwnershipTypesLocally(OwnershipTypesResponse);

                var PlantationStatusesResponse = await _plantationStatusesRepositoryAPI.GetPlantationStatuses();
                await _plantationStatusesRepository.SavePlantationStatusesLocally(PlantationStatusesResponse);

                var PlantationTypesResponse = await _plantationTypesRepositoryAPI.GetPlantationTypes();
                await _plantationTypesRepository.SavePlantationTypesLocally(PlantationTypesResponse);

                var PlantationVarietiesResponse = await _plantationVarietiesRepositoryAPI.GetPlantationVarieties();
                await _plantationVarietiesRepository.SavePlantationVarietiesLocally(PlantationVarietiesResponse);

                var SupplyChainsResponse = await _supplyChainsRepositoryAPI.GetSupplyChains();
                await _supplyChainsRepository.SaveSupplyChainsLocally(SupplyChainsResponse);


                if (Shell.Current != null)
                {
                    
                    await Shell.Current.GoToAsync("//IndicatorsPage");
                    return;
                }
                if (this.Navigation != null)
                {
                    await Navigation.PushAsync(new IndicatorsPage(_indicatorsRepository, loginResponse.UserId));
                    
                    return;
                }



                Application.Current.MainPage = new NavigationPage(new IndicatorsPage(_indicatorsRepository, loginResponse.UserId));
                return;
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }

    
}