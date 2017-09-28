// Write your Javascript code.

function scrollToBottom() {
        var objDiv = document.getElementById("ul_conversationbox");
        objDiv.scrollTop = objDiv.scrollHeight;
    }

window.onload = function () {
    var objDiv = document.getElementById("ul_conversationbox");
    objDiv.scrollTop = objDiv.scrollHeight;
}