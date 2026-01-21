using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Models_View;

namespace Efarming_Sustainability.App.Views.Productivity;

public partial class DashboardProductivity : ContentPage
{
    private Guid _farmId;
    private readonly IAlert _alert;
    public DashboardProductivity(Guid FarmId, IAlert alert)
    {
        InitializeComponent();
        _alert = alert;
        _farmId = FarmId;
        BindingContext = new DashboardProductivityViewModel(_farmId, _alert);

        NavigationPage.SetHasNavigationBar(this, false);
    }
}