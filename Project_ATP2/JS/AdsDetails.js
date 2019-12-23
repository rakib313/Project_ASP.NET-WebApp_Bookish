function btnClick(str, type) {
    var report = document.getElementById("Report");
    var reportRes = document.getElementById("ReportRes");
    var bookId = document.getElementById("Id");
    var review = document.getElementById("ReviewStr");
    var stars = document.getElementById("RateStar");
    var userId = document.getElementById("User_Id");
    var commentReview = document.getElementById("CommentReview");

    if (str == "Submit Report") {
        if (report.value != "") {
            var obj = {
                Book_Id: bookId.value,
                Status: "pending",
                Detail: report.value
            }
            reportRes.innerHTML = "";
            submitReport(report, obj, reportRes);
        }
        else {
            report.value = "";
            reportRes.innerHTML = "Please write a report";
        }
    } else if (str == "Add") {
        if (stars.value != ""){
            var obj = {
                Review: review.value,
                Stars: stars.value,
                Book_Id: bookId.value,
                User_Id: userId.value
            };
            commentReview.innerHTML = "";
            stars.value = "";
            review.value = "";
            submitReview(commentReview, obj);
        }
        else {
            commentReview.color = "Red";
            commentReview.innerHTML = "Please select a rate form 1 to 5";
        }
    }

    
}

function submitReview(commentReview, obj){
    $.ajax({
        url: "/Home/AddReview",
        method: "POST",
        data: {
            obj: obj
        },
        success: function (data) {
            var review = document.getElementById("ReviewStr");
            var stars = document.getElementById("RateStar");
            review.value = obj.Review;
            stars.value = obj.Stars;
            commentReview.innerHTML = "Review successfully added";

        },
        error: function (err) {
            console.log(err);
            reportRes.innerHTML = "Something Went wrong!!! Please try again";
        }

    });
}


function submitReport(report, obj, reportRes)
{
    
    $.ajax({
        url: "/Home/AddReport",
        method: "POST",
        data: {
            obj: obj
        },
        success: function (data) {
            reportRes.innerHTML = "Reprot successfully added";
        },
        error: function (err) {
            console.log(err);
            reportRes.innerHTML = "Something Went wrong!!! Please try again";
        }

    });

}


function addToCart(bookId, cart) {

    var userId = document.getElementById("User_Id");
    if (userId.value == "") {
        if (confirm("Login to add to cart. Continue to login page?"))
            window.location.href = "/Account/Login";
        else
            return false;
    }
    else {
        addCart(userId, bookId);
    }

}

function addCart(userId, bookId) {

    //var Book_Id = bookId;

    let ob = {
        User_Id: userId.value,
        Book_Id: bookId,
        QuantityOrdered: 1
    };

    $.ajax({
        url: "/Home/AddCart",
        type: "POST",
        data: {
            c: ob
        },
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
}