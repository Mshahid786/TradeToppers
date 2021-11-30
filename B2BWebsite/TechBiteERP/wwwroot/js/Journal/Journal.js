
function getVoucherType() {
    var id = $('#VoucherTYPE').val();
    $.ajax({
        url: '/GeneralLedger/api/getVoucherType?id=' + id,
        type: 'GET'
    }).done(function (data) {
        $('#VoucherTYPEDESC').val(data);
    });
   

}

function emptyCR() {

    var debit = Number($('#Debit').val());
     
    if (debit > 0) {
        $('#Credit').val(0);
    }  
}

function emptyDR() {
    
    var credit = Number($('#Credit').val());
    if (credit > 0) {
        $('#Debit').val(0);
    }
}

function validate() {
    debugger;
    var rowCount = $("#tblvoucher tr").length;
    var totalDebit = 0.0000;
    var totalCredit = 0.0000;
    var difference = 0.00;
   
    if (rowCount <=1) {

        swal("", "Must be Enter Debit and Credit Entry.", "error");       
    }
   
    $("#tblvoucher tr").each(function () {
        $(this).find('td:eq(7)').each(function () {
            totalDebit += parseFloat($(this).text());
        });

        $(this).find('td:eq(8)').each(function () {
            totalCredit += parseFloat($(this).text());
        });

        difference = totalDebit - totalCredit;
        $('#tDebit').val(totalDebit);
        $('#tCredit').val(totalCredit);
        $('#difference').val(difference);
    });
    $("#tblvoucher tfoot tr").remove();
    var row = "<tr>" +
        "<th></th>" +
        "<th></th>" +
        "<th hidden></th>" +
        "<th></th>" +
        "<th> Total :</th>" +
        "<th class='text-right'>" + totalDebit.toFixed(4) + "</th>" +
        "<th class='text-right'>" + totalCredit.toFixed(4) + "</th>" +
        "<th></th>" +
        "</tr>";
    $("#tblvoucher tfoot").append(row);

    if (difference != 0) {
        swal("", "Difference must be equal to Zero. "
             + "Difference : " + difference +"", "error");
    }
}

function clearScreen() {
 
    $('#JournalDetID').val('');
    $('#Particular').val('');
    $('#RefNo').val('');
    $('#Debit').val('');
    $('#Credit').val('');
}