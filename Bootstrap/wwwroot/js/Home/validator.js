$("#departmentForm").validator().on("submit", function (event) {
    if (event.isDefaultPrevented()) {
        // handle the invalid form...
        formError();
        sumbitMSG(false, "Did you fill in the form properly?");
    } else {
        // everything looks good!
        event.preventDefault();       
    }
});

function formError(){
    $("#departmentForm").removeClass().addClass('shake animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function(){
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