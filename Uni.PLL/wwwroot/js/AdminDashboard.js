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
  if (selectedTable === "Takes") {
    TableBody.innerHTML = ``;
    headers.innerHTML = "<th>Year</th><th>GPA</th><th>Semester</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      let content = `<tr><td>2023</td><td>3.5</td><td>Fall</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
      TableBody.innerHTML += content;
    }
  }

  else if (selectedTable === "Student") {
      // Clear existing table content
      TableBody.innerHTML = ``;

      // Set table headers
      headers.innerHTML = "<th>Course Code</th><th>Title</th><th>Credit Hours</th><th>Description</th><th>Year</th><th>Semester</th><th>Action</th>";

      // Fetch data from server
      $.ajax({
          url: '@Url.Action("Index", "AdminController")',
          type: 'GET',
          success: function (GetStudentDataVM) {
              // Process successful response
              courses.forEach(function (GetStudentDataVM) {
                  TableBody.innerHTML += `
                    <tr>
                        <td>${GetStudentDataVM.}</td>
                        <td>${GetStudentDataVM.Title}</td>
                        <td>${GetStudentDataVM.CreditHours}</td>
                        <td>${GetStudentDataVM.Description}</td>
                        <td>${GetStudentDataVM.Year}</td>
                        <td>${GetStudentDataVM.Semester}</td>
                        <td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td>
                    </tr>
                `;
              });
          },
          error: function (error) {
              // Handle errors
              console.error("Error fetching courses:", error);
          }
      });
  }

  else if (selectedTable === "Course") {
    TableBody.innerHTML = ``;
    headers.innerHTML = "  <th>Course Code</th><th>Title</th><th>Credit Hours</th><th>Description</th><th>Year</th><th>Semester</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td>alice@example.com</td><td>alice@example.com</td><td>alice@example.com</td><td>alice@example.com</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  else if (selectedTable === "Teaches") {
    TableBody.innerHTML = ``;
    headers.innerHTML = "  <th>Year</th><th>Semester</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  else if (selectedTable === "Department") {
    TableBody.innerHTML = ``;
    headers.innerHTML = "  <th>Department Name</th><th>Head of Department</th><th>Building</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td>Alex Fergsoun Building</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  else if (selectedTable === "Instructor") {
    TableBody.innerHTML = ``;
    headers.innerHTML = "  <th>Instructor ID</th><th>Name</th><th>Salary</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td>$100k</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
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
      for (let i = 1; i < fields.length - 1; i++) {
        fields[i].setAttribute("contenteditable", "false");
      }
    })
  });
})

