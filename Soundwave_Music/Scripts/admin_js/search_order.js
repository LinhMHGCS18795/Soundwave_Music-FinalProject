$(document).ready(function () {
    $("#Order").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Order/SuggestOrderSearch",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Order_id, value: item.Order_id };
                    }))
                }
            })
        },
        minLength: 0,
        messages: {
            noResults: "No song founded.",
            results: function (count) {
                return count + (count > 1 ? ' results' : ' result ') + ' found';
            }
        }, focus: function (event, ui) {
            $("#Order").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#Order").val(ui.item.label);
            return false;
        }
    })

})