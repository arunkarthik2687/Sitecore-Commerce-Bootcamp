using System;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using Plugin.Bootcamp.Exercises.Order.ConfirmationNumber.Policies;
using Sitecore.Commerce.Plugin.Orders;
using System.Diagnostics.Contracts;


namespace Plugin.Bootcamp.Exercises.Order.ConfirmationNumber.Blocks
{
    [PipelineDisplayName("OrderConfirmation.OrderConfirmationIdBlock")]
    public class OrderPlacedAssignCustomConfirmationIdBlock : PipelineBlock<Sitecore.Commerce.Plugin.Orders.Order, Sitecore.Commerce.Plugin.Orders.Order, CommercePipelineExecutionContext>
    {
        public override Task<Sitecore.Commerce.Plugin.Orders.Order> Run(Sitecore.Commerce.Plugin.Orders.Order arg, CommercePipelineExecutionContext context)
        {

            Contract.Requires(arg != null);
            Contract.Requires(context != null);
            
            OrderPlacedAssignCustomConfirmationIdBlock confirmationIdBlock = this;
            // ISSUE: explicit non-virtual call
            Condition.Requires<Sitecore.Commerce.Plugin.Orders.Order>(arg).IsNotNull<Sitecore.Commerce.Plugin.Orders.Order>((confirmationIdBlock.Name) + ": The order can not be null");
            string uniqueCode=string.Empty;
            try
            {
                uniqueCode = GenerateCustomID(context);
            }
            catch(Exception ex)
            {
                context.CommerceContext.LogException((confirmationIdBlock.Name) + "UniqueIDException", ex);
                throw;
            }
            arg.OrderConfirmationId = uniqueCode;
            return Task.FromResult<Sitecore.Commerce.Plugin.Orders.Order>(arg);
        }
        /*Custom id generation block*/
        private string GenerateCustomID(CommercePipelineExecutionContext context)
        {
            var orderidPolicy = context.GetPolicy<OrderNumberPolicy>();
            if (orderidPolicy.IncludeDate == true)
            {
                return $"{orderidPolicy.Prefix},{DateTime.Today.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)},{orderidPolicy.Suffix},{Guid.NewGuid().ToString()}";
            }
            else
            {
                return $"{orderidPolicy.Prefix},{string.Empty},{orderidPolicy.Suffix},{Guid.NewGuid().ToString()}";
            }

            
        }
    }

}
