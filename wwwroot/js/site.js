var searchForm = document.getElementById('search-form');
var searchInput = document.getElementById('search-input');
searchInput.addEventListener('keyup', function (event) {
    event.preventDefault();
    if (event.keyCode === 13) {
        searchForm.submit();
    }
});
