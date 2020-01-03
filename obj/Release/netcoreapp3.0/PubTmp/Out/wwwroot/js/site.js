// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your Javascript code.

function update()
{
    //Get data
    var id = document.getElementById("uid").value;
    var key = document.getElementById("key").value;
    var myValue = document.getElementById("value").value;


    //Create a form object to send data
    var form = new FormData();
    form.append("id", id);
    form.append("key", key);
    form.append("value", myValue);
    //Create a request object and send the data
    var req = new XMLHttpRequest();
    req.open("POST", "https://localhost:44318/totems/update"); //Needs to be updated when deploying to a live server

    req.send(form);
    $("#mymodal").modal("hide");
    clearUpdate();
    req.onreadystatechange = function () //Check on ready state change
    {
        if (req.readyState == 3)
        {
            alert(req.responseText);
        }
    }
}

function add() {
    //Get data
    var id = document.getElementById("myid").value;
    var name = document.getElementById("name").value;
    var origin = document.getElementById("origin").value;
    var description = document.getElementById("description").value;
    var image = document.getElementById("image").value;
    var body = document.getElementById("body").value;
    

    //Create a form object to send data
    var form = new FormData();
    form.append("id", id);
    form.append("name", name);
    form.append("origin", origin);
    form.append("description", description);
    form.append("image", image);
    form.append("body", body);
    //Create a request object and send the data
    var req = new XMLHttpRequest();
    req.open("POST", "https://localhost:44318/totems/add");

    req.send(form);
    $("#addModal").modal("hide");
    clearAdd();
    req.onreadystatechange = function () {
        if (req.readyState == 3) {
           
            alert(req.responseText); 
        }
    }

}
//May need improvement
function clearUpdate() {
    document.getElementById("uid").value = "";
    document.getElementById("key").valu = "";
    document.getElementById("value").value = "";
}

function clearAdd() {
    document.getElementById("myid").value ="";
    document.getElementById("name").value = "";
    document.getElementById("origin").value = "";
    document.getElementById("description").value = "";
    document.getElementById("image").value = "";
    document.getElementById("body").value = "";
}