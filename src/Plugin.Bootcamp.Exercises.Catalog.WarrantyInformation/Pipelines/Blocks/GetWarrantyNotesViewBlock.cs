using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Components;
using Serilog;

namespace Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Pipelines.Blocks
{
    [PipelineDisplayName("GetWarrantyNotesViewBlock")]
    public class GetWarrantyNotesViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{Name}: The argument cannot be null.");
            var catalogViewsPolicy = context.GetPolicy<KnownCatalogViewsPolicy>();

            var isvariationView = arg.Name.Equals(catalogViewsPolicy.Variant, StringComparison.OrdinalIgnoreCase);
            var ismasterView = arg.Name.Equals(catalogViewsPolicy.Master, StringComparison.OrdinalIgnoreCase);
            var isWarrantyNotesView = arg.Name.Equals("WarrantyNotes", StringComparison.OrdinalIgnoreCase);
            var isConnectView = arg.Name.Equals(catalogViewsPolicy.ConnectSellableItem, StringComparison.OrdinalIgnoreCase);
            var request = context.CommerceContext.GetObject<EntityViewArgument>();

            /* Making sure only the exact request goes through */
            if (string.IsNullOrEmpty(arg.Name) || (!isConnectView && !isvariationView && !ismasterView && !isWarrantyNotesView))
            {
                return Task.FromResult<EntityView>(arg);
            }

            if (!(request.Entity is SellableItem))
            {
                return Task.FromResult<EntityView>(arg);
            }

            var sellableItem = (SellableItem)request.Entity;

            var variationId = string.Empty;
            /*Checking whether the required view is for Variant*/
            if(isvariationView && !string.IsNullOrEmpty(arg.ItemId))
            {
                variationId = arg.ItemId;
            }

            /* Checking for Edit Request*/
            var isEditView = !string.IsNullOrEmpty(arg.Action) && arg.Action.Equals("WarrantyNotes-Edit", StringComparison.OrdinalIgnoreCase);
            var componentView = arg;

            if(!isEditView)
            {
                componentView = new EntityView()
                {
                    Name="WarrantyNotes",
                    DisplayName="Warranty Information",
                    EntityId=arg.EntityId,
                    EntityVersion=request.EntityVersion==null?1:(int)request.EntityVersion,
                    ItemId=variationId
                };
                arg.ChildViews.Add(componentView);
                Log.Information("Get entity view block, version:", arg.EntityVersion);
            }
            else
            {
                Log.Information("Edit entity view , version:", arg.EntityVersion);
            }
            //Adding properties for Warranty view
            if(sellableItem!=null && (sellableItem.HasComponent<WarrantyNotesComponent>(variationId)|| isConnectView||isEditView))
            {
                var component = sellableItem.GetComponent<WarrantyNotesComponent>(variationId);

                componentView.Properties.Add(new ViewProperty
                {
                    Name = nameof(WarrantyNotesComponent.WarrantyType),
                    DisplayName = "Description",
                    RawValue = component.WarrantyType,
                    IsReadOnly = !isEditView,
                    IsRequired = false
                });
               
                componentView.Properties.Add(new ViewProperty
                {
                    Name = nameof(WarrantyNotesComponent.WarrantyTerm),
                    DisplayName = "Warranty Term(years)",
                    RawValue = component.WarrantyTerm,
                    IsReadOnly = !isEditView,
                    IsRequired = false
                });

            }
            return Task.FromResult(arg);
        }
    }
}
