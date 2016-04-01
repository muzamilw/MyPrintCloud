// Setup requirejs
(function () {

    var root = this;
    var ist = window.ist;

    if (!ist.siteUrl) {
        ist.siteUrl = $("#siteUrl").val();
    }

    requirejs.config({
        baseUrl: ist.siteUrl + "/Scripts/App",
        waitSeconds: 20,
        paths: {
            "sammy": ist.siteUrl + "/Scripts/sammy-0.7.5.min",
            "common": ist.siteUrl + "/Areas/Common/Scripts",
            "myOrganization": ist.siteUrl + "/Areas/Settings/Scripts",
            "paperSheet": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "inventory": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "inventoryCategory": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "inventorySubCategory": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "stores": ist.siteUrl + "/Areas/Stores/Scripts",
            "product": ist.siteUrl + "/Areas/Products/Scripts",
            "dropzone": ist.siteUrl + "/Scripts/dropzone-amd-module",
            "prefix": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "costcenter": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "machine": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "crm": ist.siteUrl + "/Areas/CRM/Scripts",
            "calendar": ist.siteUrl + "/Areas/CRM/Scripts",
            "toDoList": ist.siteUrl + "/Areas/CRM/Scripts",
            "order": ist.siteUrl + "/Areas/Orders/Scripts",
            "itemJobStatus": ist.siteUrl + "/Areas/Production/Scripts",
            "liveJobs": ist.siteUrl + "/Areas/Production/Scripts",
            "lookupMethods": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "sectionflags": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "deliverycarrier": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "emails": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "invoice": ist.siteUrl + "/Areas/Invoices/Scripts",
            "reportBanner": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "deliveryNotes": ist.siteUrl + "/Areas/DeliveryNotes/Scripts",
            "purchaseOrders": ist.siteUrl + "/Areas/Orders/Scripts",
        }
    });

    function defineThirdPartyModules() {
        // These are already loaded via bundles. 
        // We define them and put them in the root object.
        define("jquery", [], function () { return root.jQuery; });
        define("ko", [], function () { return root.ko; });
        define("underscore-knockout", [], function () { });
        define("underscore-ko", [], function () { });
        define("knockout", [], function () { return root.ko; });
        define("knockout-validation", [], function () { });
        define("moment", [], function () { return root.moment; });
        define("amplify", [], function () { return root.amplify; });
        define("underscore", [], function () { return root._; });
    }

    defineThirdPartyModules();


})();
