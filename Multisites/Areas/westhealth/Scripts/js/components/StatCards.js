import { matchBreakpoint } from '../utils';
import Swiper from 'swiper';
import { Pagination } from "swiper/modules";
import 'swiper/css';
import 'swiper/css/pagination';
import gsap from 'gsap';

let swiperInstance;
const breakpoint = matchBreakpoint('lt-lg');
function enableSwiper() {
    const container = document.querySelector('.js-stat-card-slider');
    if (!container || container.classList.contains('swiper-initialized')) return;
    const row = container.querySelector('.row');
    row.classList.add('swiper-wrapper');
    container.classList.add('swiper');
    row.classList.remove('row');// remove Bootstrap row so Swiper styles apply

    const slides = container.querySelectorAll('.js-stat-card-slide');

    slides.forEach(slide => {
        slide.removeAttribute('style');
        slide.classList.add('swiper-slide');
    });


    // setTimeout(() => {
    swiperInstance = new Swiper(container, {
        modules: [Pagination],
        slidesPerView: 1.15,
        spaceBetween: 16,
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
        breakpoints: {
            // when window width is >= 700px
            // so cards don't get too wide before we show the desktop view
            700: {
                slidesPerView: 2.15
            },
        }
    });
    // }, 3000);
}

function destroySwiper() {
    const container = document.querySelector('.js-stat-card-slider');
    if (!container || !swiperInstance) return;

    swiperInstance.destroy(true, true);
    swiperInstance = undefined;

    const slides = container.querySelectorAll('.swiper-slide');
    slides.forEach(slide => {
        slide.classList.remove('swiper-slide');
    });

    container.classList.remove('swiper', 'swiper-initialized');
    const row = container.querySelector('.swiper-wrapper');
    row.classList.add('row');
    row.classList.remove('swiper-wrapper'); // restore Bootstrap layout
}

let maxFullHeight = 0;
function handleHoverState(cards) {
    const destroyFns = [];

    cards.forEach(card => {
        const wrapper = card.parentElement;
        const content = card.querySelector(".card-content-animate");
        const svg = card.querySelector("svg");



        // Clear styles to measure natural height
        gsap.set(content, { clearProps: "all" });
        const cardHeight = card.scrollHeight;

        if (cardHeight > maxFullHeight) {
            maxFullHeight = cardHeight;
        }

        // Measure sizes
        const collapsedHeight = maxFullHeight * 0.9;
        const svgOriginalHeight = svg.getBoundingClientRect().height;
        const contentOriginalHeight = content.getBoundingClientRect().height;

        // Set measured height as baseline
        wrapper.style.height = `${maxFullHeight}px`;
        card.style.maxHeight = `${collapsedHeight}px`;
        content.style.height = 0;

        // Initial GSAP setup
        gsap.set(content, { autoAlpha: 0, y: 20 });
        gsap.set(svg, { autoAlpha: 1, y: 0 });

        // Hover-in handler
        const onMouseEnter = () => {
            gsap.to(card, {
                maxHeight: maxFullHeight,
                duration: 0.2,
                ease: "power1.in"
            });

            gsap.to(svg, {
                autoAlpha: 0,
                marginTop: -20,
                height: 0,
                duration: 0.1,
                ease: "power1.in"
            });

            gsap.to(content, {
                autoAlpha: 1,
                y: 0,
                height: contentOriginalHeight,
                duration: 0.2,
                ease: "power1.in"
            });
        };

        // Hover-out handler
        const onMouseLeave = () => {
            gsap.to(svg, {
                autoAlpha: 1,
                marginTop: 0,
                height: svgOriginalHeight,
                duration: 0.1,
                ease: "power1.in"
            });

            gsap.to(content, {
                autoAlpha: 0,
                y: 20,
                height: 0,
                duration: 0.2,
                ease: "power1.in"
            });

            gsap.to(card, {
                maxHeight: collapsedHeight,
                duration: 0.2,
                ease: "power1.in"
            });
        };

        // Add event listeners
        card.addEventListener("mouseenter", onMouseEnter);
        card.addEventListener("mouseleave", onMouseLeave);

        // Store destroy function for this card
        destroyFns.push(() => {
            card.removeEventListener("mouseenter", onMouseEnter);
            card.removeEventListener("mouseleave", onMouseLeave);
            gsap.killTweensOf([card, content, svg]);
            gsap.set([card, content, svg], { clearProps: "all" });
        });
    });

    // Return a single function to clean up all cards
    return () => {
        destroyFns.forEach(fn => fn());
    };
}

let destroyHover = null;

const initHover = (cards) => {
    if (destroyHover) {
        destroyHover(); // Clean up old hover state
    }
    destroyHover = handleHoverState(cards); // Recalculate and rebind
};

const debounce = (fn, delay) => {
    let timer;
    return (...args) => {
        clearTimeout(timer);
        timer = setTimeout(() => fn(...args), delay);
    };
};

// Resize handler
const handleResize = debounce(() => {
    if (destroyHover) {
        initHover(cardsRef);
    }
}, 200);


function handleBreakpointChange(e, cards) {
    if (e.matches) {
        // Mobile mode: Enable Swiper and destroy hover animations
        if (destroyHover) {
            destroyHover(); // remove all hover effects
            destroyHover = null;
            window.removeEventListener("resize", handleResize);
        }
        setTimeout(() => {
            enableSwiper();
        }, 200);

    } else {
        // Desktop mode: Destroy Swiper and enable hover animations
        destroySwiper();
        setTimeout(() => {
            initHover(cards);
            window.addEventListener("resize", handleResize);
        }, 200);

    }
}
let cardsRef;
export const init = (cards) => {
    cardsRef = cards;
    breakpoint.addEventListener('change', (e) => { handleBreakpointChange(e, cards) });
    handleBreakpointChange(breakpoint, cards);
}