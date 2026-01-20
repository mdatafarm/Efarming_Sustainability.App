using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Models_View;
using Efarming_Sustainability.Core.Models;

namespace Efarming_Sustainability.App.Views.FUM;

public partial class EditFUM : ContentPage
{

    public EditFUM(FamilyUnitMembers fum, IAlert alert)
    {
        InitializeComponent();

        BindingContext = new EditFUMViewModel(fum, alert);

        NavigationPage.SetHasNavigationBar(this, false);
    }
}