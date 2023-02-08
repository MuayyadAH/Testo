let timeElapsed_HTML = document.getElementById('time');
let timePerWebsite_HTML = document.getElementById('timePerWebsite');
let elements_HTML = document.getElementById('elements');
let countedElements_HTML = document.getElementById('countedElements');
let visitedSites_HTML = document.getElementById('visitedSites');

chrome.storage.local.get(["lastUsedTimer", "selectedElements","savedUrls"], (res) => {
    let savedUrls = res.savedUrls ?? []; // IMPORTANT: All the urls that been saved during recording
    // timer
    const lastTimer = res.lastUsedTimer ?? 0;
    let minutes = Math.floor(lastTimer/60);
    let seconds = lastTimer % 60;
    // timeElapsed_HTML.textContent = `${Math.floor(lastTimer/60)} minutes, ${lastTimer % 60} seconds`;
    timeElapsed_HTML.textContent = (Math.floor(lastTimer/60) === 0) ?
                                    `${lastTimer % 60} seconds` :
                                    `${Math.floor(lastTimer/60)} minutes, ${lastTimer % 60} seconds`;
    // average time spent per website
    let averageTime = lastTimer / savedUrls.length;
    timePerWebsite_HTML.innerHTML = (Math.floor(averageTime/60) === 0) ?
                                    `${(averageTime % 60)} seconds` :
                                    `${Math.floor(averageTime/60)} minutes, ${averageTime % 60} seconds`;


    // selected elements
    let displaySelectedElements = res.selectedElements ?? [];
    console.log(displaySelectedElements)

    /* START OF VISITED SITES AND COUNTING ELEMENT */
    // Visited sited section
    let cnt = 0;
    for (let i=0; i<savedUrls.length; i++) {
        cnt++;
        var anchorTag = document.createElement('a');
        anchorTag.href = savedUrls[i].fullUrl;
        anchorTag.innerHTML = `${savedUrls[i].baseUrl}`;
        anchorTag.target= '_blank';
        visitedSites_HTML.appendChild(anchorTag);
        if (cnt !== savedUrls.length) {
            visitedSites_HTML.appendChild(addArrowToRight());
        }
    }
    function addArrowToRight() {
        const arrowClasses = ["fa-solid","fa-angles-right","fa-lg","m-2"];
        var newArrow = document.createElement('i');
        newArrow.classList.add(...arrowClasses);
        return newArrow;
    }

    /* Counting elements and display them */
    let classListOfComponentBox = ["col-5","mb-4","componentBox","d-flex","align-items-center"]
    for (let i=0; i<displaySelectedElements.length; i++) {
        let counter = 0;
        let element = displaySelectedElements[i];
        for (let j=0; j<savedUrls.length; j++) {
            counter += savedUrls[j].elements[element]; // to count the value of all selected element
        }
        var componentBox = document.createElement('div');
        if (i %2 == 0) {
            componentBox.classList.add(...classListOfComponentBox);
        }
        else {
            componentBox.classList.add(...classListOfComponentBox);
            componentBox.classList.add("offset-1")
        }
        componentBox.innerHTML = `  <div class="circle me-4">
                                        <span class="fw-bold fs-5">${counter}</span>
                                    </div>
                                    <h3 class="fw-bold">${capitalize(element)} found</h3>`
        countedElements_HTML.appendChild(componentBox);
    }
    /* (END) VISITED SITES AND COUNTING ELEMENT */


    // for debugging
    console.log(`${lastTimer}`);
    console.log(`${displaySelectedElements}`);
    console.log(res.savedUrls);
}

//

);


// for elements capitalization
function capitalize(word) {
    return word[0].toUpperCase() + word.slice(1).toLowerCase();
}

/* (START) TEST CASES FUNCTIONALITY */
let checkboxes = document.querySelectorAll('input[type="checkbox"]');
let testcases = document.querySelectorAll('input[type="text"]')
let addNewTestCaseIcon = document.getElementById('addNewTestCase'); // Adding new test case icon
let testCasesListGroup = document.getElementById('testCasesListGroup');

// Displaying test cases upon report loading
chrome.storage.local.get(['testCases'], (res) => {
    let testCases = res.testCases ?? [];
    for (let i=0; i<testCases.length; i++) {
        let list = document.createElement('li')
        list.classList.add('list-group-item');
        if (testCases[i].isDisabled === true) {
            list.innerHTML = `<div class="input-group">
                                <div class="input-group-text">
                                    <input checked class="custom_checkbox mt-0" type="checkbox" value="" aria-label="Checkbox for following text input">
                                </div>
                                <input disabled type="text" class="form-control" placeholder="${testCases[i].testCaseContent}">
                            </div>`;
        }
        else {
            list.innerHTML = `<div class="input-group">
                                <div class="input-group-text">
                                    <input class="custom_checkbox mt-0" type="checkbox" value="" aria-label="Checkbox for following text input">
                                </div>
                                <input type="text" class="form-control" placeholder="${testCases[i].testCaseContent}">
                            </div>`;
        }

        testCasesListGroup.appendChild(list);
    }
})

// Changing test case status (enable or disable)
document.addEventListener('DOMContentLoaded', function() {
    let checkboxes = document.querySelectorAll('input[type="checkbox"]');
    let testcases = document.querySelectorAll('input[type="text"]')
    checkboxes.forEach((item, index) => {
        chrome.storage.local.get(['testCases'], (res) => {
            let testCasesArray = res.testCases ?? [];
            item.addEventListener('click', () => {
                if (item.checked) {
                    testcases[index].disabled = true;
                    testCasesArray[index].isDisabled = true;
                }
                else {
                    testcases[index].disabled = false;
                    testCasesArray[index].isDisabled = false;
                }
                chrome.storage.local.set({
                    testCases: testCasesArray
                })
            })
        })
    })
}, false)

addNewTestCaseIcon.addEventListener('click', () => {
    testCasesListGroup.appendChild(addNewTestCase());

})
function addNewTestCase() { // Adding new test case element to HTML and STORAGE
    let element = document.createElement('li')
    chrome.storage.local.get(['testCases'], (res) => {
        let testCasesArray = res.testCases ?? [];
        testCasesArray.push({
            testCaseContent: `Test case ${testCasesArray.length+1}`,
            isDisabled: false
        })
        chrome.storage.local.set({
            testCases: testCasesArray
        })
        element.classList.add('list-group-item');
        element.innerHTML = `<div class="input-group">
                                <div class="input-group-text">
                                    <input class="custom_checkbox mt-0" type="checkbox" value="" aria-label="Checkbox for following text input">
                                </div>
                                <input type="text" class="form-control" placeholder="Testcase ${testCasesArray.length}">
                            </div>`;
    })
    return element;
}

// Update test cases after typing the test case
document.addEventListener('DOMContentLoaded', function() {
    var typingTimer;                //timer identifier
    var doneTypingInterval = 1000;  //time in ms, 5 seconds for example
    let testcases = document.querySelectorAll('input[type="text"]')
    testcases.forEach((item, index) => {
        let i = index
        item.addEventListener('keyup', () => {
            clearTimeout(typingTimer)
            typingTimer = setTimeout((i) => {
                console.log(item.value)
                chrome.storage.local.get(['testCases'], (res) => {
                    let testCasesArray = res.testCases ?? [];
                    console.log(testCasesArray)
                    testCasesArray[index].testCaseContent = `${item.value}`;
                    chrome.storage.local.set({
                        testCases: testCasesArray
                    })
                })
            }, doneTypingInterval)
        })
        item.addEventListener('keydown', () => {
            clearTimeout(typingTimer);
        })
    })



    /* chrome.alarms.create({
        periodInMinutes: 1/60
    });
    chrome.alarms.onAlarm.addListener((alarm) => {
        chrome.storage.local.get(['testCases'], (res) => {
            let testCases = res.testCases;
                console.log(testCases)
        })
    }) */
}, false)
