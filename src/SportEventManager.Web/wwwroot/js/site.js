kubi7
#4028

kubi7 — 05.12.2022 01: 33
Jeżeli chodzi o przegląd to stworzyłem kanał tutorials gdzie Marcin od siebie coś wrzucił co bardzo polecam, no ja planuje dzisiaj chociaż cześć zrobić z Teams Managera aby dodawanie drużyn i zawodników było
Piotr Głąb — 05.12.2022 01: 38
No ja widzialem te tutoriale kiedyś ale raczej wiedzialem większość nie że się chwałę 🤣 a co u cb było nie tak z tym projektem ? I czym się w końcu zajmujesz teraz ?
    kubi7 — 05.12.2022 01: 41
No tworzę sekcję dla Team Manager do wprowadzania drużyn i zawodników a okazało się że pomimo tego że wszystkie pakiety miałem w wersji 6.0.10 to i tak mi wyłapywał 7.0 i dopiero z linii poleceń odinstalowanie i zainstalowanie zadziałało
Piotr Głąb — 05.12.2022 01: 42
No spoko.A to nie chciałeś w końcu zaczac tego widoku home ?
    To może ja bym to jutro zaczął jeśli by mi poszło szybko z tymi klasami ...
Bo Javę to w końcu chyba wcale nie będę robić xD
Tylko problem mam czy kontynuowanie tego w prototypie nie sprawi że bez finalnej wersji nie będę mógł nic zrobić ale no chuj.Teraz to padam na pysk i zaraz idę w kime
kubi7 — 05.12.2022 01: 43
Znaczy mógłbym tylko chyba Kamil powiedział że to tamto jest ważniejsze to mówię spoko mogę tamto XD
No ja się cieszę że mam Java dopiero za tydzień XD
Piotr Głąb — 05.12.2022 20: 32
Kubuś, czy ty będziesz nawalać na osobnym branchu jeszcze, bo chwilowo jest tylko main ? To rozumiem, że lokalnie wprowadzasz zmiany, a przed commitem robisz branch ta ?\
kubi7 — 05.12.2022 21: 42
Ta trochę się zajebałem i właśnie robię branch i wszystko wrzucę
Piotr Głąb — 05.12.2022 22: 02
Zrobiłem pull request'a i dałem cię jako recenzenta, bo ogarniasz też trochę git. To zerknij proszę w wolnej chwili na to co jest w gitignore lub listę plików, którą on wywali. Lokalnie po zrobieniu zmian na tym branchu wyjebałem repo, sklonowałem jeszcze raz i się kompiluje i działa, więc chyba nie wyrzucam nic potrzebnego
Patryk W — 05.12.2022 22: 02
dawaj tu na chwile
szybkooo
majster
@Piotr Głąb
Piotr Głąb — 05.12.2022 23: 34
Ups teraz widzę, że byłem potrzebny 😦
Piotr Głąb — 09.01.2023 12: 43
Obraz
Piotr Głąb — 16.01.2023 01: 15
Siema.Jak idzie ten widok zarządzania eventem ? Luźno pytam, bo pamiętam, że mówiłeś, że chciałeś robić w tym tygodniu czy coś ruszyło
Ja dopiero pod koniec tego tygodnia do io wrócę na cały tydzień napierdalania bo w tym tygodniu mam smiw i asm zamiar skończyć projekty
kubi7 — 16.01.2023 19: 06
ehhh to prawda chciałem.... tylko na czwartek rzeczy ogarnę i zabiorę się za to
Piotr Głąb — 29.01.2023 15: 00
Obraz
Piotr Głąb — 29.01.2023 15: 31
Padł mi laptop
I tak muszę już jechać, więc nwm czy ten timestamp coś zmienił możecie spróbować.Jak nie zadziała to może zostawcie ta datę zakomentowana ja się z tym pobawię jak będę w kato
A spróbujcie dojść o co chodzi z tym że wchodzi w post z tego przycisku next
Imo to coś zwiazego z formularzem tymi klasami form, form - control, form - page że on bazowo chce ten formularz postowac chuj wie czemu
Najwyżej zostaw to z błędami a idź dalej robić kolejne elementy, czy to już wszystko ?
    Z tego widoku
Patryk W — 29.01.2023 15: 43
https://stackoverflow.com/questions/17418258/datetime-format-to-sql-format-using-c-sharp
Stack Overflow
DateTime format to SQL format using C#
I am trying to save the current date time format from C# and convert it to an SQL Server date format like so yyyy - MM - dd HH: mm:ss so I can use it for my UPDATE query.

This was my first code:

DateT...
Obraz
string format = "yyyy-MM-dd HH:mm:ss.FFFFFFF"
Patryk W — 29.01.2023 16: 02
C#
    < input type = "button"
value = "Go Somewhere Else"
onclick = "location.href='<%: Url.Action("Action", "Controller") %>'" />
    C#
        < input type = "button"
value = "Go Somewhere Else"
onclick = "location.href='<%: Url.Action("Action", "Controller") %>'" />
    <button type="button" onclick="location.href='@Url.Action(" MyAction", "MyController")'" />
        Patryk W — 29.01.2023 16: 21
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
Patryk W — 29.01.2023 22: 22
pisałem do Marcina
i mi powiedział ze nie ma czasu
Piotr Głąb — 29.01.2023 22: 22
Luz
Patryk W — 29.01.2023 22: 22
ale oni uzywaja tez DateTime u siebie
i ze masz dostep do jego
repo
i mamy se zobaczyc
Piotr Głąb — 29.01.2023 22: 22
No mam, w razie czego sb poszukam
Patryk W — 29.01.2023 22: 22
ja bede niedlugo
Piotr Głąb — 30.01.2023 00: 27
    - Skończyć widok Event managera, tj.uwzględnienie meczy i statystyk przynajmniej na etapie tworzenie eventu jako całości, chociaż dobrze byłoby zrobić też opcję z dodawaniem np.statystyk dla pojedynczych meczy, gdy event już istnieje
Piotr Głąb — 30.01.2023 01: 32
https://www.pragimtech.com/blog/blazor/blazor-select-bind-database-data/
Binding select element with database data in Blazor
Binding select element with database data in Blazor
Piotr Głąb — 30.01.2023 01: 46
https://www.learnrazorpages.com/razor-pages/forms/select-lists
Select Lists in a Razor Pages Form | Learn Razor Pages
Select lists or DropDown lists are used in a Razor Pages form to enable users to select zero or more predefined options.They are rendered in HTML as a select element
Obraz
Patryk W — 30.01.2023 16: 55
Obraz
Piotr Głąb — 30.01.2023 23: 43
https://github.com/Piotr-Glab495/SportEventManager/pull/22
Piotr Głąb — Dziś o 20: 46
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
... (Pozostałe wiersze: 73)
    Zwiń
    message.txt
    7 KB

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
    message.txt
    7 KB