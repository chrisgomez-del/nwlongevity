import {matchBreakpoint} from '../utils';
import Swiper from 'swiper';
import {Pagination} from "swiper/modules";
import 'swiper/css';
import 'swiper/css/pagination';

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
    console.log('swiper enabled');
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

function handleBreakpointChange(e) {
    console.info('Breakpoint Changed', e);
    if (e.matches) {
        enableSwiper();
    } else {
        destroySwiper();
    }
}

export const init = (targetSelector) => {
    console.info('StatCards Loaded', targetSelector);
    breakpoint.addEventListener('change', handleBreakpointChange);
    handleBreakpointChange(breakpoint);
}