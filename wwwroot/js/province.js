import provinceInfo from '../json/province.json' assert { type: "json" };

const provinceSelection = document.getElementById("province");
const districtSelection = document.getElementById("district");
const wardSelection = document.getElementById("ward");
console.log(provinceSelection);
districtSelection.disabled = true;
wardSelection.disabled = true;

console.log(provinceInfo)

const loadDistrict = (provinceId) => {
    districtSelection.disabled = false;
    wardSelection.disabled = true;
    districtSelection.length = 1;
    wardSelection.length = 1;
    var cnt = 0;
    var isFirst = true;
    for (let province of provinceInfo) {
        if (provinceId == province["Id"]) {
            for (let district of province["Districts"]) {
                var option = document.createElement("option");
                option.id = district["Id"];
                option.text = district["Name"];
                option.value = district["Id"];
                if (isFirst === true) {
                    isFirst = false;
                    option.selected = "selected";
                    loadWard(provinceId, district["Id"])
                }
                districtSelection.add(option, districtSelection[cnt++]);
            }
            break;
        }
    }
}

const loadWard = (provinceId, districtId) => {
    wardSelection.disabled = false;
    wardSelection.length = 1;
    var isFirst = true;
    var cnt = 0;
    for (let province of provinceInfo) {
        if (provinceId == province["Id"]) {
            for (let district of province["Districts"]) {
                if (districtId == district["Id"]) {
                    for (let ward of district["Wards"]) {
                        var option = document.createElement("option");
                        option.id = ward["Id"];
                        option.text = ward["Name"];
                        option.value = ward["Id"];
                        if (isFirst === true) {
                            isFirst = false;
                            option.selected = "selected";
                        }
                        wardSelection.add(option, wardSelection[cnt++]);
                    }
                    break;
                }
            }
            break;
        }
    }
}

window.onload = function () {
    var cnt = 0;
    var isFirst = true;
    for (let province of provinceInfo) {
        console.log(province["Name"])
        var option = document.createElement("option");
        option.id = province["Id"];
        option.text = province["Name"];
        option.value = province["Id"];
        if (isFirst === true) {
            isFirst = false;
            option.selected = "selected";
            loadDistrict(province["Id"])
        }
        provinceSelection.add(option, provinceSelection[++cnt]);
    } 

    provinceSelection.onchange = (e) => {
        loadDistrict(provinceSelection.value);        
    }

    districtSelection.onchange = (e) => {
        loadWard(provinceSelection.value, districtSelection.value)
    }
    wardSelection.onchange = (e) => {
        DataView["WardId"] = (int)(wardSelection.options[wardSelection.selectedIndex].id);
    }
}