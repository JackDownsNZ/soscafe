﻿using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace SosCafe.Admin
{
    public class VendorDetailsEntity : TableEntity
    {
        public VendorDetailsEntity()
        {
            PartitionKey = "Vendors";
        }

        public DateTime RegisteredDate { get; set; }

        public string BusinessName { get; set; }

        private string shopifyId;
        public string ShopifyId
        {
            get
            {
                return shopifyId;
            }
            set
            {
                shopifyId = value;
                RowKey = shopifyId;
            }
        }

        public string ContactName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string BankAccountNumber { get; set; }

        public bool IsValidated { get; set; }

        public DateTime? DateAcceptedTerms { get; set; }
    }
}
