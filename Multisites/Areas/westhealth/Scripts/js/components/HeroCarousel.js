import Swiper from 'swiper';
import {Pagination, Navigation, Keyboard} from "swiper/modules";
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import {animateGradient} from "../utils";

export const init = (element, moduleSelector) => {
    console.info('HeroCarousel Loaded', element, moduleSelector);
    animateGradient(moduleSelector);
    const swiper = new Swiper('.hero-swiper', {
        modules: [Navigation, Pagination, Keyboard],
        loop: true,
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        },
        slidesPerView: 1,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev'
        }
    });
}