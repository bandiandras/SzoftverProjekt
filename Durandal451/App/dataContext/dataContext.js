define(['knockout'], function (ko) {
    var vm = {
        NewLobby: NewLobby,
        AddUser: AddUser,
        GetAllLobbies: GetAllLobbies,
        CancelLobby: CancelLobby,
        JoinLobby: JoinLobby,
        JoinLobbyPOST: JoinLobbyPOST,
        LeaveLobby: LeaveLobby
    };
    return vm;

    function NewLobby(paramArray) {
        $.ajax({
            method: "GET",
            url: 'http://bandi-pc:40577/api/Lobby/NewLobby/?paramArray=' + paramArray,
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            async: false
        })
    }

    function AddUser(name) {
        $.ajax({
            method: "GET",
            url: "http://bandi-pc:40577/api/User/AddUser/?name=" + name,
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            async: true
        });
    };

    function CancelLobby(id) {
        return $.ajax({
            method: "GET",
            url: 'http://bandi-pc:40577/api/Lobby/DeleteLobby/?id=' + id,
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            async: true
        });
    };

    function JoinLobby(joinobject) {
        return $.ajax({
            method: "GET",
            url: 'http://bandi-pc:40577/api/Lobby/JoinLobby/?paramArray=' + joinobject,
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            async: true
        });
    };

    function JoinLobbyPOST(joinobject) {
        return $.ajax({
            method: "POST",
            url: 'http://bandi-pc:40577/api/Lobby/JoinLobbyPOST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            data: JSON.stringify(joinobject)
        });
    };


    //function LeaveLobby(leaveobject) {
    //    return $.ajax({
    //        method: "GET",
    //        url: 'http://bandi-pc:40577/api/Lobby/LeaveLobby/?paramArray=' + leaveobject,
    //        contentType: 'application/json; charset=utf-8',
    //        dataType: 'application/json',
    //        async: true
    //    });
    //};

    function LeaveLobby(joinobject) {
        return $.ajax({
            method: "POST",
            url: 'http://bandi-pc:40577/api/Lobby/LeaveLobby',
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            data: JSON.stringify(joinobject)
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
});

