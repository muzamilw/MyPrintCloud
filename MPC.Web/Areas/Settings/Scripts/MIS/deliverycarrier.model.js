define(["ko", "underscore", "underscore-ko"], function (ko) {
    var DeliveryCarrier = function () {

        var
            self,
            carrierId = ko.observable(),
            carrierName = ko.observable().extend({ required: true }),
            url = ko.observable().extend({ required: true }),
            apiKey = ko.observable().extend({ required: true }),
            apiPassword = ko.observable().extend({ required: true }),
            carrierPhone = ko.observable().extend({ required: true }),
            isenable = ko.observable(),
            showErrors = ko.observable(false),
            readonly = ko.observable(false),
            
            
            errors = ko.validation.group({
                carrierId: carrierId,
                carrierName: carrierName,
                url: url,
                apiKey: apiKey,
                apiPassword: apiPassword,
                carrierPhone: carrierPhone,
                isenable: isenable,
                showErrors: showErrors
                
            }),
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            dirtyFlag = new ko.dirtyFlag({
                carrierId: carrierId,
                carrierName: carrierName,
                url: url,
                apiKey: apiKey,
                apiPassword: apiPassword,
                carrierPhone: carrierPhone,
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
                carrierPhone: carrierPhone,
                isenable: isenable,
                isValid: isValid,
                showErrors: showErrors,
                readonly:readonly,
                hasChanges: hasChanges,
                

            };
        return self;
    };

    DeliveryCarrier.CreateFromClientModel = function (source) {
        var odeliverycarrier = new DeliveryCarrier();
        odeliverycarrier.carrierId(source.carrierId);
        odeliverycarrier.carrierName(source.carrierName);
        odeliverycarrier.url(source.url);
        odeliverycarrier.apiKey(source.apiKey);
        odeliverycarrier.apiPassword(source.apiPassword);
        odeliverycarrier.carrierPhone(source.carrierPhone);
        odeliverycarrier.isenable(source.isenable);

        return odeliverycarrier;
    };

    var deliverycarrierServertoClientMapper = function (source) {
        return DeliveryCarrier.Create(source);
    };

    DeliveryCarrier.Create = function (source) {
        return new DeliveryCarrier(source.CarrierId, source.CarrierName, source.Url, source.ApiKey, source.ApiPassword, source.isEnable);
    };

    var deliverycarrierClientMapper = function (source)
    {
        var odeliverycarrier = new DeliveryCarrier();
        odeliverycarrier.carrierId(source.CarrierId);
        odeliverycarrier.carrierName(source.CarrierName);
        odeliverycarrier.url(source.Url);
        odeliverycarrier.apiKey(source.ApiKey);
        odeliverycarrier.apiPassword(source.ApiPassword);
        odeliverycarrier.carrierPhone(source.CarrierPhone);
        odeliverycarrier.isenable(source.isEnable);
        
        return odeliverycarrier;

    };
    
    var deliverycarrierServermapper = function (source)
    {
        var result = {};
        result.CarrierId = source.carrierId();
        result.CarrierName = source.carrierName();
        result.Url = source.url();
        result.ApiKey = source.apiKey();
        result.ApiPassword = source.apiPassword();
        result.CarrierPhone = source.carrierPhone();
        result.isEnable = source.isenable();
        
        return result;
    };

    return {
        DeliveryCarrier: DeliveryCarrier,
        deliverycarrierClientMapper: deliverycarrierClientMapper,
        deliverycarrierServermapper: deliverycarrierServermapper,
        deliverycarrierServertoClientMapper: deliverycarrierServertoClientMapper,
        
        

    };
});