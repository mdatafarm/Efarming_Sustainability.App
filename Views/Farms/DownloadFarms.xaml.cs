using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Models_View;

namespace Efarming_Sustainability.App.Views.Farm;

public partial class DownloadFarms : ContentPage
{

    private readonly LocalFarmsViewModel _viewModel;
    public DownloadFarms(IAlert alert)
    {
        InitializeComponent();
        BindingContext = new LocalFarmsViewModel(new FarmsRepository(),alert);
        Console.WriteLine("BindingContext asignado: " + (BindingContext != null));
        NavigationPage.SetHasNavigationBar(this, false);
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is LocalFarmsViewModel vm)
            await vm.LoadFarmsAsync();
    }

    

}