function switchEnableFun(id) {
    $.ajax({
        url: '../Project/UpdateEnableStatus',
        type: 'POST',
        data: JSON.stringify({ id: id }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            debugger
            if (result !== null) {
                $("#ProjectCards").empty();
                $("#ProjectCards").html(result);
            } else {
                alert("A problem occured!");
            }
        },
    });
}

function handleSuccesUpdateProject(result) {
    if (result !== null) {
        $("#ProjectCards").empty();
        $("#ProjectCards").html(result);
    } else {
        alert("A problem occured!");
    }
}