function clearCombo(area) {

    let len = area.length;
    for (let index = 0; index < len ; index++) {
        area.remove(area[index]);

    }
    //area.length=0;
}


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