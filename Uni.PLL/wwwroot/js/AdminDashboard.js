let select = document.getElementById("tableSelect"); 
let headers = document.getElementById("tableHeader"); 
select.addEventListener("change", function () {
  let selectedTable = select.value;
  let TableBody = document.getElementsByTagName("tbody")[0];
  console.log(TableBody);
  if (selectedTable === "Takes") {
    TableBody.innerHTML = "";
    headers.innerHTML = "<th>Year</th><th>GPA</th><th>Semester</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      let content = `<tr><td>2023</td><td>3.5</td><td>Fall</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
      TableBody.innerHTML += content;
    }
  }
  else if (selectedTable === "Student") {
    TableBody.innerHTML = "";
    headers.innerHTML = "  <th>ID</th><th>Name</th><th>Date of Birth</th><th>Address</th><th>Phone Number</th><th>National ID</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td>alice@example.com</td><td>alice@example.com</td><td>alice@example.com</td><td>alice@example.com</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  else if (selectedTable === "Course") {
    TableBody.innerHTML = "";

    headers.innerHTML = "  <th>Course Code</th><th>Title</th><th>Credit Hours</th><th>Description</th><th>Year</th><th>Semester</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td>alice@example.com</td><td>alice@example.com</td><td>alice@example.com</td><td>alice@example.com</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  else if (selectedTable === "Course") {
    TableBody.innerHTML = "";

    headers.innerHTML = "  <th>Course Code</th><th>Title</th><th>Credit Hours</th><th>Description</th><th>Year</th><th>Semester</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td>alice@example.com</td><td>alice@example.com</td><td>alice@example.com</td><td>alice@example.com</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  else if (selectedTable === "Teaches") {
    TableBody.innerHTML = "";

    headers.innerHTML = "  <th>Year</th><th>Semester</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  else if (selectedTable === "Department") {
    TableBody.innerHTML = "";

    headers.innerHTML = "  <th>Department Name</th><th>Head of Department</th><th>Building</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td>Alex Fergsoun Building</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  else if (selectedTable === "Instructor") {
    TableBody.innerHTML = "";
    headers.innerHTML = "  <th>Instructor ID</th><th>Name</th><th>Salary</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td>$100k</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  else {
    TableBody.innerHTML = "";
    headers.innerHTML = "<th>Record ID</th><th>Course Code</th><th>Semester</th><th>Year</th><th>GPA</th><th>Improved</th><th>Action</th>";
    for (let i = 1; i <= 10; i++) {
      TableBody.innerHTML += ` <tr><td>${i}</td><td >Alice</td><td>$100k</td><td>100</td><td>100</td><td>100</td><td><button class='Edit'>Edit</button> <button class='Save'>Save</button></td></tr>`;
    }
  }
  let editbutt = document.getElementsByClassName("Edit");
  let savebutt = document.getElementsByClassName("Save");
  console.log(editbutt);
  console.log(savebutt);

  Array.from(editbutt).forEach((button) => {
    button.addEventListener("click", () => {
      let fields = button.parentElement.parentElement.children;
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
