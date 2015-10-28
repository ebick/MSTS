window.onload = function() {

    document.getElementById('InLineDelete').onclick = confirmTheDelete;
}

function confirmTheDelete() {

    var retValue = false;
    if (confirm("Are you sure you want to delete ")) {
        //alert("Clicked Ok");
        retValue = true;
    } else {
        //alert("Clicked Cancel");
        retValue = false;
    } 
    return retValue;
}