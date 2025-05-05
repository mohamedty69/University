let inputs = document.getElementsByTagName('input');
let labels = document.getElementsByTagName('label');
let RigthAnimation = document.getElementById('animation2');

inputs[0].addEventListener('focus', function () {
    let label = document.getElementById('E-label');
    label.style.transition = '0.5s ease';
    label.style.transform = 'translateY(-25px)';
    label.style.fontSize = '13px';
    label.style.color = 'white';
    label.style.fontWeight = 'bold';
    label.style.backgroundColor = '#1a1a1a';
    label.style.opacity = '1';
    RigthAnimation.play();
});

inputs[1].addEventListener('focus', function () {
    let label = document.getElementById('P-label');
    label.style.transition = '0.5s ease';
    label.style.transform = 'translateY(-25px)';
    label.style.fontSize = '13px';
    label.style.color = '#white';
    label.style.fontWeight = 'bold';
    label.style.backgroundColor = '#1a1a1a';
    label.style.opacity = '1';
});

inputs[0].addEventListener('blur', function () {
    if (inputs[0].value == "") {
        let label = document.getElementById('E-label');
        label.style.transition = '0.5s ease';
        label.style.transform = 'translateY(0px)';
        label.style.fontSize = '16px';
        label.style.color = '#e8ecef';
        label.style.opacity = '0.5';
    }
});


inputs[1].addEventListener('blur', function () {
    if (inputs[1].value == "") {
        let label = document.getElementById('P-label');
        label.style.transition = '0.5s ease';
        label.style.transform = 'translateY(0px)';
        label.style.fontSize = '16px';
        label.style.color = '#e8ecef';
        label.style.opacity = '0.5';
    }
});

let time1 = (108 / 30) * 1000;
let time2 = (128 / 30) * 1000;
let media = window.matchMedia('(max-width: 767px)');

if (media.matches) {
    time1 = 1000;
    time2 = 1500;
}


let welcome = document.getElementById('Welcome');
welcome.style.transform = 'translateX(-10px)';
setTimeout(function () {
    welcome.style.transition = '1s ease';
    welcome.style.transform = 'translateX(0px)';
    welcome.style.opacity = '1';
}, time1);

let intro = document.getElementById('intro');
intro.style.transform = 'translateX(-10px)';
setTimeout(function () {
    intro.style.transition = '1s ease';
    intro.style.transform = 'translateX(0px)';
    intro.style.opacity = '1';
}, time2);


