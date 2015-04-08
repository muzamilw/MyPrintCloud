define(["ko", "underscore", "underscore-ko"], function (ko) {
    var deliverycarrier = function (carrierId, carrierName, carrierUrl, apikey, apipassword, isenable) {

        var
            self,
            carrierId = ko.observable(carrierId),
            carrierName = ko.observable(carrierName),
            url = ko.observable(carrierUrl),
            apiKey = ko.observable(apikey),
            apiPassword = ko.observable(apipassword),
            isenable = ko.observable(isenable)
            
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
        odeliverycarrier.carrierId(source.CarrierId),
        odeliverycarrier.carrierName(source.CarrierName),
        odeliverycarrier.url(source.Url),
        odeliverycarrier.apiKey(source.ApiKey),
        odeliverycarrier.apiPassword(source.ApiPassword),
        odeliverycarrier.isenable(source.isEnable)
        
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