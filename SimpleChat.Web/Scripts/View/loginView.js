var inputNickname = $("#input-nickname");
var inputPassword = $("#input-password");

var alertLogin = $("#alert-login");

var handler = handler || {};

handler.clickLogin = function () {

    var tokenRequest = {
        Nickname: inputNickname.val(),
        Password: inputPassword.val()
    };

    var json = JSON.stringify(tokenRequest);

    var result = publicClient.postToken(json);

    result.OnSuccess = function (token) {
        localStorage.setItem("token", token);
        localStorage.setItem("nickname", inputNickname.val());
        window.location.href = '/chat';
    };

    result.OnComplete = function (xhr) {
        if(xhr.status == '401'){
            alertLogin.show();
        }
    };

    return false;
};

handler.clickRegister = function () {
    window.location.href = "/home/register";
};

$(function () {
    alertLogin.hide();
});