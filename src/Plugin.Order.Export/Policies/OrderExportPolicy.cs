using Sitecore.Commerce.Core;

namespace Plugin.Bootcamp.Exercises.Order.Export.Policies
{
    public class OrderExportPolicy : Policy
    {   //File location, gets its value from the json file
        public string ExportToFileLocation { get; set; }
    }
}
