function checkPesel(input) {
    var inputPesel = String(input.value);

    var existingPeselNumbers = document.getElementById('existingPeselNumbers').value

    var existingPeselNumbersList = existingPeselNumbers.split(',');

    if (existingPeselNumbersList.includes(inputPesel)) {
        alert('This PESEL number ' + inputPesel + ' already exists!');
        input.value = '';
    }
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