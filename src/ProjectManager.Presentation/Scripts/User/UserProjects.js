﻿function switchEnableFun(id) {
    $.ajax({
        url: '../Project/UpdateEnableStatus',
        type: 'POST',
        data: JSON.stringify({ id: id }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.StatusCode === 204) {
                if (result.message !== null)
                    toastr.success(result.message);
                GetProjectCards();
            } else {
                if (result.message !== null)
                    toastr.error(result.message);
                alert("A problem occured!");
            }
        },
    });
}

function handleCreateUpdateProject(response) {
    if (response.success) {
         if (response.message !== null)
            toastr.success(response.message);

        $('#modal').modal('hide');
        GetProjectCards(1);
    } else {
        debugger;
        if (response.message !== null || response.message !== "")
            toastr.error(response.message);

        $('span[data-valmsg-for]').text('');

        for (var key in response.errors) {
            var messages = response.errors[key];
            var errorElement = $('span[data-valmsg-for="' + key + '"]');
            errorElement.text(messages);
        }
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
    $.ajax({
        url: '../Project/GetByFilters',
        type: 'GET',
        data: formData,
        success: function (result) {
            if (result !== null) {
                $("#ProjectCards").empty();
                $("#ProjectCards").html(result);
            } else {
                alert('ERROR');
            }
        },
        error: function () {
            alert("Internal error");
        }
    });
}

function GetCollaborators(page, projectId) {
    var search = $('#searchProject').val() || '';
    page = page || 1

    debugger;
    $.ajax({
        url: '../ProjectCollaborators/GetByFilters',
        type: 'GET',
        data: { projectId: projectId, search: search, page: page },
        success: function (result) {
            if (result !== null) {
                $("#CollaboratorsTable").empty();
                $("#CollaboratorsTable").html(result);
            } else {
                alert('ERROR');
            }
        },
        error: function () {
            alert('ERROR X2')
        }
    });

}

function HandleCreateUpdateCollaborator(response) {
    if (response.success) {
        if (response.message !== null)
            toastr.success(response.message);
        $('#modal').modal('hide');
        GetCollaborators(1, response.projectId);
    } else {
        if (response.message !== null)
            toastr.error(response.message);
        $('span[data-valmsg-for]').text('');

        for (var key in response.errors) {
            var messages = response.errors[key];
            var errorElement = $('span[data-valmsg-for="' + key + '"]');
            errorElement.text(messages);
        }
    }
}
 function HandleEditOrChangePasswordUser(response) {
     if (response.success) {
         if (response.message !== null)
             toastr.success(response.message);
        $('#modal').modal('hide');
    }
     else if (response.errors !== null) {
         if (response.message !== null)
             toastr.error(response.message);
        $('span[data-valmsg-for]').text('');

    for (var key in response.errors) {
                    var messages = response.errors[key];
    var errorElement = $('span[data-valmsg-for="' + key + '"]');
    errorElement.text(messages);
                }
            }
}