using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using System.Data;

namespace NETCoreAPIConectaBarrio.Services
{
    public class SharedService : ISharedService
    {
        public List<OptionsModel> GetMenuOptions()
        {
            List<OptionsModel> options = new List<OptionsModel>();
            try
            {
                DataTable dt = SQLConnectionHelper.GetResultTable("SYS_M_OPTIONS_MENU");
                foreach(DataRow row in dt.Rows)  
                {
                    options.Add(new OptionsModel(row));
                }
            }
            catch(Exception ex) { }
            return options;
        }
    }
}
