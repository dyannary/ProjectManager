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
            if (success) {
                toastr.success("Notification was removed")
                GetNotification();
            } else {
                toastr.success("A problem on the server occured. Try Again")
            }
        },
        error: function () {
            toastr.success("A problem on the server occured. Try Again")
        }
    });
}

function RemoveMultipleNotificationsHandler() {
    $.ajax({
        url: '../Notification/RemoveMultipleNotifications',
        type: 'POST',
        success: function (success) {
            if (success) {
                GetNotification();
                toastr.success("All notifications were removed")
            } else {
                toastr.success("A problem on the server occured. Try Again")
            }
        },
        error: function () {
            toastr.success("A problem on the server occured. Try Again")
        }
    });
}