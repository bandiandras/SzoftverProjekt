
define(['knockout'], function (ko) {
    var vm = {
        Lobby: Lobby,
        LobbyToAdd: LobbyToAdd,
        LobbyToJoin: LobbyToJoin
    };

    return vm

    function Lobby(Id, GameId, NrOfPlayers, StartTime) {
        this.Id = ko.observable(Id);
        this.GameId = ko.observable(GameId);
        this.NrOfPlayers = ko.observable(NrOfPlayers);
        this.StartTime = ko.observable(StartTime);
    }

    function Lobby(Id, GameId, NrOfPlayers, StartTime, CreatorName, CurrentlyInLobby) {
        this.Id = ko.observable(Id);
        this.GameId = ko.observable(GameId);
        this.NrOfPlayers = ko.observable(NrOfPlayers);
        this.StartTime = ko.observable(StartTime);
        this.CreatorName = ko.observable(CreatorName);
        this.CurrentlyInLobby = ko.observable(CurrentlyInLobby);
    }

    function LobbyToAdd(GameId, NrOfPlayers, StartTime, CreatorName) {
        this.GameId = GameId;
        this.NrOfPlayers = NrOfPlayers;
        this.StartTime = StartTime;
        this.CreatorName = CreatorName;
    }

    function LobbyToJoin(UserName, LobbyId) {
        this.UserName = UserName;
        this.LobbyId = LobbyId;
    }
})