let select = document.getElementById("tableSelect");
let headers = document.getElementById("tableHeader");
let addbutt = document.getElementsByClassName("add")[0];
let TableBody = document.getElementsByTagName("tbody")[0];
let options = select.children;

// ✅ Use event delegation for Edit and Save
TableBody.addEventListener("click", (e) => {
  if (e.target.classList.contains("Edit")) {
    let fields = e.target.parentElement.parentElement.children;
    for (let i = 1; i < fields.length - 1; i++) {
      fields[i].setAttribute("contenteditable", "true");
    }
  }

  if (e.target.classList.contains("Save")) {
    let fields = e.target.parentElement.parentElement.children;
    for (let i = 1; i < fields.length - 1; i++) {
      fields[i].setAttribute("contenteditable", "false");
    }
  }
});

select.addEventListener("change", function () {
  Array.from(options).forEach((e) => {
    e.classList.remove("choosen");
  });

  let selectedTable = select.value;

  Array.from(options).forEach((e) => {
    if (e.value === selectedTable) e.classList.add("choosen");
  });

  TableBody.innerHTML = ``;

  if (selectedTable === "Takes") {
    headers.innerHTML = "<th>Year</th><th>GPA</th><th>Semester</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += `<tr><td>2023</td><td>3.5</td><td>Fall</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  } else if (selectedTable === "Student") {
    headers.innerHTML = "<th>Course Code</th><th>Title</th><th>Credit Hours</th><th>Description</th><th>Year</th><th>Semester</th><th>Action</th>";

    // Example only; replace with real AJAX
    // For now, we'll just add dummy rows
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += `<tr><td>CS${i}</td><td>Title ${i}</td><td>3</td><td>Description ${i}</td><td>2023</td><td>Fall</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }

    /*
    // For actual AJAX (make sure jQuery is included)
    $.ajax({
      url: '@Url.Action("Index", "AdminController")',
      type: 'GET',
      success: function (GetStudentDataVM) {
        GetStudentDataVM.forEach(function (student) {
          TableBody.innerHTML += `
            <tr>
              <td>${student.Code}</td>
              <td>${student.Title}</td>
              <td>${student.CreditHours}</td>
              <td>${student.Description}</td>
              <td>${student.Year}</td>
              <td>${student.Semester}</td>
              <td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td>
            </tr>`;
        });
      },
      error: function (error) {
        console.error("Error fetching data:", error);
      }
    });
    */
  } else if (selectedTable === "Course") {
    headers.innerHTML = "<th>Course Code</th><th>Title</th><th>Credit Hours</th><th>Description</th><th>Year</th><th>Semester</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += `<tr><td>${i}</td><td>Alice</td><td>3</td><td>Intro</td><td>2023</td><td>Fall</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  } else if (selectedTable === "Teaches") {
    headers.innerHTML = "<th>Year</th><th>Semester</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += `<tr><td>202${i % 10}</td><td>Spring</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  } else if (selectedTable === "Department") {
    headers.innerHTML = "<th>Department Name</th><th>Head of Department</th><th>Building</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += `<tr><td>Dept ${i}</td><td>Dr. Alice</td><td>Main Building</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  } else if (selectedTable === "Instructor") {
    headers.innerHTML = "<th>Instructor ID</th><th>Name</th><th>Salary</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += `<tr><td>${i}</td><td>Instructor ${i}</td><td>$${i}0k</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  } else {
    headers.innerHTML = "<th>Record ID</th><th>Course Code</th><th>Semester</th><th>Year</th><th>GPA</th><th>Improved</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += `<tr><td>${i}</td><td>CS${i}</td><td>Fall</td><td>2023</td><td>3.5</td><td>No</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
});

addbutt.addEventListener("click", () => {
  Array.from(options).forEach((e) => {
    if (!e.classList.contains("choosen")) return;

    let row = "";

    switch (e.value) {
      case "Student":
        row = `<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
        break;
      case "Takes":
        row = `<tr><td></td><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
        break;
      case "Course":
        row = `<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
        break;
      case "Teaches":
        row = `<tr><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
        break;
      case "Department":
        row = `<tr><td></td><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
        break;
      case "Instructor":
        row = `<tr><td></td><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
        break;
      case "Records":
        row = `<tr><td></td><td></td><td></td><td></td><td></td><td></td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
        break;
    }

    TableBody.innerHTML += row;
  });
});
