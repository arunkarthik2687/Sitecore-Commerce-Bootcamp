using Sitecore.Commerce.Core;

namespace Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Components
{
    public class WarrantyNotesComponent : Component
    {
        /* STUDENT: Add properties as specified in the requirements */
        public WarrantyNotesComponent()
        {
            this.WarrantyTerm = 1;
            this.WarrantyType = string.Empty;
        }
        public string WarrantyType { get; set; }
        public int WarrantyTerm { get; set; }
    }
}
