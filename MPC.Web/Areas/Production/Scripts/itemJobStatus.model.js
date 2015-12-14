/*
    Module with the model for the Item Job Status
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    // #region __________________  Item   ______________________

    // ReSharper disable once InconsistentNaming
     Item = function (specifiedItemId, specifiedEstimateId, specifiedCode, specifiedCompanyName, specifiedProductName, specifiedQty1, specifiedStatusId,
         specifiedJobEstimatedCompletionDateTime, specifiedQty1NetTotal, specifiedJobEstimatedStartDateTime, specifiedCodeOrder) {

         var self,
             id = ko.observable(specifiedItemId),
             estimateId = ko.observable(specifiedEstimateId),
             code = ko.observable(specifiedCode),
             orderCode = ko.observable(specifiedCodeOrder),
             companyName = ko.observable(specifiedCompanyName),
             productName = ko.observable(specifiedProductName),
             qty1 = ko.observable(specifiedQty1),
             statusId = ko.observable(specifiedStatusId),
             startdateTimeCalculation = ko.observable(specifiedJobEstimatedStartDateTime),
             completiondateTimeCalculation = ko.observable(specifiedJobEstimatedCompletionDateTime),
              // Job Estimated Start Date Time
                jobEstimatedStartDateTime = ko.observable(specifiedJobEstimatedStartDateTime ? moment(specifiedJobEstimatedStartDateTime).toDate() : undefined),
             //jobEstimatedCompletionDateTime = ko.observable((specifiedJobEstimatedCompletionDateTime !== null && specifiedJobEstimatedCompletionDateTime !== undefined) ? moment(specifiedJobEstimatedCompletionDateTime, ist.datePattern).toDate() : undefined),
             jobEstimatedCompletionDateTime = ko.observable((specifiedJobEstimatedCompletionDateTime !== null && specifiedJobEstimatedCompletionDateTime !== undefined) ? moment(specifiedJobEstimatedCompletionDateTime).toDate() : undefined),
             qty1NetTotal = ko.observable(specifiedQty1NetTotal),
             convertToServerData = function () {
                 return {
                     ItemId: id(),
                     StatusId: statusId()
                 }
             };

         self = {
             startdateTimeCalculation: startdateTimeCalculation,
             completiondateTimeCalculation:completiondateTimeCalculation,
             id: id,
             jobEstimatedStartDateTime:jobEstimatedStartDateTime,
             estimateId: estimateId,
             code: code,
             companyName: companyName,
             productName: productName,
             qty1: qty1,
             orderCode:orderCode,
             statusId: statusId,
             jobEstimatedCompletionDateTime: jobEstimatedCompletionDateTime,
             qty1NetTotal: qty1NetTotal,
             convertToServerData:convertToServerData
         };
         return self;
     };

    Item.Create = function (source) {
        return new Item(source.ItemId, source.EstimateId, source.ItemCode, source.CompanyName, source.ProductName, source.Qty1, source.StatusId, source.JobEstimatedCompletionDateTime,
            source.Qty1NetTotal, source.JobEstimatedStartDateTime, source.OrderCode);

    };
    // #endregion __________________  Item   ______________________


    return {
        Item: Item
    };
});