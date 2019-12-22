$(document).ready(function () {
    debugger;
    $('#articleDT').DataTable({
        "processing": true,
        "serverSide": true,
        "columns": [
            //{ "name": "id" },
            { "name": "title" }
        ],
        columnDefs: [
            //{
            //    "targets": 0,
            //    "data": "id",
            //},
            //{
            //    // This works fine, but I want col1 to be the 'empty' col ...
            //    "targets": 1,
            //    "data": null,
            //    "defaultContent": 'def. content...',
            //},

            //{
            //    "targets": 0,
            //    "data": "id"
            //},
            {
                "targets": 0,
                "data": "title",
                "className": "article-grid",
                "render": function (data, type, row, meta) {
                    return '<a href="/' + row.url + '"> ' + data + ' </a>';
                }
            }
        ],
        "ajax": {
            "url": "/Article/JDataTable",
            "type": "Post"
        }
    });
});