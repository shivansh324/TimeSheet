var projectDataTable;
var milestoneDataTable;
var workingHoursDataTable;
var monday;
var tuesday;
var wednesday;
var thursday;
var friday;
var saturday;
var sunday;
var currentStatus = "Open";
function ticksToTimespan(ticks) {
    const ticksPerMillisecond = 10000;
    const ms = ticks / ticksPerMillisecond;

    const totalMinutes = Math.floor(ms / (1000 * 60));
    const hours = Math.floor(totalMinutes / 60);
    const minutes = totalMinutes % 60;

    return `${hours}h ${minutes}m`;
}
function safeJSString(str) {
    return encodeURIComponent(str || '').replace(/'/g, "\\'");
}
function escapeHTML(str) {
    return str
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;')
        .replace(/\n/g, '<br/>'); // Optional: convert newlines to <br/>
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
function WeekModal(date, id, hours, remarks, IsBillable, project, timesheetId) {
    remarks = decodeURIComponent(remarks || '');
    $("#IsBillable").val("true");
    $("#IsProject").val(project);
    $("#MilestoneId").val(id);
    $("#Date").val(date);
    $("#TimesheetId").val(timesheetId)
    if (hours == 0 && remarks == null) {
        $('#Hours').val('');
        $('#Remarks').val('');
    } else {
        $('#Hours').val(hours);
        $('#Remarks').val(remarks);
    }

    if (IsBillable == false) {
        $("#IsBillable").val("false");
    }
    modal.show();
}
function ToolTip() {
    var tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    var tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl, { html: true }))
}
function getDayOfWeek(dateStr) {//Return the day of the week from date string
    const date = new Date(dateStr);
    const days = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    return days[date.getDay()];
}
function getWorkingHoursForDay(workingHours, day) {
    if (!workingHours || !Array.isArray(workingHours)) return '-';
    const entry = workingHours.find(wh => getDayOfWeek(wh.date) === day);
    return entry ? ticksToTimespan(entry.hours) : '-';
}

function getHoursForDay(timesheets, day, id, date, project, status = "Open") {
    if (!timesheets || !Array.isArray(timesheets)) return '-';
    const entry = timesheets.find(ts => getDayOfWeek(ts.date) === day);
    if (status === "Pending" || status === "Approved") {
        var remarks = entry ? escapeHTML(entry.remarks) : '';
        var isBillable = entry ? escapeHTML(entry.isBillable.toString()) : '-';

        return `<p data-bs-toggle="tooltip" data-bs-html="true" data-bs-title="Billable: ${isBillable} <br/>Remarks: ${remarks}">${entry ? entry.hours : '-'}</p>`;
    } else {
        if (entry) {
            return `<button class="btn btn-sm btn-light day-btn timesheet-btn"
                 onClick="WeekModal('${entry.date}',${id},'${entry.hours}','${safeJSString(entry.remarks)}',${entry.isBillable}, ${project}, ${entry.id})">
                     ${entry ? entry.hours : '-'}
                  </button>`;
        } else {
            return `<button class="btn btn-sm btn-light day-btn timesheet-btn"
                 onClick="WeekModal('${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}',${id},0,null,${project ? true : false},${project},0)">-</button>`;
        }
    }
}

$(document).ready(async function () {
    workingHoursDataTable = await $('#wokingHoursTable').DataTable({
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
                render: function (data) {
                    const status = data.submissionLog?.status || "Open";
                    let hours = data.submissionLog?.hours || "-";
                    let rejectionRemarks = data.submissionLog?.rejectionRemarks || "-";
                    if (hours !== "-") {
                        hours = ticksToTimespan(hours);
                    }
                    currentStatus = status;
                    $("#Status").val(status);
                    $("#WorkingHours").val(hours);
                    $("#RejectionRemarks").val(rejectionRemarks);
                    return data.type;
                }
            },
            { data: null, render: data => getWorkingHoursForDay(data.hours, 'Mon') },
            { data: null, render: data => getWorkingHoursForDay(data.hours, 'Tue') },
            { data: null, render: data => getWorkingHoursForDay(data.hours, 'Wed') },
            { data: null, render: data => getWorkingHoursForDay(data.hours, 'Thu') },
            { data: null, render: data => getWorkingHoursForDay(data.hours, 'Fri') },
            { data: null, render: data => getWorkingHoursForDay(data.hours, 'Sat') },
            { data: null, render: data => getWorkingHoursForDay(data.hours, 'Sun') }
        ],
        drawCallback: await function () {
            if (currentStatus === "Approved" || currentStatus === "Pending") {
                $("#SubmitBtn").addClass("disabled").hide();
            } else {
                $("#SubmitBtn").removeClass("disabled").show();
            }
        }
    });
    milestoneDataTable = await $('#milestoneTable').DataTable({
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
            { data: null, render: data => getHoursForDay(data.timesheets, 'Mon', data.id, monday, false, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Tue', data.id, tuesday, false, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Wed', data.id, wednesday, false, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Thu', data.id, thursday, false, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Fri', data.id, friday, false, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sat', data.id, saturday, false, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sun', data.id, sunday, false, data.status) }
        ],
        drawCallback: await function () {
            ToolTip();
        }
    });
    projectDataTable = await $('#projectTable').DataTable({
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
            { data: null, render: data => getHoursForDay(data.timesheets, 'Mon', data.id, monday, true, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Tue', data.id, tuesday, true, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Wed', data.id, wednesday, true, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Thu', data.id, thursday, true, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Fri', data.id, friday, true, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sat', data.id, saturday, true, data.status) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sun', data.id, sunday, true, data.status) },
            { data: 'totalWorkingHours', render: data => ticksToTimespan(data) },
            { data: 'pendingWorkingHours', render: data => ticksToTimespan(data) }
        ],
        drawCallback: await function () {
            ToolTip();
        }
    });
    updateWeekRange();
});


$('#prevWeek').on('click', function () {
    currentWeekOffset--;
    updateWeekRange();
    projectDataTable.ajax.reload();
    milestoneDataTable.ajax.reload();
    workingHoursDataTable.ajax.reload();
});

$('#nextWeek').on('click', function () {
    currentWeekOffset++;
    updateWeekRange();
    projectDataTable.ajax.reload();
    milestoneDataTable.ajax.reload();
    workingHoursDataTable.ajax.reload();
});

// Modal Logic
let modal = new bootstrap.Modal(document.getElementById('entryModal'));

$('#weekForm').on('submit', function (e) {
    e.preventDefault();
    var formData = new FormData(this);
    $.ajax({
        url: '/TimeSheet/SetTimesheet',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            console.log(response);
            if (response.success) {
                modal.hide();
                projectDataTable.ajax.reload();
                milestoneDataTable.ajax.reload();
                workingHoursDataTable.ajax.reload();
                Swal.fire({
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 800
                });
            } else {
                Swal.fire({
                    icon: "error",
                    title: 500,
                    text: 'Contact IT for Help!!'
                });
            }
        },
        error: function (error) {
            modal.hide();
            Swal.fire({
                icon: "error",
                title: error.status,
                text: error.responseJSON.error,
                footer: 'Contact IT for Help!!'
            });
        }
    });
});