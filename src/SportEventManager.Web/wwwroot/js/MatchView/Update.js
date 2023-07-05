function resetDetails() {
   
    const teamElements = document.querySelectorAll(".nr-input");
   
    teamElements.forEach((element) => {
        element.value = "0";
    });

    const playersElements = document.querySelectorAll(".number-input");

    playersElements.forEach((element) => {
        element.value = "0";
    });
}
function backToIndex(id) {
    window.location.href = `/MatchView/Index/${id}`;
}
