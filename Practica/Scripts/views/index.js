$(document).ready(function () {

    $("#btn-save").click(function () {
        var form = $("#form-calculate");
        if (form.valid()) {
            calculate();
        }
    });

    $("#btn-clear").click(function () {
        $("#form-calculate").trigger("reset");
        $("#form-calculate").data('validator').resetForm();
        $("#change-container").empty();
    });

    init();
});

function calculate() {
    var model = {
        itemprice: parseFloat($("input[name=itemPrice]").val()),
        payment: parseFloat($("input[name=payment]").val())
    }

    var claseGeneral = {};
    claseGeneral.json = JSON.stringify(model);

    $.ajax({
        url: "/Home/calculateChange",
        type: "POST",
        data: JSON.stringify(claseGeneral),
        contentType: "application/json",
        success: function (data) {
            var response = JSON.parse(data);
            $.notify("The change has been calculated", "success");
            $("#change-container").empty();
            $("#change-container").append("<div class='row'><label>Change: </label> " + response.change + "</div>");
            response.listChangeDenominations.forEach(function(element){
                $("#change-container").append("<div class='row'><label>Currency Denomination: </label> " + element.currencyDenomination + " <label class='ml-1'>Quantity: </label> " + element.quantity+"</div>");
                if (element.isRemaining) {
                    $("#change-container").append("<label>Remaining: </label>" + element.currencyDenomination);
                }
            });
        }, error: function (XMLHttpRequest) {
            $.notify(XMLHttpRequest.responseText, "error");
        }
    });
}
function init() {
    $("#form-calculate").validate({
        rules: {
            itemPrice: {
                required: true,
                number: true
            },
            payment: {
                required: true,
                number: true
            }
        },
        messages: {
            itemPrice: {
                required: "Please enter the price of the item"
            },
            payment: {
                required: "Please enter the payment"
            }
        }
    });
}