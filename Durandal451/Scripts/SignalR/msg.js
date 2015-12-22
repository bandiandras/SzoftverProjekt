
(function () {
    var msgHub = $.connection.msgHub;
    $.connection.hub.logging = true;
    $.connection.hub.start()
                .done(function () {

                })
                .fail(function () {
                    alert("Could not Connect!");
                });

    function newMessage(message) {
        model.addMessage(message);
    };

    msgHub.client.newMessage = function (message) {
        model.addMessage(message);
    };

    msgHub.client.newMessageSuccess = function (message) {
        model.addMessageSuccess(message);
    };

    msgHub.client.newMessageToGroup = function (message, groupname) {
        msgHub.join(groupname);
        model.addMessageSuccess(message);
    };

    var Model = function () {
        var self = this;
        self.message = ko.observable("");
        //       self.messages = ko.observableArray();
    };

    Model.prototype = {
        sendMessage: function () {
            var self = this;
            msgHub.server.send(self.message());
            self.message("");
        },

        sendMessageSuccess: function () {
            var self = this;
            msgHub.server.sendMessageSuccess(self.message());
            self.message("");
        },

        sendMessageToGroup: function () {
            var self = this;
            msgHub.server.sendMessageToGroup(self.message(), self.groupname());
        },

        addMessage: function (message) {
            var self = this;
            //self.messages.push(message);
            toastr.info('Message: ' + message);
        },

        addMessageSuccess: function (message) {
            var self = this;
            toastr.success(message);
        }

    };

    var model = new Model();

    $(function () {
        ko.applyBindings(model);
    });

}());