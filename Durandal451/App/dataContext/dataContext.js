define(['knockout'], function (ko) {
    var vm = {
        NewLobby: NewLobby,
        AddUser: AddUser,
        GetAllLobbies: GetAllLobbies,
        CancelLobby: CancelLobby
    };
    return vm;

    function NewLobby() {
        $.ajax({
            method: "GET",
            url: 'http://bandi-pc:40577/api/Lobby/NewLobby/',
            contentType: 'application/json; charset=utf-8',
            dataType: 'application/json',
            async: true
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

