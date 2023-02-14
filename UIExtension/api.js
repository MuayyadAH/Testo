async function sendData() {
    const data = document.getElementById("data").value;
    const response = await fetch("https://localhost:7221/api/Api", {
    method: "POST",
    headers: {
        "Content-Type": "application/json"
    },
    body: JSON.stringify(data)
    });
    const result = await response.text();
    console.log(result);
}

/* fetch('https://localhost:7221/api/CaseStudies', { 
    method: 'GET'
    })
    .then(function(response) { return response.json(); })
    .then(function(json) {
        navbar.innerText = 'TEST';
}); */


/* START -- FOR SHOWING TASKS IN NAVBAR */
async function fetchCaseStudy() {
    const response = await fetch('https://localhost:7221/api/CaseStudies');
    const data = await response.json();
    return data;
}

async function fetchTasks() {
    const response = await fetch('https://localhost:7221/api/TestCases/Tasks');
    const data = await response.json();
    return data;
}

async function showFetchedData() {
    const caseStudy = await fetchCaseStudy();
    const tasks = await fetchTasks();

    let showTasksInPopup = document.getElementById('showFetchedData');
    
    let dataStyle = document.createElement('style');
    dataStyle.innerHTML += `/* checkbox */
                            .custom_checkbox{
                                height:30px;
                                width:30px;
                                accent-color:#17A2B8;
                            }`
    showTasksInPopup.prepend(dataStyle);
    console.log(caseStudy.startingPoint);
    // show data
    showTasksInPopup.innerHTML = `<h3>Test cases for: <strong>${caseStudy.caseName}</strong></h3>
                        <div>${caseStudy.guideLines}</div>
                        <p>Starting point from this <a target="_blank" style="display: inline;" href="${caseStudy.startingPoint}">link</a>
                        </p>`;

    showTasksInPopup.innerHTML += `<ul style="text-align: left;">`
    for (let i=0; i<tasks.length; i++) {
        showTasksInPopup.innerHTML += `<li>${tasks[i]}</li>`
    }
    showTasksInPopup.innerHTML += `</ul>`
  }
/* END -- FOR SHOWING TASKS IN NAVBAR */
