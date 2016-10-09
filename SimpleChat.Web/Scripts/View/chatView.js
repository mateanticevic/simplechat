var listConversations = $("#list-conversations");
var listMessages = $("#list-messages");

var inputSay = $("#input-say");

var templateConversation = $("#template-conversation");
var templateMessage = $("#template-message");

var selectedConversation;

var conversationsETag;
var messagesETag;

var intervalMessages;

$(function () {
    setInterval(loadConversation, 2000);
});

function loadConversation() {

    var get = securedClient.getConversations(conversationsETag);

    get.OnSuccess = function (conversations) {

        if(conversations != undefined){
            listConversations.empty();
            for (var i = 0; i < conversations.length; i++) {
                var conversation = conversations[i];
                var template = templateConversation.html();
                listConversations.append(Mustache.to_html(template, conversation));
            }
        }
    };

    get.OnComplete = function (xhr) {
        conversationsETag = xhr.getResponseHeader("ETag");
    };
};

function loadConversationMessages(identifier) {
    setInterval(loadConversationMessagesT, 2000, identifier);
}

function loadConversationMessagesT(identifier) {

    selectedConversation = identifier;

    var get = securedClient.getConversationMessages(identifier, messagesETag);

    get.OnSuccess = function (messages) {

        if (messages != undefined && messages.length > 0) {

            listMessages.empty();

            for (var i = 0; i < messages.length; i++) {
                var message = messages[i];
                var template = templateMessage.html();
                listMessages.append(Mustache.to_html(template, message));
            }

            securedClient.putConversationSeen(selectedConversation).OnSuccess = function () {
            };
        }
    };

    get.OnComplete = function (xhr) {
        messagesETag = xhr.getResponseHeader("ETag");
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