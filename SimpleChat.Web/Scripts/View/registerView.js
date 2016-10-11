var inputNickname = $("#input-nickname");
var inputEmail = $("#input-email");
var inputPassword = $("#input-password");
var inputPasswordRepeat = $("#input-password-repeat");

var handler = handler || {};

handler.clickRegister = function () {
    var profile = {
        Nickname: inputNickname.val(),
        Email: inputEmail.val(),
        Password: inputPassword.val()
    };

    var json = JSON.stringify(profile);

    publicClient.putProfile(json).OnSuccess = function (data)
    {
        window.location.href = "/home/login";
    }

    return false;
};

function isNicknameTaken() {

    var callback = {};

    publicClient.headProfile(inputNickname.val()).OnSuccess = function (xhr) {
        if(xhr.status == 200)
        {
            callback.OnSuccess(true);
        }
        else
        {
            callback.OnSuccess(false);
        }
    };

    return callback;
};