/* START - FOR AJAX RELOAD */
let lastUrl = location.href; 
new MutationObserver(() => {
  const url = location.href;
  if (url !== lastUrl) {
    lastUrl = url;
    onUrlChange();
  }
}).observe(document, {subtree: true, childList: true});

let startTime = new Date();

function onUrlChange() {
  console.log('URL changed!', location.href);
  chrome.storage.local.get(["recordStatus","savedUrls"], (res) => {
    if (res.recordStatus === true) {
        console.log("status "+res.recordStatus);
        let urls = res.savedUrls ?? [];
        console.log('test true status');
        urls.push({
            baseUrl: location.host,
            fullUrl: location.href,
            timePerTask: secondsSinceEnter(startTime)
        });
        chrome.storage.local.set({
            "savedUrls": urls
        });
    }
  });
  contnet_script();
}
/* END - FOR AJAX RELOAD */


/* NORMAL PAGE RELOAD */
if(document.readyState !== 'complete') {
    window.addEventListener('load',contnet_script);
    // document.addEventListener("DOMContentLoaded", contnet_script);
} else {
    // contnet_script();
}


// FOR RESET
/* chrome.storage.local.set({
    "recordStatus": false,
    "taskSuccess": 0,
    "Errors": 0,
    "taskUrls": null,
    "taskUrlsNoChange": null,
    "userClicks": 0,
    "scrollDepth": 0
});

chrome.storage.local.get(["lastUsedTimer","taskUrls","savedUrls","Errors","taskSuccess","userClicks","scrollDepth"], (res) => {
    console.log("Time Elapsed: "+res.lastUsedTimer);
    console.log(`Average Time Elapsed (per Task): ${res.lastUsedTimer/res.savedUrls.length}`)
    console.log("Saved URLS from user browsing: "+ res.savedUrls);
    console.log("Errors: "+res.Errors);
    console.log("Task Success: "+res.taskSuccess);
    console.log("User Clicks: "+res.userClicks);
    console.log("Average Scroll Depth: "+ (res.scrollDepth/res.savedUrls.length)+"px");
}); */

// Task time
/* window.addEventListener('load', () =>{
    chrome.storage.local.get(["timePerTask","savedUrls"], (res) =>{
        console.log(res.timePerTask ?? 0);
    })
    const startTime = new Date();
    chrome.storage.local.get(["recordStatus", "taskUrls","taskUrlsNoChange","savedUrls"], (res) => {
        let savedUrls = res.savedUrls ?? [];
        console.log(`Recording status: ${res.recordStatus}`);
        if (res.recordStatus === true) {
            window.addEventListener('beforeunload', () => {
                /*
                for (let i=0; i<res.taskUrls.length; i++) {
                    console.log(res.taskUrls[i] === location.href)
                    if (res.taskUrls[i] === location.href) {
                        savedUrls.push({
                            baseUrl: location.host,
                            fullUrl: location.href,
                            timeElapsed: secondsSinceEnter(startTime)
                        });
                        chrome.storage.local.set({
                            savedUrls: savedUrls
                        });
                        break;
                    }
                }
                chrome.storage.local.set({
                    timePerTask: secondsSinceEnter(startTime)
                });
            })
        }
    });
    
});  */
function secondsSinceEnter(startTime)
{
    return Math.floor((new Date() - startTime) / 1000);
}

function contnet_script() {

    chrome.storage.local.get(["recordStatus", "selectedElements","savedUrls"], (res) => {
        const recordStatus = res.recordStatus;
        const savedUrls = res.savedUrls ?? [];
        let selectedElementsArray = res.selectedElements ?? [];
        console.log("Saved Links After testing:");
        console.log(savedUrls);

        fetch('https://localhost:7221/api/TestCases/Tasks')
        .then((response) => response.json())
        .then((data) => {
            console.log("Tasks:");
            console.log(data);
        });


        // Task Success AND Errors
        fetch('https://localhost:7221/api/TestCases/Links')
        .then((response) => response.json())
        .then((data) => {
            chrome.storage.local.set({
                taskUrlsNoChange: data
            });
            chrome.storage.local.get(["timePerTask","taskUrls","savedUrls","Errors", "taskSuccess","taskUrlsNoChange"], (res) => {
                console.log("Task Links:");
                console.log(res.taskUrlsNoChange);
                console.log("Remaining Tasks Links:");
                console.log(res.taskUrls);
                if (recordStatus === true) {
                    let taskUrls = res.taskUrls ?? data;
                    let urls = res.savedUrls ?? [];
                    let taskSuccess = res.taskSuccess ?? 0;
                    let errors = res.Errors ?? 0;

                    if (taskUrls.includes(location.href)) {
                        if (taskSuccess < data.length) {
                            taskSuccess++;
                            console.log("Task Success: "+taskSuccess);
                        }
                        console.log(taskUrls);
                        for (let i=0; i<taskUrls.length; i++) {
                            if (taskUrls[i] === location.href) {
                                taskUrls.splice(i, 1);
                                break;
                            }
                        }
                        chrome.storage.local.set({
                            "taskSuccess": taskSuccess,
                            "taskUrls": taskUrls,
                        });
                        window.addEventListener('beforeunload', () => {
                            urls.push({
                                baseUrl: location.host,
                                fullUrl: location.href,
                                timePerTask: secondsSinceEnter(startTime)
                            });
                            chrome.storage.local.set({
                                "savedUrls": urls
                            });
                        })
                    }
                    else {
                        errors++;
                        console.log("Errors: "+errors);
                        chrome.storage.local.set({
                            "Errors": errors
                        });
                    }
                }

            })
        });
        
        // User Clicks
        chrome.storage.local.get(["recordStatus","userClicks"], (res) => {
            const recordStatus = res.recordStatus;
            let userClicks = res.userClicks ?? 0;
            if (recordStatus === true) {
                document.addEventListener('click', () => {
                    userClicks++;                    
                    chrome.storage.local.set({
                        "userClicks": userClicks
                    });
                })
            }
        });



        // Scroll Depth
        let maxScrollDepth = 0;
        window.addEventListener("scroll", function() {
            maxScrollDepth = Math.max(maxScrollDepth, window.pageYOffset + window.innerHeight);
            console.log("Max Scroll Depth: ", maxScrollDepth);
        });
        window.addEventListener('beforeunload', () => {
            chrome.storage.local.get(["recordStatus","scrollDepth"], (res) => {
                const recordStatus = res.recordStatus;
                let scrollDepth = res.scrollDepth ?? 0;
                if (recordStatus === true) {
                    scrollDepth += maxScrollDepth
                    chrome.storage.local.set({
                        "scrollDepth": scrollDepth
                    });
                }
            });
        });


        /* if (recordStatus === true) {
            chrome.storage.local.get(["savedUrls"], (res) => {
                let urls = res.savedUrls ?? [];
                console.log(urls);
                urls.push({
                    baseUrl: location.host,
                    fullUrl: location.href,
                    elements: {
                        img: countImages(selectedElementsArray),
                        video: countVideos(selectedElementsArray),
                        button: countButtons(selectedElementsArray)
                    }
                })
                chrome.storage.local.set({
                    savedUrls: urls
                })
            })
    
        }
        else {
            addNoteInit();
        } */
    
    })
    
    // START -- for counting elements
    const countImages = (selectedElementsArray) => {
        if (selectedElementsArray.includes('img')) {
            return document.querySelectorAll('img').length;
        }
        else return 0
    }
    
    const countButtons = (selectedElementsArray) => {
        if (selectedElementsArray.includes('button')) {
            return document.querySelectorAll('button').length;
        }
        else return 0
    }
    
    const countVideos = (selectedElementsArray) => {
        if (selectedElementsArray.includes('video')) {
            return document.querySelectorAll('video').length;
        }
        else {
            return 0;
        }
    }
    // END -- for counting elements

    // START -- Adding notes functions
    function addNoteInit() {
        // adding a new button to the dom to enable developers to add new note while recording their test
        let addNoteIcon = document.createElement('button');
        addNoteIcon.style.position = 'fixed';
        addNoteIcon.style.bottom = '35px';
        addNoteIcon.style.right = '35px';
        addNoteIcon.innerHTML = "Add new note";
        addNoteIcon.setAttribute('id','add-note-button');

        let createBlockedStyle = document.createElement('style');
        createBlockedStyle.innerHTML += `.blocked
        {
            position:relative;
        }
        .blocked:after
        {
            content: '';
            position: absolute;
            left:0;
            right:0;
            top:0;
            bottom:0;
            z-index:1;
            background: transparent;
        }`;

        document.body.appendChild(addNoteIcon);
        document.body.appendChild(createBlockedStyle);
        
        let noteStatus = false;
        let noteButton = document.getElementById('add-note-button');
        noteButton.addEventListener('click', (evt) => {
            document.getElementById("testo-mySidenav").style.width = "330px";
            /* console.log("box clicked");
            noteAddingStyle();
            if (!noteStatus) {
              evt.stopPropagation();
            }   
            noteStatus = !noteStatus
            
            document.addEventListener('click', noteRemovingStyle, {once:true}); */
          });
    }

    function noteAddingStyle() {
        document.getElementsByTagName('body')[0].style.opacity = '0.5';
        document.getElementsByTagName('body')[0].classList.add('blocked')
        document.getElementsByTagName('body')[0].style.cursor = 'crosshair';
    }

    function noteRemovingStyle() {
        let body = document.getElementsByTagName('body')[0];
        body.style.backgroundColor = null;
        body.style.opacity = null;
        body.classList.remove('blocked')
        body.style.cursor = null;   
        noteStatus = false;  
        addNote(event); 
    }

    function addNote(event) {
        console.log(event);
        let body = document.getElementsByTagName('body')[0];
        // Create a new div to hold the note
        var noteBox = document.createElement("div");
        noteBox.style.position = "absolute";
        noteBox.style.left = event.pageX + "px";
        noteBox.style.top = event.pageY + "px";
        noteBox.style.backgroundColor = "yellow";
        noteBox.style.padding = "10px";

        let note = prompt('Enter your note here:')

        noteBox.innerHTML = "Note: "+note;
      
        body.appendChild(noteBox);
      }  
      // END -- Adding notes functions


      /* START -- VIEW TEST CASE INSIDE THE PAGES (API.JS INCLUDED) */

      // Navbar open button
      let navbarOpenButton = document.createElement('div');
      navbarOpenButton.classList.add('testo-openNavButton');
      // navbarOpenButton.innerHTML = `<img src="test_icon_no_bg.png"></img>`
      navbarOpenButton.innerHTML += `<img src="${chrome.runtime.getURL('testo_icon_no_bg.png')}" style="height: 28px; width: 28px; margin-top: 20%; margin-right: auto; margin-left: auto;"/>`;
      navbarOpenButton.setAttribute('onclick','showFetchedData()');
      navbarOpenButton.addEventListener('click', () => {
        document.getElementById("testo-mySidenav").style.width = "330px";
      });

      // Navbar style 
      let navbarStyle = document.createElement('style');
      navbarStyle.innerText += `/* The side navigation menu */
      .testo-sidenav {
      height: 75%; /* 100% Full-height */
      width: 0; /* 0 width - change this with JavaScript */
      position: fixed; /* Stay in place */
      z-index: 99; /* Stay on top */
      top: 10%;
      background-color: #bcbcbc; /* background color of cases*/
      overflow-x: hidden; /* Disable horizontal scroll */
      padding-top: 20px; /* Place content 60px from the top */
      transition: 0.5s; /* 0.5 second transition effect to slide in the sidenav */
      border-color: black;
      border-width: thin;
      border-style: solid;
      }
      
      /* The navigation menu links */
      .testo-sidenav a {
      /* padding: 8px 8px 8px 32px; */
      text-decoration: none;
      font-size: 25px;
      color: #818181;
      display: block;
      transition: 0.3s
      }
      
      /* When you mouse over the navigation links, change their color */
      .testo-sidenav a:hover, .offcanvas a:focus{
      color: #f1f1f1;
      }
      
      /* Position and style the close button (top right corner) */
      .testo-sidenav .closebtn {
      position: absolute;
      top: 0;
      right: 25px;
      font-size: 36px;
      margin-left: 50px;
      }
      
      /* Style page content - use this if you want to push the page content to the right when you open the side navigation */
      #testo-main {
      transition: margin-left .5s;
      padding: 20px;
      }
      .testo-sidenav {
      right: 0;
      }
      /* On smaller screens, where height is less than 450px, change the style of the sidenav (less padding and a smaller font size) */
      @media screen and (max-height: 450px) {
      .testo-sidenav {padding-top: 15px;}
      .testo-sidenav a {font-size: 18px;}
      }
      .testo-sidenav {
      right: 0;
      }
      .testo-openNavButton {
        position: fixed;
        top: 10px;
        right: 10px;
        height: 50px;
        line-height: 50px;  
        width: 50px;  
        font-size: 2em;
        font-weight: bold;
        border-radius: 50%;
        background-color: #c0c0c0;
        color: white;
        text-align: center;
        cursor: pointer;
        z-index: 100;
      }
      `;

      // Navbar content (Test Cases)
      let navbar = document.createElement('div');
      navbar.setAttribute('id','testo-mySidenav');
      navbar.classList.add('testo-sidenav');
      navbar.innerHTML += `
      <div id="showFetchedData" style="margin-left: 20px;"></div>
      <a id="testo-closeButton" href="javascript:void(0)" class="closebtn">&times;</a>`
                            /* <a href="#">ITEM 1</a>
                            <a href="#">ITEM 2</a>
                            <a href="#">ITEM 3</a>
                            <a href="#">ITEM 4</a>
                            <input type="text" id="data" />
                            <button onclick="sendData()">Send Data</button>`; */


      let testJs = document.createElement('script');
      testJs.src = chrome.runtime.getURL('api.js');
      testJs.onload = function () {
        this.remove();
      }
      document.head.appendChild(testJs);

      // Appending to body
      document.body.prepend(document.createAttribute('script').innerHTML = ``)
      document.body.prepend(navbarOpenButton);
      document.body.prepend(navbarStyle);
      document.body.prepend(navbar);
      
      // Navbar Event (close navbar when user clicks exit button)
      let navbarCloseButton = document.getElementById('testo-closeButton');
      navbarCloseButton.addEventListener('click', () => {
        document.getElementById("testo-mySidenav").style.width = "0";
      });

      // IMPORTANT NOTE: SHOWING CONTENT INSIDE NAVBAR IS THROUGH (API.JS) 
}



/* let mo = fetch('https://localhost:7221/api/CaseStudies').then(response => {
    return response.json();
    }).then(data => {
    // Work with JSON data here
        guideLines += data.guideLines;
        return guideLines;
    }).catch(err => {
    // Do something for an error here
    });

console.log(mo); */

/* async function fetchGuideLines() {
    let obj;
    const res = await fetch('https://localhost:7221/api/CaseStudies')
    obj = await res.json();
    return obj;
  } 
  
console.log(fetchGuideLines())*/