var listConversations = $("#list-conversations");
var listMessages = $("#list-messages");

var inputSay = $("#input-say");

var templateConversation = $("#template-conversation");
var templateMessage = $("#template-message");

var selectedConversation;

$(function () {
    setInterval(loadConversation, 2000);
});

function loadConversation() {

    listConversations.empty();

    securedClient.getConversations().OnSuccess = function (conversations) {
        for (var i = 0; i < conversations.length; i++) {
            var conversation = conversations[i];
            var template = templateConversation.html();
            listConversations.append(Mustache.to_html(template, conversation));
        }
    };
};

function loadConversationMessages(identifier) {

    selectedConversation = identifier;

    securedClient.getConversationMessages(identifier).OnSuccess = function (messages) {

        listMessages.empty();

        for (var i = 0; i < messages.length; i++) {
            var message = messages[i];
            var template = templateMessage.html();
            listMessages.append(Mustache.to_html(template, message));
        }

        securedClient.putConversationSeen(selectedConversation).OnSuccess = function () {

        };
    };
};

function say() {
    securedClient.putConversationMessage(selectedConversation, inputSay.val()).OnSuccess = function () {
        inputSay.val('');
    };

    return false;
};

function singOut() {
    localStorage.removeItem("token");
    window.location.href = '/home/login';

    return false;
};