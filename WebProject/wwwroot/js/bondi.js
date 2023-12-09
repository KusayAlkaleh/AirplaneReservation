var toast = document.querySelector(".profile-toast");
var btn = document.querySelector(".btn-update");
var close = document.querySelector(".toast-close");
var progress = document.querySelector(".profile-progress");


if (localStorage.getItem('formUpdate') === 'true') {

    toast.classList.add("active");
    progress.classList.add("active");

    setTimeout(() => {
        toast.classList.remove("active");
    }, 5000);

    setTimeout(() => {
        progress.classList.remove("active");
    }, 5300);
}

btn.addEventListener("click", (event) => {
    localStorage.setItem('formUpdate', 'true');
});

close.addEventListener("click", () => {
    toast.classList.remove("active");

    setTimeout(() => {
        progress.classList.remove("active");
    }, 300);
});