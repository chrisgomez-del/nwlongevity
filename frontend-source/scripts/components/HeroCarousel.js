import Swiper from 'swiper';
import {Pagination, Navigation} from "swiper/modules";
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import {animateGradient} from "../utils";

export const init = (element, moduleSelector) => {
    console.info('HeroCarousel Loaded', element, moduleSelector);
    animateGradient(moduleSelector);
    const swiper = new Swiper('.hero-swiper', {
        modules: [Navigation, Pagination],
        loop: true,
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev'
        },
        effect: 'fade',
        fadeEffect: { crossFade: true },
        breakpoints: {
            0: {
                slidesPerView: 1,
                effect: 'slide',
                navigation: false
            },
            769: {
                slidesPerView: 1,
                effect: 'fade'
            }
        }
    });
}