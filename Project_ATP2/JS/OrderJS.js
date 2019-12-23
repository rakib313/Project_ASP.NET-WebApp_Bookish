$(document).ready(function () {
    getCart();

    $.ajax({
        url: "/Json/dhaka.json",
        method: "GET",
        success: function (data) {
            let area = document.getElementById('Area');

            for (let index = 0; index < data.length; index++) {

                let option = document.createElement("option");
                option.text = data[index].area;


                area.add(option);
            }
        },
        error: function (err) {
            console.log(err);
        }

    });

});

function getCart() {
    $.ajax({
        url: "/Cart/GetCart",
        method: "GET",
        success: function (data) {
            var btn = document.getElementById("btnSubmit");
            var data = JSON.parse(data);
            if (data.length == 0) {
                btn.disabled = true;
            } else {
                btn.disabled = false;
                calculation(data);
            }


        },
        error: function (err) {
            console.log(err);
        }

    });
}

function calculation(d) {
    var subtotal = document.getElementById("Subtotal");
    var couponKey = document.getElementById("CouponKey");
    var total = document.getElementById("Total");
    var couponPercentage = document.getElementById("CouponPercentage");
    var payableTotal = document.getElementById("Payable-Total");
    var shippingPrice = 40;

    let sub = 0;
    for (var i = 0; i < d.length; i++) {
        sub += d[i].QuantityOrdered * d[i].Price;
    }
    subtotal.innerHTML = sub;
    total.value = sub + shippingPrice;
    total.innerHTML = sub + shippingPrice;

    payableTotal.value = sub + shippingPrice;
    payableTotal.innerHTML = sub + shippingPrice;

    if (couponPercentage.value != "") {
        payableTotal.value -= total.value * (couponPercentage.value / 100);
        payableTotal.innerHTML = Math.ceil(payableTotal.value);
    }
}

function addOrder(type) {
    if (!validation()) {
        return;
    }

    var userId = document.getElementById("User_Id");
    var name = document.getElementById("Name");
    var phoneNumber = document.getElementById("PhoneNumber");
    var area = document.getElementById("Area");
    var address = document.getElementById("Address");
    var addedDate = document.getElementById("AddedDate");

    var obj = {
        User_Id: userId.value,
        Name: name.value,
        PhoneNumber: phoneNumber.value,
        Area: area.value,
        Address: address.value,
        AddedDate: addedDate.value,
        Status: "Pending"
    };

    $.ajax({
        url: "/Cart/AddOrder",
        method: "POST",
        data: {
            obj: obj 
        },
        success: function (data) {
            var data = JSON.parse(data);
            calculation(data);

        },
        error: function (err) {
            console.log(err);
        }

    });
}


function validation() {
    var name = document.getElementById("Name").value;
    var phoneNumber = document.getElementById("PhoneNumber").value;
    var area = document.getElementById("Area").value;
    var address = document.getElementById("Address").value;

    if (name!="" && phoneNumber!="" && area!="" && address!="") {
        return true;
    }
    else {
        return false;
    }
}