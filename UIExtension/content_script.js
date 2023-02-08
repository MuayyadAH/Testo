if(document.readyState !== 'complete') {
    window.addEventListener('load',contnet_script);
} else {
    contnet_script();
}

function contnet_script() {
    chrome.storage.local.get(["recordStatus", "selectedElements","savedUrls"], (res) => {
        const recordStatus = res.recordStatus;
        const test = res.savedUrls ?? [];
        let selectedElementsArray = res.selectedElements ?? [];
        console.log(test);
    
        if (recordStatus === true) {
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
        }
    
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


      /* START -- VIEW TEST CASE INSIDE THE PAGES */

      // Navbar open button
      let navbarOpenButton = document.createElement('div');
      navbarOpenButton.classList.add('testo-openNavButton');
      // navbarOpenButton.innerHTML = `<img src="test_icon_no_bg.png"></img>`
      navbarOpenButton.innerHTML += `<img src="${chrome.runtime.getURL('testo_icon_no_bg.png')}" style="height: 28px; width: 28px; margin-top: 20%;"/>`;
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
      z-index: 1; /* Stay on top */
      top: 10%;
      background-color: #bcbcbc; /* background color of cases*/
      overflow-x: hidden; /* Disable horizontal scroll */
      padding-top: 60px; /* Place content 60px from the top */
      transition: 0.5s; /* 0.5 second transition effect to slide in the sidenav */
      }
      
      /* The navigation menu links */
      .testo-sidenav a {
      padding: 8px 8px 8px 32px;
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
      }
      `;

      // Navbar content (Test Cases)
      let navbar = document.createElement('div');
      navbar.setAttribute('id','testo-mySidenav');
      navbar.classList.add('testo-sidenav');
      navbar.innerHTML += `<a id="testo-closeButton" href="javascript:void(0)" class="closebtn">&times;</a>
                            <a href="#">ITEM 1</a>
                            <a href="#">ITEM 2</a>
                            <a href="#">ITEM 3</a>
                            <a href="#">ITEM 4</a>`;

      // Appending to body
      document.body.appendChild(navbarOpenButton)
      document.body.appendChild(navbarStyle);
      document.body.appendChild(navbar);

      
      // Navbar Event (close navbar when user clicks exit button)
      let navbarCloseButton = document.getElementById('testo-closeButton');
      navbarCloseButton.addEventListener('click', () => {
        document.getElementById("testo-mySidenav").style.width = "0";
      });


}
