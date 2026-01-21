namespace Efarming_Sustainability.App.Views.Productivities;

using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Models_View;
using Efarming_Sustainability.Core.Models;
public partial class EditProductivity : ContentPage
{
	public EditProductivity(Productivity prod, IAlert alert)
	{
		InitializeComponent();

		BindingContext = new EditProductivityViewModel(prod, alert);

		NavigationPage.SetHasNavigationBar(this, false);
    }
}