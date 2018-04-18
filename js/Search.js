$(function () {
    $("#txtsearch").autocomplete({
        source: function (request, response) {

            var obj = {
                Prefix: request.term
            }

            $.ajax({
                type: "POST",
                url: "../../WebService2.asmx/GetCustomers1",
                data: JSON.stringify({ "objserc": obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    console.log(data.d)
                    if (data.d.length == 0) {
                        //var html = $("<center><h4 style='color:red;'>No Data Found</h4><center>");
                        //$("#getResutrants").append(html);
                        $("#serachdata").empty().append("<center><h6 style='color:red;'>No Data Found</h6></center>");

                    } else {
                        $("#serachdata").empty();
                        response($.map(data.d, function (item) {

                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }

                        }))

                    }
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#get").val(i.item.val);

            //window.location.pathname = '/contentPage/sample.aspx?id=' + i.item.val;
            window.location.href = 'Coursedetails.aspx?id=' + i.item.val + '';
            // console.log(i.item.val);
        },
        minLength: 1
    });

});