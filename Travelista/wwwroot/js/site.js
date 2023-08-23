
window.onload = function () {
    var today = new Date().toISOString().split('T')[0];
    var startDateInput = document.getElementsByName("strart_date")[0];
    startDateInput.setAttribute('min', today);
    var to = new Date().toISOString().split('T')[0];
    var endDateInput = document.getElementsByName("end_date")[0];
    endDateInput.setAttribute('min', to);
    
}


let l = 'en';
function show( id, lan) {

    if (l == lan) { l = lan; } else { }
        let to = document.getElementById(`language-select-to-` + id).value;
        console.log(to);
        let inp = document.getElementById("content-" + id).textContent;
        console.log(l);
        if (l == to) {

        }
        else {

            fetch(`https://api.mymemory.translated.net/get?q=${inp}!&langpair=${l}|${to}`).then(translat => translat.json())
                .then(data => {
                    document.getElementById("content-" + id).innerText = data.responseData.translatedText;
                    console.log(data.responseData.translatedText);


                });

        }
        l = to;
        console.log(l);


}






//function translate(id) {
//    let select = document.getElementById(`language-select-to-${id}`);
//    let to = select.value;
//    show(to);
//}
//function show(to) {
//    let inp = document.getElementById("content").textContent;

//    console.log(l);
//    if (l === to) {
//        // Do nothing if the language hasn't changed
//        return;
//    }

//    // Fetch the translation from the API
//    fetch(`https://api.mymemory.translated.net/get?q=${inp}!&langpair=${l}|${to}`)
//        .then(translat => translat.json())
//        .then(data => {
//            document.getElementById("content").innerText = data.responseData.translatedText;
//        });

//    // Update the current language
//    l = to;
//    console.log(l);
//}


var comment = document.getElementById("comment");
var error = document.getElementById("comment-error");

comment.addEventListener("input", function (event) {
    if (!isValidComment(comment.value)) {
        comment.setCustomValidity("The comment must contain only English language characters.");
        error.textContent = comment.validationMessage;
    } else {
        comment.setCustomValidity("");
        error.textContent = "";
    }
});

function isValidComment(comment) {
    var pattern = /^[a-zA-Z0-9.,!?'"()$#@%&*\s]*$/;
    return pattern.test(comment);
}


