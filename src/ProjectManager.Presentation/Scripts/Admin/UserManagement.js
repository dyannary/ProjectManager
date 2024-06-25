function switchEnableUserFun(id) {
    debugger;
    $.ajax({
        url: '../Admin/UpdateUserEnableStatus',
        type: 'POST',
        data: JSON.stringify({ id: id }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            debugger
            if (result.StatusCode === 204) {

                var row = $('#UserDatabase').DataTable().row('#' + id);
              
                row.invalidate();
              
                row.draw('full-hold');
            } else {
                alert("A problem occured!");
            }
        },
    });
}

function reloadData() {
    
    var table = $('#UserDatabase').DataTable();
    table.ajax.reload();
}