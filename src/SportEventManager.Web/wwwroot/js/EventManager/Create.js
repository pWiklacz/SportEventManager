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
    else {
        return true;
    }
}

function showNextPage1() {

    if (checkParity() === true && document.getElementById('NumberOfTeam').value > 0) {
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

    var chosenTeamsNames = document.querySelectorAll('#TeamSelect');
    var chosenTeamsNamesCount = chosenTeamsNames.length;
    var teams = [];
    chosenTeamsNames.forEach(teamNameElement => {
        teams.push(teamNameElement.value);
    });

    var enterNumber = document.getElementById('NumberOfTeam').value;

    if (chosenTeamsNamesCount < enterNumber) {
        alert("Invalid number of selected teams");
    }
    else {
        var uniqueTeams = [];

        for (var i = 0; i < teams.length; i++) {
            if (uniqueTeams.indexOf(teams[i]) === -1) {
                uniqueTeams.push(teams[i]);
            }
        }
        console.log(uniqueTeams.length)
        console.log(enterNumber)
        if (uniqueTeams.length !== Number(enterNumber)) {
            alert("Wybrano nieunikalne druzyny");
            uniqueTeams = [];
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