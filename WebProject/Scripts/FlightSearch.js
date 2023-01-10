
function oneWay() {
    document.querySelector(".One-Way").classList.remove('hide');
    document.querySelector(".Two-Way").classList.add('hide');
}

function twoWay() {
    document.querySelector(".One-Way").classList.add('hide');
    document.querySelector(".Two-Way").classList.remove('hide');
}
