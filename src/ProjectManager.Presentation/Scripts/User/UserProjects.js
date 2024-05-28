function switchEnableFun(id) {
    $.ajax({
        url: '../Project/UpdateEnableStatus',
        type: 'POST',
        data: JSON.stringify({ id: id }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            debugger
            if (result.StatusCode === 204) {
                GetProjectCards();
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

function GetProjectCards(page) {
    page = page || 1
    var formData = {
        ProjectType: $('#ProjectType').val() || '',
        ProjectStatus: $('#ProjectStatus').val() || '',
        ProjectEnable: $('#ProjectEnable').val() || '',
        searchProject: $('#searchProject').val() || '',
        SortBy: $('#SortBy').val() || '',
        SortOrd: $('#SortOrd').val() || '',
        page: page
    };
    debugger;
    $.ajax({
        url: '../Project/GetByFilters',
        type: 'GET',
        data: formData,
        success: function (result) {
            if (result !== null) {
                $("#ProjectCards").empty();
                $("#ProjectCards").html(result);
            } else {
                debugger;
                alert('ERROR');
            }
        },
        error: function () {
            alert('ERROR X2')
        }
    });
}