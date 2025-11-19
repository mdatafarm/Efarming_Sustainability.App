using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class AlertRepository:IAlert
    {
        public async Task ShowAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
