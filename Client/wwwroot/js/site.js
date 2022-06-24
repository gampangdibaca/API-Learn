var navItem = document.querySelectorAll(".nav-item");
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
