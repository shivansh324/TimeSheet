let currentWeekOffset = 0;
var projectDataTable;
var milestoneDataTable;
var wokingHoursDatatable;
var monday;
var tuesday;
var wednesday;
var thursday;
var friday;
var saturday;
var sunday;
function ticksToTimespan(ticks) {
    const ticksPerMillisecond = 10000;
    const ms = ticks / ticksPerMillisecond;

    const totalMinutes = Math.floor(ms / (1000 * 60));
    const hours = Math.floor(totalMinutes / 60);
    const minutes = totalMinutes % 60;

    return `${hours}h ${minutes}m`;
}

const updateWeekRange = () => {
    const today = new Date();
    monday = new Date(today.setDate(today.getDate() - today.getDay() + 1 + currentWeekOffset * 7));
    tuesday = new Date(monday.getTime() + 1 * 24 * 60 * 60 * 1000);
    wednesday = new Date(monday.getTime() + 2 * 24 * 60 * 60 * 1000);
    thursday = new Date(monday.getTime() + 3 * 24 * 60 * 60 * 1000);
    friday = new Date(monday.getTime() + 4 * 24 * 60 * 60 * 1000);
    saturday = new Date(monday.getTime() + 5 * 24 * 60 * 60 * 1000);
    sunday = new Date(monday.getTime() + 6 * 24 * 60 * 60 * 1000);

    const formatDate = d => d.toLocaleDateString(undefined, { month: 'short', day: 'numeric' });
    document.getElementById("weekRange").innerText = `${formatDate(monday)} - ${formatDate(sunday)}`;
    $(".MonWeekDay").html("Mon " + formatDate(monday));
    $(".TueWeekDay").html("Tue " + formatDate(tuesday));
    $(".WedWeekDay").html("Wed " + formatDate(wednesday));
    $(".ThuWeekDay").html("Thu " + formatDate(thursday));
    $(".FriWeekDay").html("Fri " + formatDate(friday));
    $(".SatWeekDay").html("Sat " + formatDate(saturday));
    $(".SunWeekDay").html("Sun " + formatDate(sunday));
};
function WeekModal(date, id, hours, remarks, IsBillable) {
    if (hours == 0 && remarks == null) {
        $('#Hours').val('');
        $('#Remarks').val('');
    } else {
        $('#Hours').val(hours);
        $('#Remarks').val(remarks);
    }
    $("#MilestoneId").val(id);
    $("#Date").val(date);
    if (IsBillable == false) {
        $("#IsBillable").val("false");
    } else {
        $("#IsBillable").val("true");
    }
    modal.show();
}

function getDayOfWeek(dateStr) {//Return the day of the week from date string
    const date = new Date(dateStr);
    const days = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    return days[date.getDay()];
}

function getHoursForDay(timesheets, day, id, date) {
    if (!timesheets || !Array.isArray(timesheets)) return '-';
    const entry = timesheets.find(ts => getDayOfWeek(ts.date) === day);
    if (entry) {
        return `<button class="btn btn-sm btn-light day-btn btn-outline-secondary text-dark"
                 onClick="WeekModal('${entry.date}',${id},'${entry.hours}','${entry.remarks}',${entry.isBillable})">
                     ${entry ? entry.hours : '-'}
                  </button>`;
    } else {
        return `<button class="btn btn-sm btn-light day-btn btn-outline-secondary text-dark"
                 onClick="WeekModal('${date}',${id},0,null,true)">-</button>`;
    }

}

$(document).ready(function () {
    projectDataTable = $('#projectTable').DataTable({
        paging: false,
        info: false,
        searching: false,
        ordering: false,
        responsive: true,
        serverSide: true,
        processing: true,
        ajax: {
            url: '/TimeSheet/GetProjectTimesheet',
            contentType: "application/json",
            type: 'POST',
            data: function (d) {
                d.weekOffset = currentWeekOffset;
                return JSON.stringify(d);
            }
        },
        "columns": [
            { data: 'projectCode' },
            { data: 'projectDescription' },
            { data: 'milestoneCode' },
            { data: 'milestoneDescription' },
            { data: 'taskCode' },
            { data: 'taskDescription' },
            { data: 'assignedHours', render: data => ticksToTimespan(data) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Mon', data.id, monday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Tue', data.id, tuesday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Wed', data.id, wednesday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Thu', data.id, thursday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Fri', data.id, friday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sat', data.id, saturday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sun', data.id, sunday) },
            { data: 'totalWorkingHours', render: data => ticksToTimespan(data) },
            { data: 'pendingWorkingHours', render: data => ticksToTimespan(data) }
        ]
    });
    milestoneDataTable = $('#milestoneTable').DataTable({
        paging: false,
        info: false,
        searching: false,
        ordering: false,
        responsive: true,
        serverSide: true,
        processing: true,
        ajax: {
            url: '/TimeSheet/GetTimesheet',
            contentType: "application/json",
            type: 'POST',
            data: function (d) {
                d.weekOffset = currentWeekOffset;
                return JSON.stringify(d);
            }
        },
        "columns": [
            { data: 'name' },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Mon', data.id, monday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Tue', data.id, tuesday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Wed', data.id, wednesday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Thu', data.id, thursday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Fri', data.id, friday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sat', data.id, saturday) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sun', data.id, sunday) }
        ]
    });
    wokingHoursDataTable = $('#wokingHoursTable').DataTable({
        paging: false,
        info: false,
        searching: false,
        ordering: false,
        responsive: true,
        serverSide: true,
        processing: true,
        ajax: {
            url: '/TimeSheet/GetWorkingHours',
            contentType: "application/json",
            type: 'POST',
            data: function (d) {
                d.weekOffset = currentWeekOffset;
                return JSON.stringify(d);
            }
        },
        "columns": [
            {
                data: null,
                render: function () {
                    return 'Working Hours';
                }
            },
            {
                data: null,
                render: function () {
                    return 'Working Hours';
                }
            },
            {
                data: null,
                render: function () {
                    return 'Working Hours';
                }
            },
            {
                data: null,
                render: function () {
                    return 'Working Hours';
                }
            },
            {
                data: null,
                render: function () {
                    return 'Working Hours';
                }
            },
            {
                data: null,
                render: function () {
                    return 'Working Hours';
                }
            },
            {
                data: null,
                render: function () {
                    return 'Working Hours';
                }
            },
            {
                data: null,
                render: function () {
                    return 'Working Hours';
                }
            }
            //{ data: null, render: data => getHoursForDay(data.timesheets, 'Mon', data.id, monday) },
            //{ data: null, render: data => getHoursForDay(data.timesheets, 'Tue', data.id, tuesday) },
            //{ data: null, render: data => getHoursForDay(data.timesheets, 'Wed', data.id, wednesday) },
            //{ data: null, render: data => getHoursForDay(data.timesheets, 'Thu', data.id, thursday) },
            //{ data: null, render: data => getHoursForDay(data.timesheets, 'Fri', data.id, friday) },
            //{ data: null, render: data => getHoursForDay(data.timesheets, 'Sat', data.id, saturday) },
            //{ data: null, render: data => getHoursForDay(data.timesheets, 'Sun', data.id, sunday) }
        ]
    });
    updateWeekRange();
});


$('#prevWeek').on('click', function () {
    currentWeekOffset--;
    updateWeekRange();
    projectDataTable.ajax.reload();
    milestoneDataTable.ajax.reload();
});

$('#nextWeek').on('click', function () {
    currentWeekOffset++;
    updateWeekRange();
    projectDataTable.ajax.reload();
    milestoneDataTable.ajax.reload();
});

// Modal Logic
let modal = new bootstrap.Modal(document.getElementById('entryModal'));




//$(document).on('click', '.day-btn', function () {
//	$('#targetBtn').val(this.id = this.id || Date.now()); // Assign unique id
//	$(this).attr('id', $('#targetBtn').val()); // Set if not set
//	$('#entryModal').data('btn', this.id);
//	$('#hoursInput').val('');
//	$('#remarkInput').val('');
//	modal.show();
//});

$('#weekForm').on('submit', function (e) {
    //e.preventDefault();
    //var formData = new FormData(this);
    //$.ajax({
    //    url: '/TimeSheet/UpdateWeeklyTimeSheet',
    //    type: 'POST',
    //    data: formData,
    //    contentType: false,
    //    processData: false,
    //    success: function (response) {
    //        console.log(response);
    //        if (response.success) {
    //            modal.hide();
    //            datatable.ajax.reload();
    //            Swal.fire({
    //                icon: "success",
    //                title: "Your work has been saved",
    //                showConfirmButton: false,
    //                timer: 800
    //            });
    //        } else {
    //            Swal.fire({
    //                icon: "error",
    //                title: 500,
    //                text: 'Contact IT for Help!!'
    //            });
    //        }
    //    },
    //    error: function (error) {
    //        modal.hide();
    //        Swal.fire({
    //            icon: "error",
    //            title: error.status,
    //            text: error.responseJSON.error,
    //            footer: 'Contact IT for Help!!'
    //        });
    //    }
    //});
});