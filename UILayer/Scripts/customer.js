$("#txtQuantity").kendoNumericTextBox();

    function viewDetails(element)
    {
        var imgid = $(element).attr('data-id');
        $.ajax({
            url: "http://localhost:56891/Customer/PhotoDetail?id=" + imgid,
            type: "Get",
            dataType: 'json',
            success: function (result) {
                var mywindow = $("#window");
                var template = kendo.template($("#imagedetails").html());
                mywindow.kendoWindow({
                    width: "670px",
                    height: "400px",
                    tittle: "Image Details",
                    model: true,
                    content: result,
                    visible: false,
                    actions: ["close"],
                    close: onclose,
                }).data("kendoWindow").content(template(result)).center().open();
            },
            error: function (exception) {
                alert("Fail");
            }
        });
    }

    $("#txtQuantity").change(function () {
        var quantity = $("txtQuantity").val();
        var price = $("#hdnprice").val();

    });
    function onclose() {
        var myWindow = $("#window").data("kendoWindow");
        myWindow.bind("close");
    };

    function addCard() {
        var obj = {
            Quantity: parseInt($("#txtQuantity").val()),
            photo_id: parseInt($("#hdnalbid").val()),
        };
        $.ajax({
            url: "/Customer/AddCart",
            type: "POST",
            data: JSON.stringify(obj),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                window.location.replace("/Customer/CartData");

            }, error: function (errormessage) {
                window.location.replace("/Customer/CartData");
            }
        });
    }
    function getTotal() {
        var quantity = parseInt($("#txtQuantity").val());
        var price = parseInt($("#hdnprice").val());
        $("#lblTotalAmount").html(quantity * price);
    }

    //function myPrice(id) {
    //    var x = document.getElementById(id).value;
    //    var y = parseInt(x);
    //    var obj = {
    //        photo_id: id,
    //        cart_itemquantity: y,
    //    }
    //    $.ajax({
    //        url: "/Customer/UpdatePrise",
    //        type: "POST",
    //        data: JSON.stringify(obj),
    //        dataType: 'json',
    //        contentType: "application/json; charset=utf-8",
    //        success: function (data) {
    //            window.location.replace("/Customer/Index");

    //        }, error: function (errormessage) {
    //            window.location.replace("/Customer/Index");
    //        }
    //    });
        //$.post("/Customer/UpdatePrise",
        //      { photo_id: id, cart_itemquantity: y },
        //      function (response) {
        //          window.location.href = "/Customer/Index";
        //      });
    //}