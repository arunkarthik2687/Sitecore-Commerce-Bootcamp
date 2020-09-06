﻿namespace Plugin.Bootcamp.Exercises.VatTax.Entities
{
    using System;
    using System.Collections.Generic;

    using Sitecore.Commerce.Core;
    
    public class VatTaxEntity : CommerceEntity
    {
        public VatTaxEntity()
        {
            this.DateCreated = DateTime.UtcNow;
            this.DateUpdated = this.DateCreated;
            this.CountryCode = "US";
            this.TaxTag = string.Empty;
            this.TaxPct = 0M;
        }

        public VatTaxEntity(string id): this()
        {
            this.Id = id;
        }

        public string CountryCode { get; set; }

        public string TaxTag { get; set; }

        public decimal TaxPct { get; set; }
    }
}