let select = document.getElementById("tableSelect");
let headers = document.getElementById("tableHeader");


let addbutt = document.getElementsByClassName("add")[0];
let editbutt = document.getElementsByClassName("Edit");
let savebutt = document.getElementsByClassName("Save");
let TableBody = document.getElementsByTagName("tbody")[0];
let options = select.children;


select.addEventListener("change", function () {

  Array.from(options).forEach((e) => {
    e.classList.remove("choosen")
  })
  let selectedTable = select.value;
  Array.from(options).forEach((e) => {
    if (e.value == selectedTable)
      e.classList.add("choosen")
  })
    if (selectedTable === "Student") {
        tableBody.innerHTML = ``;
        tableHeader.innerHTML = `
            <th>ID</th>
            <th>First Name</th>
            <th>Birth Date</th>
            <th>Phone Number</th>
            <th>National ID</th>
            <th>Address</th>
            <th>Action</th>
        `;

        $.ajax({
            url: apiEndpoints.getAllStudents,
            type: 'GET',
            success: function (students) {
                tableBody.innerHTML = '';
                students.forEach(function (student) {
                    tableBody.innerHTML += `
                        <tr>
                            <td>${student.id}</td>
                            <td>${student.firstName}</td>
                            <td>${student.birthDate}</td>
                            <td>${student.phoneNumber}</td>
                            <td>${student.nationalId}</td>
                            <td>${student.address}</td>
                            <td>
                                <button class='Edit'>Edit</button>
                                <button class='Save'>Save</button>
                            </td>
                        </tr>
                    `;
                });
            },
            error: function (error) {
                console.error("Error fetching students:", error);
            }
        });
    }

    else if (selectedTable === "Course") {
        tableHeader.innerHTML = `
                <th>Course Code</th>
                <th>Title</th>
                <th>Credit Hours</th>
                <th>Description</th>
                <th>Year</th>
                <th>Semester</th>
                <th>Action</th>
            `;

        $.ajax({
            url: apiEndpoints.getAllCourses,
            type: 'GET',
            success: function (data) {
                tableBody.innerHTML = '';
                data.forEach(function (course) {
                    tableBody.innerHTML += `
                            <tr>
                                <td>${course.courseCode}</td>
                                <td>${course.title}</td>
                                <td>${course.creditHours}</td>
                                <td>${course.description}</td>
                                <td>${course.year}</td>
                                <td>${course.semester}</td>
                                <td>
                                    <button class='Edit'>Edit</button> 
                                    <button class='Save'>Save</button>
                                </td>
                            </tr>
                        `;
                });
            },
            error: function (err) {
                console.error("Failed to fetch courses:", err);
            }
        });
    }
    else if (selectedTable === "Takes") {
        tableHeader.innerHTML = `
                <th>Year Code</th>
                <th>GPA</th>
                <th>Semester</th>
                <th>Action</th>
            `;

        $.ajax({
            url: apiEndpoints.getAllTakes,
            type: 'GET',
            success: function (data) {
                tableBody.innerHTML = '';
                data.forEach(function (takes) {
                    tableBody.innerHTML += `
                            <tr>
                                <td>${takes.year}</td>
                                <td>${takes.gpa}</td>
                                <td>${takes.semester}</td>
                                <td>
                                    <button class='Edit'>Edit</button> 
                                    <button class='Save'>Save</button>
                                </td>
                            </tr>
                        `;
                });
            },
            error: function (err) {
                console.error("Failed to fetch courses:", err);
            }
        });
    }
    else if (selectedTable === "Teaches") {
        tableHeader.innerHTML = `
                <th>Year</th>
                <th>Semester</th>
                <th>Action</th>
            `;

        $.ajax({
            url: apiEndpoints.getAllTeaches,
            type: 'GET',
            success: function (data) {
                tableBody.innerHTML = '';
                data.forEach(function (teaches) {
                    tableBody.innerHTML += `
                            <tr>
                                <td>${teaches.year}</td>
                                <td>${teaches.semester}</td>
                                <td>
                                    <button class='Edit'>Edit</button> 
                                    <button class='Save'>Save</button>
                                </td>
                            </tr>
                        `;
                });
            },
            error: function (err) {
                console.error("Failed to fetch department:", err);
            }
        });
    }
    else if (selectedTable === "Department") {
        tableHeader.innerHTML = `
                <th>Department Name</th>
                <th>Head of Department</th>
                <th>Building</th>
                <th>Action</th>
            `;

        $.ajax({
            url: apiEndpoints.getAllDepartments,
            type: 'GET',
            success: function (data) {
                tableBody.innerHTML = '';
                data.forEach(function (department) {
                    tableBody.innerHTML += `
                            <tr>
                                <td>${department.deptName}</td>
                                <td>${department.head}</td>
                                <td>${department.building}</td>
                                <td>
                                    <button class='Edit'>Edit</button> 
                                    <button class='Save'>Save</button>
                                </td>
                            </tr>
                        `;
                });
            },
            error: function (err) {
                console.error("Failed to fetch department:", err);
            }
        });
    }
    else if (selectedTable === "Instructor") {
        tableHeader.innerHTML = `
                <th>Instructor ID</th>
                <th>Name</th>
                <th>Salary</th>
                <th>Action</th>
            `;

        $.ajax({
            url: apiEndpoints.getAllInstructor,
            type: 'GET',
            success: function (data) {
                tableBody.innerHTML = '';
                data.forEach(function (istructor) {
                    tableBody.innerHTML += `
                            <tr>
                                <td>${istructor.iid}</td>
                                <td>${istructor.name}</td>
                                <td>${istructor.salary}</td>
                                <td>
                                    <button class='Edit'>Edit</button> 
                                    <button class='Save'>Save</button>
                                </td>
                            </tr>
                        `;
                });
            },
            error: function (err) {
                console.error("Failed to fetch department:", err);
            }
        });
    }
  else {
    TableBody.innerHTML = ``;
    headers.innerHTML = "<th>Record ID</th><th>Course Code</th><th>Semester</th><th>Year</th><th>GPA</th><th>Improved</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td>$100k</td><td>100</td><td>100</td><td>100</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  Array.from(editbutt).forEach((button) => {
    button.addEventListener("click", () => {
      let fields = button.parentElement.parentElement.children;
      console.log(fields)
      for (let i = 1; i < fields.length - 1; i++) {
        fields[i].setAttribute("contenteditable", "true");
      }
    })
  });
  Array.from(savebutt).forEach((button) => {
    button.addEventListener("click", () => {
      let fields = button.parentElement.parentElement.children;
      for (let i = 1; i < fields.length - 1; i++) {
        fields[i].setAttribute("contenteditable", "false");
      }
    })
  });
});

addbutt.addEventListener("click", () => {
  Array.from(options).forEach((e) => {
    if (e.classList.contains("choosen") && e.value == "Student")
      TableBody.innerHTML += `<tr><td></td><td ></td><td></td><td></td><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`
    else if (e.classList.contains("choosen") && e.value == "Takes")
      TableBody.innerHTML += `<tr><td></td><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    else if (e.classList.contains("choosen") && e.value == "Course")
      TableBody.innerHTML += ` <tr><td></td><td ></td><td></td><td></td><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    else if (e.classList.contains("choosen") && e.value == "Teaches")
      TableBody.innerHTML += ` <tr><td></td><td ></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    else if (e.classList.contains("choosen") && e.value == "Department")
      TableBody.innerHTML += ` <tr><td></td><td ></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    else if (e.classList.contains("choosen") && e.value == "Instructor")
      TableBody.innerHTML += ` <tr><td></td><td ></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    else if (e.classList.contains("choosen") && e.value == "Records")
      TableBody.innerHTML += ` <tr><td></td><td ></td><td></td><td></td><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
  })
  Array.from(editbutt).forEach((button) => {
    button.addEventListener("click", () => {
      let fields = button.parentElement.parentElement.children;
      console.log(fields)
      for (let i = 1; i < fields.length - 1; i++) {
        fields[i].setAttribute("contenteditable", "true");
      }
    })
  });

  Array.from(savebutt).forEach((button) => {
    button.addEventListener("click", () => {
        let fields = button.parentElement.parentElement.children;
        console.log(fields)
      for (let i = 1; i < fields.length - 1; i++) {
        fields[i].setAttribute("contenteditable", "false");
        }
        let newRecord = {};
    })
  });
})

