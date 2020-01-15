// Onemogući skok na vrh kada se pritisne na gumb "Dodaj" i validacija ne uspije.
window.scrollTo = function (x, y) {
    return true;
};


// Provjeri je li suma udjela namirnica u obroku jednaka 100.
function ValidateSum(sender, args) {
    try {
        var sum = 0;
        $(sender.parentNode)
            .find("table.obrokTable")
            .find("[id*='_txtUdio']")
            .each(function (index, element) {
                sum += parseInt(element.value);
            });

        if (sum === 100) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    } catch (e) {
        args.IsValid = false;
    }
    //console.log(args.IsValid);
}


// Provjeri je li suma dnevnih udjela obroka jednaka 100.
function ValidateSumDnevno(sender, args) {
    try {
        var sum = 0;
        $(sender.parentNode)
            .find("div.obrokDiv")
            .find("[id*='_txtDnevniUdio']")
            .each(function (index, element) {
                if (index === 0) return;
                sum += parseInt(element.value);
            });

        if (sum === 100) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    } catch {
        args.IsValid = false;
    }
    //console.log(args.IsValid);
}