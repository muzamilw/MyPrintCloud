define(["ko", "underscore", "underscore-ko"], function (ko) {
    var deliverycarrier = function () {

        var
            self,
            carrierId = ko.observable().extend({ required: true }),
            carrierName = ko.observable(),
            url = ko.observable(),
            apiKey = ko.observable(),
            apiPassword = ko.observable(),
            isenable = ko.observable()
            
            errors = ko.validation.group({
                carrierId: carrierId,
                carrierName: carrierName,
                url: url,
                apiKey: apiKey,
                apiPassword: apiPassword,
                isenable: isenable
                
            }),
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;;
            }),
            dirtyFlag = new ko.dirtyFlag({
                carrierId: carrierId,
                carrierName: carrierName,
                url: url,
                apiKey: apiKey,
                apiPassword: apiPassword,
                isenable: isenable
                
            }),
            hasChanges = ko.computed(function ()
            {
                return dirtyFlag.isDirty();
            }),

            reset = function ()
            {
                dirtyFlag.reset();
            };
            self =
            {
                carrierId: carrierId,
                carrierName: carrierName,
                url: url,
                apiKey: apiKey,
                apiPassword: apiPassword,
                isenable: isenable
            };
        return self;
    };

    var deliverycarrierClientMapper = function (source)
    {
        var odeliverycarrier = new deliverycarrier();
        odeliverycarrier.carrierId = ko.observable(source.CarrierId),
        odeliverycarrier.carrierName = ko.observable(source.CarrierName),
        odeliverycarrier.url = ko.observable(source.Url),
        odeliverycarrier.apiKey = ko.observable(source.ApiKey),
        odeliverycarrier.apiPassword = ko.observable(source.ApiPassword),
        odeliverycarrier.isenable = ko.observable(source.isEnable)
        
        return odeliverycarrier;

    };
    var deliverycarrierServerMapper = function (source)
    {
        var result = {};
        result.CarrierId = source.carrierId(),
        result.CarrierName = source.carrierName(),
        result.Url = source.url(),
        result.ApiKey = source.apiKey(),
        result.ApiPassword = source.apiPassword(),
        result.isEnable = source.isenable()
        
        return result;
    };

    return {
        deliverycarrier: deliverycarrier,
        deliverycarrierClientMapper: deliverycarrierClientMapper,
        deliverycarrierServerMapper: deliverycarrierServerMapper
    };
});