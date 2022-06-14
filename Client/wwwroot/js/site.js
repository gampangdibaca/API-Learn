// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var navItem = document.querySelectorAll(".nav-item");
//var currPage = document.URL.split("/")[document.URL.split("/").length-1]
//for (var i = 0; i < navItem.length; i++) {
//    if (currPage == '') {
//        navItem[0].childNodes[1].classList.add("active");
//        break;
//    }
//    if (navItem[i].innerText.replace(" ", "") == currPage) {
//        navItem[i].childNodes[1].classList.add("active");
//    }
//}

var currPage = document.URL;
for (var i = 0; i < navItem.length; i++) {
    if (currPage == navItem[i].childNodes[1].href) {
        navItem[i].childNodes[1].classList.add("active");
        break;
    }
}

var scrollTop = document.querySelector(".scroll-top");
scrollTop.addEventListener("click", () => {
    //document.body.scrollTop = 0;
    //document.documentElement.scrollTop = 0;
    window.scrollTo({ top: 0, behavior: 'smooth' });
});

window.onscroll = () => {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        scrollTop.style.display = "block";
    } else {
        scrollTop.style.display = "none";
    }
};
