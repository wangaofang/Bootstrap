
$.ajax({
    url: "/api/product",
    // contentType:'application/json',        
    type: "get",
    // data: JSON.stringify({ NAME: "Jim",DES:"备注" }),
    // data: { NAME: "Jim",DES:"备注" },           
    success: function (data, status) {
        if (status == "success") {
            $("#divTest").html(JSON.stringify(data));
        };
    }
});
