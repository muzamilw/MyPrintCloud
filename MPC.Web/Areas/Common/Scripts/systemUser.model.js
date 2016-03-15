define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Section view Entity
    SystemUser = function (specifiedSignature, specifiedId) {
        var self,
            signature = ko.observable(specifiedSignature),
            userId = ko.observable(specifiedId),
            dirtyFlag = new ko.dirtyFlag({
                signature: signature
            }),
            // True If Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset Dirty State
            reset = function () {
                dirtyFlag.reset();
            },

            convertToServerData = function () {
                return {
                    SystemUserId: userId(),
                    EmailSignature: signature()
                };
            };

        self = {
            signature: signature,
            userId: userId,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData

        };
        return self;
    };
    

    return {
        SystemUser: SystemUser,
        
    };
});