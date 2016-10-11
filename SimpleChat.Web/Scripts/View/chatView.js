var currentNickname = localStorage.getItem("nickname");

var listConversations = $("#list-conversations");
var listMessages = $("#list-messages");
var listProfiles = $("#list-profiles");
var listProfilesSearch = $("#list-profiles-search");

var inputSay = $("#input-say");
var inputSearch = $("#input-search");

var conversationControl = $("#conversation-control");
var conversationOptions = $("#conversation-options");

var templateConversation = $("#template-conversation");
var templateMessage = $("#template-message");
var templateProfile = $("#template-profile");
var templateProfileSearch = $("#template-profile-search");

var model = model || {};
var selectedConversation;

var etag = etag || {};

var intervalMessages;

var messagesIntervalId;
var conversationProfilesIntervalId;

$(function () {
    loadConversations();
    setInterval(loadConversations, 2000);
    handler.onConversationUnselected();
});

listMessages.enscroll({
    showOnHover: true,
    verticalTrackClass: 'track3',
    verticalHandleClass: 'handle3'
});

inputSearch.keyup(function (e) {

    if (inputSearch.val().length < 1) {
        listProfilesSearch.empty();
        return;
    };

    securedClient.getProfiles(inputSearch.val()).OnSuccess = function (profiles) {

        listProfilesSearch.empty();

        for (var i = 0; i < profiles.length; i++) {
            var profile = profiles[i];
            var template = templateProfileSearch.html();
            listProfilesSearch.append(Mustache.to_html(template, profile));
        }
    };
});

var handler = handler || {};

handler.clickAddProfile = function (nickname) {

    listProfilesSearch.empty();
    inputSearch.val('');

    securedClient.putConversationProfile(selectedConversation, nickname).OnSuccess = function () {
        loadConversations();
        loadConversationProfiles();
    };

    return false;
};

handler.clickConversation = function (identifier) {

    selectedConversation = identifier;
    var conversation = getConversation(identifier);

    loadConversationProfiles();
    loadConversationMessages();
    handler.onConversationSelected();

    if (messagesIntervalId != undefined) {
        clearInterval(messagesIntervalId);
        clearInterval(conversationProfilesIntervalId);
    }

    messagesIntervalId = setInterval(loadConversationMessages, 2000, identifier);
    conversationProfilesIntervalId = setInterval(loadConversationProfiles, 2000);
};

handler.clickDeleteMessage = function (identifier) {
    deleteMessage(identifier);
    return false;
};

handler.clickDeleteMessages = function () {
    deleteMessages(selectedConversation);
    return false;
};

handler.newConversation = function () {
    securedClient.putConversation().OnSuccess = function (identifier) {
        selectedConversation = identifier;
        handler.onConversationSelected();
        loadConversations();
        loadConversationMessages();
        loadConversationProfiles();
    };

    return false;
};

handler.clickLeaveConversation = function () {
    leaveConversation();
    return false;
};

handler.clickSay = function () {
    sendMessage();
    return false;
};

handler.clickSignOut = function () {
    localStorage.removeItem("token");
    window.location.href = '/home/login';

    return false;
};

handler.onConversationSelected = function () {
    conversationOptions.show();
    conversationControl.show();
};

handler.onConversationUnselected = function () {
    conversationOptions.hide();
    conversationControl.hide();
};

function deleteMessage(identifier) {

    securedClient.deleteMessage(identifier).OnSuccess = function () {
        loadConversationMessages();
    };
};

function deleteMessages(identifier) {

    securedClient.deleteConversationMessages(selectedConversation).OnSuccess = function () {

    };
};

function loadConversations() {

    var get = securedClient.getConversations(etag.conversationsETag);

    get.OnSuccess = function (conversations) {

        if (conversations != undefined) {
            model.conversations = conversations;
            listConversations.empty();
            for (var i = 0; i < conversations.length; i++) {
                var conversation = conversations[i];
                var template = templateConversation.html();
                listConversations.append(Mustache.to_html(template, conversation));

                if(conversation.Identifier == selectedConversation){
                    highlightConversation(selectedConversation);
                }
            }

            if (conversations.length == 0) {
                handler.onConversationUnselected();
            }

            $(".activatable").click(function (e) {
                $(".activatable").removeClass("active");
                $(this).addClass("active");
            });
        }
    };

    get.OnComplete = function (xhr) {
        etag.conversationsETag = xhr.getResponseHeader("ETag");
    };
};

function leaveConversation() {

    securedClient.deleteConversationProfile(selectedConversation).OnSuccess = function () {
        listMessages.empty();
        listProfiles.empty();
        handler.onConversationUnselected();
        loadConversations();
    };
};

function addMessage(message) {
    var template = templateMessage.html();
    listMessages.append(Mustache.to_html(template, message));
};

function addMessageToTop(message) {
    var template = templateMessage.html();
    listMessages.prepend(Mustache.to_html(template, message));
};

function loadConversationMessages() {
    var get = securedClient.getConversationMessages(selectedConversation, etag.messagesETag);

    get.OnSuccess = function (messages) {

        if (messages != undefined) {

            listMessages.empty();

            for (var i = 0; i < messages.length; i++) {
                var message = messages[i];
                addMessage(message);
            }

            securedClient.putConversationSeen(selectedConversation).OnSuccess = function () {
            };
        }
    };

    get.OnComplete = function (xhr) {
        etag.messagesETag = xhr.getResponseHeader("ETag");
    };
};

function loadConversationProfiles() {

    var get = securedClient.getConversationProfiles(selectedConversation, etag.conversationProfilesETag);

    get.OnSuccess = function (profiles) {

        if (profiles != undefined) {

            listProfiles.empty();

            for (var i = 0; i < profiles.length; i++) {
                var profile = profiles[i];
                var template = templateProfile.html();
                listProfiles.append(Mustache.to_html(template, profile));
            }
        }
    };

    get.OnComplete = function (xhr) {
        etag.conversationProfilesETag = xhr.getResponseHeader("ETag");
    };
};

function getConversation(identifier) {
    for (var i = 0; i < model.conversations.length; i++) {
        if (model.conversations[i].Identifier == identifier) {
            return model.conversations[i];
        }
    }

    return null;
};

function sendMessage() {
    securedClient.putConversationMessage(selectedConversation, inputSay.val()).OnSuccess = function (identifier) {
        addMessageToTop({ CanDelete: true, Profile: { Nickname: currentNickname }, Content: inputSay.val(), Identifier: identifier });
        inputSay.val('');
    };
};

function highlightConversation (identifier){
    $("#conversation-" + identifier).addClass("active");
};