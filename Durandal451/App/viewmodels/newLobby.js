define(['plugins/router', 'durandal/app', 'global/session', 'services/logger', 'knockout', 'dataContext/dataContext', 'model/lobbyModel'],
    function (router, app, session, logger, ko, datacontext, lobbymodel) {


        // reveal the bindable properties and functions
        var vm = {
            activate: activate,
            title: 'newlobby',
            gameid: ko.observable().extend({ required: true }),
            nrofplayers: ko.observable().extend({ required: true }),
            starttime: ko.observable().extend({ required: true }),
            lobbymodel: lobbymodel,
            addlobby: addlobby,
        };
        return vm;

        function addlobby() {
            var mylobby = new lobbymodel.LobbyToAdd(vm.gameid(), vm.nrofplayers(), vm.starttime(), session.userName())
            datacontext.NewLobby(mylobby)
            router.navigate();
            router.navigate('#/lobby', 'replace');
        }

        function activate() {
            return true;
        }

    });