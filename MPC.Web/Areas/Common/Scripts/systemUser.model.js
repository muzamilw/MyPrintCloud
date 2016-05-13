define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Section view Entity
    SystemUser = function (specifiedSignature, specifiedId, specifiedFullName, specifiedEmail, specifiedRole, specifiedRoleName) {
        var self,
            userId = ko.observable(specifiedId),
            signature = ko.observable(specifiedSignature),
            fullName = ko.observable(specifiedFullName),
            email = ko.observable(specifiedEmail),
            roleId = ko.observable(specifiedRole),
            roleName = ko.observable(specifiedRoleName),
           
            dirtyFlag = new ko.dirtyFlag({
                signature: signature,
                fullName: fullName,
                roleId: roleId
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
                    EmailSignature: signature(),
                    FullName: fullName(),
                    RoleId : roleId()
                };
            };

        self = {
            signature: signature,
            userId: userId,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData,
            email: email,
            fullName: fullName,
            roleId: roleId,
            roleName: roleName

        };
        return self;
    };
    SystemUser.Create = function(source) {
        var user = new SystemUser(source.EmailSignature, source.SystemUserId, source.FullName, source.Email, source.RoleId, source.RoleName);
        
        // Reset State to Un-Modified
        user.reset();

        return user;
    };

    return {
        SystemUser: SystemUser,
        
    };
});