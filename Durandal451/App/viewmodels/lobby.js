define(['plugins/router', 'services/logger', 'dataContext/dataContext', 'model/lobbyModel', 'knockout', 'global/session'], function (router, logger, dataContext, LobbyModel, ko, session) {
    var title = 'Play';
    var listOflobbies = ko.observableArray([])

    function NothingAtAll() { };

    function DisplayData() {
        this.cacheViews = false;
        var lobbies;
        listOflobbies.removeAll();
        dataContext.GetAllLobbies().done(function (data) {
            lobbies = data
            for (var i = 0; i < lobbies.length; i++) {
                var g = new LobbyModel.Lobby(lobbies[i].id, lobbies[i].gameId, lobbies[i].nrOfPlayers, lobbies[i].startTime, lobbies[i].creatorName, lobbies[i].currentlyInLobby)
                listOflobbies.push(g)
            }
        })
    }

    function cancellobby(targetlobby) {
        if (targetlobby.CreatorName() == session.userName()) {
            dataContext.CancelLobby(targetlobby.Id());
            router.navigate();
            router.navigate('#/Play', 'replace');
            logger.log({
                message: "You have been successfully deleted the lobby!",
                data: "",
                showToast: true,
                type: "info"
            });
        }
        else {
            logger.log({
                message: "Only the creator of the lobby can delete it!",
                data: "",
                showToast: true,
                type: "warning"
            });
        }
    }

    var vm = {
        activate: activate,
        title: title,
        DisplayData: DisplayData,
        Lobby: listOflobbies,
        cancellobby: cancellobby
    };
    return vm;


    function activate() {
        vm.cacheViews = false;
        DisplayData();
        return false;
    }

});