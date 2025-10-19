(function () {
    // Keep everything scoped; expose only confirmDelete on window
    $(function () {
        var $table = $('#productsTable');
        if (!$table.length) return;

        var ajaxUrl = $table.data('ajax-url') || '';
        var deleteUrl = $table.data('delete-url') || '';

        function getAntiForgeryToken() {
            return $('#antiforgeryForm input[name="__RequestVerificationToken"]').val();
        }

        var table = $table.DataTable({
            ajax: ajaxUrl,
            columns: [
                { data: 'name' },
                { data: 'price' },
                { data: 'color' },
                { data: 'category' },
                {
                    data: 'id',
                    orderable: false,
                    render: function (id) {
                        return '<a class="btn btn-sm btn-outline-secondary me-1" href="/Products/Edit/' + id + '" title="Edit"><i class="bi bi-pencil-square" aria-hidden="true"></i></a>' +
                            '<a class="btn btn-sm btn-outline-info me-1" href="/Products/Details/' + id + '" title="Details"><i class="bi bi-info-circle" aria-hidden="true"></i></a>' +
                            '<button type="button" class="btn btn-sm btn-outline-danger" data-delete-id="' + id + '" title="Delete"><i class="bi bi-trash" aria-hidden="true"></i></button>';
                    }
                }
            ],
            paging: true,
            searching: true,
            ordering: true,
            responsive: true,
            order: [[0, 'asc']],
            language: {
                decimal: ",",
                thousands: ".",
                info: "Gösterilen _START_ - _END_ / _TOTAL_ kayıt",
                infoEmpty: "Gösterilen 0 - 0 / 0 kayıt",
                lengthMenu: "Sayfa başına _MENU_ kayıt",
                search: "Ara:",
                paginate: {
                    first: "İlk",
                    previous: "Önceki",
                    next: "Sonraki",
                    last: "Son"
                },
                zeroRecords: "Eşleşen kayıt bulunamadı",
                loadingRecords: "Yükleniyor...",
                processing: "İşleniyor..."
            }
        });

        // event delegation for delete buttons
        $table.on('click', 'button[data-delete-id]', function () {
            var id = $(this).data('delete-id');
            confirmDelete(id);
        });

        // expose function for compatibility if needed
        window.confirmDelete = function (id) {
            Swal.fire({
                title: 'Delete product',
                text: 'This action cannot be undone. Do you want to continue?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, delete it',
                cancelButtonText: 'Cancel',
                focusCancel: true,
                reverseButtons: true
            }).then(function (result) {
                if (!result.isConfirmed) return;

                var token = getAntiForgeryToken();

                $.ajax({
                    url: deleteUrl,
                    type: 'POST',
                    data: { id: id, __RequestVerificationToken: token },
                    success: function (res) {
                        if (res && res.success) {
                            Swal.fire({
                                title: 'Deleted!',
                                text: 'Product has been deleted.',
                                icon: 'success',
                                timer: 1500,
                                showConfirmButton: false
                            });
                            table.ajax.reload(null, false);
                        } else {
                            Swal.fire('Error', res && res.message ? res.message : 'Delete failed.', 'error');
                        }
                    },
                    error: function () {
                        Swal.fire('Error', 'An error occurred while deleting the product.', 'error');
                    }
                });
            });
        };
    });
})();