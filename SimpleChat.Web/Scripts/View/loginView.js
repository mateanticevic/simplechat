var inputNickname = $("#input-nickname");
var inputPassword = $("#input-password");

function login() {

    var tokenRequest = {
        Nickname: inputNickname.val(),
        Password: inputPassword.val()
    };

    var json = JSON.stringify(tokenRequest);

    publicClient.postToken(json).OnSuccess = function (token) {
        localStorage.setItem("token", token);
        window.location.href = '/chat';
    };

    return false;
};