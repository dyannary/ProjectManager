$(function () {

    function updateNotificationCount(data) {
        $('#notificationCount').text(data);
    }

    function updateNotificationCountForStart() {
        $.ajax({
            url: '../Notification/GetNotificationCount',
            type: 'GET',
            success: function (data) {
                $('#notificationCount').text(data);
            },
            error: function (error) {
                console.log("Error fetching notification count: ", error);
            }
        });
    }

    function getUserPhoto() {
        $.ajax({
            url: '../Admin/GetUserPhoto',
            type: 'GET',
            success: function (data) {
                $('#UserAvatar').attr('src', data);
            },
            error: function (error) {
                console.log("Error fetching photo for user: ", error);
            }
        });
    }

    getUserPhoto();

    var connection = $.hubConnection('/signalr');
    var hubProxy = connection.createHubProxy('notificationHub');

    hubProxy.on('updateNotificationCount', function (data) {
        updateNotificationCount(data)
    });

    connection.start().done(function () {
        console.log('SignalR connected');
        updateNotificationCountForStart();
    });

});
