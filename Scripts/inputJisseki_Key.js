//押されたキーがエンターならタブを押されたことにする
function SkipEnter() {

    if (event.keyCode == 13) {
        //event.keyCode = 9;
        document.getElementById("lblMsg").innerText = "tabキーで移動してください";
        return false;

    } else {
        document.getElementById("lblMsg").innerText = "";

    }

}