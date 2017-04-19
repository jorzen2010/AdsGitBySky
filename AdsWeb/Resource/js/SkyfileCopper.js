

//copper插件的配合使用
function SkyfileCopper(ac,result) {

    switch (ac)
    {
        case "skyavatar":
            var token = $('[name=__RequestVerificationToken]').val();
            $.ajax({
                type: 'POST',
                url: "/File/UploadImgBase64",
                data: {
                    id: $('#CustomerId').val(),
                    img: result.toDataURL('image/jpeg'),
                    __RequestVerificationToken: token,
                    rootpath: "/Resource/Upload",
                    folder: "zhaozheng",
                },
                dataType: "json",
                success: function (data) {
                    alert('Success：头像上传成功');
                    $('#skyAvatar').attr('src', data.MessageUrl);
                    $('#CustomerAvatar').val(data.MessageUrl);
                    $('#avatar-modal').modal('hide');
                },
                error: function () {
                    alert("error：有错误发生了")
                }
            });
            break;

        case "IdCardSC":
            var token = $('[name=__RequestVerificationToken]').val();
            $.ajax({
                type: 'POST',
                url: "/File/UploadImgBase64",
                data: {
                    id: $('#CustomerId').val(),
                    img: result.toDataURL('image/jpeg'),
                    __RequestVerificationToken: token,
                    rootpath: "/Resource/Upload",
                    folder: "IdcardHold",
                },
                dataType: "json",
                success: function (data) {
                   
                //    $('#skyAvatar').attr('src', data.MessageUrl);
                    alert('Success：手持身份证照片上传成功');
                    $('#avatar-modal').modal('hide');
                    $('#IdCardHoldImg').attr('src', data.MessageUrl);
                    $('[name="CustomerHoldCard"]').val(data.MessageUrl);
                    $('#getCroppedCanvasbtn').attr('data-skyac', "");
                    
                },
                error: function () {
                    alert("error：有错误发生了")
                }
            });
            break;

        case "skyIdCardZM":
            var token = $('[name=__RequestVerificationToken]').val();
            $.ajax({
                type: 'POST',
                url: "/File/UploadImgBase64",
                data: {
                    id: $('#CustomerId').val(),
                    img: result.toDataURL('image/jpeg'),
                    __RequestVerificationToken: token,
                    rootpath: "/Resource/Upload",
                    folder: "Idcardzm",
                },
                dataType: "json",
                success: function (data) {
                    alert('Success：身份证正面照片上传成功');
                    //    $('#skyAvatar').attr('src', data.MessageUrl);
                    $('#IdCardZMImg').attr('src', data.MessageUrl);
                    $('#getCroppedCanvasbtn').attr('data-skyac', "");
                    $('[name="CustomerIDCardzm"]').val(data.MessageUrl);
                    $('#avatar-modal').modal('hide');
                    
                },
                error: function () {
                    alert("error：有错误发生了")
                }
            });
            break;

        case "skyIdCardFM":
            var token = $('[name=__RequestVerificationToken]').val();
            $.ajax({
                type: 'POST',
                url: "/File/UploadImgBase64",
                data: {
                    id: $('#CustomerId').val(),
                    img: result.toDataURL('image/jpeg'),
                    __RequestVerificationToken: token,
                    rootpath: "/Resource/Upload",
                    folder: "Idcardfm",
                },
                dataType: "json",
                success: function (data) {
                    alert('Success：身份证反面照片上传成功');
                    //    $('#skyAvatar').attr('src', data.MessageUrl);
                    $('#IdCardFMImg').attr('src', data.MessageUrl);
                    $('#getCroppedCanvasbtn').attr('data-skyac', "");
                    $('[name="CustomerIDCardsm"]').val(data.MessageUrl);
                    $('#avatar-modal').modal('hide');
                   
                },
                error: function () {
                    alert("error：有错误发生了")
                }
            });
            break;

    }

}

