/*jshint esversion: 6 */
const uri = "/api/manufacturers/";
let items = null;

function loadManufs() {
    var i, j, x = "";
    var request = new XMLHttpRequest();
    request.open("GET", uri, false);
    request.onload = function () {
        items = JSON.parse(request.responseText);
        for (i in items) {
            x += "<hr>";
            x += "<h4> Производитель: " + items[i].name + " : <a href='" + items[i].manufId + "'>" + items[i].manufId + "</a></h4>";
            x += "<button type='button' class='btn btn-sm btn-outline-secondary' onclick='deleteManufs(" + items[i].manufId + ");'>Удалить</button>";

            for (j in items[i].сars) { //не реализовано (я слишком тугодум)
                x += "<div class='col-10'>";
                x += "<h4>" + items[i].сars[j].ModelName + "</h4>";
                x += "<p>" + items[i].сars[j].YearOfProd + "</p><br>";
                x += "</div>";
            }

        }
        document.getElementById("manufsDiv").innerHTML = x;
    };
    request.send();
}

function deleteManufs(id) {
    var request = new XMLHttpRequest();
    var url = uri + id;
    request.open("DELETE", url, false);
    request.onload = function () {
        loadManufs();
    };
    request.send();
}

function createManufs() {
    var nameText = document.getElementById("createDiv").value;
    var request = new XMLHttpRequest();
    request.open("POST", uri);
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.onload = function () {
        loadManufs();
    };
    request.send(JSON.stringify({ name: nameText }));
}