$("#btnlogin").click(function () {
    var url = "/UserAccount/Login";
    $(".modal-body").load(url, function () {
        $(".modal-body #func-login").removeClass("mt-5 mb-5");
        $(".modal-body #func-login-col").removeClass("col-md-4 card");
        $(".modal-body  nav").remove();
        $(".modal-body  footer").remove();
        $(".modal-body  hr").remove();
        $("#exampleModal").modal();
    });

});