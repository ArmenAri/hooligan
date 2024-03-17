function dropHandler(event) {
    //event.preventDefault();

    //var droppedItem = event.dataTransfer.getData("text");
    console.log("Element dropped: ", droppedItem);
}

window.setupDropHandler = function (element) {
    element.addEventListener("drop", function (event) {
        event.preventDefault();
        var droppedItem = event.dataTransfer.getData("text");
        console.log("Element dropped:", droppedItem);
        // Effectuez d'autres actions en fonction de l'élément déposé
    });
};