
function checkAllPeselNumbers(event) {
    event.preventDefault();
    var peselInputs = document.querySelectorAll('#pesel-input');
    var postedPeselNumbersList = [];
    peselInputs.forEach(input => {
        postedPeselNumbersList.push(input.value);
    })

    var uniquePeselNumbersSet = [...new Set(postedPeselNumbersList)];
    if (uniquePeselNumbersSet.length !== postedPeselNumbersList.length) {
        alert('You can\'t post two players with the same pesel numbers!');
    }
}

//nie dzia³a to gówno jebane ani z click ani z niczym - nie da siê zatrzymaæ wejœcia do kontrolera
//bo wywo³aæ inaczej sam¹ metodê to akurat luz, ale jak z html to nie podajesz eventu do preventDefault()
//jak sie uda zrobiæ coœ z tym peslem to trzeba to samo zrobiæ z tagiem i numerami na koszulkach
('#TeamsForm').addEventListener("submit", checkAllPeselNumbers);

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
    var inputsCollection = newRow.getElementsByTagName('input');
    for (var i = 0; i < inputsCollection.length; i++) {
        if (i % 5 != 0) {
            inputsCollection[i].value = "";
        }
        if (inputsCollection[i].type == "number" && !inputsCollection[i].classList.contains('id-input')) {
            inputsCollection[i].value = 1;
        }
    }
}

function subtractRow(table, rows) {
    if(rows.length == 2) {
        return;
    }
    table.rows[table.rows.length - 1].remove();
}