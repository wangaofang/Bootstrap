$("#contactForm").submit(function(event){
    // cancels the form submission
    alert( "Handler for .submit() called." );
    event.preventDefault();
    submitForm();
});

// $( "#contactForm" ).submit(function( event ) {
//     alert( "Handler for .submit() called." );
//     // event.preventDefault();
//   });

// $("#form-submit").click(function(){
//     submitForm();
// });

function submitForm(){
    var name=$("#name").val();
    var email=$("#email").val();
    var message=$("#message").val();

    $.ajax({
        type:"POST",
        contenttype:"JSON",
        url:"api/FormProcess/abc",
        data:{
            "Name":name,
            "Email":email,
            "Message":message
        },
        success:function(text){
            if(text=="success")
                formSuccess();
        }
    });
};

function formSuccess(){
    $("#msgSubmit").removeClass("hidden");
}
