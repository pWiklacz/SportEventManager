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

    var table = document.getElementById('StadiumsTable');
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
    var test = document.getElementById('NumberOfTeam').value;
    var numTest = parseInt(test);
    var count = rows.length;
    if (count > numTest || count < numTest) {
        document.getElementById("btnNext2").style.display = "none";
    }
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
    var test = document.getElementById('NumberOfTeam').value;
    var numTest = parseInt(test);
    var count = rows.length;
    if (count === numTest) {
        document.getElementById("btnNext2").style.display = "block";
    }
    
    if (rows.length > document.getElementById('NumberOfTeam').value) {
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
    var table = document.getElementById('StadiumsTable');
    var rows = table.getElementsByTagName('tr');
    if (rows.length > 10) {
        alert("Maximum stadium' quantity exceeded!");
        return;
    }
    var rowOutherHtml = rows[rows.length - 1].outerHTML;
    var lastrowIdx = document.getElementById('hdnLastIndexStadium').value;
    var nextrowIdx = eval(lastrowIdx) + 1;
    document.getElementById('hdnLastIndexStadium').value = nextrowIdx;
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


function checkParity() {
    var numberOfTeam = document.getElementById('NumberOfTeam').value;
    if (numberOfTeam % 2 != 0 && numberOfTeam != 0) {
        return false
    }
    else
    {
        return true;
    }
}

function showNextPage1() {

    if (checkParity() === true) {
        document.getElementById("page-1").style.display = "none";
        document.getElementById("page-2").style.display = "block";
        document.getElementById("btnBack").style.display = "block";
        document.getElementById("btnReset").style.display = "none";
        document.getElementById("btnNext1").style.display = "none";
        document.getElementById("btnNext2").style.display = "none";
        document.getElementById("BackendErrorSpan").style.display = "none";
    }
    else {
        alert("Number from Team must be even and grater than 0!");
    }

}

function showNextPage2() {

    var teamsCount = document.getElementById('teamsCount').value;
    var teams = JSON.parse(teamsCount);

    var enterNumber = document.getElementById('NumberOfTeam').value;

    if (teamsCount < enterNumber) {
        alert("Invalid number of selsected teams");
    }
    else
    {
        var uniqueTeams = [];

        for (var i = 0; i < teams.length; i++) {
            if (uniqueTeams.indexOf(teams[i]) === -1) {
                uniqueTeams.push(teams[i]);
            }
        }

        if (uniqueTeams === 0) {
            alert("Wartości są takie same");
        }
        else {
            document.getElementById("page-1").style.display = "none";
            document.getElementById("page-2").style.display = "none";
            document.getElementById("btnBack").style.display = "none";
            document.getElementById("page-3").style.display = "block";
            document.getElementById("btnNext2").style.display = "none";
        }
    }
}



function backPage() {
    document.getElementById("page-2").style.display = "none";
    document.getElementById("page-1").style.display = "block"
    document.getElementById("btnBack").style.display = "none";
    document.getElementById("btnReset").style.display = "block";
    document.getElementById("btnNext1").style.display = "block";
    document.getElementById("btnNext2").style.display = "none";
}

function resetDetails() {
    document.getElementById("EName").value = '';
    document.getElementById("EStadium").value = '';
    document.getElementById("SDate").value = '';
    document.getElementById("NPlayer").value = '';
}


// Write your JavaScript code.