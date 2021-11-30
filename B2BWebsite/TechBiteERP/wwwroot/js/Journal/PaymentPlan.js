function getItemDetail() {
    var PlotId = $('#PlotId').val();

    //Get block 
    $.ajax({
        url: '/RealEstate/Sale/getItemDetail?id=' + PlotId,
        type: 'GET'
    }).done(function (data) {
        debugger;
        $('#BlockId').val(data.blockID);
    });

    // get  Plot Price
    var blockId = $('#BlockId').val();
 
        $.ajax({
            // url: '/RealEstate/PaymentHistory/getRegisterationDetail?PlotId=' + PlotId + '&blockId=' + blockId,
            url: '/RealEstate/PaymentHistory/getRegisterationDetail?PlotId=' + PlotId,
            type: 'GET'
        }).done(function (data) {
            debugger;
            if (data != undefined) {
                $('#PlotPrice').val(data.plotPrice);
                $('#PayableAmount').val(data.plotPrice);
                $('#Balance').val(data.plotPrice);
                $('#NoOfInstallment').val(data.installments);

                calculateInstallments();
            }
            else {

                $('#PlotPrice').val(0);
                $('#PayableAmount').val(0);
                $('#Balance').val(0);
                $('#NoOfInstallment').val(0);
                calculateInstallments();
            }

        });
    
    
}

function calculateInstallments2() {
    debugger;
    var payableAmount = 0.00;
    var noOfInstallments = parseInt($('#NoOfInstallment').val());
    var BubbleAmount = Number($('#BubbleAmount').val());
    if (BubbleAmount != 0) {
        payableAmount = Number(Number($('#Balance').val()) - BubbleAmount);
        if (noOfInstallments != 0) {
            var installmentamount = Number(payableAmount / noOfInstallments).toFixed(2);
            $('#InstallmentAmount').val(installmentamount);
            $('#Balance').val(payableAmount);
        }
    }
    else {
        payableAmount = Number(Number($('#Balance').val()) - BubbleAmount);
        if (noOfInstallments != 0) {
            var installmentamount = Number(payableAmount / noOfInstallments).toFixed(2);
            $('#InstallmentAmount').val(installmentamount);
        }
    }
    //  $('#Balance').val(payableAmount);
    calculateQuartly();
}

function calculateQuartly() {
    var installmentAmount = Number($('#InstallmentAmount').val());
    if ($('#IsQuartleyInstall').is(":checked")) {
        var QuarterAmount = installmentAmount * 3
        $('#QuartleyInstall').val(QuarterAmount);
    }
    else {
        $('#QuartleyInstall').val(0);
    }
}

function calculateInstallments() {
    debugger;
    var noOfInstallments = parseInt($('#NoOfInstallment').val());
    var BubbleAmount = Number($('#BubbleAmount').val());
    var Booking = Number($('#Booking').val());
    var Confirmation = Number($('#Confirmation').val());
    var Allotment = Number($('#Allotment').val());
    var Possesion = Number($('#Possesion').val());
    var PayableAmount = Number($('#PayableAmount').val());
    var TotalPayable = PayableAmount - Booking - Confirmation - Allotment - Possesion - BubbleAmount;
    if (noOfInstallments != 0) {
        var installmentamount = Number(TotalPayable / noOfInstallments).toFixed(0);
        $('#InstallmentAmount').val(installmentamount);
    }
    $('#Balance').val(TotalPayable);

   // calculateQuartly();
}

function validate() {
    debugger;
    var rowCount = $("#tblpaymentPlan tr").length;
    var totalAmount = 0.0000;

    if (rowCount <= 1) {

        swal("", "Must be Enter Amount.", "error");
    }
    $("#tblpaymentPlan tr").each(function () {
        $(this).find('td:eq(5)').each(function () {
            totalAmount += parseFloat($(this).text());
        });
        /*$('#BubbleAmount').val(totalAmount);*/
    });
    $("#tblpaymentPlan tfoot tr").remove();
    var row = "<tr>" +
        "<th></th>" +
        "<th></th>" +
        "<th class='text-right'> Total :</th>" +
        "<th class='text-right'>" + totalAmount.toFixed(2) + "</th>" +
        "</tr>";
    $("#tblpaymentPlan tfoot").append(row);
    //update row number
    $('#tblpaymentPlan tbody tr').each(function (idx) {
        // $(this).children("td:eq(0)").html(idx + 1);
        $(this).children("td:eq(1)").html(idx + 1);
    });

    if (Number(totalAmount) > 0) {
        calculateInstallments();
    }
}

function clearScreen() {

    $('#JournalDetID').val('');
    $('#Sr').val('');
    $('#DueDate').val('');
    $('#BubbleDuration').val('');
    $('#Amount').val('');
}

function getNewDate(type, iteration, i) {
   // debugger;
    var today = new Date(Date.parse($('#BookingDate').val(), "MM/dd/yyyy"));
    //var startdate = '26-12-2020'
    //var newDate = moment(startdate, "DD-MM-YYYY").add(3, 'months').format('DD/MM/YYYY');
    var dd = Number(String(today.getDate()).padStart(2, '0'));
    var mm = Number(String(today.getMonth() + 1).padStart(2, '0')); //January is 0!
    var yyyy = Number(today.getFullYear());
    if (type == "Quarterly") {
        mm = mm + 3 + iteration
        if (mm > 12) {
            yyyy = yyyy + 1;
            mm = mm - 12;
            if (mm > 12) {
                yyyy = yyyy + 1;
                mm = mm - 12;
            }
        }

    } else if (type == "After 4 Month") {
        mm = mm + 4 + iteration
        if (mm > 12) {
            yyyy = yyyy + 1;
            mm = mm - 12;
            if (mm > 12) {
                yyyy = yyyy + 1;
                mm = mm - 12;
            }
        }
    }
    else if (type == "Half Yearly") {
        mm = mm + 6 + iteration
        do {
            yyyy = yyyy + 1;
            mm = mm - 12;
        } while (mm > 12)

    }
    else if (type == "Annualy") {
        yyyy = yyyy + 1 + i;
    }
    today = mm + '/' + dd + '/' + yyyy;
    return today;
}

function refreshGrid() {
    debugger;
    $("#tblpaymentPlan tbody").empty();
    $("#tblpaymentPlan tfoot").empty();

    var row = "<tr>" +
        "<th></th>" +
        "<th></th>" +
        "<th class='text-right'> Total :</th>" +
        "<th class='text-right'>" + 0.00 + "</th>" +
        "</tr>";
    $("#tblpaymentPlan tfoot").append(row);
    $('#BubbleAmount').val(0);
    calculateInstallments();
}

function submitdetails() {
    debugger;
     checkValidation();
    var formdetails = [];
    $.each($("#tblpaymentPlan tbody tr"), function () {
        formdetails.push({
            PlanDetailId: Number($(this).find('.PlanDetailId').html()),
            Sr: parseInt($(this).find('.Sr').html()),
            DueDate: $(this).find('.DueDate').html(),
            Amount: $(this).find('.ItemId').html(),
            Amount: $(this).find('.Amount').html(),
        });

    });
    if (formdetails.length == 0) {

        swal("", "Enter at least 1 record", "error");
        return false;
    }
    else {
        var model = JSON.stringify(formdetails);
        $("#tblPaymentPlanDetails").val(model);
    }
     
    return true;
}