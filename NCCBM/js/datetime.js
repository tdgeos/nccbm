$(document).ready(function () {
    $(".datepicker").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd',
        minDate: new Date(1950, 0, 1),
        maxDate: -1,
        defaultDate: new Date(2050, 0, 1),
        yearRange: "1950:2050",
        showButtonPanel: true
    });

    $(".datepicker-sr").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd',
        minDate: new Date(1950, 0, 1),
        maxDate: -7305,
        defaultDate: new Date(2050, 0, 1),
        yearRange: "1950:2050",
        showButtonPanel: true
    });


    $(".datepicker-jstime").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd',
        minDate: new Date(1950, 0, 1),
        defaultDate: -1,
        yearRange: "1950:2050",
        showButtonPanel: true
    });

    $(".datepicker-ym").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm',
        minDate: new Date(1990, 0, 1),
        maxDate: -1,
        defaultDate: new Date(2090, 0, 1),
        yearRange: "1990:2090",
        showButtonPanel: true
    });
});