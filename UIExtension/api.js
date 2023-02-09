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

async function fetchData() {
    const response = await fetch('https://localhost:7221/api/CaseStudies');
    const data = await response.json();
    return data;
}

async function showFetchedData() {
    const data = await fetchData();
    let theDiv = document.getElementById('showFetchedData');
    
    let dataStyle = document.createElement('style');
    dataStyle.innerHTML += `/* checkbox */
                            .custom_checkbox{
                                height:30px;
                                width:30px;
                                accent-color:#17A2B8;
                            }`
    theDiv.prepend(dataStyle)
    
    // show data
    theDiv.innerHTML = `<h3>Test cases for: <strong>${data.caseName}</strong></h3>
                        <div>${data.guideLines}</div>
                        <p>Starting point from this <a href="${data.startingPoint}" target="_blank>link</a>.</p>`;
  }