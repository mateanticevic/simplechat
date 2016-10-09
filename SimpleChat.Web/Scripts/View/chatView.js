var listConversations = $("#list-conversations");
var listMessages = $("#list-messages");

var inputSay = $("#input-say");

var selectedConversation;

$(function () {
    securedClient.getConversations().OnSuccess = function (data) {
        for (var i = 0; i < data.length; i++) {
            listConversations.append("<div onclick='loadConversationMessages(\"" + data[i].Identifier + "\")'>" + data[i].Identifier + "</div>");
        }
    };
});

function loadConversationMessages(identifier) {

    selectedConversation = identifier;

    securedClient.getConversationMessages(identifier).OnSuccess = function (messages) {

        listMessages.empty();

        for (var i = 0; i < messages.length; i++) {
            var message = messages[i];
            listMessages.append("<div>" + message.Nickname + ": " + message.Content + "</div>");
        }
    };
};

function say() {
    securedClient.putConversationMessage(selectedConversation, inputSay.val()).OnSuccess = function () {
        inputSay.val('');
    };

    return false;
};