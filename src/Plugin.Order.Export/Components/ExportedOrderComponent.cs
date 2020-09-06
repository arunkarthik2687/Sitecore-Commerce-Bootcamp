using System;
using Sitecore.Commerce.Core;

namespace Plugin.Bootcamp.Exercises.Order.Export.Components
{
    public class ExportedOrderComponent : Component
    {   //Export Date
        public DateTime? ExportedDate { get; set; }
        //Export Filename
        public string ExportedFilename { get; set; }

    }
}
