import provinceInfo from '../json/province.json' assert { type: "json" };


window.onload = function () {
    const provinceSelection = document.getElementById("province");
    const districtSelection = document.getElementById("district");
    const wardSelection = document.getElementById("ward");
    console.log(provinceSelection);
    districtSelection.disabled = true;
    wardSelection.disabled = true;

    var cnt = 0;
    for (let province of provinceInfo) {
        var option = document.createElement("option");
        option.id = province["Id"];
        option.text = province["Name"];
        option.value = province["Id"];
        provinceSelection.add(option, provinceSelection[++cnt]);
    }

    provinceSelection.onchange = (e) => {
        districtSelection.disabled = false;
        wardSelection.disabled = true;
        districtSelection.length = 1;
        wardSelection.length = 1;
        cnt = 0;
        for (let province of provinceInfo) {
            if (provinceSelection.value == province["Id"]) {
                for (let district of province["Districts"]) {
                    var option = document.createElement("option");
                    option.id = district["Id"];
                    option.text = district["Name"];
                    option.value = district["Id"];
                    districtSelection.add(option, districtSelection[++cnt]);
                }
                break;
            }
        }
    }

    districtSelection.onchange = (e) => {
        wardSelection.disabled = false;
        wardSelection.length = 1;
        for (let province of provinceInfo) {
            if (provinceSelection.value == province["Id"]) {
                for (let district of province["Districts"]) {
                    if (districtSelection.value == district["Id"]) {
                        for (let ward of district["Wards"]) {
                            var option = document.createElement("option");
                            option.id = ward["Id"];
                            option.text = ward["Name"];
                            option.value = ward["Id"];
                            wardSelection.add(option, wardSelection[++cnt]);
                        }
                        break;
                    }                   
                }
                break;
            }           
        }
    }
    wardSelection.onchange = (e) => {
        DataView["WardId"] = (int)(wardSelection.options[wardSelection.selectedIndex].id);
    }
}