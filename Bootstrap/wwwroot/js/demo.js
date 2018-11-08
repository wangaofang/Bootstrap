$("#contactForm").validator().on("submit", function (event) {
    if (event.isDefaultPrevented()) {
        // handle the invalid form...
        formError();
        sumbitMSG(false, "Did you fill in the form properly?");
    } else {
        // everything looks good!
        event.preventDefault();
        submitForm();
    }
});

// $("#contactForm").submit(function (event) {
//     // cancels the form submission 
//     event.preventDefault();
//     submitForm();
// });

function submitForm() {
    var name = $("#name").val();
    var email = $("#email").val();
    var message = $("#message").val();

    $.ajax({
        type: "POST",
        contenttype: "JSON",
        url: "api/FormProcess/abc",
        data: {
            "Name": name,
            "Email": email,
            "Message": message
        },
        success: function (text) {
            if (text == "success") {
                $("#contactForm")[0].reset();
                sumbitMSG(true, "Message Submitted!");
            }
        }
    });
};

function formSuccess() {
    $("#msgSubmit").removeClass("hidden");
}

function formError(){
    $("#contactForm").removeClass().addClass('shake animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function(){
        $(this).removeClass();
    });
}

function sumbitMSG(valid, msg) {
    var msgClasses;
    if (valid) {
        msgClasses = "h3 text-center tada  animated text-success";
    } else {
        msgClasses = "h3 text-center  text-danger";
    }
    $("#msgSubmit").removeClass().addClass(msgClasses).text(msg);
}
