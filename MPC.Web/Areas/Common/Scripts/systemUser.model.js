define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Section view Entity
    SystemUser = function (specifiedSignature, specifiedId, specifiedFullName, specifiedEmail, specifiedRole, specifiedRoleName) {
        var self,
            userId = ko.observable(specifiedId),
            signature = ko.observable(specifiedSignature),
            fullName = ko.observable(specifiedFullName || undefined).extend({ required: true, message: 'Name is required'}),
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
             errors = ko.validation.group({
                 fullName: fullName
             }),
            isValid = ko.computed(function () {
                    return errors().length === 0;
                }),
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
            roleName: roleName,
            errors: errors,
            isValid: isValid

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