let recordButton = document.getElementById('recButton');
let displayTimer = document.getElementById('timer');
let recMessage = document.getElementById('recordingMessage');

let elementsBox = document.getElementById('checbox-group-id')
let reportButton = document.getElementById('report-button');

let cbImages = document.getElementById('checkbox-img');
let cbButtons = document.getElementById('checkbox-button');
let cbVideos = document.getElementById('checkbox-video');


function checkSelectedElements() {
    let getAllSelectedElements = document.querySelectorAll('input[name="element-selection"]');
    let selectedElementsArray = [];

    if (getAllSelectedElements.length !== 0) {
        for (let i=0; i<getAllSelectedElements.length; i++) {
            if (getAllSelectedElements[i].checked === true) {
                selectedElementsArray.push(getAllSelectedElements[i].id.split('-')[1])
            }
        }
        return selectedElementsArray;
    }
    return [];
}

recordButton.addEventListener("click", () => {
    chrome.storage.local.get(["recordStatus","taskUrlsNoChange", "timer","lastUsedTimer", "selectedElements","testCases"], (res) => {
        const status = res.recordStatus ?? false;
        const timer = res.timer ?? 0;
        const lastTimer = res.lastUsedTimer ?? 0;
        console.log(`Status now is "${status}"`);
        if (status === false) {
            chrome.storage.local.set({
                recordStatus: true,
                timer: 0,
                selectedElements: checkSelectedElements(),
                savedUrls: [],
                testCases: [],
                Errors: 0,
                userClicks: 0,
                scrollDepth: 0,
                taskSuccess: 0,
                taskUrls: res.taskUrlsNoChange,
                taskUrlsNoChange: null
            });
            console.log("status set to true: popup.js")
            recMessage.textContent = 'Recording in progress!';
        }
        else {
            chrome.storage.local.set({
                recordStatus: false
            });
            console.log("status set to false: popup.js")
            recMessage.textContent = 'Click to start recording!';
        }
    })

});

updateTimer();
setInterval(updateTimer, 1000);

function updateTimer() {
    chrome.storage.local.get(["recordStatus","timer"], (res) => {
        const status = res.recordStatus ?? false;
        const time = res.timer ?? 0;
        if (status === true) {
            recMessage.textContent = 'Recording in progress!';
            // elementsBox.style.visibility = 'hidden';
            recordButton.classList.remove('notRec');
            recordButton.classList.add('Rec');
            displayTimer.textContent = `${new Date(time * 1000).toISOString().substring(14, 19)
            }`
            chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
                var tabUrl = tabs[0].url;
                console.log(tabUrl);
              });
        }
        else {
            displayTimer.textContent = 'TIMER';
            // elementsBox.style.visibility = 'visible';
            recordButton.classList.remove('Rec');
            recordButton.classList.add('notRec');
        }
    })
}


reportButton.addEventListener("click", () => {
    chrome.storage.local.get(["lastUsedTimer","taskUrlsNoChange","taskUrls","savedUrls","Errors","taskSuccess","userClicks","scrollDepth"], (res) => {
        console.log(res.taskUrlsNoChange);
        const data = {
            timeElapsed: res.lastUsedTimer,
            averageTimeOnTask: (res.lastUsedTimer/res.taskUrlsNoChange.length),
            visitedSites: JSON.stringify(res.savedUrls),
            errors: res.Errors,
            userClicks: res.userClicks,
            taskSucessRate: (res.taskSuccess/res.taskUrlsNoChange.length),
            scrollDepthRate: res.scrollDepth/res.savedUrls.length
          };
          // Send data to database
          fetch('https://localhost:7221/api/TestResults/Send', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
            })  .then(response => {
            if (!response.ok) {
              throw new Error('Network response was not ok');
            }
            return response.json();
          })
          .then(data => {
            console.log(data);
          })
          .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
          });
                
        
        
    });
    
    chrome.storage.local.get(["lastUsedTimer","taskUrlsNoChange","taskUrls","savedUrls","Errors","taskSuccess","userClicks","scrollDepth"], (res) => {
        console.log("Time Elapsed: "+res.lastUsedTimer);
        console.log(`Average Time Elapsed (per Task): ${res.lastUsedTimer/res.savedUrls.length}`)
        console.log(res.savedUrls);
        console.log(res.taskUrlsNoChange);
        console.log(res.taskUrls);
        console.log("Errors: "+res.Errors);
        console.log("Task Success: "+res.taskSuccess);
        console.log("User Clicks: "+res.userClicks);
        console.log("Average Scroll Depth: "+ (res.scrollDepth/res.savedUrls.length)+"px");
    });
    /* let x = chrome.runtime.getURL("report.html");
    window.open(x) */
})


/* chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
    var tabUrl = tabs[0].url;
    chrome.storage.local.get({'urlList':[]}, function(item){
      var newUrlList = item.urlList;
      if (newUrlList.indexOf(tabUrl) === -1) {
        newUrlList.push(tabUrl);
      }
      console.log(newUrlList);
      chrome.storage.local.set({urlList: newUrlList});
    });
  }); */