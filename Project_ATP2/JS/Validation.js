$(document).ready(function () {

    $.ajax({
        url: "http://localhost:56991/api/Authors",
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







function Validation() {


    var errors = "";
    errors += CheckName();
    errors += CheckEmail();
    errors += CheckPassword();
    errors += CheckConfirmPassword();
    errors += CheckMobileNo();

    if (errors !== "") {
        alert(errors);
        return false;
    }
    return true;
}

function CheckName() {
    var TN = document.getElementById('Name');
    var exp = /^[a-zA-Z ]+$/;
    if (TN.value == "") {
        return 'please enter your name !!\n';
    }
    else if (exp.test(TN.value)) {
        return "";
    }
    else {
        return 'please enter only alphabets !!\n';
    }
}

function CheckEmail() {
    var TM = document.getElementById('Email');
    var exp = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (TM.value == "") {
        return 'Please Enter Email !!\n';
    }
    else if (exp.test(TM.value)) {
        return "";
    }
    else {
        return 'Please enter Valid Email!!\n';
    }

}

function CheckPassword() {
    var TP = document.getElementById('Password');
    if (TP.value == "") {
        return 'Please enter Password\n';
    }
    else {
        return "";
    }
}

function CheckConfirmPassword() {
    var TP = document.getElementById('Password');
    var TCF = document.getElementById('ConfirmPassword');
    if (TCF.value == "") {
        return 'Please enter Confirm Password!!\n';
    }
    else if (TP.value == TCF.value) {
        return "";
    }
    else {
        return "Password and Confirm Password do not Match!!\n";
    }
}

function CheckMobileNo() {
    var TMN = document.getElementById('PhoneNumber');
    //var exp = /^[0]?[789]\d{9}$/;
    if (TMN.value == "") {
        alert(TMN.value);
        return 'Please Enter Mobile No!!\n';
    }
    var ok = false;
    for (var i = 0; i < TMN.value.length; i++) {
        if (TMN.value[i] >= '0' && TMN.value[i] <= '9') {
            ok = true;
        }
        else {
            ok = false; break;
        }
    }

    if (ok==false) {
        return 'Please Enter Mobile No!!\n';
    }
    else {
        return "";
    }
}