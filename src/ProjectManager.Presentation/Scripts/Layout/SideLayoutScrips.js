function updateNotificationCount(data) {
    if (data > 9)
        data = "9+"
    $('#notificationCount').text(data);
}

function updateNotificationCountForStart() {
    $.ajax({
        url: '../Notification/GetNotificationCount',
        type: 'GET',
        success: function (data) {
            updateNotificationCount(data)
        },
        error: function (error) {
            console.log("Error fetching notification count: ", error);
        }
    });
}

function getUserPhoto() {
    $.ajax({
        url: '../Account/GetUserPhoto',
        type: 'GET',
        success: function (data) {
            $('#UserAvatar').attr('src', data);
        },
        error: function (error) {
            console.log("Error fetching photo for user");
        }
    });
}

function getUserUsername() {
    $.ajax({
        url: '../Account/GetUserUsername',
        type: 'GET',
        success: function (data) {
            $('#username').text(data)
        },
        error: function (error) {
            console.log("Error fetching username for user");
        }
    });
}

function HandleUpdateUser(response) {
    if (response.success) {
        getUserPhoto();
        getUserUsername();
        $('#modal').modal('hide');
    }
    else if (response.errors !== null) {
        $('span[data-valmsg-for]').text('');

        for (var key in response.errors) {
            var messages = response.errors[key];
            var errorElement = $('span[data-valmsg-for="' + key + '"]');
            errorElement.text(messages);
        }
    }
}
