var publicClient = publicClient || {};

publicClient.putProfile = function (json) {
    var callback = {};

    $.ajax({
        url: '/api/public.svc/profile',
        contentType: "application/json",
        type: 'PUT',
        data: json,
        success: function (data) {
            callback.OnSuccess(data);
        }
    });

    return callback;
};

publicClient.headProfile = function (nickname) {
    var callback = {};

    $.ajax({
        url: '/api/public.svc/profile/' + nickname,
        type: 'HEAD',
        complete: function (xhr) {
            callback.OnSuccess(xhr);
        }
    });

    return callback;
};

publicClient.postToken = function (json) {
    var callback = {};

    $.ajax({
        url: '/api/public.svc/token',
        contentType: "application/json",
        type: 'POST',
        data: json,
        success: function (token) {
            callback.OnSuccess(token);
        },
        complete: function (xhr) {
            callback.OnComplete(xhr);
        }
    });

    return callback;
};