const today = new Date();
const monday = new Date();
const sunday = new Date();
if (today.getDay() === 1) {
    monday.setDate(today.getDate() - today.getDay() + 1 + 7 * -1);
} else {
    monday.setDate(today.getDate() - today.getDay() + 1);
}
sunday.setDate(monday.getDate() + 6);

let rows = "";
for (let [day, time] of Object.entries(hours_data)) {
    if (time !== "00:00:00") {
        rows += `
            <tr>
                <td>${day}</td>
                <td>${time}</td>
            </tr>
        `;
    }
}
let html = "";
if (rows !== "") {
    html = `You still have some working hours left to allot in week from ${monday.toDateString()} to ${sunday.toDateString()}. 
    <table class="table table-bordered" >
	<thead>
		<tr>
			<th>Day</th>
			<th>Time Left</th>
		</tr>
	</thead>
	<tbody>
		${rows}
	</tbody>
</table >`;
} else {
    html = `Submission of Working hours for the week from <br/> ${monday.toDateString()} - ${sunday.toDateString()}.`;
}

function SubmitWeek() {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success mx-5",
            cancelButton: "btn btn-danger mx-5"
        },
        buttonsStyling: true
    });
    swalWithBootstrapButtons.fire({
        icon: "question",
        title: "Are you sure you want to submit All the projects?",
        html: `${html}`,
        showCancelButton: true,
        confirmButtonText: "Yes, Submit it!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/TimeSheet/Submit",
                type: "POST",
                success: function (response) {
                    swalWithBootstrapButtons.fire({
                        title: "Submitted!",
                        text: "Your timesheet has been submitted.",
                        icon: "success"
                    });
                },
                error: function (error) {
                    swalWithBootstrapButtons.fire({
                        icon: "error",
                        title: error.status,
                        text: error.responseJSON.message
                    });
                }
            })
        }
    });
}