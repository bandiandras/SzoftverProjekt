define(['plugins/router', 'durandal/app', 'global/session', 'services/logger', 'knockout', 'dataContext/dataContext', 'model/lobbyModel', 'services/notifier'],
    function (router, app, session, logger, ko, datacontext, lobbymodel, notifier) {


        // reveal the bindable properties and functions
        var vm = {
            activate: activate,
            title: 'newlobby',
            nrofplayers: ko.observable().extend({ required: true }),
            starttime: ko.observable().extend({ required: true }),
            lobbymodel: lobbymodel,
            addlobby: addlobby,
        };
        return vm;

        function addlobby() {
            var mylobby = "1,"
            mylobby = mylobby.concat(session.userName(), ",", vm.starttime(), ",", vm.nrofplayers())
            datacontext.NewLobby(mylobby)
            notifier.joinGroup(session.userName());
            router.navigate();
            router.navigate('#/lobby', 'replace');
        }

        function activate() {
            return true;
        }

    });