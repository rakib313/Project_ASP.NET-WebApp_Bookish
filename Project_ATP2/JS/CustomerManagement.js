function changeStatus(str, type) {

    var Email = document.getElementById("Em");


    var obj = {
        Status: str,
        Email: Email.value
    };

    $.ajax({
        url: "/AdminAccount/ChangeStatus",
        method: "POST",
        data: {
            obj: obj
        },
        success: function (data) {
            alert("Successfully " + str);

        },
        error: function (err) {
            console.log(err);
        }

    });
}