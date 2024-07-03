document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("menu-toggle").addEventListener("click", function (e) {
        e.preventDefault();
        var wrapper = document.getElementById("wrapper");
        wrapper.classList.toggle("toggled");
    });
});

let navLinks = document.querySelectorAll('.admin_nav_link');

navLinks.forEach(navLink => {
    if (navLink.classList.contains('active')) {
        navLink.classList.remove('active');
    }
});

navLinks.forEach(navLink => {
    let currentPath = window.location.pathname;
    // Check if navLink's href matches currentPath
    if (navLink.getAttribute('href') === currentPath) {
        navLink.classList.add('active');
    }
});