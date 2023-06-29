//document.getElementById("NumberOfPlayers").addEventListener("keydown", function (event) {
  //  event.preventDefault();
    //document.getElementById("NumberOfPlayers").value = 1;
//}); IN PROGRESSSSSSS

function checkAllPeselNumbers() {
    var postedPeselNumbersList = document.querySelectorAll('pesel-input').value;

    console.log(postedPeselNumbersList)
    //if (postedPeselNumbersList.includes(inputPesel)) {
      //  alert('This PESEL number ' + inputPesel + ' already exists!');
        //input.value = '';
    //}
}

function DeleteItem(btn) {

    var table = document.getElementById('PlayersTable');
    var rows = table.getElementsByTagName('tr');
    if (rows.length == 2) {
        alert("This row cannot be deleted!");
        return;
    }
    if (rows.length !== Number(document.getElementById('NumberOfPlayers').value)) {
        document.getElementById('submit-button').classList.remove("visible");
        document.getElementById('submit-button').classList.add("invisible");
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
    if (rows.length === Number(document.getElementById('NumberOfPlayers').value)) {
        document.getElementById('submit-button').classList.remove("invisible");
        document.getElementById('submit-button').classList.add("visible");
    } else {
        document.getElementById('submit-button').classList.remove("visible");
        document.getElementById('submit-button').classList.add("invisible");
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

function adjustPlayersRows() {
    var table = document.getElementById('PlayersTable');
    var rows = table.getElementsByTagName('tr');
    var numberOfPlayers = document.getElementById('NumberOfPlayers').value;
    while (rows.length - 1 < numberOfPlayers) {
        addRow(table, rows)
    }
    while (rows.length - 1 > numberOfPlayers) {
        subtractRow(table, rows)
    }
}

function addRow(table, rows) {
    var rowOutherHtml = rows[rows.length - 1].outerHTML;
    var lastrowIdx = document.getElementById('hdnLastIndex').value;
    var nextrowIdx = eval(lastrowIdx) + 1;
    document.getElementById('hdnLastIndex').value = nextrowIdx;
    rowOutherHtml = rowOutherHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOutherHtml = rowOutherHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOutherHtml = rowOutherHtml.replaceAll('-' + lastrowIdx, nextrowIdx + '_');
    var newRow = table.insertRow();
    newRow.innerHTML = rowOutherHtml;
}

function subtractRow(table, rows) {
    if(rows.length == 2) {
        return;
    }
    table.rows[table.rows.length - 1].remove();
}