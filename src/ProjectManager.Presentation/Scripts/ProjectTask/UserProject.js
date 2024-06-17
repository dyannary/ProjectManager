function reloadTaskData() {

    var table = $('#TasksTable2').DataTable();
    table.ajax.reload();
}

function deleteTask(id) {
    debugger;
    $.ajax({
        url: 'ProjectTask/DeleteTasks',
        type: 'DELETE',
        data: JSON.stringify({ id: id }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            debugger
            if (result.StatusCode === 204) {

                var row = $('#TasksTable2').DataTable().row('#' + id);

                row.invalidate();

                row.draw('full-hold');
            } else {
                alert("A problem occured!");
            }
        },
    });
}