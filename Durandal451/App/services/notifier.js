define(['toastr', 'global/session'], function (toastr, session) {
    var boot = function () {

        function toastrconfig() {
            toastr.options.closeButton = true;
            toastr.options.positionClass = 'toast-top-right';
            toastr.options.backgroundpositionClass = 'toast-top-right';
            toastr.options.fadeOut = 1000;
            toastr.options.timeOut = 0;
            toastr.options.extendedTimeOut = 0;
        };


        var msgHub = $.connection.msgHub;

        msgHub.client.newMessage = function (message) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = 'toast-top-right';
            toastr.options.backgroundpositionClass = 'toast-top-right';
            toastr.options.fadeOut = 1000;
            toastr.options.timeOut = 0;
            toastr.options.extendedTimeOut = 0;
            toastr.success(message);
        };

        msgHub.client.newMessageSuccess = function (message) {
            model.addMessageSuccess(message);
        };

        $.connection.hub.logging = true;
        $.connection.hub.start("~/signalr")
                    .done(function () {

                    })
                    .fail(function () {
                        alert("Could not Connect!");
                    });
    };

    var joinGroup = function (name) {
        var msgHub = $.connection.msgHub;
        msgHub.server.joinGroup(name);
    };

    var leaveGroup = function (groupname) {
        var msgHub = $.connection.msgHub;
        msgHub.server.leaveGroup(groupname);
    }

    var sendMessageToGroup = function (message, groupname) {
        var msgHub = $.connection.msgHub;
        msgHub.server.sendMessageToGroup(message, groupname)
    }

    return {
        boot: boot,
        joinGroup: joinGroup,
        leaveGroup: leaveGroup,
        sendMessageToGroup: sendMessageToGroup
    };

});