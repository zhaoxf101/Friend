﻿function ANP_checkInput(bid, mv, ormsg, ivmsg) {
    var el = document.getElementById(bid);
    var r = new RegExp("^\\s*(\\d+)\\s*$");
    if (r.test(el.value)) {
        if (RegExp.$1 < 1 || RegExp.$1 > mv) {
            alert(ormsg);
            el.focus();
            el.select();
            return false;
        }
        return true;
    }
    alert(ivmsg);
    el.focus();
    el.select();
    return false;
}

function ANP_keydown(e, btn) {
    if (e.which === 13 || e.keyCode === 13) {
        document.getElementById(btn).click();
        if (e.preventDefault) {
            e.preventDefault();
        } else {
            event.returnValue = false;
        }
    }
}

function ANP_keyup(box) {
    var be = document.getElementById(box);
    be.value = be.value.replace(/\D/g, '');
}

function ANP_goToPage(e, p, f, u, t, c, r) {
    var el = document.getElementById(e);
    if (el != null) {
        var pi;
        if (el.tagName == "SELECT") {
            pi = el.options[el.selectedIndex].value;
        } else {
            pi = el.value;
        }
        if (r) {
            pi = c - pi + 1;
        }
        u = ((pi == 1 && !r) || (r && pi === c)) ? f : u.replace("{" + p + "}", pi);
        if (t) {
            window.open(u, t)
        } else {
            location.href = u
        }
    }
}