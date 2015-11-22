define(['knockout'], function (ko) {
    var vm = {
        NewLobby: NewLobby,
        GetAllLobbies: GetAllLobbies,
        JoinLobby: JoinLobby,
        CancelLobby: CancelLobby,
        LeaveLobby: LeaveLobby
    };
    return vm;

    function NewLobby(lobbyobject) {
        $.ajax({
            method: "POST",
            url: 'http://bandi-pc:40577/api/Lobby/NewLobby/' + lobbyobject,
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            data: JSON.stringify(lobbyobject)
        })
    }

    function JoinLobby(joinobject) {
        return $.ajax({
            method: "POST",
            url: 'http://bandi-pc:40577/api/Play/JoinLobby',
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            data: JSON.stringify(joinobject)
        });
    };

    function LeaveLobby(joinobject) {
        return $.ajax({
            method: "POST",
            url: 'http://bandi-pc:40577/api/Leave/LeaveLobby',
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            data: JSON.stringify(joinobject)
        });
    };

    function CancelLobby(id) {
        return $.ajax({
            method: "POST",
            url: 'http://bandi-pc:40577/api/Cancel/CancelLobby',
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            data: JSON.stringify(id)
        });
    };

    function GetAllLobbies() {
        return $.ajax({
            method: "GET",
            url: 'http://bandi-pc:40577/api/Lobby/GetAllLobbies',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: true
        });
    };

    function GetLobbyById(id) {
        return $.ajax({
            method: "GET",
            url: 'http://bandi-pc:40577/api/Play/GetLobbyById?id=' + id,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: true
        });
    }
});

