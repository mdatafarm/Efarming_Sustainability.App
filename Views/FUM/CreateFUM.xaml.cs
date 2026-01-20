using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.App.Models_View;

namespace Efarming_Sustainability.App.Views.FUM;

public partial class CreateFUM : ContentPage
{
	private readonly Efarming_Sustainability.Core.Models.FamilyUnitMembers _familyUnitMembers;

	public CreateFUM(Guid farmId)
	{
		InitializeComponent();

		BindingContext = BindingContext = new FamilyUnitMembersViewModel(farmId.ToString(), new AlertRepository());

        NavigationPage.SetHasNavigationBar(this, false);
    }
}