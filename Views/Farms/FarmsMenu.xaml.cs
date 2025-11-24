namespace Efarming_Sustainability.App.Views.Farms;

public partial class FarmsMenu : ContentPage
{
    private readonly Guid _farmId;
    public FarmsMenu(Guid farmId)
	{
		InitializeComponent();
        _farmId = farmId;
    }
}