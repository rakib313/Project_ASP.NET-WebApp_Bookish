





function validation() {

    let name = document.getElementById('Name');
    let email = document.getElementById('Email');
    let password = document.getElementById('Password').value;
    let confirmPassword = document.getElementById('ConfirmPassword').value;
    let phoneNumber = document.getElementById('Contact_Number');
    let address = document.getElementById('Address');
    let comment = document.getElementById('Comment');
    let r1 = document.getElementById('r1');
    let r2 = document.getElementById('r2');

    

    //console.log(name.value);
    if (name.value == '') {
        comment.innerHTML = "Please enter First and Last name";
        return false;
    } else if (!checkNameSpace(name.value)) { //2  words missing
        comment.innerHTML = "Name must contain atleast two words";
        return false;
    } else if (checkNameIsDigit(name.value)) { //dot missing
        comment.innerHTML = "Name cannot contain digits";
        return false;
    } else {
        comment.innerHTML = "";
    }

    if (email.value == '') {
        comment.innerHTML = "email cannot be empty;";
        return false;
    }  else {
        comment.innerHTML = "";
    }

    if (password == '') {
        comment.innerHTML = "Password cannot be empty";
        return false;
    } else if (chackAtTheRate(password)) { //@ missing
        comment.innerHTML = "password must contain at least one '@'";
        return false;
    } else if (checkStrLength(password, 8)) {
        comment.innerHTML = "password must contain at least 8 characters";
        return false;
    } else {
        comment.innerHTML = "";
    }

    if (confirmPassword == "") {
        comment.innerHTML = "Please comfitm password";
        return false;
    } else if (confirmPassword != password) {
        comment.innerHTML = "Password not matching";
        return false;
    } else {
        comment.innerHTML = "";
    }

    if (phoneNumber.value == "") {
        comment.innerHTML = "Please enter phone number";
        return false;
    } else if (checkNameSpace(phoneNumber.value)) {
        comment.innerHTML = 'Please enter accurate phone number';
        return false;
    } else if (checkIsNotDigit(phoneNumber.value)) {
        comment.innerHTML = "Phone number can only contain digits";
        return false;
    } else {
        comment.innerHTML = "";
    }

    if (address.value == '') {
        comment.innerHTML = 'Please enter address';
        return false;
    } else {
        comment.innerHTML = '';
    }

    /* checks if radio button is checked or not */
    if (!(r1.checked || r2.checked )) {
        comment.innerHTML = 'Please select gender';
        return false;
    } else {
        comment.innerHTML = '';
    }

    



    /* checks if str is between first and last */
    function checkBetween(str, first, last) {
        if (str < first || str > last) {
            return true;
        }
        return false;
    }

    //returns true if not digit
    function checkIsNotDigit(str) {
        let num = str.charCodeAt(0);
        let charCodeZero = "0".charCodeAt(0);
        let charCodeNine = "9".charCodeAt(0);
        for (var i = 0; i < str.length; i++) {
            let num = str[i].charCodeAt(0);
            if (num < charCodeZero || num > charCodeNine) {
                return true;
            }
        }
        return false;
    }
    //matches str with len if grater returns true
    function checkStrLength(str, len) {
        if (str.length <= len) {
            return true;
        } else {
            return false;
        }
    }
    //checks if name contains two different words or not
    function checkNameSpace(name) {
        for (var i = 0; i < name.length; i++) {
            if (name[i] == " ") {
                return true;
            }
        }
        return false;
    }

    //checks if str is digit or not
    function checkNameIsDigit(str) {
        let charCodeZero = "0".charCodeAt(0);
        let charCodeNine = "9".charCodeAt(0);
        console.log(charCodeNine);
        for (var i = 0; i < str.length; i++) {
            let charCodeNum = str[i].charCodeAt(0);
            if (charCodeNum >= charCodeZero && charCodeNum <= charCodeNine) {
                return true;
            }
        }
        return false;
    }

    //checks if email contains str
    function chackAtTheRate(str) {
        console.log("");
        for (var i = 0; i < str.length; i++) {
            if (str[i].charCodeAt(0) == '@'.charCodeAt(0)) {
                return false;
            }
        }
        return true;
    }

    //checks if email contains dots
    function chackDot(str) {
        for (var i = 0; i < str.length; i++) {
            if (str[i].charCodeAt(0) == '.'.charCodeAt(0)) {
                return false;
            }
        }
        return true;
    }
}