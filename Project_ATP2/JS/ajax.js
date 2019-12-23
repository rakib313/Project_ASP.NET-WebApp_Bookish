$(function () {

    var page = 1,
        pagelimit = 5,
        totalrecord = 0;

    fetchData();

    // handling the prev-btn
    $(".prev-btn").on("click", function () {
        pagePre();
    });

    // handling the next-btn
    $(".next-btn").on("click", function () {
        pageNext();
    });

    function pagePre() {
        if (page > 1) {
            page--;
            fetchData();
        }
        console.log("Prev Page: " + page);
    }

    function pageNext() {
        if (page * pagelimit < totalrecord) {
            page++;
            fetchData();
        }
        console.log("Next Page: " + page);
    }

    function fetchData() {
        var searchStr = $("SearchStr").val(),
            orderBy = $("Order-By").val(),
            language = $("#Language").val(),
            category = $("#Category").val(),
            author = $("#Author").val(),
            publisher = $("#Publisher").val();
        console.log(publisher);
        // ajax() method to make api calls
        $.ajax({
            url: "/Home/GetBooks",
            type: "GET",
            data: {
                page: page,
                pagelimit: pagelimit,
                searchStr: searchStr,
                orderBy: orderBy,
                category: category,
                author: author,
                language: language,
                publisher: publisher
            },
            success: function (data) {
                console.log(data);

                if (data.success) {
                    //var dataArr = data.success.data;
                    var dataArr = JSON.parse(data);
                    totalrecord = data.success.totalrecord;
                    console.log("total" + totalrecord);
                    console.log(dataArr);
                    //var html = "";
                    //for (var i = 0; i < dataArr.length; i++) {
                    //    html += "<div class='sample-user'>" +
                    //		"<h3>ID: " + dataArr[i].id + "</h3>" +
                    //		"<p>First Name: " + dataArr[i].firstname + "</p>" +
                    //		"<p>Last Name: " + dataArr[i].lastname + "</p>" +
                    //		"<p>Last Modified At: " + dataArr[i].lastmodified + "</p>" +
                    //		"<p>Created At: " + dataArr[i].created + "</p>" +
                    //	"</div>" +
                    //	"<hr />";
                    //}
                    //$("#result").html(html);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
});