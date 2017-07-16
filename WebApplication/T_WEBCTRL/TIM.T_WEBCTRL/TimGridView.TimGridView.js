var TimGridView_Objs = new Array();

selectedRowObj = function () {
    this.rowId = "";
    this.bgColor = "";
    this.foreColor = "";
};

var selectedRow = new selectedRowObj();

function getTimGridViewObj(id) {
    for (var i = 0; i < TimGridView_Objs.length; i++) {
        if (TimGridView_Objs[i].id == id.substr(0, TimGridView_Objs[i].id.length))
            return TimGridView_Objs[i];
    }
    return null;
}

function checkedRow(row,event) {
    var obj = getTimGridViewObj(row.id);
    if (obj != null) {
        if ($get(obj.id).getAttribute("selectedIndex") >= 0)
            obj.unSelected();
        obj.selected(row,event);
    }
}

function TimGridView(id) {
    this.id = id;
    this.tbl = $get(this.id);

    if (!getTimGridViewObj(id))
        TimGridView_Objs.push(this);
    else {
        for (var i = 0; i < TimGridView_Objs.length; i++) {
            if (TimGridView_Objs[i].id == id.substr(0, TimGridView_Objs[i].id.length))
                TimGridView_Objs[i] = this;
        }
    }
};

TimGridView.prototype.selected = function (row,event) {
    if (row != null && row.tagName == "TR") {
        //保存未选中之前的样式
        selectedRow.rowId = row.id;
        selectedRow.bgColor = row.style.backgroundColor;
        selectedRow.foreColor = row.style.color;

        //设置新样式
        row.style.backgroundColor = $get(this.id).getAttribute("selectedRow_bgColor");
        row.style.color = $get(this.id).getAttribute("selectedRow_foreColor");

        //保存当前被选择行号到input-hidden中,行id格式"XXX_rowX"
        //input-hidden元素id格式"XXX_selectedIndex"
        $get(this.id + "_selectedIndex").value = row.getAttribute("rowIndex");
        $get(this.id).setAttribute("selectedIndex", row.getAttribute("rowIndex"));
    }

    //触发onclick事件的元素
    var sender = event.srcElement ? event.srcElement : event.target;
    //选中的单元格（TD）
    var cell = sender;
    if ("DIV" == sender.tagName) {
        cell = sender.parentNode;
    }
    else if ("NOBR" == sender.tagName) {
        if ("DIV" == sender.parentNode.tagName) {
            cell = sender.parentNode.parentNode;
        }
    }
    else if ("IMG" == sender.tagName) {
        cell = sender.parentNode;
    }
    if (cell != null && cell.tagName == "TD" && cell.parentNode.id == row.id) {
        cell.className = cell.className.replace(/cellselected/g, "") + " cellselected";
    }
};

TimGridView.prototype.unSelected = function () {
    var oldRow = $get(selectedRow.rowId);
    if (oldRow == null) {
        if (this.tbl.rows[1]) {
            var oldRowid = this.tbl.rows[1].id.substring(0, this.tbl.rows[1].id.indexOf("_row") + 4) + $get(this.id + "_selectedIndex").value;
            oldRow = $get(oldRowid);
        }
    }

    if (oldRow != null) {
        oldRow.style.backgroundColor = selectedRow.bgColor;
        oldRow.style.color = selectedRow.foreColor;
        var cells = oldRow.cells;
        var cellsCount = cells.length;
        for (var i = 0; i < cellsCount; i++) {
            if (cells[i].className.indexOf("cellselected") != -1) {
                cells[i].className = cells[i].className.replace(/cellselected/g, "");
                break;
            }
        }
    }
};

function showTips(event) {
    event = event ? event : window.event;
    var sender = event.srcElement ? event.srcElement : event.target;
    if (sender && sender.tagName == "TD" && sender.className.indexOf("tips") != -1) {
        if (!sender.getAttribute("title")) {
            var sTitle = sender.innerHTML;
            if (sender.firstChild && sender.firstChild.tagName == "TEXTAREA" && sender.children[2]) {
                sTitle = sender.children[2].innerText;
            }
            if (sTitle.trim() == "")
                return false;
            sender.setAttribute("title", sTitle.trim());
        }

        return true;
    }
};