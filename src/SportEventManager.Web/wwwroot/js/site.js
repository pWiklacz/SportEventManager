// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function hasClass(elem, className) {
    return elem.classList.contains(className);
}

function DeleteItem(btn) {

    var table = document.getElementById('PlayersTable');
    var rows = table.getElementsByTagName('tr');
    if (rows.length == 2) {
        alert("This row cannot be deleted!");
        return;
    }
    $(btn).closest('tr').remove();
}

function DeleteStadiumItem(btn) {

    var table = document.getElementById('EventTable');
    var rows = table.getElementsByTagName('tr');
    if (rows.length == 2) {
        alert("This row cannot be deleted!");
        return;
    }
    $(btn).closest('tr').remove();
}

function DeleteTeam(btn) {

    var table = document.getElementById('TeamTable');
    var rows = table.getElementsByTagName('tr');
    if (rows.length == 2) {
        alert("This row cannot be deleted!");
        return;
    }
    $(btn).closest('tr').remove();
}

function AddItem(btn) {
    var table = document.getElementById('PlayersTable');
    var rows = table.getElementsByTagName('tr');
    if (rows.length > document.getElementById('NumberOfPlayers').value) {
        alert("Maximum players' quantity exceeded!");
        return;
    }
    var rowOutherHtml = rows[rows.length - 1].outerHTML;
    var lastrowIdx = document.getElementById('hdnLastIndex').value;
    var nextrowIdx = eval(lastrowIdx) + 1;
    document.getElementById('hdnLastIndex').value = nextrowIdx;
    rowOutherHtml = rowOutherHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOutherHtml = rowOutherHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOutherHtml = rowOutherHtml.replaceAll('-' + lastrowIdx, nextrowIdx + '_');
    var newRow = table.insertRow();
    newRow.innerHTML = rowOutherHtml;
    var btnAddID = btn.id;
    var btnDeleteid = btn.replaceAll('btnadd', 'btnremove');
    var delbtn = document.getElementById(btnDeleteid);
    delbtn.classList.add("visible");
    delbtn.classList.remove("invisible");
    var addbtn = document.getElementById(btnAddID);
    delbtn.classList.remove("visible");
    delbtn.classList.add("invisible");
}

function AddTeam(btn) {
    var table = document.getElementById('TeamTable');
    var rows = table.getElementsByTagName('tr');
    if (rows.length > 10) {
        alert("Maximum team' quantity exceeded!");
        return;
    }
    var rowOutherHtml = rows[rows.length - 1].outerHTML;
    var lastrowIdx = document.getElementById('hdnLastIndexTeam').value;
    var nextrowIdx = eval(lastrowIdx) + 1;
    document.getElementById('hdnLastIndexTeam').value = nextrowIdx;
    rowOutherHtml = rowOutherHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOutherHtml = rowOutherHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOutherHtml = rowOutherHtml.replaceAll('-' + lastrowIdx, nextrowIdx + '_');
    var newRow = table.insertRow();
    newRow.innerHTML = rowOutherHtml;
    var btnAddID = btn.id;
    var btnDeleteid = btn.replaceAll('btnadd', 'btnremove');
    var delbtn = document.getElementById(btnDeleteid);
    delbtn.classList.add("visible");
    delbtn.classList.remove("invisible");
    var addbtn = document.getElementById(btnAddID);
    delbtn.classList.remove("visible");
    delbtn.classList.add("invisible");
}

function AddStadiumItem(btn) {
    var table = document.getElementById('EventTable');
    var rows = table.getElementsByTagName('tr');
    if (rows.length > 10) {
        alert("Maximum players' quantity exceeded!");
        return;
    }
    var rowOutherHtml = rows[rows.length - 1].outerHTML;
    var lastrowIdx = document.getElementById('hdnLastIndex').value;
    var nextrowIdx = eval(lastrowIdx) + 1;
    document.getElementById('hdnLastIndex').value = nextrowIdx;
    rowOutherHtml = rowOutherHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOutherHtml = rowOutherHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOutherHtml = rowOutherHtml.replaceAll('-' + lastrowIdx, nextrowIdx + '_');
    var newRow = table.insertRow();
    newRow.innerHTML = rowOutherHtml;
    var btnAddID = btn.id;
    var btnDeleteid = btn.replaceAll('btnadd', 'btnremove');
    var delbtn = document.getElementById(btnDeleteid);
    delbtn.classList.add("visible");
    delbtn.classList.remove("invisible");
    var addbtn = document.getElementById(btnAddID);
    delbtn.classList.remove("visible");
    delbtn.classList.add("invisible");
}

function showNextPage1() {
    document.getElementById("page-1").style.display = "none";
    document.getElementById("page-2").style.display = "block";
    document.getElementById("btnBack").style.display = "block";
    document.getElementById("btnReset").style.display = "none";
    document.getElementById("btnNext1").style.display = "none";
    document.getElementById("btnNext2").style.display = "block";

}

function showNextPage2() {
    document.getElementById("page-1").style.display = "none";
    document.getElementById("page-2").style.display = "none";
    document.getElementById("btnBack").style.display = "none";
    document.getElementById("btnReset").style.display = "none";
    document.getElementById("btnNext1").style.display = "none";
    document.getElementById("page-3").style.display = "block";
    document.getElementById("btnNext2").style.display = "none";
}

function backPage() {
    document.getElementById("page-2").style.display = "none";
    document.getElementById("page-1").style.display = "block"
    document.getElementById("btnBack").style.display = "none";
    document.getElementById("btnReset").style.display = "block";
    document.getElementById("btnNext").style.display = "block";
}

function resetDetails() {
    document.getElementById("EName").value = '';
    document.getElementById("EStadium").value = '';
    document.getElementById("SDate").value = '';
    document.getElementById("NPlayer").value = '';
}


function validateForm() {
    var teams = document.getElementsByClassName("team-input");
    var values = [];

    for (var i = 0; i < teams.length; i++) {
        if (values.includes(teams[i].value)) {
            alert("Duplicate team name found.");
            return false;
        } else {
            values.push(teams[i].value);
        }
    }

    showNextPage2();

    return true;
}

// Write your JavaScript code.