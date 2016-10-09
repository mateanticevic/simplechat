var securedClient = securedClient || {};

var token = localStorage.getItem("token").toString();

securedClient.getConversations = function () {
    var callback = {};

    $.ajax({
        url: '/api/secured.svc/conversation',
        contentType: "application/json",
        type: 'GET',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Token " + token);
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

securedClient.getConversationMessages = function (identifier) {
    var callback = {};

    $.ajax({
        url: '/api/secured.svc/conversation/' + identifier + '/messages',
        contentType: "application/json",
        type: 'GET',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Token " + token);
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

securedClient.putConversationMessage = function (identifier, message) {
    var callback = {};

    var messageObject = {
        Content: message
    };

    var json = JSON.stringify(messageObject);

    $.ajax({
        url: '/api/secured.svc/conversation/' + identifier + '/message',
        contentType: "application/json",
        type: 'PUT',
        data: json,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Token " + token);
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

securedClient.putConversationSeen = function (identifier) {
    var callback = {};

    $.ajax({
        url: '/api/secured.svc/conversation/' + identifier + '/seen',
        contentType: "application/json",
        type: 'PUT',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Token " + token);
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