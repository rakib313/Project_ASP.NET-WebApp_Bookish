function clearCombo(area) {

    let len = area.length;
    for (let index = 0; index < len ; index++) {
        area.remove(area[index]);

    }
    //area.length=0;
}

//function loadArea(str,type)
//{
//    let city = document.getElementById('City');
//    let area = document.getElementById('Area');
//    clearCombo(area);

//    $.ajax({
//        url: "/Json/dhaka.json",
//        method: "GET",
//        success: function (data) {
//            console.log(data);
//        },
//        error: function (err) {
//            console.log(err);
//        }

//    })
//}




function loadArea(str, type) {
    let city = document.getElementById('City');
    let area = document.getElementById('Area');
    clearCombo(area);
    let ajax = new XMLHttpRequest();
    ajax.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            let areaData = JSON.parse(ajax.responseText);
            for (let index = 0; index < areaData.length; index++) {

                let option = document.createElement("option");
                if (city.value == "Dhaka") {
                    option.text = areaData[index].area;

                }
                else if (city.value == "Chittagong") {
                    option.text = areaData[index].area;
                }
                else if (city.value == "Khulna") {
                    option.text = areaData[index].area;
                }
                area.add(option);



            }

        }
    }
    if (city.value == "Dhaka") {
        ajax.open("GET", "/Json/dhaka.json", true);
        ajax.send();
    }
    else if (city.value == "Chittagong") {
        ajax.open("GET", "/Json/chittagong.json", true);
        ajax.send();
    }
    else if (city.value == "Khulna") {
        ajax.open("GET", "/Json/khulna.json", true);
        ajax.send();
    }
    else {

    }
}

//$(document).ready(function () {
//    //Initially load pagenumber=1
//    GetPageData(1);
//});
//function GetPageData(pageNum, pageSize) {
//    //After every trigger remove previous data and paging
//    $.getJSON("/Home/Filter", { pageNumber: pageNum, pageSize: pageSize }, function (response) {
//        var data = JSON.parse(response);
//        console.log(data);
//        var rowData = "";

//        $("#AdsGrid_Window").append("hello");
//    });
//}

//function loadAllBooks(str, type) {
//    let city = document.getElementById('City');
//    let area = document.getElementById('Area');
//    let searchStr = document.getElementById('SearchStr');
//    let orderBy = document.getElementById('Order-By');

//    console.log(orderBy.value);

//    $.ajax({
//        url: "/Post/Search?&city=" + city.value + "&area=" + area.value + "&searchStr=" + searchStr.value +"&orderBy="+orderBy.value,
//        method: "GET",
//        success: function (data) {
//            console.log(data);
//        },
//        error: function (err) {
//            console.log(err);
//        }

//    })
//}