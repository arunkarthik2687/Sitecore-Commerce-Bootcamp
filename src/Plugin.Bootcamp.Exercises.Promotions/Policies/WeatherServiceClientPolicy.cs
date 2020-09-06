using Sitecore.Commerce.Core;


namespace Plugin.Bootcamp.Exercises.Promotions
{
    class WeatherServiceClientPolicy : Policy
    {
        public WeatherServiceClientPolicy()
        {
            this.ApplicationId = string.Empty;
        }

        public string ApplicationId { get; set; }
    }
}
