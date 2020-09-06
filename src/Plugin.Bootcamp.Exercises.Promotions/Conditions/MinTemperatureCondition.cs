using System;

using Sitecore.Commerce.Core;
using Sitecore.Framework.Rules;
using Sitecore.Commerce.Plugin.Carts;

namespace Plugin.Bootcamp.Exercises.Promotions
{
    [EntityIdentifier("MinTemperatureCondition")]
    public class MinTemperatureCondition : ICartsCondition
    {
        /* Rule Values are needed for checking a condition
         */
        public IRuleValue<Decimal> MinimumTemperature { get; set; }
        public IRuleValue<String> City { get; set; }
        public IRuleValue<String> Country { get; set; }

        public bool Evaluate(IRuleExecutionContext context)
        {
            /* STUDENT: Complete the Evaluate method to:
             * Retrieve the current temperature
             * Compare it to the temperature stored in the Policy
             * Return the result of that comparison
             */
            var minimumTemperature = MinimumTemperature.Yield(context);
            var city = City.Yield(context);
            var country = Country.Yield(context);


            // Fetch applicationid from the policy created.
            CommerceContext commerceContext = context.Fact<CommerceContext>((string)null);
            var weatherServicePolicy = commerceContext.GetPolicy<WeatherServiceClientPolicy>();

            var currentTemperature = GetCurrentTemperature(city, country, weatherServicePolicy.ApplicationId);

            if(currentTemperature > minimumTemperature)
            return true;
            else
            return false;
           
        }
        //Using Weatherservice to fetch the current temperature
        public decimal GetCurrentTemperature(string city, string country, string applicationId)
        {
            WeatherService weatherService = new WeatherService(applicationId);
            var temperature = weatherService.GetCurrentTemperature(city, country).Result;

            return (decimal)(temperature.Max);
        }
    }
}
