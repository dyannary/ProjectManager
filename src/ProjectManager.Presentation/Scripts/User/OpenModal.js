function openModal(parameters) {
    const id = parameters.data;
    const url = parameters.url;
    const modal = $('#modal');

    if (!parameters.hasOwnProperty('data') || !parameters.hasOwnProperty('url')) {
        alert('Error: Missing required parameters "id" and "url".');
        return;
    }

    const data = { id: id };
    if (parameters.hasOwnProperty('additionalData')) {
        Object.assign(data, parameters.additionalData); 
    }

    $.ajax(
        {
            type: 'GET',
            url: url,
            data: data,
            success: function (response) {
                $('.modal-dialog');
                modal.find(".modal-content").html(response);
                modal.modal('show')
            },
            failure: function () {
                modal.modal('hide')
            },
            error: function (response) {
                alert(response.responseText)
            }
        });
};

function openModalNoParam() {
    const modal = $('#modal');

   $('.modal-dialog');
   modal.find(".modal-content").html(response);
   modal.modal('show')
};

function reloadUserData() {
    var table = $('#UserDatabase').DataTable();
    table.ajax.reload();
}

function handleCreateUpdateUser(response) {
    if (response.errors) {
        $('span[data-valmsg-for]').text('');

        $('.is-invalid').removeClass('is-invalid');

        for (var key in response.errors) {
            var messages = response.errors[key];
            var errorElement = $('span[data-valmsg-for="' + key + '"]');
            errorElement.text(messages);

            var inputElement = $('[name="' + key + '"]');
            inputElement.addClass('is-invalid');
        }

    } else {
        if (response.message !== null) {
            toastr.success(response.message);
        }
        $('#modal').modal('hide');
        reloadUserData();
    }
}
