﻿/*
    Data service module with ajax calls to the server
*/
define("machine/machine.dataservice", function () {

    // Data service for forecast 
    var dataService = (function () {
        var // True if initialized
            isInitialized = false,
            // Initialize
            initialize = function () {
                if (!isInitialized) {

                    // Define request to get MachineList
                    amplify.request.define('GetMachineList', 'ajax', {
                        url: ist.siteUrl + '/Api/Machine',
                        dataType: 'json',
                        type: 'GET'
                    });

                    // Define request to delete MachineList
                    //amplify.request.define('deleteMachine', 'ajax', {
                    //    url: ist.siteUrl + '/Api/MachineList',
                    //    dataType: 'json',
                    //    type: 'DELETE'
                    //});
                    //// Define request to save New MachineList
                    //amplify.request.define('saveNewMachine', 'ajax', {
                    //    url: ist.siteUrl + '/Api/MachineList',
                    //    dataType: 'json',
                    //    decoder: amplify.request.decoders.istStatusDecoder,
                    //    type: 'Put'
                    //});
                    //// Define request to save Machine
                    //amplify.request.define('saveMachine', 'ajax', {
                    //    url: ist.siteUrl + '/Api/MachineList',
                    //    dataType: 'json',
                    //    decoder: amplify.request.decoders.istStatusDecoder,
                    //    type: 'POST'
                    //});
                    // Define request to get Machine
                    amplify.request.define('getMachineById', 'ajax', {
                        url: ist.siteUrl + '/Api/Machine',
                        dataType: 'json',
                        type: 'GET'
                    });
                    isInitialized = true;
                }
            },
            // Get Machine List
            GetMachineList = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'GetMachineList',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            },
             //Get Machine by Id 
            getMachineById = function (params, callbacks) {
                initialize();
                return amplify.request({
                    resourceId: 'getMachineById',
                    success: callbacks.success,
                    error: callbacks.error,
                    data: params
                });
            };
        //deleteMachine = function (params, callbacks) {
        //    initialize();
        //    return amplify.request({
        //        resourceId: 'deleteMachine',
        //        success: callbacks.success,
        //        error: callbacks.error,
        //        data: params
        //    });
        //},
        //// Save New paper Sheet
        //saveNewMachine = function (param, callbacks) {
        //    initialize();
        //    return amplify.request({
        //        resourceId: 'saveNewMachine',
        //        success: callbacks.success,
        //        error: callbacks.error,
        //        data: param
        //    });
        //},
        //// Save Cost Center
        //saveMachine = function (param, callbacks) {
        //    initialize();
        //    return amplify.request({
        //        resourceId: 'saveMachine',
        //        success: callbacks.success,
        //        error: callbacks.error,
        //        data: param
        //    });
        //};

        return {
            GetMachineList: GetMachineList,
            getMachineById: getMachineById
            //deleteCostCenter: deleteCostCenter,
            //saveNewCostCenter: saveNewCostCenter,
            //saveCostCenter: saveCostCenter
        };
    })();

    return dataService;
});