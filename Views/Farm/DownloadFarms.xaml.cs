using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Models_View;

namespace Efarming_Sustainability.App.Views.Farm;

public partial class DownloadFarms : ContentPage
{

    private readonly LocalFarmsViewModel _viewModel;
    public DownloadFarms()
	{
		InitializeComponent();
		BindingContext = new LocalFarmsViewModel(new FarmsRepository());
        Console.WriteLine("BindingContext asignado: " + (BindingContext != null));
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is LocalFarmsViewModel vm)
            await vm.LoadFarmsAsync();
    }

}