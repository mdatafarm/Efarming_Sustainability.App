using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Models_View;
using Efarming_Sustainability.App.Views.Farm;
using Efarming_Sustainability.Core.Models;

namespace Efarming_Sustainability.App.Views.FUM;

public partial class DashboardFUM : ContentPage
{
   
    private Guid _farmId;
    private readonly IAlert _alert;
    public DashboardFUM(Guid FarmId, IAlert alert)
	{
		InitializeComponent();
        _alert = alert;
        BindingContext = new FUMMenuViewModel(FarmId,alert);

        _farmId = FarmId;

        NavigationPage.SetHasNavigationBar(this, false);
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is FUMMenuViewModel vm)
        {
            await vm.LoadFUMMembers();
        }
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var radio = (RadioButton)sender;
            var fum = (FamilyUnitMembers)radio.BindingContext;
            ((FUMMenuViewModel)BindingContext).SelectedFUM = fum;
        }
    }

    
}