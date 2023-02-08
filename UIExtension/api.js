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