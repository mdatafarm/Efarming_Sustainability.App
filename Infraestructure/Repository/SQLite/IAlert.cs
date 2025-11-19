using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public interface IAlert
    {
        Task ShowAlert(string title, string message, string cancel);
    }
}
