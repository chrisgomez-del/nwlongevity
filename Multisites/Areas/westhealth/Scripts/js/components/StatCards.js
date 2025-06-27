import { matchBreakpoint } from '../utils';
import Swiper from 'swiper';
import { Pagination } from "swiper/modules";
import 'swiper/css';
import 'swiper/css/pagination';
import gsap from 'gsap';

let swiperInstance;
const breakpoint = matchBreakpoint('lt-md');
function enableSwiper() {
    const container = document.querySelector('.js-stat-card-slider');
    if (!container || container.classList.contains('swiper-initialized')) return;

    const slides = container.querySelectorAll('.js-stat-card-slide');

    slides.forEach(slide => {
        slide.classList.add('swiper-slide');
    });

    container.classList.add('swiper', 'swiper-initialized');
    const row = container.querySelector('.row');
    row.classList.remove('row');
    row.classList.add('swiper-wrapper'); // remove Bootstrap row so Swiper styles apply

    swiperInstance = new Swiper(container, {
        modules: [Pagination],
        slidesPerView: 1.15,
        spaceBetween: 16,
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
    });
}

function destroySwiper() {
    const container = document.querySelector('.js-stat-card-slider');
    if (!container || !swiperInstance) return;

    swiperInstance.destroy(true, true);
    swiperInstance = undefined;
    console.log('swiper destroyed');

    const slides = container.querySelectorAll('.swiper-slide');
    slides.forEach(slide => {
        slide.classList.remove('swiper-slide');
    });

    container.classList.remove('swiper', 'swiper-initialized');
    const row = container.querySelector('.swiper-wrapper');
    row.classList.add('row');
    row.classList.remove('swiper-wrapper'); // restore Bootstrap layout
}

function handleHoverState(cards) {
    cards.forEach(card => {
        const wrapper = card.parentElement;
        const content = card.querySelector(".card-content-animate");
        const svg = card.querySelector("svg");

        // 1. Ensure everything is visible for measurement
        gsap.set(content, { clearProps: "all" });

        // 2. Measure full height with content visible
        const fullHeight = card.scrollHeight;
        const collapsedHeight = fullHeight * 0.9;
        const svgOriginalHeight = svg.getBoundingClientRect().height;
        const contentOriginalHeight = content.getBoundingClientRect().height;

        // 3. Set measured height as maxHeight baseline
        wrapper.style.height = `${fullHeight}px`; // lock outer height
        card.style.maxHeight = `${collapsedHeight}px`;  // collapse inner card
        content.style.height = 0;

        // 4. Now hide content
        gsap.set(content, { autoAlpha: 0, y: 20 });
        gsap.set(svg, { autoAlpha: 1, y: 0 });

        // 5. Hover in animation
        card.addEventListener("mouseenter", () => {
            gsap.to(svg, {
                autoAlpha: 0,
                marginTop: -20,
                height: 0,
                duration: 0.2,
                ease: "sine.out"
            });

            gsap.to(content, {
                autoAlpha: 1,
                y: 0,
                height: contentOriginalHeight,
                duration: 0.2,
                ease: "sine.out"
            });

            gsap.to(card, {
                maxHeight: fullHeight,
                duration: 0.2,
                ease: "sine.out"
            });
        });

        // 6. Hover out animation
        card.addEventListener("mouseleave", () => {
            gsap.to(svg, {
                autoAlpha: 1,
                marginTop: 0,
                height: svgOriginalHeight,
                duration: 0.2,
                delay: 0.2,
                ease: "sine.inOut"
            });

            gsap.to(content, {
                autoAlpha: 0,
                y: 20,
                height: 0,
                duration: 0.2,
                ease: "sine.inOut"
            });

            gsap.to(card, {
                maxHeight: collapsedHeight,
                duration: 0.2,
                ease: "sine.inOut"
            });
        });
    });
}

// function destroyHoverState(cards) {
//     console.log(cards);
//     cards.forEach(card => {
//         const wrapper = card.parentElement;
//         const content = card.querySelector(".card-content-animate");
//         const svg = card.querySelector("svg");
//         gsap.killTweensOf(wrapper);
//         gsap.killTweensOf(content);
//         gsap.killTweensOf(svg);
//     })
// }
function handleBreakpointChange(e, cards) {
    if (e.matches) {
        enableSwiper();
        // destroyHoverState(cards);
    } else {
        destroySwiper();
        handleHoverState(cards);
    }
}

export const init = (cards) => {
    console.info('StatCards Loaded', cards);
    breakpoint.addEventListener('change', (e) => { handleBreakpointChange(e, cards) });
    handleBreakpointChange(breakpoint, cards);
}