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

function getHoursForDay(timesheets, day) {
    if (!timesheets || !Array.isArray(timesheets)) return '-';
    const entry = timesheets.find(ts => getDayOfWeek(ts.date) === day);
    var remarks = entry ? escapeHTML(entry.remarks) : '';
    var isBillable = entry ? escapeHTML(entry.isBillable.toString()) : '-';
    return `<p data-bs-toggle="tooltip" data-bs-html="true" data-bs-title="Billable: ${isBillable} <br/>Remarks: ${remarks}">${entry ? entry.hours : '-'}</p>`;
}

$(document).ready(async function () {
    const data = await $.ajax({
        type: "GET",
        url: "/Approve/GetData/" + id,
        dataType: "json",
    });
    const TimesheetData = data;

    workingHoursDataTable = await $('#wokingHoursTable').DataTable({
        paging: false,
        info: false,
        searching: false,
        ordering: false,
        responsive: true,
        data: TimesheetData.workingHours,
        "columns": [
            {
                data: null,
                render: function (data) {
                    const status = data.submissionLog?.status || "Open";
                    let hours = data.submissionLog?.hours || "-";
                    if (hours !== "-") {
                        hours = ticksToTimespan(hours);
                    }
                    currentStatus = status;
                    $("#Status").val(status);
                    $("#WorkingHours").val(hours);
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
        ]
    });
    milestoneDataTable = await $('#milestoneTable').DataTable({
        paging: false,
        info: false,
        searching: false,
        ordering: false,
        responsive: true,
        data: TimesheetData.milestone,
        "columns": [
            { data: 'name' },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Mon') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Tue') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Wed') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Thu') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Fri') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sat') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sun') }
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
        data: TimesheetData.projectMilestone,
        "columns": [
            { data: 'projectCode' },
            { data: 'projectDescription' },
            { data: 'milestoneCode' },
            { data: 'milestoneDescription' },
            { data: 'taskCode' },
            { data: 'taskDescription' },
            { data: 'assignedHours', render: data => ticksToTimespan(data) },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Mon') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Tue') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Wed') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Thu') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Fri') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sat') },
            { data: null, render: data => getHoursForDay(data.timesheets, 'Sun') },
            { data: 'totalWorkingHours', render: data => ticksToTimespan(data) },
            { data: 'pendingWorkingHours', render: data => ticksToTimespan(data) }
        ],
        drawCallback: await function () {
            ToolTip();
        }
    });
});

$('input[name="Approval"]').change(function () {
    if ($('#Rejected').is(':checked')) {
        $('.Rejection_Remarks').prop('disabled', false).attr('required', true);
    } else {
        $('.Rejection_Remarks').prop('disabled', true).removeAttr('required').val('');
        $(".Rejection_Remarks").next(".error-msg").text('');
    }
});

$("#Approve_Form").submit(function (e) {
    e.preventDefault();
    let id = $("#Approver_Id").val();
    let approvalStatus = $('input[name="Approval"]:checked').val();
    let remarks = $('.Rejection_Remarks').val();
    if (approvalStatus === "Open" && remarks.trim() === "") {
        $(".Rejection_Remarks").next(".error-msg").text("Remarks are required when Rejecting Request!!");
        return;
    }
    let data = {
        id: id,
        approvalStatus: approvalStatus,
        remarks: remarks
    };
    $('#loading-overlay').addClass('active');
    $('#approveModal').modal('hide');
    $.ajax({
        url: '/Approve/Approve',
        type: 'POST',
        data: data,
        success: function (response) {
            $('#loading-overlay').removeClass('active');
            window.location.reload();
        },
        error: function (error) {
            $('#loading-overlay').removeClass('active');
            console.log(error);
            Swal.fire({
                icon: "error",
                title: error.status,
                text: error.responseJSON.message
            });
        }
    });
});