var securedClient = securedClient || {};

securedClient.token = localStorage.getItem("token").toString();

securedClient.default = function (callback) {

    callback.OnComplete = function () { };
    callback.OnSuccess = function () { };

    var ajax = {
        contentType: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Token " + securedClient.token);
        },
        complete: function (xhr) {
            if (xhr.status == "401") {
                window.location.href = '/home/login';
            }

            callback.OnComplete(xhr);
        },
        success: function (data) {
            callback.OnSuccess(data);
        }
    };

    return ajax;
};

securedClient.apiPrefix = "/api/secured.svc/";

securedClient.getConversations = function (etag) {
    var callback = {};

    var ajax = securedClient.default(callback);

    ajax.url = securedClient.apiPrefix + 'conversation';
    ajax.type = 'GET';
    ajax.beforeSend = function (xhr) {
        xhr.setRequestHeader("Authorization", "Token " + securedClient.token);
        xhr.setRequestHeader("If-None-Match", etag);
    };

    $.ajax(ajax);

    return callback;
};

securedClient.getConversationMessages = function (identifier, etag) {
    var callback = {};

    var ajax = securedClient.default(callback);

    ajax.url = securedClient.apiPrefix + 'conversation/' + identifier + '/messages';
    ajax.type = 'GET';
    ajax.beforeSend = function (xhr) {
        xhr.setRequestHeader("Authorization", "Token " + securedClient.token);
        xhr.setRequestHeader("If-None-Match", etag);
    };

    $.ajax(ajax);

    return callback;
};

securedClient.putConversationMessage = function (identifier, message) {
    var callback = {};

    var messageObject = {
        Content: message
    };

    var json = JSON.stringify(messageObject);

    $.ajax({
        url: securedClient.apiPrefix + 'conversation/' + identifier + '/message',
        contentType: "application/json",
        type: 'PUT',
        data: json,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Token " + securedClient.token);
        },
        complete: function (xhr) {
            if (xhr.status == "401") {
                window.location.href = '/home/login';
            }
        },
        success: function (data) {
            callback.OnSuccess(data);
        }
    });

    return callback;
};

securedClient.putConversationProfile = function (identifier, nickname) {
    var callback = {};

    var messageObject = {
        Nickname: nickname
    };

    var json = JSON.stringify(messageObject);

    var ajax = securedClient.default(callback);

    ajax.url = securedClient.apiPrefix + 'conversation/' + identifier + '/profile';
    ajax.type = 'PUT';
    ajax.data = json;

    $.ajax(ajax);

    return callback;
};

securedClient.putConversationSeen = function (identifier) {
    var callback = {};

    $.ajax({
        url: securedClient.apiPrefix + 'conversation/' + identifier + '/seen',
        contentType: "application/json",
        type: 'PUT',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Token " + securedClient.token);
        },
        complete: function (xhr) {
            if (xhr.status == "401") {
                window.location.href = '/home/login';
            }
        },
        success: function (data) {
            callback.OnSuccess(data);
        }
    });

    return callback;
};

securedClient.deleteMessage = function (identifier) {
    var callback = {};

    var ajax = securedClient.default(callback);

    ajax.url = securedClient.apiPrefix + 'message/' + identifier;
    ajax.type = 'DELETE';

    $.ajax(ajax);

    return callback;
};

securedClient.deleteConversationMessages = function (identifier) {
    var callback = {};

    var ajax = securedClient.default(callback);

    ajax.url = securedClient.apiPrefix + 'conversation/' + identifier + '/messages';
    ajax.type = 'DELETE';

    $.ajax(ajax);

    return callback;
};

securedClient.deleteConversationProfile = function (identifier) {
    var callback = {};

    var ajax = securedClient.default(callback);

    ajax.url = securedClient.apiPrefix + 'conversation/' + identifier + '/profile';
    ajax.type = 'DELETE';

    $.ajax(ajax);

    return callback;
};

securedClient.getProfiles = function (searchQuery) {
    var callback = {};

    var ajax = securedClient.default(callback);

    ajax.url = securedClient.apiPrefix + 'profiles/' + searchQuery;
    ajax.type = 'GET';

    $.ajax(ajax);

    return callback;
};