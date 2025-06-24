import gsap from 'gsap';

// === Constants ===
const ringLabelsContainer = document.querySelector(".ring-labels");
const sliderContainer = document.querySelector(".diagram__card-slider");
const rings = document.querySelectorAll(".ring");
const groups = document.querySelectorAll(".subcircle-group");
//const parsedJSON = JSON.parse(document.getElementById("ring-data")?.textContent?.trim());
const raw = document.getElementById("ring-data")?.textContent?.trim();
const cleaned = raw ?
    raw.replace(/^'/, '')
        .replace(/';?$/, '')
        .replace(/};?%/, '}')
        .trim() ; { };

let parsedJSON = null;
try {
    parsedJSON = JSON.parse(cleaned);
} catch (err) {
    console.error("Failed to parse JSON from ring-data:", err, cleaned);
}
const introData = parsedJSON;
const ringContentData = parsedJSON.Rings.reverse();
const dotConfigs = [
    { scale: 0.166, angles: { desktop: [265, 170, 85], mobile: [265, 170, 80] } },
    { scale: 0.33, angles: { desktop: [270, 176, 85], mobile: [210, 180, 150] } },
    { scale: 0.5, angles: { desktop: [278, 238, 198, 158, 118, 78], mobile: [225, 205, 185, 165, 145, 125] } }
];

// === State ===
let activeRingIndex = null;
let activeSubcircleEl = null;
let mobileSwiper = null;
const ringSubcircleState = new Map();
let labelsHaveAnimated = false;

// === Utility ===
const isMobile = () => window.matchMedia("(max-width: 767px)").matches;

const getAllSubcircles = () =>
    [...groups].slice().reverse().flatMap(group => [...group.querySelectorAll(".subcircle")]);

const getPlusIconSVG = () => `
  <svg class="accordion-icon" xmlns="http://www.w3.org/2000/svg" width="32" height="33" viewBox="0 0 32 33" fill="none">
    <circle cx="16" cy="16.199" r="15" stroke-width="2"/>
    <rect class="icon-bar-horizontal" x="6.58789" y="17.1404" width="1.88235" height="18.8235" transform="rotate(-90 6.58789 17.1404)" fill="#FFF"/>
    <rect class="icon-bar-vertical" x="15.0586" y="7" width="1.88235" height="18.8235" fill="#FFF"/>
  </svg>`;

const getArrowIconSVG = () => `<svg xmlns="http://www.w3.org/2000/svg" width="17" height="16" viewBox="0 0 17 16" fill="none">
        <path d="M7.79289 0.292893C8.18342 -0.0976311 8.81658 -0.0976311 9.20711 0.292893L16.2071 7.29289C16.5976 7.68342 16.5976 8.31658 16.2071 8.70711L9.20711 15.7071C8.81658 16.0976 8.18342 16.0976 7.79289 15.7071C7.40237 15.3166 7.40237 14.6834 7.79289 14.2929L13.0858 9L1.5 9C0.947715 9 0.5 8.55228 0.5 8C0.5 7.44771 0.947715 7 1.5 7L13.0858 7L7.79289 1.70711C7.40237 1.31658 7.40237 0.683417 7.79289 0.292893Z" fill="white"/>
      </svg>`;

function createDot(angle, distance, width = 26, label = null, ringIndex, dotIndex, theme) {
    const wrapper = document.createElement("div");
    wrapper.className = "dot-wrapper";
    const correctedAngle = angle - 90;
    wrapper.style.setProperty("--angle", `${correctedAngle}deg`);
    wrapper.style.setProperty("--distance", `${distance}px`);
    wrapper.style.setProperty("--width", `${width}px`);

    const dot = document.createElement("div");
    dot.className = "subcircle";
    dot.dataset.subcircleId = `ring${ringIndex + 1}-dot${dotIndex + 1}`;

    if (label) {
        dot.classList.add("has-label");

        const labelEl = document.createElement("div");
        labelEl.className = "circle-label";
        labelEl.innerHTML = label;
        labelEl.style.transform = `rotate(${-correctedAngle}deg)`;
        labelEl.dataset.topic = label.toLowerCase();
        labelEl.style.setProperty("--theme-color", theme);
        dot.appendChild(labelEl);
    }

    wrapper.appendChild(dot);
    return wrapper;
}

function renderRingDots() {
    const wrapperSize = document.querySelector(".ring-wrapper").offsetWidth;
    groups.forEach(group => (group.innerHTML = ""));

    dotConfigs.forEach(({ scale, angles }, ringIndex) => {
        const distance = wrapperSize * scale;
        const ringData = ringContentData[ringIndex];
        const theme = ringData.ThemeCssColor;
        const cards = ringData.Cards || [];
        const angleList = isMobile() ? angles.mobile : angles.desktop;

        angleList.forEach((angle, i) => {
            if (cards.length >= i + 1) {
                const label = cards[i]?.cardTitle || null;
                groups[ringIndex].appendChild(
                    createDot(angle, distance, 26, label, ringIndex, i, theme)
                );
            }
        });
    });
}

function renderRingLabels(callback) {
    ringLabelsContainer.innerHTML = "";
    const wrapperSize = document.querySelector(".ring-wrapper").offsetWidth;

    const labelEls = ringContentData.map((ring, i) => {
        const label = document.createElement("div");
        label.className = "ring-label";
        label.style.setProperty("--theme-color", ring.ThemeCssColor);
        label.dataset.ring = (i + 1).toString();
        label.setAttribute("role", "button");
        label.setAttribute("tabindex", "0");
        label.setAttribute("aria-pressed", "false");

        label.innerHTML = `${ring.ringLabel} <span class="icon">${getPlusIconSVG()}</span>`;

        const distance = wrapperSize * dotConfigs[i].scale;
        const angleRad = -Math.PI / 2;
        label.style.left = `calc(50% + ${Math.cos(angleRad) * distance}px)`;
        label.style.top = `calc(48% + ${Math.sin(angleRad) * distance}px)`;
        label.style.opacity = 0;

        label.addEventListener("keydown", e => {
            if (e.key === "Enter" || e.key === " ") {
                e.preventDefault();
                setActiveRing(i);
            }
        });

        ringLabelsContainer.appendChild(label);
        return label;
    });

    document.querySelectorAll('.ring-label').forEach(label => {
        const ringIndex = label.dataset.ring;
        const ringName = label.textContent.trim();
        label.setAttribute('aria-label', `${ringName}, activate ring ${ringIndex}`);
    });

    callback?.(labelEls);
}

/*function playRingAnimation(labelEls) {
    const tl = gsap.timeline({ defaults: { ease: "power2.out", duration: 1 } });
    const anyRingIsActive = [...rings].some(r => r.classList.contains("active"));

    rings.forEach((ring, i) => {
        const isActive = ring.classList.contains("active");
        const opacity = !anyRingIsActive || isActive ? 1 : 0.2;
        tl.to(ring, { scale: 1, opacity }, i * 0.5);
    });

    if (!labelsHaveAnimated) {
        labelEls.slice().reverse().forEach((el, i) => {
            tl.fromTo(el, { opacity: 0 }, { opacity: 1, duration: 0.5 }, `+=${0.1 * i}`);
        });
        labelsHaveAnimated = true;
    } else {
        labelEls.forEach(el => gsap.set(el, { opacity: 1 }));
    }
}*/

function clearActiveRing() {
    rings.forEach(r => r.classList.remove("active"));
    groups.forEach(g => gsap.set(g.querySelectorAll(".subcircle"), { opacity: 0, scale: 0 }));
    document.querySelectorAll(".ring-label").forEach(label => label.setAttribute("aria-pressed", "false"));
}

function swapDiagramCards(ringIndex) {
    const primaryCard = document.querySelector("[data-primaryCard]");

    if (primaryCard && primaryCard.getAttribute("aria-hidden") === "false") {
        primaryCard.setAttribute("aria-hidden", "true");
    }

    document.querySelectorAll('[data-ringCard]').forEach(card => {
        const cardIndex = parseInt(card.dataset.ringcard, 10) - 1;
        const isActive = cardIndex === ringIndex;
        card.setAttribute('aria-hidden', String(!isActive));
    });

    renderDiagramCard(ringIndex);
}

function stripHTML(htmlString) {
    const div = document.createElement("div");
    div.innerHTML = htmlString;
    return div.textContent || div.innerText || "";
}

export function setActiveRing(index) {
    clearActiveRing();
    activeRingIndex = index;

    const themeColor = ringContentData[index]?.ThemeCssColor || '#000000';

    if (themeColor) {
        document.querySelector(".diagram__card-container")?.style.setProperty("--theme-color", themeColor);
        document.querySelector(".ring-wrapper")?.style.setProperty("--theme-color", themeColor);
    }

    swapDiagramCards(index);

    rings.forEach((ring, i) => gsap.to(ring, { opacity: i === index ? 1 : 0.2 }));
    rings[index].classList.add("active");

    const dots = groups[index].querySelectorAll(".subcircle");
    gsap.to(dots, { opacity: 1, scale: 1, stagger: 0.05, duration: 0.5 });

    document.querySelectorAll("[data-ring]").forEach(el => {
        el.classList.remove("active");
        el.setAttribute("aria-pressed", "false");
    });

    const activeLabel = document.querySelector(`.ring-label[data-ring="${index + 1}"]`);
    if (activeLabel) {
        activeLabel.classList.add("active");
        activeLabel.setAttribute("aria-pressed", "true");
    }

    const storedId = ringSubcircleState.get(index);
    const found = storedId ? [...groups[index].querySelectorAll(".subcircle")].find(
        el => el.dataset.subcircleId === storedId
    ) : null;

    setActiveSubcircle(found || dots[0]);
}

function setActiveSubcircle(el) {
    if (!el?.classList.contains("subcircle")) return;

    activeSubcircleEl?.classList.remove("active");
    el.classList.add("active");
    activeSubcircleEl = el;

    const subcircleId = el.dataset.subcircleId;
    const ringIndex = [...groups].indexOf(el.closest(".subcircle-group"));
    if (ringIndex !== -1 && subcircleId) {
        ringSubcircleState.set(ringIndex, subcircleId);
    }

    const label = el.querySelector(".circle-label");
    const topic = label?.dataset.topic;

    if (topic && ringContentData[ringIndex]) {
        const { Cards } = ringContentData[ringIndex];
        const matchingIndex = Cards.findIndex(card => card.cardTitle.toLowerCase() === topic);

        const currentCard = document.querySelector(`.diagram__card[data-ringCard="${ringIndex + 1}"]`);
        const currentTitle = currentCard?.querySelector('.diagram__card-title')?.textContent.trim().toLowerCase();

        if (matchingIndex !== -1 && currentTitle !== topic) {
            renderDiagramCard(ringIndex, matchingIndex);
        } else if (matchingIndex !== -1) {
            // update copy and read more UI without full rerender
            const copy = currentCard.querySelector('.diagram__card-copy');
            const existingReadMore = currentCard.querySelector('.diagram__card-readmore');

            const fullCopy = stripHTML(Cards[matchingIndex].cardCopy);
            const truncated = fullCopy.length > 130 ? fullCopy.slice(0, 130).trim() + "…" : fullCopy;
            const copyId = copy.id || `copy-ring${ringIndex + 1}-card${matchingIndex}`;

            copy.textContent = isMobile() && fullCopy.length > 130 ? truncated : fullCopy;
            copy.id = copyId;

            if (isMobile() && fullCopy.length > 130) {
                if (!existingReadMore) {
                    const newReadMore = createReadMoreToggle({
                        fullCopy,
                        truncated,
                        copyElem: copy,
                        copyId
                    });
                    currentCard.insertBefore(newReadMore, currentCard.querySelector('.diagram__card-btn-container'));
                }
            } else {
                if (existingReadMore) existingReadMore.remove();
            }
        }
    }
}

function goToSubcircleByOffset(offset) {
    const allDots = getAllSubcircles();
    const currentIndex = allDots.indexOf(activeSubcircleEl);
    if (currentIndex === -1) return;

    const newIndex = (currentIndex + offset + allDots.length) % allDots.length;
    const targetDot = allDots[newIndex];
    if (!targetDot) return;

    const ringEl = targetDot.closest(".subcircle-group");
    const newRingIndex = [...groups].indexOf(ringEl);

    // Sync Swiper on mobile
    if (newRingIndex !== activeRingIndex) {
        setActiveRing(newRingIndex);

        if (isMobile() && mobileSwiper) {
            const slideIndex = ringContentData.length - 1 - newRingIndex;
            mobileSwiper.slideTo(slideIndex);
        }
    }

    setActiveSubcircle(targetDot);
}

function bindSubcircleClicks() {
    document.body.addEventListener("click", e => {
        const target = e.target.closest(".subcircle");
        if (target) setActiveSubcircle(target);
    });
}

function playRingAnimation(labelEls) {
    const tl = gsap.timeline({ defaults: { ease: "power2.out", duration: 1 } });
    const anyRingIsActive = [...rings].some(ring => ring.classList.contains("active"));

    rings.forEach((ring, i) => {
        const isActive = ring.classList.contains("active");
        const opacity = !anyRingIsActive || isActive ? 1 : 0.2;
        tl.to(ring, { scale: 1, opacity }, i * 0.5);
    });

    if (!labelsHaveAnimated) {
        labelEls.slice().reverse().forEach((el, i) => {
            tl.fromTo(
                el,
                { opacity: 0 },
                { opacity: 1, duration: 0.5 },
                `+=${0.1 * i}`
            );
        });
        labelsHaveAnimated = true;
    } else {
        labelEls.forEach(el => gsap.set(el, { opacity: 1 }));
    }
}

function bindRingLabelClicks() {
    document.querySelectorAll("[data-ring]").forEach(btn => {
        btn.addEventListener("click", () => {
            const index = parseInt(btn.dataset.ring) - 1;
            if (isNaN(index)) return;

            if (index === activeRingIndex) {
                // Clicking the same active ring again — reset state
                clearActiveRing();
                activeRingIndex = null;
                activeSubcircleEl = null;

                // Reset cards
                document.querySelectorAll('[data-ringCard]').forEach(card => {
                    card.setAttribute('aria-hidden', 'true');
                });
                document.querySelector('[data-primaryCard]')?.setAttribute('aria-hidden', 'false');

                // Restore ring visuals
                rings.forEach(r => gsap.to(r, { opacity: 1 }));
                document.querySelectorAll('.ring-label').forEach(l => l.classList.remove('active'));
                return;
            }

            setActiveRing(index);

            if (mobileSwiper && isMobile()) {
                const slideIndex = ringContentData.length - 1 - index;
                mobileSwiper.slideTo(slideIndex);
            }
        });
    });
}

function enableSubcircleArrowNavigation() {
    document.addEventListener("keydown", (e) => {
        if (!activeSubcircleEl) return;

        // Left or Up = backward, Right or Down = forward
        if (e.key === "ArrowLeft" || e.key === "ArrowUp") {
            e.preventDefault();
            goToSubcircleByOffset(-1);
        } else if (e.key === "ArrowRight" || e.key === "ArrowDown") {
            e.preventDefault();
            goToSubcircleByOffset(1);
        }
    });
}

export function initSwiperIfMobile() {
    const isNowMobile = isMobile();
    const sliderEl = document.querySelector("[data-mobile-slider]");

    if (isNowMobile && !mobileSwiper) {
        mobileSwiper = new Swiper(sliderEl, {
            slidesPerView: 1.15,
            spaceBetween: 16,
            centeredSlides: true,
            pagination: {
                el: ".swiper-pagination",
                clickable: true
            }
        });

        mobileSwiper.on("slideChange", () => {
            const slideIndex = mobileSwiper.activeIndex;
            const ringIndex = ringContentData.length - 1 - slideIndex;
            if (ringIndex !== activeRingIndex) {
                setActiveRing(ringIndex);
            }
        });

        if (activeRingIndex !== null) {
            const slideIndex = ringContentData.length - 1 - activeRingIndex;
            mobileSwiper.slideTo(slideIndex, 0);
        }
    }

    if (!isNowMobile && mobileSwiper) {
        mobileSwiper.destroy(true, true);
        mobileSwiper = null;
    }
}

function createReadMoreToggle({ fullCopy, truncated, copyElem, copyId }) {
    const readMore = document.createElement("button");
    readMore.className = "diagram__card-readmore";
    readMore.innerHTML = `Read more<span class="icon">${getPlusIconSVG()}</span>`;
    readMore.setAttribute("aria-expanded", "false");
    readMore.setAttribute("aria-controls", copyId);
    readMore.setAttribute("aria-label", "Expand full description");

    readMore.addEventListener("click", () => {
        const expanded = readMore.getAttribute("aria-expanded") === "true";
        copyElem.textContent = expanded ? truncated : fullCopy;
        readMore.firstChild.textContent = expanded ? "Read more" : "Read less";
        readMore.querySelector("span.icon")?.classList.toggle("expanded", !expanded);
        readMore.setAttribute("aria-expanded", String(!expanded));
    });

    return readMore;
}

let MAX_CARD_HEIGHT = 0;

function measureMaxDiagramCardHeight() {
    const hiddenCards = document.querySelectorAll('.diagram__card[aria-hidden="true"]');
    if (!hiddenCards.length) return;

    let tallest = 0;
    const originalStyles = [];

    hiddenCards.forEach(card => {
        // Store original styles
        originalStyles.push({
            el: card,
            display: card.style.display,
            visibility: card.style.visibility,
            position: card.style.position
        });

        // Temporarily reveal and remove layout constraints
        card.style.visibility = 'hidden';
        card.style.display = 'block';
        card.style.position = 'absolute';

        // Now measure
        const height = card.offsetHeight;
        if (height > tallest) tallest = height;
    });

    // Revert styles
    originalStyles.forEach(({ el, display, visibility, position }) => {
        el.style.display = display;
        el.style.visibility = visibility;
        el.style.position = position;
    });

    MAX_CARD_HEIGHT = tallest;
    document.documentElement.style.setProperty('--max-card-height', `${tallest}px`);
}

function setCircleLabelMinWidths() {
    document.querySelectorAll('.circle-label').forEach(label => {
        // Create a temporary span for measurement
        const tempSpan = document.createElement('span');
        tempSpan.style.position = 'absolute';
        tempSpan.style.visibility = 'hidden';
        tempSpan.style.whiteSpace = 'nowrap';
        tempSpan.style.font = getComputedStyle(label).font;
        tempSpan.style.fontWeight = getComputedStyle(label).fontWeight;
        tempSpan.style.fontSize = getComputedStyle(label).fontSize;
        tempSpan.textContent = label.textContent;

        document.body.appendChild(tempSpan);

        let width = tempSpan.getBoundingClientRect().width;
        width = width > 200 ? 220 : width > 145 ? width : width + 40;
        label.style.minWidth = `${Math.ceil(width)}px`; // add padding buffer if needed (1em left/right = ~16px)

        document.body.removeChild(tempSpan);
    });
}

function renderDiagramCard(ringIndex, defaultCardIndex = 0) {
    const ringData = ringContentData[ringIndex];
    if (!ringData) return;

    const { Cards, ThemeCssColor, ringShortLabel } = ringData;
    const currentCard = Cards[defaultCardIndex];
    const primaryCard = document.querySelector('[data-primaryCard]');
    const card = document.querySelector(`.diagram__card[data-ringCard="${ringIndex + 1}"]`);
    card.style.minHeight = `${MAX_CARD_HEIGHT}px`;

    if (!card) return;

    // Apply theme color
    card.style.setProperty('--theme-color', ThemeCssColor);

    // Show/hide cards appropriately
    if (primaryCard.getAttribute('aria-hidden') === "true") {
        if (isMobile()) {
            document.querySelectorAll('.diagram__card[data-ringCard]').forEach(c => c.setAttribute('aria-hidden', 'false'));
        } else {
            document.querySelectorAll('.diagram__card[data-ringCard]').forEach(c => c.setAttribute('aria-hidden', 'true'));
            card.setAttribute('aria-hidden', 'false');
        }
    }

    const transitionWrapper = document.createElement("div");
    transitionWrapper.className = "diagram__card-transition";
    transitionWrapper.style.opacity = "0";

    // Clear card content
    card.innerHTML = '';

    // === Header ===
    const header = document.createElement("div");
    header.className = "diagram__card-header mb-2";

    const title = document.createElement("h4");
    title.className = "diagram__card-title me-3";
    title.textContent = currentCard.cardTitle;

    const badge = document.createElement("div");
    badge.className = "diagram__card-badge col-auto";
    badge.textContent = ringShortLabel;

    header.appendChild(title);
    header.appendChild(badge);
    transitionWrapper.appendChild(header);

    // === Copy with mobile "Read more" toggle ===
    const fullCopy = stripHTML(currentCard.cardCopy);
    const copy = document.createElement("p");
    const copyId = `copy-ring${ringIndex + 1}-card${defaultCardIndex}`;

    copy.className = "diagram__card-copy";
    copy.id = copyId;

    const truncated = fullCopy.length > 130 ? fullCopy.slice(0, 130).trim() + "…" : fullCopy;

    if (isMobile() && fullCopy.length > 130) {
        copy.textContent = truncated;

        const readMore = createReadMoreToggle({
            fullCopy,
            truncated,
            copyElem: copy,
            copyId
        });

        transitionWrapper.appendChild(copy);
        transitionWrapper.appendChild(readMore);
    } else {
        copy.textContent = fullCopy;
        transitionWrapper.appendChild(copy);
    }
    card.appendChild(transitionWrapper);
    gsap.to(transitionWrapper, {
        opacity: 1,
        duration: 0.4,
        ease: "linear",
        // delay: 0.05
    });

    // === Secondary Buttons ===
    const btnContainer = document.createElement("div");
    btnContainer.className = "diagram__card-btn-container";

    Cards.forEach((cardObj, index) => {
        const btn = document.createElement("button");
        btn.className = "diagram__card-btn diagram__card-btn--secondary text-start";
        btn.textContent = cardObj.cardTitle;
        btn.dataset.cardIndex = index;

        btn.addEventListener("click", () => {
            title.textContent = cardObj.cardTitle;

            const newFull = stripHTML(cardObj.cardCopy);
            const newTrunc = newFull.length > 130 ? newFull.slice(0, 130).trim() + "…" : newFull;

            if (isMobile() && newFull.length > 130) {

                copy.textContent = newTrunc;
                let readMore = card.querySelector(".diagram__card-readmore");

                if (!readMore) {
                    readMore = document.createElement("button");
                    readMore.className = "diagram__card-readmore";
                    readMore.innerHTML = `Read more<span class="icon">${getPlusIconSVG()}</span>`;
                    readMore.setAttribute("aria-expanded", "false");
                    readMore.setAttribute("aria-controls", copyId);
                    readMore.setAttribute('aria-label', "Expand full description");
                    card.insertBefore(readMore, card.querySelector('.diagram__card-btn-container'));
                }

                readMore.onclick = () => {
                    const expanded = readMore.getAttribute("aria-expanded") === "true";
                    copy.textContent = expanded ? newTrunc : newFull;
                    readMore.setAttribute("aria-expanded", String(!expanded));
                    readMore.firstChild.textContent = expanded ? "Read more" : "Read less";
                    readMore.querySelector("span.icon")?.classList.toggle("expanded", !expanded);
                };


            } else {
                copy.textContent = newFull;
                const existing = card.querySelector(".diagram__card-readmore");
                if (existing) existing.remove();
            }

            [...btnContainer.children].forEach(b => b.classList.remove("active"));
            btn.classList.add("active");

            const group = groups[ringIndex];
            if (group) {
                const topic = cardObj.cardTitle.toLowerCase();
                const match = [...group.querySelectorAll(".subcircle")].find(dot => {
                    const label = dot.querySelector(".circle-label");
                    return label?.dataset.topic === topic;
                });

                if (match) {
                    setActiveSubcircle(match);
                }
            }
        });

        if (index === defaultCardIndex) btn.classList.add("active");
        btnContainer.appendChild(btn);
    });

    card.appendChild(btnContainer);

    // === Desktop Nav Buttons ===
    if (!isMobile()) {
        const desktopNav = document.createElement("div");
        desktopNav.className = "diagram__card-nav d-none d-md-flex justify-content-between mt-auto w-100";

        const prevBtn = document.createElement("button");
        prevBtn.className = "diagram__card-btn diagram__card-btn--nav diagram__card-btn--nav-previous";
        prevBtn.innerHTML = `${getArrowIconSVG()} Previous`;
        prevBtn.addEventListener("click", () => goToSubcircleByOffset(-1));

        const nextBtn = document.createElement("button");
        nextBtn.className = "diagram__card-btn diagram__card-btn--nav";
        nextBtn.innerHTML = `Next
      ${getArrowIconSVG()}`;
        nextBtn.addEventListener("click", () => goToSubcircleByOffset(1));

        desktopNav.appendChild(prevBtn);
        desktopNav.appendChild(nextBtn);
        card.appendChild(desktopNav);
    }
}


function renderPrimaryCard(intro, ringsData) {
    const card = document.querySelector('[data-primaryCard]');
    if (!card) return;

    card.innerHTML = ''; // clear existing content

    const title = document.createElement('h4');
    title.className = 'diagram__card-title';
    title.textContent = intro?.introTitle || 'Welcome';

    const copy = document.createElement('p');
    copy.className = 'diagram__card-copy';
    copy.textContent = intro?.introCopy || '';

    const btnContainer = document.createElement('div');
    btnContainer.className = 'diagram__card-btn-container';

    const total = ringsData.length;
    for (let i = 0; i < total; i++) {
        const displayIndex = total - i;
        const ring = ringsData[displayIndex - 1];

        const btn = document.createElement('button');
        btn.className = 'diagram__card-btn mb-2 text-start';
        btn.dataset.ring = displayIndex;
        btn.style.setProperty('--theme-color', ring.ThemeCssColor);
        btn.innerHTML = `${ring.ringLabel}
      ${getArrowIconSVG()}`;

        btnContainer.appendChild(btn);
    }

    card.appendChild(title);
    card.appendChild(copy);
    card.appendChild(btnContainer);
}

function initialize() {
    renderPrimaryCard(introData, ringContentData);
    renderRingDots();
    renderRingLabels(labelEls => {
        playRingAnimation(labelEls);
        bindSubcircleClicks();
        bindRingLabelClicks();
        enableSubcircleArrowNavigation();

        document.querySelectorAll('[data-ring]').forEach(btn => {
            btn.setAttribute('tabindex', '0');
            btn.setAttribute('role', 'button');
            btn.setAttribute('aria-pressed', 'false');
            btn.addEventListener('keydown', (e) => {
                if (e.key === 'Enter' || e.key === ' ') {
                    e.preventDefault();
                    btn.click();
                }
            });
        });

        ringContentData.forEach((_, ringIndex) => {
            renderDiagramCard(ringIndex);
        });

        if (activeRingIndex !== null) {
            setActiveRing(activeRingIndex);
        }

        setCircleLabelMinWidths();

        initSwiperIfMobile();
    });
}

// initialize();
// measureMaxDiagramCardHeight();

/*window.addEventListener("resize", () => {
    clearTimeout(window._resizeTimer);
    window._resizeTimer = setTimeout(() => {
        const currentRing = activeRingIndex;
        initialize();
        if (currentRing !== null) {
            setActiveRing(currentRing); // preserve state
        }
        initSwiperIfMobile();
    }, 200);
});*/

let _resizeListenerInitialized = false;

export function setupDiagram() {
    if (!document.querySelector('#ring-data')) return;
    initialize();
    measureMaxDiagramCardHeight();

    if (!_resizeListenerInitialized) {
        window.addEventListener("resize", () => {
            clearTimeout(window._resizeTimer);
            window._resizeTimer = setTimeout(() => {
                const currentRing = document.querySelector('.ring.active');
                const currentRingIndex = currentRing ? [...document.querySelectorAll('.ring')].indexOf(currentRing) : null;

                initialize();
                measureMaxDiagramCardHeight();

                if (currentRingIndex !== null) {
                    setActiveRing(currentRingIndex);
                }

                initSwiperIfMobile();
            }, 200);
        });
        _resizeListenerInitialized = true;
    }
}