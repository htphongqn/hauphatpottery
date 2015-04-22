//Regular Expression ex : 
//int : onkeypress="return ValidateKeypress(/\d/,event);"
//|	đại diện cho vế trước hoặc vế sau
//.	đại diện cho ký tự bất kỳ
//\w	đại diện cho ký tự chữ cái
//\d	đại diện cho ký tự số
//[0-9]	đại diện cho các số 0 đến 9
//[abc]	đại diện cho các chữ cái a hoặc b hoặc c
//?	số lần lặp lại từ 0 đến 1 lần
//*	số lần lặp lại từ 0 đến nhiều lần
//+	số lần lặp lại từ 1 đến nhiều lần
//{9}	số lần lặp lại 9 lần
//{3,4}	số lần lặp lại 3 đến 4 lần
function ValidateKeypress(numcheck, e) {
    var keynum, keychar, numcheck;
    if (window.event) {//IE
        keynum = e.keyCode;
    }
    else if (e.which) {// Netscape/Firefox/Opera
        keynum = e.which;
    }
    if (keynum == 8 || keynum == 127 || keynum == null || keynum == 9 || keynum == 0 || keynum == 13) return true;
    keychar = String.fromCharCode(keynum);
    var result = numcheck.test(keychar);
    return result;
}
//ex decimal: onkeypress="return digits(this, event, true, true);"
function digits(obj, e, allowDecimal, allowNegative) {
    var key;
    var isCtrl = false;
    var keychar;
    var reg;
    if (window.event) {
        key = e.keyCode;
        isCtrl = window.event.ctrlKey
    }
    else if (e.which) {
        key = e.which;
        isCtrl = e.ctrlKey;
    }
    if (isNaN(key)) return true;
    keychar = String.fromCharCode(key);
    // check for backspace or delete, or if Ctrl was pressed
    if (key == 8 || isCtrl) {
        return true;
    }
    reg = /\d/;
    var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
    var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;
    return isFirstN || isFirstD || reg.test(keychar);
}