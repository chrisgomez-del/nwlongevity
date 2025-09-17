//Fixed Navbar on Scroll
window.addEventListener('scroll', function () {
    if (window.scrollY >= 500) {
        document.getElementById('nav').classList.remove('home');
    }
    else {
        document.getElementById('nav').classList.add('home');
    }
});

document.addEventListener('DOMContentLoaded', function () {

    const video = document.getElementById("loopingVideo");
    let current = 0;

    function playNextVideo() {
        video.src = videoFiles[current];
        video.currentTime = 0;
        video.play();
        current = (current + 1) % videoFiles.length;
    }

    video.addEventListener("loadeddata", () => {
        setTimeout(playNextVideo, 5000);
    });

    // Start the loop
    playNextVideo();



});


// Transition Copy Component GSAP Animations
gsap.registerPlugin(ScrollTrigger);

// Animate #svg3 independently
gsap.from("#svg3", {
    x: -100,
    y: -100,
    opacity: 0,
    duration: 1.2,
    ease: "power2.out",
    visibility: "visible",
    scrollTrigger: {
        trigger: "#component2",
        start: "top 40%",
        toggleActions: "play none none none"
    }
});

// Timeline for #svg4 and #intro-copy1
let tlSvg4Intro = gsap.timeline({
    scrollTrigger: {
        trigger: "#component2",
        start: "top 40%",
        toggleActions: "play none none none"
    }
});
tlSvg4Intro.from("#svg4", {
    x: 300,
    y: 300,
    opacity: 0,
    duration: 1.2,
    ease: "power2.out",
    visibility: "visible"
})
    .from("#intro-copy1", {
        y: 50,
        opacity: 0,
        duration: .5,
        ease: "power2.out",
        visibility: "visible"
    })
    .from("#intro-copy2", {
        y: 50,
        opacity: 0,
        duration: .7,
        ease: "power2.out",
        visibility: "visible"
    });

// Timeline for Flipping Cards Animation
let tlCards = gsap.timeline({
    scrollTrigger: {
        trigger: ".component4",
        start: "top 80%",
        toggleActions: "play none none none"
    }
});
gsap.from("#card1", {
    x: -300,
    y: -600,
    opacity: 0,
    duration: 1.2,
    ease: "power2.out",
    visibility: "visible",
    scrollTrigger: {
        trigger: ".component4",
        start: "top 80%",
        toggleActions: "play none none none"
    }
});
gsap.from("#card2", {
    x: -200,
    y: -900,
    opacity: 0,
    duration: 1.2,
    ease: "power2.out",
    visibility: "visible",
    scrollTrigger: {
        trigger: ".component4",
        start: "top 80%",
        toggleActions: "play none none none"
    }
});
gsap.from("#card3", {
    x: -100,
    y: -1300,
    opacity: 0,
    duration: 1.2,
    ease: "power2.out",
    visibility: "visible",
    scrollTrigger: {
        trigger: ".component4",
        start: "top 80%",
        toggleActions: "play none none none"
    }
});
gsap.from("#card4", {
    x: 100,
    y: -600,
    opacity: 0,
    duration: 1.2,
    ease: "power2.out",
    visibility: "visible",
    scrollTrigger: {
        trigger: ".component4",
        start: "top 80%",
        toggleActions: "play none none none"
    }
});
gsap.from("#card5", {
    x: 200,
    y: -400,
    opacity: 0,
    duration: 1.2,
    ease: "power2.out",
    visibility: "visible",
    scrollTrigger: {
        trigger: ".component4",
        start: "top 80%",
        toggleActions: "play none none none"
    }
});

// Flipping Cards slider logic with mobile support
(function () {
    const container = document.querySelector('.cards-container');
    const cards = Array.from(container.querySelectorAll('.flip-card'));
    let startIdx = 0;

    function getMaxVisible() {
        return window.innerWidth <= 600 ? 1 : 5;
    }

    function updateVisibleCards() {
        const maxVisible = getMaxVisible();
        cards.forEach((card, i) => {
            if (i >= startIdx && i < startIdx + maxVisible) {
                card.style.display = '';
            } else {
                card.style.display = 'none';
            }
        });
    }

    document.querySelector('.next-button').addEventListener('click', function () {
        const maxVisible = getMaxVisible();
        if (startIdx + maxVisible < cards.length) {
            startIdx++;
            updateVisibleCards();
        }
    });
    document.querySelector('.prev-button').addEventListener('click', function () {
        if (startIdx > 0) {
            startIdx--;
            updateVisibleCards();
        }
    });

    // Update on resize
    window.addEventListener('resize', function () {
        // Clamp startIdx if needed
        const maxVisible = getMaxVisible();
        if (startIdx + maxVisible > cards.length) {
            startIdx = Math.max(0, cards.length - maxVisible);
        }
        updateVisibleCards();
    });

    // Initialize
    updateVisibleCards();
})();


// Modal logic for Plan Your Visit Form
function showPlanModal() {
    document.getElementById('planModalOverlay').style.display = 'flex';
}
function hidePlanModal() {
    document.getElementById('planModalOverlay').style.display = 'none';
}
// Attach to all .plan-your-visit elements
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.plan-your-visit').forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            showPlanModal();
        });
    });
    document.getElementById('closePlanModal').addEventListener('click', hidePlanModal);
    document.getElementById('planModalOverlay').addEventListener('click', function (e) {
        if (e.target === this) hidePlanModal();
    });
});