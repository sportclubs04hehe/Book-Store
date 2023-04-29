var dataTable;

$(document).ready(() => {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        "ajax": { url: '/Admin/product/GetAll' },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "15%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'price', "width": "10%" },
            { data: 'price50', "width": "10%" },
            { data: 'price100', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                   return `<div class="w-75 btn-group " role="group">
                        <a class="btn btn-dark" href="/admin/product/upsert?id=${data}">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a class="btn btn-danger" onClick=Delete('/Admin/Product/Delete/${data}')>
                            <i class="bi bi-trash"></i> Delete
                        </a>
                    </div>
                    `
                },
                "width": "15%"
            },
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}

