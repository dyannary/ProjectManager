function reloadTaskData() {
    debugger;
    var table = $('#TasksTable2').DataTable();
    table.ajax.reload();
}

function deleteTask(id) {
    $.ajax({
        url: 'ProjectTask/DeleteTasks',
        type: 'DELETE',
        data: JSON.stringify({ id: id }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.StatusCode === 204) {

                var row = $('#TasksTable2').DataTable().row('#' + id);

                row.invalidate();

                row.draw('full-hold');
            } else {

            }
        },
    });
}

function handleCreateUpdateTask(response) {
    if (response.errors) {
        if (response.message !== null)
            toastr.error(response.message);
        $('span[data-valmsg-for]').text('');

        for (var key in response.errors) {
            var messages = response.errors[key];
            var errorElement = $('span[data-valmsg-for="' + key + '"]');
            errorElement.text(messages);
        }
       
    } else {
        if (response.message !== null)
            toastr.success(response.message);
        $('#modal').modal('hide');
        reloadTaskData();
    }
}

function preview() {
    let fileInput = document.getElementById("file-input");
    let imageContainer = document.getElementById("images");
    var maxSize = 5 * 1024 * 1024;

    for (let i of fileInput.files) {
        if (i.size > maxSize) {
            document.getElementById('file-size-error').style.display = 'block';
            fileInput.value = '';
            return;
        } else {
            document.getElementById('file-size-error').style.display = 'none';
        }

        let reader = new FileReader();
        let figure = document.createElement("figure");
        let figCap = document.createElement("figcaption");
        let deleteBtn = document.createElement("button");

        figCap.innerText = i.name;
        figure.appendChild(figCap);

        reader.onload = () => {
            let img = document.createElement("img");
            img.setAttribute("src", reader.result);
            figure.insertBefore(img, figCap);

            deleteBtn.textContent = "x";
            deleteBtn.classList.add("delete-btn");
            deleteBtn.addEventListener("click", () => deleteImage(figure));
            figure.appendChild(deleteBtn);

            figure.addEventListener("dblclick", () => openImageModal(img));
        };

        reader.readAsDataURL(i);

        imageContainer.appendChild(figure);
    }
}

function deleteImage(figureElement) {
    figureElement.remove(); 
}

function openImageModal(imageElement) {
    let modal = document.getElementById("imageModal");
    let modalImg = document.getElementById("modalImage");

    modal.style.display = "block";

    //if it is details page then without src, if add image, it needs src
    modalImg.src = imageElement.src;
    
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}

function openImageModalForDetails(imageElement) {
    let modal = document.getElementById("imageModal");
    let modalImg = document.getElementById("modalImage");

    modal.style.display = "block";
    
    modalImg.src = imageElement;

    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}