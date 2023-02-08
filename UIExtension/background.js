chrome.alarms.create({
    periodInMinutes: 1/60
});
chrome.alarms.onAlarm.addListener((alarm) => {
    chrome.storage.local.get(["timer","recordStatus"], (res) => {
        const status = res.recordStatus ?? false;
        let time = res.timer ?? 0; // the time will be added to storage
        if (status === true) {
            chrome.storage.local.set({
                timer: time + 1,
            });
            chrome.action.setBadgeText({
                text: `${time + 1}`
            })
        }
        else {
            chrome.storage.local.set({
                lastUsedTimer: time
            });
            chrome.action.setBadgeText({
                text: ``
            })
        }
    })
})




/*chrome.storage.onChanged.addListener((changes, namespace) => {
    const status = changes.recordStatus.newValue;
    for (let [key, { oldValue, newValue }] of Object.entries(changes)) {
        console.log(
          `Storage key "${key}" in namespace "${namespace}" changed.`,
          `Old value was "${oldValue}", new value is "${newValue}".`
        );
      }
    chrome.storage.local.get(["timer"], (res) => {
        let time = res.timer ?? 0;
        if (status === true) {
            chrome.alarms.onAlarm.addListener((alarm) => {
                console.log(alarm)
                chrome.storage.local.set({
                    timer: time + 1
                });
                chrome.action.setBadgeText({
                    text: `${time + 1}`
                });
            })
        }
    }) 
});
*/
