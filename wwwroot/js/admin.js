const buttons = document.getElementsByClassName("nav-link category-item__link");
const url = window.location.href.toString();
const namePage = url.substring(url.lastIndexOf("/") + 1, url.length);
console.log(namePage);
for (let button of buttons) {
    if (button.id === namePage) {
        button.classList.add("category-item--active");
        break;
    }
}

/*const lis = document.getElementsByClassName("category-item");

function Display(li) {
    console.log(li.id);
    // lay class cuoi cung, roi tim cac element co class do de them class display vao
}

for (let li of lis) {
    console.log(li);
    li.addEventListener("click", Display(li));
}
*/

function ShowStaffMenu() {
    var elements = document.getElementsByClassName("staff");
    for (let element of elements) {
        if (element.style.display === "none") {
            element.style.display = "block";
        }
        else {
            element.style.display = "none";
        }
    }
}

function ShowCategoryMenu() {
    var elements = document.getElementsByClassName("category");
    for (let element of elements) {
        if (element.style.display === "none") {
            element.style.display = "block";
        }
        else {
            element.style.display = "none";
        }
    }
}

function ShowBookMenu() {
    var elements = document.getElementsByClassName("book");
    for (let element of elements) {
        if (element.style.display === "none") {
            element.style.display = "block";
        }
        else {
            element.style.display = "none";
        }
    }
}

function ShowProviderMenu() {
    var elements = document.getElementsByClassName("provider");
    for (let element of elements) {
        if (element.style.display === "none") {
            element.style.display = "block";
        }
        else {
            element.style.display = "none";
        }
    }
}