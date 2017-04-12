

//获取地址栏参数//只能是英文参数
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

//获取地址栏参数//可以是中文参数
function getUrlParam(key) {
    // 获取参数
    var url = window.location.search;
    // 正则筛选地址栏
    var reg = new RegExp("(^|&)" + key + "=([^&]*)(&|$)");
    // 匹配目标参数
    var result = url.substr(1).match(reg);
    //返回参数值
    return result ? decodeURIComponent(result[2]) : null;
}

//使用Js用Post或者get提交请求（未测试）http://www.w3school.com.cn/ajax/ajax_xmlhttprequest_send.asp
function JsPostSubmit() {
    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            document.getElementById("myDiv").innerHTML = xmlhttp.responseText;
        }
    }
   // xmlhttp.open("POST", "/ajax/demo_post.asp", true);
    //xmlhttp.open("POST", "/ajax/demo_post.asp", true);
    xmlhttp.send();
}
//使用bootbox的确认删除对话框，此函数依赖<script type="text/javascript" src="/plugins/bootbox.min.js"></script>
function delconfirm(id, url,tourl) {
    bootbox.confirm({
        buttons: {
            confirm: {
                label: '删除',
                className: 'btn-danger'
            },
            cancel: {
                label: '取消',
                className: 'btn-default'
            }
        },
        message: '你确定要删除本条信息吗？',
        callback: function (result) {
            if (result) {
                // window.location(url);
                // alertconfirm('有关联数据不能删除');
                // window.location.href = '/sysUser/DeleteConfirmed/' + id;
                //获取防伪标记
                var token = $('[name=__RequestVerificationToken]').val();
                var headers = {};
                //防伪标记放入headers
                //也可以将防伪标记放入data
                headers["__RequestVerificationToken"] = token;
                $.ajax({
                    url: url,
                    headers: headers,
                    type: 'post',
                    timeout: 3000,
                    data: { id: id, __RequestVerificationToken: token },
                    success: function (msg) {
                        if (msg.MessageStatus == 'true') {
                            //window.location.reload();
                            window.location.href = tourl;
                        }
                        else {
                            alertconfirm('删除失败');
                        }
                    },
                    error: function (e) {
                        alertconfirm('出现意外错误，删除失败！');
                    }
                });
            }
            else {
                // alertconfirm('您已经取消删除了');
            }
        },
        title: "删除确认提示框",
    });
}

//使用bootbox的信息对话框，此函数依赖<script type="text/javascript" src="/plugins/bootbox.min.js"></script>
function alertconfirm(message) {

    bootbox.alert({
        buttons: {
            ok: {
                label: '确定',
                className: 'btn-danger'
            }
        },
        message: message,
        callback: function (result) {
            if (result) {
                window.location(url);
            }
        },
        title: "信息提示框",
    });
}

