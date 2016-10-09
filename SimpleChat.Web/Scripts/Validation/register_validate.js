var validationNicknameFree = $("#validation-nickname-free");
var validationNicknameTaken = $("#validation-nickname-taken");
var validationEmail = $("#validation-email");
var validationPassword = $("#validation-password");
var validationPasswordRepeat = $("#validation-password-repeat");

validationNicknameFree.hide();
validationNicknameTaken.hide();
validationEmail.hide();
validationPassword.hide();
validationPasswordRepeat.hide();

inputNickname.focusout(function (e) {
    isNicknameTaken().OnSuccess = function (isTaken) {
        if (isTaken) {
            validationNicknameFree.hide();
            validationNicknameTaken.show();
        }
        else {
            validationNicknameFree.show();
            validationNicknameTaken.hide();
        }
    };
});

inputEmail.focusout(function (e) {
    var emailPattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;

    if (emailPattern.test(inputEmail.val()))
    {
        validationEmail.hide();
    }
    else
    {
        validationEmail.show();
    }
});

inputPassword.focusout(function (e) {
    if (inputPassword.val().length > 5) {
        validationPassword.hide();
    }
    else {
        validationPassword.show();
    }
});

inputPasswordRepeat.focusout(function (e){
    if (inputPassword.val() === inputPasswordRepeat.val()) {
        validationPasswordRepeat.hide();
    }
    else {
        validationPasswordRepeat.show();
    }
});