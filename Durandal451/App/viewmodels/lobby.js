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

    function joinlobby(targetlobby) {
        if (targetlobby.CurrentlyInLobby() < targetlobby.NrOfPlayers()) {
            var joinobject = new LobbyModel.LobbyToJoin(session.userName(), targetlobby.Id());
            dataContext.JoinLobby(joinobject);
            logger.log({
                message: "You have been successfully joined the lobby. When the lobby fills, you will receive a notification.",
                data: "",
                showToast: true,
                type: "info"
            });
            var groupname = targetlobby.CreatorName();
            notifier.joinGroup(groupname);
        }
        else {
            logger.log({
                message: "You can't join this lobby, because it is already full!",
                data: "",
                showToast: true,
                type: "warning"
            });
        }

    }

    function leavelobby(targetlobby) {
        var joinobject = new LobbyModel.LobbyToJoin(session.userName(), targetlobby.Id());
        dataContext.LeaveLobby(joinobject);
        logger.log({
            message: "You have been successfully left the lobby!",
            data: "",
            showToast: true,
            type: "info"
        });
        var groupname = targetlobby.CreatorName();
        notifier.leaveGroup(groupname);
        router.navigate();
        router.navigate('#/Play', 'replace');
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
        joinlobby: joinlobby,
        leavelobby: leavelobby,
        cancellobby: cancellobby
    };
    return vm;


    function activate() {
        vm.cacheViews = false;
        DisplayData();
        return false;
    }

});