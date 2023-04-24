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


function toggleTable() {
        document.getElementById('secondTable').style.display = "block"
        document.getElementById('firstTable').style.display = "none"
        document.getElementById('secondDisplay').style.display = "block"
        document.getElementById('firstDisplay').style.display = "none"
}

function Back() {
    document.getElementById('secondTable').style.display = "none"
    document.getElementById('firstTable').style.display = "block"
    document.getElementById('secondDisplay').style.display = "none"
    document.getElementById('firstDisplay').style.display = "block"
}

function resetDetails() {
    document.getElementById("EName").value = '';
    document.getElementById("EStadium").value = '';
    document.getElementById("SDate").value = '';
    document.getElementById("NPlayer").value = '';
}

// Write your JavaScript code.
