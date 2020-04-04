var Request =
{
    showPage: function (path, targetDiv) {
        var jqxhr = jQuery.get(path, function (data) {
            jQuery("#" + targetDiv).html(data);
        });
    },

    getAccountType: function (accountType) {
        var accountName = ["Savings", "Current", "Fixed Deposit", "DEMAT", "NRI", "Salary"];
        return accountName[accountType-1];
    },

    getInterest: function (accountType) {
        var interest = [4.4, 4.0, 6.5, 5.6, 5.0, 4.0];
        return interest[accountType - 1];
    },

    getBalance: function (accountType) {
        var balance = [4000, 0, 500, 8000, 15000, 0];
        return balance[accountType - 1];
    },

    getCheque: function (accountType) {
        var cheque = ["Applicable", "Applicable", "Not Applicable", "Not Applicable", "Not Applicable", "Applicable"];
        return cheque[accountType - 1];
    },

    getDebit: function (accountType) {
        var debit = ["Choice of cards(VISA/RUPAY/Master)", "VISA", "Not Applicable", "Not Applicable", "VISA", "Choice of cards(VISA/RUPAY/Master)"];
        return debit[accountType - 1];
    },

    getCredit: function (accountType) {
        var credit = ["Depends on CIBIL Score", "Depends on CIBIL Score", "Available with a limit : 90% of FD Amount", "Not Applicable", "Not Applicable", "180% of Monthly Salary"];
        return credit[accountType - 1];
    },

    getTransactionType: function (transactionType) {
        var transactionName = ["NEFT", "IMPS", "RTGS", "UPI"];
        return transactionName[transactionType - 1];
    },

    showError: function (message) {
        var html = "<div class='alert alert-danger'><strong>" + message + "</strong></div>";
        jQuery("#errorDiv").html(html);
    },

    showSuccess: function (message) {
        var html = "<div class='alert alert-success'><strong>" + message + "</strong></div>";
        jQuery("#errorDiv").html(html);
    },

    showMenu: function () {
        Request.showPage("html/menu.html", "menuDiv");
    },

    showBMenu: function () {
        Request.showPage("html/bmenu.html", "menuDiv");
    },

    showCMenu: function () {
        Request.showPage("html/cmenu.html", "menuDiv");
    },

    showHome: function () {
        Request.showPage("html/home.html", "bodyDiv");
    },

    showLogin: function () {
        Request.showPage("html/login.html", "bodyDiv");
    },

    showCustomer: function () {
        Request.showPage("html/customer.html", "bodyDiv");
    },

    showSummary: function () {
        Request.showPage("html/summary.html", "bodyDiv");
    },

    chequeBook: function () {
        Request.showPage("html/chequeBook.html", "bodyDiv");
    },

    addTransaction: function () {
        Request.showPage("html/transaction.html", "bodyDiv");
    },

    //addCustomer: function () {
    //    Request.addCustomer();
    //}

    viewTransactions: function () {
        Request.showPage("html/viewTransactions.html", "bodyDiv");
    },

    searchTransactions: function () {
        Request.showPage("html/searchTransactions.html", "bodyDiv");
    },

    enquiry: function () {
        Request.showPage("html/enquiry.html", "bodyDiv");
    },

    showBank: function () {
        Request.showPage("html/bank.html", "bodyDiv");
    },

    changePassword: function () {
        Request.showPage("html/password.html", "bodyDiv");
    },

    showErrorPage: function () {
        Request.showPage("html/error.html", "bodyDiv");
    },

    showLogout: function () {
        jQuery.removeCookie("role");
        jQuery.removeCookie("user");
        jQuery.removeCookie("isLoggedIn", false);
        Request.showPage("html/logout.html", "bodyDiv");
    },

    post: function (url, data){
        var responseData = null;
        jQuery.ajax(
            {
                url: url,
                type: 'post',
                data: JSON.stringify(data),
                dataType: 'json',
                async: false,
                cache: false,
                processData: false,
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    responseData = response;
                },
                error: function () {
                    Request.showError("<strong>Oops!</strong> Unable to reach the server.");
                }
            });
        return responseData;
    }
};