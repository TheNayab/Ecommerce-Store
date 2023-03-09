function Sub() {
    let value = document.getElementById("where").value;
    if (value > 1 ) {
        document.getElementById("where").value = document.getElementById("where").value- 1;
        console.log(document.getElementById("where").value);
        document.getElementById("plus").style.backgroundColor = 'Tomato'; 
        document.getElementById("plus").style.color = 'White';
        document.getElementById("plus").style.border = "0px solid White";
    }
    else {  
        document.getElementById("minus").style.backgroundColor = 'White';
        document.getElementById("minus").style.color = 'Black';
        document.getElementById("minus").style.border = "1px solid Black";

    }
    document.getElementById("multi").value = parseInt(document.getElementById("price").value) * parseInt(document.getElementById("where").value);
}
function Add() {

    let value = document.getElementById("qtyshow").value;
    let value2 = document.getElementById("where").value;
    if (value2!=value && value>0) {
        document.getElementById("where").value = parseInt(document.getElementById("where").value) + 1;
        console.log(document.getElementById("where").value);
        document.getElementById("minus").style.backgroundColor = 'Tomato';
        document.getElementById("minus").style.color = 'White';
        document.getElementById("minus").style.border = "0px solid White";
        
    }
    if (value == 0) {
        document.getElementById("where").value = 0;
    }
    else {
        document.getElementById("plus").style.backgroundColor = 'White';
        document.getElementById("plus").style.color = 'Black';
        document.getElementById("plus").style.border = "1px solid Black";
        
        
        //  garbarha abi edr buhat zyada ////////////////....................
    }
    document.getElementById("multi").value = parseInt(document.getElementById("price").value) * parseInt(document.getElementById("where").value);
}

function Changedd() {

    document.getElementById("yourinstall").value = document.getElementById("takeinstall").value;
    if (document.getElementById("takeinstall").value == " " || document.getElementById("takeinstall").value == "" || document.getElementById("takeinstall").value == "   ") {
        document.getElementById("totpr").value = document.getElementById("refre").value;
    }
    else {
        document.getElementById("totpr").value = parseInt(document.getElementById("totpr").value / document.getElementById("yourinstall").value) + 1;
    }

}
function Rep() {
    if (document.getElementById("insreply").value == "Canceled") {
        document.getElementById("insreply").value = "Reply Soon";
    }
    else {
        document.getElementById("insreply").value = document.getElementById("insreply").value;
    }
    
}