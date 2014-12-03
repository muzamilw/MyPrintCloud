// Setup requirejs
(function () {

    var root = this;
    var ist = window.ist;

    requirejs.config({
        baseUrl: "/Scripts/App",
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
