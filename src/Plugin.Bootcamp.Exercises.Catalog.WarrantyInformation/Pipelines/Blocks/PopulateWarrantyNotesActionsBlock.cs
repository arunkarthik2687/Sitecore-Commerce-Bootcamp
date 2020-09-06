using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Components;

namespace Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Pipelines.Blocks
{
    [PipelineDisplayName("PopulateWarrantyNotesActionsBlock")]
    class PopulateWarrantyNotesActionsBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{Name}: The argument cannot be null.");

            if(string.IsNullOrEmpty(arg?.Name)||!arg.Name.Equals("WarrantyNotes", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(arg);
            }
            //Declaring the Edit Action view of Warranty
            var actionPolicy = arg.GetPolicy<ActionsPolicy>();
            actionPolicy.Actions.Add(
                new EntityActionView
                {
                    Name = "WarrantyNotes-Edit",
                    DisplayName = "Edit Sellable Item Warranty Notes",
                    Description = "Edit Sellable Item Warranty Notes",
                    IsEnabled = true,
                    EntityView = arg.Name,
                    Icon = "edit"
                }
                );

            return Task.FromResult(arg);
        }
    }
}
