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