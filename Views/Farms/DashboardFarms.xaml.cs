

using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Models_View;
using Efarming_Sustainability.App.Views.Farms;


namespace Efarming_Sustainability.App.Views.Farm;

public partial class DashboardFarms : ContentPage
{
    private readonly Efarming_Sustainability.Core.Models.Farm _farm;
    private readonly IAlert _alert;
    public DashboardFarms(Efarming_Sustainability.Core.Models.Farm farm, IAlert alert)
    {
        InitializeComponent();
        _farm = farm;
        _alert = alert;
        BindingContext = new DashboardFarmsViewModel(_farm, alert);
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {

        await Application.Current.MainPage.Navigation.PushAsync(new FarmsMenu(_farm,_alert));
    }

}