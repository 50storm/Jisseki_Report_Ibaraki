//=============================
// 先頭の空白を削除
//=============================
String.prototype.ltrim = function () {
    return this.replace(/^\s+/, "");
}
//=============================
// 末尾の空白を削除
//=============================
String.prototype.rtrim = function () {
    return this.replace(/\s+$/, "");
}
//=============================
// 先頭および末尾の空白を削除
//=============================
String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
}
//=============================
// 先頭および末尾の、全角空白、半角空白、タブ、を削除
//=============================
String.prototype.jtrim = function () {
    return unescape(escape(this).replace(/^(%u3000|%20|%09)+|(%u3000|%20|%09)+$/g, ""));
}

//=============================
//数値のチェック
//=============================
function isNumber(id) {
    //数値かどうかのチェック
    var obj = document.getElementById(id);

    //エラーの時は背景色を変える
    if (isNaN(obj.value)) {
        document.getElementById(id).style.backgroundColor = "Pink";
        document.getElementById(id).focus();
        return false;
    } else {
        document.getElementById(id).style.backgroundColor = "White";
        return true
    }
}
//=============================
//必須のチェック
//=============================
function isEmpty(id) {
    //数値かどうかのチェック
    var obj = document.getElementById(id);

    //エラーの時は背景色を変える
    if (obj.value == "") {
        document.getElementById(id).style.backgroundColor = "Pink";
        document.getElementById(id).focus();
        return false;
    } else {
        document.getElementById(id).style.backgroundColor = "White";
        return true
    }
}
