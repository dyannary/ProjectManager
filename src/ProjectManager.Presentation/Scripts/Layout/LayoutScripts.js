function openNav() {
    document.getElementById("mySidenav").style.width = "300px";
    document.getElementById("main").style.marginLeft = "250px";
    document.getElementById("openButton").style.visibility = "hidden";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
    document.getElementById("openButton").style.visibility = "visible";
}

function toggleSubMenu(submenu) {
    var subMenuElement = document.getElementById(submenu + "Submenu");
    if (subMenuElement.style.display === "none") {
        subMenuElement.style.display = "block";
    } else {
        subMenuElement.style.display = "none";
    }
}