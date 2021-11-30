

$(document).ready(function () {
    defaultParameters();
});

$('#ReportTitle').change(function () {

    debugger;
    var repName = $("#ReportTitle option:selected").text();

    alert(repName);
    if (repName == "Note Of Account") {
        $('#dvAccountLevel').show();
        $('#dvCreatedDate').show();
        $('#dvUpdatedDate').show();
        $('#dvBranch').show();
        $('#dvAccountNote').show();
    }
    else if (repName == "Trial Balance") {
        $('#dvAccountLevel').show();
        $('#dvCreatedDate').show();
        $('#dvUpdatedDate').show();
        $('#dvAccountNote').hide();
        $('#dvBranch').show();
    }
    else if (repName == "Balance Sheet") {
        $('#dvAccountLevel').hide();
        $('#dvCreatedDate').hide();
        $('#dvUpdatedDate').show();
        $('#dvAccountNote').hide();
        $('#dvBranch').show();
    }

});


function defaultParameters() {
    $('#dvAccountLevel').hide();
    $('#dvAccountNote').hide();
    $('#dvCreatedDate').hide();
    $('#dvUpdatedDate').hide();
    $('#dvBranch').hide();
    $('#dvCity').hide();
    $('#dvArea').hide();
    $('#dvRegion').hide();
    $('#dvCustomer').hide();
    $('#dvSupplier').hide();
    $('#dvSalesMan').hide();
}

