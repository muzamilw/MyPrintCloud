define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Supplier List view Entity
    SupplierListView = function (specifiedSupplierId, specifiedName, specifiedUrl
                           ) {
        var
            self,
            //Unique ID
            supplierId = ko.observable(specifiedSupplierId),
            //Name
            name = ko.observable(specifiedName),
            //URL
            url = ko.observable(specifiedUrl),
            //is Selected
            isSelected = ko.observable(false);

        self = {
            supplierId: supplierId,
            name: name,
            url: url,
            isSelected: isSelected,
        };
        return self;
    };
    //Create Factory 
    SupplierListView.Create = function (source) {
        return new SupplierListView(source.SupplierId, source.Name, source.URL);
    };
    return {
        SupplierListView: SupplierListView,
    };
});