function numberToMonth(number) {
    console.assert(number >= 1 && number <= 12);

    var months = {
        1 : 'January',
        2 : 'Feburary',
        3 : 'March',
        4 : 'April',
        5 : 'May',
        6 : 'June',
        7 : 'July',
        8 : 'August',
        9 : 'September',
        10 : 'October',
        11 : 'November',
        12 : 'December'
    };

    return months[number];
}

exports.numberToMonth = numberToMonth;