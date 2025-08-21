import Tab from 'bootstrap/js/dist/tab';
export const init = (tabs) => {
    let activeTabIndex = 0;
    
    tabs.forEach((tab, index) => {
        tab.addEventListener('shown.bs.tab', event => {
            activeTabIndex = index;
            
            //swap titles above tab nav
            const titleEls = document.querySelectorAll('.benefit-tabs-title');
            if (titleEls.length > 1) {
                titleEls.forEach((title, index) => {
                    const titleIndex = index;
                    title.classList.remove('active');
                    if (index === activeTabIndex) {
                        title.classList.add('active');
                    }
                })
            }
            
            //add classes that swap colors
            const tabsContainerEl = event.target.closest('.benefit-tabs');
            tabsContainerEl.classList.remove(...tabsContainerEl.classList);
            tabsContainerEl.classList.add(`active-tab-${index}`, 'benefit-tabs');
        })
    })
}