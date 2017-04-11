
//初始化页面
function init() {
    //加载树形菜单
    var a = getUrlParam('option');

    LoadTreeDictionary(0,a);
    //加载icheck的样式，初始化加载一次，每一次刷新都需要加载一次，所以封装一个方法。
    icheckcss("DictionaryStatus");
    //加载验证
    $('#DictionaryFrom').bootstrapValidator({
        //        live: 'disabled',
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            DictionaryName: {
                validators: {
                    notEmpty: {
                        message: '名称不能为空'
                    }
                }
            },
            DictionaryInfo: {
                validators: {
                    notEmpty: {
                        message: '说明不能为空'
                    }
                }
            },
            DictionarySort: {
                validators: {
                    notEmpty: {
                        message: '排序不能为空，如不排序请添0'
                    },
                    numeric: { message: '排序只能是数字' }
                }
            }

        }
    });
}

//加载icheck的样式，初始化加载一次，每一次刷新都需要加载一次，所以封装一个方法。
function icheckcss(name) {
    $('[name=' + name + ']').iCheck({
        labelHover: false,
        cursor: true,
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%'
    });
}
//设置form为只读
function formReadonly() {
    //所有文本框只读
    $("input[name],textarea[name]").attr("readonly", "readonly");
    //所有单选框和复选框只读
    $('input[type=radio],input[type=checkbox]').prop("disabled", true);
    //隐藏取消、保存按钮
    $("#DictionaryFrom .box-footer").hide();
    //还原新增、编辑、删除按钮样式
    $('[name="DictionaryBtn"]').removeClass("btn-primary").addClass("btn-default");
}
//清空form值
function formClear() {
    //所有文本框只读,除了radio和checkbox
    $(':input[id]:not(:radio):not(:checkbox),textarea[name]').val('');
    $('input[type="radio"][name="DictionaryStatus"][value="true"]').prop("checked", "checked");
    //还原校验框
    if ($("#DictionaryFrom").data('bootstrapValidator'))
        $("#DictionaryFrom").data('bootstrapValidator').resetForm();
}
//设置form为可写
function formWritable(action) {
    $("input[name],textarea[name]").removeAttr("readonly");
    $('input[type=radio],input[type=checkbox]').removeAttr("disabled");
    $('input[type=radio],input[type=checkbox]').removeAttr("readonly");
    $('input[type="radio"][name="DictionaryStatus"][value="true"]').prop("checked", "checked");
    $("#DictionaryFrom .box-footer").show();
    //  $('[name="DictionaryBtn"]').removeClass("btn-primary").addClass("btn-default");
    $('#DictionaryFrom').prop("action", action);
    icheckcss("DictionaryStatus");
}
//设置上级ID值
function fillParent(selectedNode) {
    $("input[name='DictionaryParentName']").val(selectedNode ? selectedNode.text : '系统字典').attr("readonly", "readonly");
    $("input[name='DictionaryParentID']").val(selectedNode ? selectedNode.id : '1').attr("readonly", "readonly");
}
//设置Form值
function fillForm(selectedNode) {
    $.ajax({
        type: "get",
        url: "/Dictionary/GetOneDictionary",
        data: {
            id: selectedNode.id
        },
        dataType: "json",
        success: function (result) {
            // $('#thisDictionaryName').val(result.DictionaryName).prop("disabled", true);
            //  $('#thisID').val(result.ID).prop("disabled", true);
            $('#DictionaryParentName').val(result.DictionaryParentName).prop("readonly", true);
            $('#DictionaryParentID').val(result.DictionaryParentID).removeAttr("readonly");
            $('#DictionaryName').val(result.DictionaryName).removeAttr("readonly");
            $('#DictionarySort').val(result.DictionarySort).removeAttr("readonly");
            $('#DictionaryInfo').val(result.DictionaryInfo).removeAttr("readonly");
            $('input[type="radio"][name="DictionaryStatus"]').removeAttr("disabled");

            $('input[type="radio"][name="DictionaryStatus"][value=' + result.DictionaryStatus.toString() + ']').prop("checked", "checked");

            $('#DictionaryFrom').prop("action", "/Dictionary/Edit");
            $("#DictionaryFrom .box-footer").show();
            icheckcss("DictionaryStatus");
        },
        error: function () {
            alert("选择的不对！")
        }
    });


}
//curd查按钮操作
function dictionaryaction(btn, ac) {

    $('[name="DictionaryBtn"]').removeClass("btn-primary").addClass("btn-default");
    $(btn).removeClass("btn-default").addClass("btn-primary");
    var selectedArr = $("#DictionaryTreeview").data("treeview").getSelected();
    var selectedNode = selectedArr.length > 0 ? selectedArr[0] : null;
    icheckcss("DictionaryStatus");

    var thisDictionaryName = $('#thisDictionaryName').val();
    var thisID = $('#thisID').val();

    if (ac == 'addtop') {
        formClear();
        formWritable("/Dictionary/Create");
        $('input[type="radio"][name="DictionaryStatus"][value="true"]').prop("checked", "checked");
        fillParent(null);

    }
    if (ac == 'addnext') {
        if (!selectedNode) {
            alertconfirm('你尚未没有选择节点！');
        }
        else {

            formClear();
            formWritable("/Dictionary/Create");
            $('input[type="radio"][name="DictionaryStatus"][value="true"]').prop("checked", "checked");
            fillParent(selectedNode);
        }



    }
    if (ac == 'edit') {
        //需要重新获取这个栏目
        if (!selectedNode)
        { alertconfirm('你尚未没有选择节点！'); }
        else
        {
            fillForm(selectedNode);
        }


    }
    if (ac == 'cancel') {
        //需要重新获取这个栏目
        formReadonly();
        return false;
    }
    if (ac == 'delete') {
        //弹出确认对话框即可
        if (!selectedNode) {
            alertconfirm('你尚未没有选择节点！');
            return false;
        }
        else {
            if (selectedNode.nodes) {
                alertconfirm('此节点有子节点，不能删除！');
                return false;
            }
            else {
                if (selectedNode.id == 2)
                {
                    alertconfirm('此节点为系统内置节点不能删除，你可以修改此节点内容！');
                    return false;
                }
                else
                {

                fillForm(selectedNode);
                formReadonly();
                delconfirm(selectedNode.id, "/Dictionary/Delete/", '/Dictionary/Index');

                }

                
            }

        }

    }


}

//加载树形菜单
function LoadTreeDictionary(rootId,a) {
    $.ajax({
        type: "get",
        url: "/dictionary/TreeJson",
        data: {
            id: rootId
        },
        dataType: "json",
        success: function (result) {

            $('#DictionaryTreeview').treeview({
                levels: 1,
                data: result,
                multiSelect: $('#chk-select-multi').is(':checked'),
                onNodeSelected: function (event, node) {
                    formClear();
                    dictionary(node.id);
                    formReadonly();

                    // alert($("#DictionaryTreeview").data("treeview").getSelected()[0].text);
                    //  alert(node.text);
                    // selectedArr = $("#tree").data("treeview").getSelected();
                },
                onNodeUnselected: function (event, node) {


                },
                onNodeCollapsed: function (event, node) {

                },
                onNodeExpanded: function (event, node) {

                }
            });
            selectnode('DictionaryTreeview',a);
        },
        error: function () {
            alert("树形结构加载失败！")
        }
    });

}

//默认选择项目
function selectnode(id, option)
{
    var option = option || '学历';
    //搜索到项目
   // var selectnode = $("#" + id).data("treeview").search([option]);
    var selectnode = $("#" + id).treeview('search', [option]);
    //清空搜索结果
    $("#"+id).treeview('clearSearch');
    //选中
    $("#" + id).data("treeview").selectNode(selectnode);
    //展开
    $("#"+id).data("treeview").expandNode(selectnode);

}

//右侧加载选中项目
function dictionary(nodeid) {
    $('[name="DictionaryStatus"]').removeAttr("checked");
    $.ajax({
        type: "get",
        url: "/Dictionary/GetOneDictionary",
        data: {
            id: nodeid
        },
        dataType: "json",
        success: function (result) {

            $('#DictionaryParentName').val(result.DictionaryParentName);
            $('#DictionaryParentID').val(result.DictionaryParentID);
            $('#DictionaryName').val(result.DictionaryName);
            $('#ID').val(result.ID);
            $('#DictionarySort').val(result.DictionarySort);
            $('#DictionaryInfo').val(result.DictionaryInfo);
            $('input[type="radio"][name="DictionaryStatus"][value=' + result.DictionaryStatus.toString() + ']').prop("checked", "checked");;
            icheckcss("DictionaryStatus");
        },
        error: function () {
            alert("加载失败！")
        }
    });



}


