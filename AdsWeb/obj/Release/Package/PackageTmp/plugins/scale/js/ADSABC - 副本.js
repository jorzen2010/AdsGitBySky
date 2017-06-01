/*!
 * 自闭症行为量表
 * 开发者信息
 * Copyright (c) 2014-2016 会写代码的心理咨询师赵征
 * Released under the MIT license
 * 使用方法
 * Date: 2016-06-01T15:05:52.606Z
 */

//量表定义
var SkyAdsABCTitle = '自闭症行为（ABC）量表';
var SkyAdsABCInfo = '《自闭症行为量表》——ABC量表，由KRUG于1978年编制，表中列出57项自闭症儿童的行为特征，包括感觉能力（S）、交往能力（R）、运动能力（B）、语言能力（L）和自我照顾能力（S）五个方面。要求评定者与儿童至少共同生活3-6周，填写者与儿童生活至少半年以上。评分时，对每一项作“是”与“否”的判断。“是”评记“∨”符号，“否”不打号。把“是”的项目合计累分，总分≥31分为自闭症筛查界限分；总分>53分作为自闭症诊断界限分（参考值）';
var SkyAdsABCMinInfo = '为保证测量准确，请认真答题，题目没有对错之分，请按照实际情况填写即可。';
var SkyGanjue = "感觉能力";
var SkyAdsABCData = [
    {
        Number: '1',
        Title: '喜欢长时间的自身旋转',
        Dimension: 'A',
        score: '4'

    },
   {
       Number: '2',
       Title: '学会做一件简单的事，但很快就忘记',
       Dimension: 'A',
       score: '4'

   },
     {
         Number: '3',
         Title: '经常没有接触环境或进行交往的要求',
         Dimension: 'C',
         score: '4'

     },
]
//初始化量表函数
function adsabcInit() {

    var title = '<div class="scaletitle"><h3>'+SkyAdsABCTitle+'</h3></div>';
    var info = '<div class="scaleinfo"><p>' + SkyAdsABCInfo + '</p> </div><div class="scalemininfo"><p>' + SkyAdsABCMinInfo + '</p> </div>';
    $('#adsabc').append(title + info);
    for (var i = 0; i < SkyAdsABCData.length; i++) {

        var aa = '<div class="scaleitem"  style="display: none">';
        var bb = '<p>(第' +SkyAdsABCData[i].Number  +"题/共"+SkyAdsABCData.length  + '题)：' + SkyAdsABCData[i].Title + '</p>';
        var cc = '<input name="' + SkyAdsABCData[i].Number + '" data-Dimension="' + SkyAdsABCData[i].Dimension + '" class="rdolist" type="radio"  value="' + SkyAdsABCData[i].score + '">';
        var dd = '<label class="rdobox unchecked items"  data-num=' + SkyAdsABCData[i].Number + ' onclick="return do_next(' + i + ')"><span class="radiobox-content">是</span> </label>';
        var ee = '<input name="' + SkyAdsABCData[i].Number + '" data-Dimension="' + SkyAdsABCData[i].Dimension + '" class="rdolist" type="radio" value="0">';
        var ff = '<label class="rdobox unchecked items"  onclick="do_next(' + i + ')"><span class="radiobox-content">否</span> </label>';
       
        $('#adsabc').append(aa + bb + cc + dd + ee+ff);


    }
     
    var prebtn = '<div class="scalebtn"><button type="button" id="id_pre_link"  style="display: none" class="skybtn skybtn-success" data-num="0"  onclick="return do_prev()">上 一 题</button> ';

    var sumbitbtn = ' <button type="submit" id="id_show_result"  style="display: none" class="skybtn skybtn-primary"  onclick="return do_result()">提交答案</button></div>';
    var hh = '</div>';
    $('#adsabc').append(prebtn + sumbitbtn + hh);

}


function do_prev() {
    var j = Number($('#id_pre_link').attr("data-num"));
    
  
    $('#id_show_result').hide();
    $('#id_pre_link').attr("data-num", j-1);
    $('.scaleitem')[j-1].style.display = "";
    $('.scaleitem')[j].style.display = "none";

   
    if (j == 1) {
        $('#id_pre_link').hide();
    }
}


function do_next(k) {
    $('#id_pre_link').show();
    if (k < SkyAdsABCData.length - 1) {

    $('.scaleitem')[k].style.display = "none";
    $('.scaleitem')[k + 1].style.display = "";
    $('#id_pre_link').attr("data-num",k+1);

    } else {
        $('#id_show_result').show();
        $('#id_pre_link').attr("data-num", k );
    }
     
}

function do_result() {

    //总得分
    var aa = "";
   
    for (var i = 1; i < SkyAdsABCData.length+1; i++) {
        var aa=aa+ $('[name="'+i+'"]:checked').val()+",";
    }
   
   
    aa = aa.substring(0, aa.length - 1);
    alert("总得分" + aa);

    //感觉能力得分
    var b = "";
    for (var i = 1; i < SkyAdsABCData.length + 1; i++) {
        if ($('[name="' + i + '"]:checked').attr("data-Dimension") == 'A') {
            var b = b + $('[name="' + i + '"]:checked').val() + ",";
        }


    }
    b = b.substring(0, b.length - 1);
    alert("感觉能力得分" + b);
   
}

$(document).ready(function () {

    //初始化量表
    adsabcInit();

    //初始化第一题
    var we;
    we = $('.scaleitem');
    $(we[0]).show();

    //参数{input类名，选择类型(单选or多选)}

    $(".rdolist").labelauty("rdolist", "rdo");

});

