using Efarming_Sustainability.App.Models_View;

namespace Efarming_Sustainability.App.Views.Farms;

public partial class FarmsMenu : ContentPage
{

    private readonly Efarming_Sustainability.Core.Models.Farm _farm;
    private readonly Guid _farmId;
    public FarmsMenu(Efarming_Sustainability.Core.Models.Farm farm)
    {
        InitializeComponent();
        _farm = farm;

        BindingContext = new FarmsMenuViewModel(_farm);


    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new EditFarms(_farm));
    }

}