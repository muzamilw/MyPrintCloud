define(["ko", "underscore", "underscore-ko"], function (ko) {
    var machineList = function () {

        var
            self,
            MachineId = ko.observable(),
            MachineCatId = ko.observable(),
            MachineName = ko.observable(),
            Description = ko.observable(),
            ImageSource = ko.observable(),
            errors = ko.validation.group({
                name: name,
                type: type
            }),
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            dirtyFlag = new ko.dirtyFlag({
                name: name,
                type: type
            }),
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),

            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            MachineId : MachineId,
            MachineCatId : MachineCatId,
            MachineName : MachineName,
            Description : Description,
            ImageSource : ImageSource,
            dirtyFlag: dirtyFlag,
            errors: errors,
            isValid: isValid,
            hasChanges: hasChanges,
            reset: reset
        };
        return self;
    };

    var machineListClientMapper = function (source) {

        var omachineList = new machineList();
        omachineList.Description(source.description);
        omachineList.MachineId(source.MachineId);
        omachineList.MachineCatId(source.MachineCatId);
        omachineList.MachineName(source.MachineName);
        omachineList.ImageSource(source.ImageSource);
        return omachineList;

    };
    var machineListServerMapper = function (source) {
        var result = {};
        result.Description= source.description;
        result.MachineId= source.MachineId;
        result.MachineCatId= source.MachineCatId;
        result.MachineName= source.MachineName;
        result.ImageSource= source.ImageSource;
        return result;
    };

    return {
        machineList: machineList,
        machineListClientMapper: machineListClientMapper,
        machineListServerMapper: machineListServerMapper
    };
});