using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    class IndicatorsViewModel
    {
        private readonly IndicatorsRepository _indicatorsRepository;

        public IndicatorsViewModel(IndicatorsRepository indicatorsRepository)
        {
            _indicatorsRepository = new IndicatorsRepository();
        }

        public int AssociatedPeopleCount { get; set; }
        public int SustainabilityContactsCount { get; set; }
        public int ContactsLastWeek { get; set; }
        public int SurveysDone { get; set; }

        public async Task LoadIndicatorsAsync(Guid userId)
        {
            try
            {
                Indicators indicators = await _indicatorsRepository.GetLocallyIndicators();
                AssociatedPeopleCount = indicators.AssociatedPeopleCount;
                SustainabilityContactsCount = indicators.SustainabilityContactsCount;
                ContactsLastWeek = indicators.ContactsLastWeek;
                SurveysDone = indicators.SurveysDone;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error loading indicators: {ex.Message}");
            }
        }

    }
}
