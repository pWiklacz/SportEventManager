// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function hasClass(elem, className) {
    return elem.classList.contains(className);
}


function DeleteItem(btn) {
    var table = document.getElementById('PlayerTable');
    var rows = table.getElementsByTagName('tr');

    if (rows.length == 2) {
        alert('This row cannot be deleted!')
        return;
    }

    $(btn).closest('tr').hide();
}

// Write your JavaScript code.
