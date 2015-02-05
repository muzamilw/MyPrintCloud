
// Customer View 
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var customerViewListModel = function(companytId,custName, custCraetionDate, custStatus, custEmail) {
        var
            self,
            id = ko.observable(companytId),
            name = ko.observable(custName),
            creationdate = ko.observable(custCraetionDate),
            status = ko.observable(custStatus),
            email = ko.observable(custEmail),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid 
            isValid = ko.computed(function() {
                return errors().length === 0 ? true : false;
            }),
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function() {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function(source) {
            },
            // Reset
            reset = function() {
                dirtyFlag.reset();
            };
        self = {
            id:id,
            name: name,
            creationdate: creationdate,
            status: status,
            email: email,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    customerViewListModel.Create = function(source) {
        var customer = new customerViewListModel(
            source.CompnayId,
            source.CustomerName,
            source.DateCreted,
            source.Status,
            source.Email
        );
        return customer;
    };
    return {
        customerViewListModel: customerViewListModel
    };
});
