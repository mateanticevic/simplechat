var inputNickname = $("#input-nickname");
var inputEmail = $("#input-email");
var inputPassword = $("#input-password");
var inputPasswordRepeat = $("#input-password-repeat");

function register() {
    var profile = {
        Nickname: inputNickname.val(),
        Email: inputEmail.val(),
        Password: inputPassword.val(),
        FirstName: "Ab",
        LastName: "Nab"
    };

    var json = JSON.stringify(profile);

    publicClient.putProfile(json).OnSuccess = function (data)
    {
        alert("success");
    }
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