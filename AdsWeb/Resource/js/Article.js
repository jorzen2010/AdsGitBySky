$(document).ready(function () {


    $('[type="radio"],[type="checkbox"]').iCheck({
        labelHover: false,
        cursor: true,
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%',
    });

    var ue = UE.getEditor('Content');

    $('#ArticleForm').bootstrapValidator({
        //        live: 'disabled',
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            Title: {
                validators: {
                    notEmpty: {
                        message: '标题不能为空'
                    }
                }
            },
            Category: {
                validators: {
                    notEmpty: {
                        message: '文章类别不能为空'
                    }
                }
            }

        }
    });

});