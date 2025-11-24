

using Efarming_Sustainability.App.Models_View;
using Efarming_Sustainability.App.Views.Farms;


namespace Efarming_Sustainability.App.Views.Farm;

public partial class DashboardFarms : ContentPage
{
    private readonly Efarming_Sustainability.Core.Models.Farm _farm;
    public DashboardFarms(Efarming_Sustainability.Core.Models.Farm farm)
    {
        InitializeComponent();
        _farm = farm;

        BindingContext = new DashboardFarmsViewModel(_farm);
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {

        await Application.Current.MainPage.Navigation.PushAsync(new FarmsMenu(_farm.Id));
    }

}