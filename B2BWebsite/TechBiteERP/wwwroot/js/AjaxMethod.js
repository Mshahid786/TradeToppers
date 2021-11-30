

function AjaxPost(Url, data, SuccessFunction, ErrorFunction, showLoader) {
    if (showLoader == undefined || showLoader == null || showLoader == true)
        $("#Loader").show();
    $.ajax({
        url: Url,
        type: 'POST',
        data: data,
        timeout: 300000,
        async: true,
        processData: false,
        cache: false,
        contentType: false,
        success: function (objdata) {
            SuccessFunction(objdata);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (ErrorFunction != null || ErrorFunction != undefined)
                ErrorFunction(jqXHR, textStatus);
            console.log("Error While Precessing Request! jqXHR " + JSON.stringify(jqXHR) + " textStatus " + JSON.stringify(textStatus) + " errorThrown " + JSON.stringify(errorThrown));
        }
    });
}



function AjaxGet(Url, SuccessFunction, ErrorFunction, showLoader) {
    if (showLoader == undefined || showLoader == null || showLoader == true)
        $("#Loader").show();
    $.ajax({
        url: Url,
        type: 'GET',
        timeout: 300000,
        async: true,
        processData: false,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        success: function (objdata) {
            if (objdata == "UnAuthorize") {
                location.href = "/Login";
            }
            else {
                SuccessFunction(objdata);
                $("#Loader").hide();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (ErrorFunction != null || ErrorFunction != undefined)
                ErrorFunction(jqXHR, textStatus);
            console.log("Error While Precessing Request! jqXHR " + JSON.stringify(jqXHR) + " textStatus " + JSON.stringify(textStatus) + " errorThrown " + JSON.stringify(errorThrown));
            error("Error While Precessing Request! " + textStatus);
            $("#Loader").hide();
        }
    });
}