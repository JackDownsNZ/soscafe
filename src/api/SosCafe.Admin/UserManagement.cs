﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using SosCafe.Admin.ApiModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SosCafe.Admin
{
    public static class UserManagement
    {
        [FunctionName("GetVendorsForUser")]
        public static async Task<IActionResult> GetVendorsForUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "vendors")] HttpRequest req,
            [Table("VendorUserAssignments", Connection = "SosCafeStorage")] CloudTable vendorUserAssignmentsTable,
            ILogger log)
        {
            var userId = "TODO-user";

            // Read all records from table storage where the partition key is the user's ID.
            TableContinuationToken token = null;
            var availableVendorAssignments = new List<VendorUserAssignmentEntity>();
            var filterToUserPartition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userId);
            do
            {
                var queryResult = await vendorUserAssignmentsTable.ExecuteQuerySegmentedAsync(new TableQuery<VendorUserAssignmentEntity>().Where(filterToUserPartition), token);
                availableVendorAssignments.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            // Map the results to a response model.
            var mappedResults = availableVendorAssignments.Select(entity => new VendorSummaryApiModel
            {
                Id = entity.VendorShopifyId,
                BusinessName = entity.VendorName
            });

            // Return the results.
            return new OkObjectResult(mappedResults);
        }

        internal static async Task<string> EnsureUserCreatedAsync()
        {
            // TODO implement
            return "TODO-user";
        }
    }
}
