function GetNotification() {
    var formData = {
        NotificationType: $('#NotificationType').val() || ''
    }

    $.ajax({
        url: '../Notification/GetList',
        type: 'GET',
        data: formData,
        success: function (result) {
            if (result !== null) {
                debugger;
                $("#NotificationList").empty();
                $("#NotificationList").html(result);
            } else {
                alert('ERROR');
            }
        },
        error: function () {
            alert("Internal error");
        }
    });
    
}

function RemoveSingleNotificationHandler(id) {
    $.ajax({
        url: '../Notification/RemoveSingleNotification',
        type: 'POST',
        data: {id : id},
        success: function (success) {
            if (success === true) {
                GetNotification();
            } else {
                alert(`Notification Couldn't be removed`);
            }
        },
        error: function () {
            alert("Internal error");
        }
    });
}