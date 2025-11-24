using Efarming_Sustainability.App.Models_View;

namespace Efarming_Sustainability.App.Views.Farms;

public partial class EditFarms : ContentPage
{
    private readonly Efarming_Sustainability.Core.Models.Farm _farm;
    public EditFarms(Efarming_Sustainability.Core.Models.Farm farm)
    {
        InitializeComponent();
        _farm = farm;

        BindingContext = new EditFarmsViewModel(_farm);
    }



}