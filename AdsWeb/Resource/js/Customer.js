
function SetIdentity(id, identity) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        type: 'POST',
        url: "/Customer/SetIdentity",
        data: {
            id: id,
            __RequestVerificationToken: token,
            identity: identity

        },
        dataType: "json",
        success: function (data) {
            if (data.MessageStatus) {
                alert(data.MessageInfo);
                window.location.reload();
            }
            else { alertconfirm("审核出现问题，不通过！"); }

        }
    });

}

function resetPassword(id) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        type: 'POST',
        url: "/Customer/ResetPassword",
        data: {
            id: id,
            __RequestVerificationToken: token,
        },
        dataType: "json",
        success: function (data) {
            if (data.MessageStatus)
            { alertconfirm(data.MessageInfo); }
            else
            { alertconfirm("重置密码失败！"); }

        }
    });

}

$(document).ready(function () {

    $('select').addClass("form-control");

    $("#searchdistpicker").distpicker({
        autoSelect: false
    });

    var iden = getUrlParam("iden");
    if (iden == null) {
        iden = 0;
    }
    //   alert(iden);
    $('.cusiden').removeClass("btn-primary");
    $('[name=iden_' + iden + ']').removeClass('btn-default');
    $('[name=iden_' + iden + ']').addClass('btn-primary');


    var rid = getUrlParam("rid");
    if (rid == null) {
        rid = 0;
    }
    $('.cusrole').removeClass("btn-primary");
    $('[name=role_' + rid + ']').removeClass('btn-default');
    $('[name=role_' + rid + ']').addClass('btn-primary');



    //开关按钮样式加载
    $('[name="status"]').bootstrapSwitch({
        onText: "启用",
        offText: "禁用",
        onColor: "success",
        offColor: "danger",
        size: "small",
        onSwitchChange: function (event, state) {
            var token = $('[name=__RequestVerificationToken]').val();
            $.ajax({
                type: 'POST',
                url: "/Customer/UpdateStatus",
                data: {
                    id: $(this).val(),
                    status: state,
                    __RequestVerificationToken: token,
                },
                dataType: "json",
                success: function (data) {
                }
            });
        }
    });

    //表单验证
    $('#CustomerForm').bootstrapValidator({
        //        live: 'disabled',
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            CustomerRole: {
                validators: {
                    notEmpty: {
                        message: '用户类型不能为空'
                    }
                }
            },
            CustomerUserName: {
                validators: {
                    notEmpty: {
                        message: '用户名不能为空'
                    },
                    stringLength: {
                        min: 6,
                        max: 20,
                        message: '用户名长度必须在6到30之间'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9]+$/,
                        message: '用户名只能包含大写、小写和数字'
                    },
                    threshold: 5,//有6字符以上才发送ajax请求，（input中输入一个字符，插件会向服务器发送一次，设置限制，6字符以上才开始）
                    remote: {//ajax验证。server result:{"valid",true or false} 向服务发送当前input name值，获得一个json数据。例表示正确：{"valid",true}
                        url: '/Ajax/CheckUserName',//验证地址
                        message: '用户名重复，不能注册',//提示消息
                        delay: 2000,//每输入一个字符，就发ajax请求，服务器压力还是太大，设置2秒发送一次ajax（默认输入一个字符，提交一次，服务器压力太大）
                        type: 'POST',//请求方式
                        data: function (validator) {
                            return {
                                CustomerUserName: $('[name="CustomerUserName"]').val()
                            };
                        }

                    },
                }
            },
            CustomerPassword: {
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    stringLength: {
                        min: 6,
                        max: 20,
                        message: '用户名长度必须在6到30之间'
                    },
                    different: {
                        field: 'CustomerUserName',
                        message: '密码与用户名长的怎么一样呢'
                    }
                }
            },
            ConfirmPassword: {
                validators: {
                    notEmpty: {
                        message: '重复密码也不能为空'
                    },
                    identical: {
                        field: 'CustomerPassword',
                        message: '两次输入密码不一致'
                    }
                }
            },
            CustomerEmail: {
                validators: {
                    notEmpty: {
                        message: '邮箱不能为空'
                    },
                    emailAddress: {
                        message: '请输入正确的邮件地址如：123@qq.com'
                    },
                    threshold: 5,
                    remote: {
                        url: '/Ajax/CheckEmail',
                        message: '此邮箱已经被注册，请换一个',
                        delay: 2000,
                        type: 'POST',
                        data: function (validator) {
                            return {
                                CustomerEmail: $('[name="CustomerEmail"]').val()
                            };
                        }

                    },
                }
            }
        }
    });


});

