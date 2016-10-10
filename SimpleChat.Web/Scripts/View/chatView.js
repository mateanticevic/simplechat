var listConversations = $("#list-conversations");
var listMessages = $("#list-messages");
var listProfiles = $("#list-profiles");
var listProfilesSearch = $("#list-profiles-search");

var inputSay = $("#input-say");
var inputSearch = $("#input-search");

var templateConversation = $("#template-conversation");
var templateMessage = $("#template-message");
var templateProfile = $("#template-profile");
var templateProfileSearch = $("#template-profile-search");

var model = model || {};
var selectedConversation;

var etag = etag || {};

var intervalMessages;

var messagesIntervalId;

$(function () {
    loadConversations();
    setInterval(loadConversations, 2000);
});

listMessages.enscroll({
    showOnHover: true,
    verticalTrackClass: 'track3',
    verticalHandleClass: 'handle3'
});

inputSearch.keypress(function (e) {

    if (inputSearch.val().length < 1) {
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

function deleteMessage(identifier) {

    securedClient.deleteMessage(identifier).OnSuccess = function () {

    };


    return false;
};

function deleteMessages() {

    securedClient.deleteConversationMessages(selectedConversation).OnSuccess = function () {

    };

    return false;
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

    };

    return false;
};

function addConversationProfile(nickname) {
    securedClient.putConversationProfile(selectedConversation, nickname).OnSuccess = function () {

    };

    return false;
};

function loadConversationMessages(identifier) {

    var conversation = getConversation(identifier);
    loadConversationProfiles(conversation.Profiles);

    loadConversationMessagesT(identifier);

    if (messagesIntervalId != undefined) {
        clearInterval(messagesIntervalId);
    }

    messagesIntervalId = setInterval(loadConversationMessagesT, 2000, identifier);
}

function loadConversationMessagesT(identifier) {

    selectedConversation = identifier;

    var get = securedClient.getConversationMessages(identifier, etag.messagesETag);

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
        etag.messagesETag = xhr.getResponseHeader("ETag");
    };
};

function loadConversationProfiles(profiles) {

    model.profiles = profiles;

    listProfiles.empty();

    for (var i = 0; i < profiles.length; i++) {
        var profile = profiles[i];
        var template = templateProfile.html();
        listProfiles.append(Mustache.to_html(template, profile));
    }
};

function getConversation(identifier) {
    for (var i = 0; i < model.conversations.length; i++) {
        if (model.conversations[i].Identifier == identifier) {
            return model.conversations[i];
        }
    }

    return null;
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