// IIFE to ensure independent scope and avoid conflicts
(function () {
  var ajaxUrl = $('#productsTable').data('ajax-url');
  if (!ajaxUrl) {
    console.error('products.js: missing data-ajax-url on #productsTable');
  }

  // inside your DataTable init (or replace existing init)
  $('#productsTable').DataTable({
    ajax: {
      url: ajaxUrl,
      dataSrc: 'data',
      error: function (xhr, error, thrown) {
        console.error('DataTables Ajax error', { status: xhr.status, response: xhr.responseText, error: error, thrown: thrown });
        // optional: show a friendly message
        Swal.fire('Error', 'Could not load products. See console/network tab for details.', 'error');
      }
    },
    columns: [
      { data: 'name' },
      { data: 'price' },
      { data: 'color' },
      { data: 'category' },
      { data: 'id', orderable: false, render: /*...*/ }
    ]
  });
})();