// Setup requirejs
(function () {

    var root = this;
    var ist = window.ist;

    requirejs.config({
        baseUrl: "/Scripts/App",
        waitSeconds: 20,
        paths: {
            "sammy": ist.siteUrl + "/Scripts/sammy-0.7.5.min",
            "businessPartner": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "product": ist.siteUrl + "/Areas/Product/Scripts",
            "common": ist.siteUrl + "/Areas/Common/Scripts",
            "myOrganization": ist.siteUrl + "/Areas/Settings/Scripts",
            "paperSheet": ist.siteUrl + "/Areas/Settings/Scripts/MIS",
            "Fleet": ist.siteUrl + "/Areas/Fleet/Scripts",
            "hireGroup": ist.siteUrl + "/Areas/Fleet/Scripts",
            "vehicle": ist.siteUrl + "/Areas/Fleet/Scripts",
            "tariffType": ist.siteUrl + "/Areas/Pricing/Scripts",
            "tariffRate": ist.siteUrl + "/Areas/Pricing/Scripts",
            "insuranceRate": ist.siteUrl + "/Areas/Pricing/Scripts",
            "serviceRate": ist.siteUrl + "/Areas/Pricing/Scripts",
            "additionalDriverCharge": ist.siteUrl + "/Areas/Pricing/Scripts",
            "additionalCharge": ist.siteUrl + "/Areas/Pricing/Scripts",
            "chaufferCharge": ist.siteUrl + "/Areas/Pricing/Scripts",
            "seasonalDiscount": ist.siteUrl + "/Areas/Pricing/Scripts",
            "rentalAgreement": ist.siteUrl + "/Areas/RentalAgreement/Scripts",
            "raQueue": ist.siteUrl + "/Areas/RentalAgreement/Scripts",
            "Organization": ist.siteUrl + "/Areas/Organization/Scripts",
            "company": ist.siteUrl + "/Areas/Organization/Scripts",
            "operation": ist.siteUrl + "/Areas/Organization/Scripts",
            "department": ist.siteUrl + "/Areas/Organization/Scripts",
            "workplace": ist.siteUrl + "/Areas/Organization/Scripts",
            "workLocation": ist.siteUrl + "/Areas/Organization/Scripts",
            "workplaceType": ist.siteUrl + "/Areas/Organization/Scripts",
            "businessSeg": ist.siteUrl + "/Areas/Organization/Scripts",
            "employee": ist.siteUrl + "/Areas/EmployeeManagement/Scripts",
            "region": ist.siteUrl + "/Areas/GeographicalHierarchy/Scripts",
            "subRegion": ist.siteUrl + "/Areas/GeographicalHierarchy/Scripts",
            "city": ist.siteUrl + "/Areas/GeographicalHierarchy/Scripts",
            "area": ist.siteUrl + "/Areas/GeographicalHierarchy/Scripts",
            "empStatus": ist.siteUrl + "/Areas/EmployeeManagement/Scripts",
            "designGrade": ist.siteUrl + "/Areas/EmployeeManagement/Scripts",
            "designation": ist.siteUrl + "/Areas/EmployeeManagement/Scripts",
            "jobType": ist.siteUrl + "/Areas/EmployeeManagement/Scripts",
            "discountType": ist.siteUrl + "/Areas/Pricing/Scripts",
            "discountSubType": ist.siteUrl + "/Areas/Pricing/Scripts",
            "serviceType": ist.siteUrl + "/Areas/Pricing/Scripts",
            "serviceItem": ist.siteUrl + "/Areas/Pricing/Scripts",
            "nRTType": ist.siteUrl + "/Areas/NonRevenueTicket/Scripts",
            "nRT": ist.siteUrl + "/Areas/NonRevenueTicket/Scripts",
            "nRTQueue": ist.siteUrl + "/Areas/NonRevenueTicket/Scripts",
            "vehicleCheckList": ist.siteUrl + "/Areas/Fleet/Scripts",
            "documentGroup": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "bpMainType": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "document": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "ratingType": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "businessLegalStatus": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "occupationType": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "relationType": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "bpSubType": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "creditLimit": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "marketingChannel": ist.siteUrl + "/Areas/BusinessPartner/Scripts",
            "maintenanceTypeGroup": ist.siteUrl + "/Areas/Fleet/Scripts",
            "maintenanceType": ist.siteUrl + "/Areas/Fleet/Scripts",
            "insuranceType": ist.siteUrl + "/Areas/Fleet/Scripts",
            "vehicleMake": ist.siteUrl + "/Areas/Fleet/Scripts",
            "vehicleCategory": ist.siteUrl + "/Areas/Fleet/Scripts",
            "vehicleModel": ist.siteUrl + "/Areas/Fleet/Scripts",
            "vehicleStatus": ist.siteUrl + "/Areas/Fleet/Scripts",

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
