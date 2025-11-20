using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Models_View;
using Efarming_Sustainability.App.Views.Farms;

namespace Efarming_Sustainability.App.Views;

public partial class IndicatorsPage : ContentPage
{
    private readonly IndicatorsRepository _indicatorsRepository;
    private readonly IndicatorsViewModel _vm;
    private readonly Guid _currentUserId;
    public IndicatorsPage(IndicatorsRepository indicatorsRepository, Guid currentUserId)
    {
        InitializeComponent();
        _indicatorsRepository = indicatorsRepository;
        _currentUserId = currentUserId;
        _vm = new IndicatorsViewModel(_indicatorsRepository);
        BindingContext = _vm;
        Shell.SetNavBarIsVisible(this, false);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Cargar los indicadores guardados y actualizar UI
        await _vm.LoadIndicatorsAsync(_currentUserId);
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new IndexFarms());
    }


}