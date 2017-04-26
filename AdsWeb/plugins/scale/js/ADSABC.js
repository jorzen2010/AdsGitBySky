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
   {
       Number: '4',
       Title: '往往不能接受简单的指令（如坐下、过来等）',
       Dimension: 'D',
       score: '1'

   },
    {
        Number: '5',
        Title: '不会玩玩具（如没完没了地转动、乱扔、揉等）',
        Dimension: 'C',
        score: '2'

    },
     {
         Number: '6',
         Title: '视觉辨别能力差（如对一种物体的特征、其大小、颜色、位置等的辨别能力差）',
         Dimension: 'A',
         score: '2'

     },
      {
          Number: '7',
          Title: '无交往性微笑（即不会与人点头、招呼、微笑）',
          Dimension: 'B',
          score: '2'

      },
       {
           Number: '8',
           Title: '代词运用的颠倒或混乱（你我分不清）',
           Dimension: 'D',
           score: '3'

       },
       {
           Number: '9',
           Title: '长时间地总拿着某种东西',
           Dimension: 'C',
           score: '3'

       },
       {
           Number: '10',
           Title: '似乎不在听人说话，以至有人怀疑他有听力问题',
           Dimension: 'A',
           score: '3'

       },
        {
            Number: '11',
            Title: '说话不合音调，无节奏',
            Dimension: 'D',
            score: '4'

        },
         {
             Number: '12',
             Title: '长时间摇摆身体',
             Dimension: 'C',
             score: '4'

         },
          {
              Number: '13',
              Title: '要去拿什么东西，但又不是身体所能达到的地方（即对自身与物体的距离估计不足）',
              Dimension: 'B',
              score: '3'

          },
          {
              Number: '14',
              Title: '对环境和日常生活规律的改变产生强烈反应',
              Dimension: 'E',
              score: '3'

          },
          {
              Number: '15',
              Title: '当与其他人在一起时，呼唤他的名字他没有反应',
              Dimension: 'D',
              score: '2'

          },
           {
               Number: '16',
               Title: '经常做出前冲、旋转、脚尖行走、手指轻掐轻弹等动作',
               Dimension: 'C',
               score: '4'

           },
            {
                Number: '17',
                Title: '对其他人的面部表情没有反应',
                Dimension: 'B',
                score: '3'

            },
             {
                 Number: '18',
                 Title: '说话时很少用“是”或“我”等词',
                 Dimension: 'D',
                 score: '2'

             },
              {
                  Number: '19',
                  Title: '有某一方面的特殊能力，似乎与智力低不相符合',
                  Dimension: 'E',
                  score: '4'

              },
               {
                   Number: '20',
                   Title: '不能执行简单的含有介词语句的指令（如把球放在盒子上或放在盒子里）',
                   Dimension: 'D',
                   score: '1'

               },
                {
                    Number: '21',
                    Title: '有时对很大的声音不产生吃惊的反应（可能让人想到儿童是聋子）',
                    Dimension: 'A',
                    score: '3'

                },
                {
                    Number: '22',
                    Title: '经常拍打手',
                    Dimension: 'C',
                    score: '4'

                },
                {
                    Number: '23',
                    Title: '发大脾气或经常发点脾气',
                    Dimension: 'E',
                    score: '3'

                },
                 {
                     Number: '24',
                     Title: '主动回避与别人的眼光接触',
                     Dimension: 'B',
                     score: '4'

                 },
                  {
                      Number: '25',
                      Title: '拒绝别人接触或拥抱',
                      Dimension: 'B',
                      score: '4'

                  },
                  {
                      Number: '26',
                      Title: '有时对很痛苦的刺激如摔伤、割破或注射不引起反应',
                      Dimension: 'A',
                      score: '3'

                  },
                  {
                      Number: '27',
                      Title: '身体表现很僵硬很难抱住',
                      Dimension: 'B',
                      score: '3'

                  },
                  {
                      Number: '28',
                      Title: '当抱着他时，感到他肌肉松弛（即使他不紧贴着抱他的人）',
                      Dimension: 'B',
                      score: '2'

                  },
                   {
                       Number: '29',
                       Title: '以姿势、手势表示所渴望得到的东西（而不倾向于用语言表示）',
                       Dimension: 'D',
                       score: '2'

                   },
                   {
                       Number: '30',
                       Title: '常用脚尖走路',
                       Dimension: 'C',
                       score: '2'

                   },
                    {
                        Number: '31',
                        Title: '用咬人、撞人、踢人等行为伤害他人',
                        Dimension: 'E',
                        score: '2'

                    },
                     {
                         Number: '32',
                         Title: '不断地重复短句',
                         Dimension: 'D',
                         score: '3'

                     },
                      {
                         Number: '33',
                         Title: '游戏时不模仿其他儿童',
                         Dimension: 'B',
                         score: '3'

                     },
                      {
                            Number: '34',
                            Title: '当强光直接照射眼睛时常常不眨眼',
                            Dimension: 'A',
                            score: '1'

                     },
                       {
                              Number: '35',
                              Title: '以撞头、咬手等行为自伤',
                              Dimension: 'C',
                              score: '2'

                       },
                       {
                           Number: '36',
                           Title: '想要什么东西不能等待（一想要什么就马上要得到）',
                           Dimension: 'E',
                           score: '2'

                       },
                       {
                           Number: '37',
                           Title: '不能指出5个以上物体的名称',
                           Dimension: 'D',
                           score: '1'

                       },
                        {
                            Number: '38',
                            Title: '不能发展任何友谊（不会和小朋友来往交朋友）',
                            Dimension: 'B',
                            score: '4'

                        },
                           {
                               Number: '39',
                               Title: '有许多声音的时候常常捂耳朵',
                               Dimension: 'A',
                               score: '4'

                           },
                           {
                               Number: '40',
                               Title: '经常旋转碰撞物体',
                               Dimension: 'C',
                               score: '4'

                           },
                             {
                                 Number: '41',
                                 Title: '在训练大小便方面有困难（不会控制大小便）',
                                 Dimension: 'E',
                                 score: '1'

                             },
                             {
                                 Number: '42',
                                 Title: '一天只能提出5个以内的要求',
                                 Dimension: 'D',
                                 score: '2'

                             },
                               {
                                   Number: '43',
                                   Title: '经常受到惊吓或非常焦虑不安',
                                   Dimension: 'B',
                                   score: '3'

                               },
                               {
                                   Number: '44',
                                   Title: '在正常光线下斜眼、闭眼、皱眉',
                                   Dimension: 'A',
                                   score: '3'

                               },
                               {
                                   Number: '45',
                                   Title: '不是经常被帮助的话，不会自己给自己穿衣',
                                   Dimension: 'E',
                                   score: '1'

                               },
                                {
                                    Number: '46',
                                    Title: '一遍遍重复一些声音或词',
                                    Dimension: 'D',
                                    score: '3'

                                },
                                {
                                    Number: '47',
                                    Title: '瞪着眼看人，好象要看穿似的',
                                    Dimension: 'B',
                                    score: '4'

                                },
                                 {
                                     Number: '48',
                                     Title: '重复别人的问话或回答',
                                     Dimension: 'D',
                                     score: '4'

                                 },
                                 {
                                     Number: '49',
                                     Title: '经常不能意识所处的环境，并且可能对危险的环境不在意',
                                     Dimension: 'E',
                                     score: '2'

                                 },
                                 {
                                     Number: '50',
                                     Title: '特别喜欢摆弄着迷于单调的东西或游戏、活动等（如来回地走或跑，没完没了地蹦、跳、拍、敲）',
                                     Dimension: 'E',
                                     score: '4'

                                 },
                                 {
                                     Number: '51',
                                     Title: '对周围东西喜欢嗅、摸或尝',
                                     Dimension: 'B',
                                     score: '3'

                                 },
                                 {
                                     Number: '52',
                                     Title: '对生人常无视觉反应（对来人不看）',
                                     Dimension: 'A',
                                     score: '3'

                                 },
                                 {
                                     Number: '53',
                                     Title: '纠缠在一些复杂的仪式行为上，就像缠在魔圈里（如走路一定要走一定的路线；饭前或睡前或干什么事前一定要把什么东西摆在什么地方或做什么动作，否则就不睡不吃）',
                                     Dimension: 'B',
                                     score: '4'

                                 },
                                  {
                                      Number: '54',
                                      Title: '经常毁坏东西（如家具、家里的一切用具很快就弄坏了）',
                                      Dimension: 'B',
                                      score: '2'

                                  },
                                  {
                                      Number: '55',
                                      Title: '在2岁以前就发现该儿童发育延迟',
                                      Dimension: 'E',
                                      score: '1'

                                  },
                                  {
                                      Number: '56',
                                      Title: '在日常生活中至少用15个但又不超过30个短句进行交往（注：不到15句也选择是）',
                                      Dimension: 'D',
                                      score: '3'

                                  },
                                   {
                                       Number: '57',
                                       Title: '长期凝视一个地方（呆呆地看一处）',
                                       Dimension: 'A',
                                       score: '4'

                                   },


]
//初始化量表函数
function adsabcInit() {

    var title = '<div class="scaletitle"><h3>' + SkyAdsABCTitle + '</h3></div>';
    var info = '<div class="scaleinfo"><p>' + SkyAdsABCInfo + '</p> </div><div class="scalemininfo"><p>' + SkyAdsABCMinInfo + '</p> </div>';
    $('#adsabc').append(title + info);
    for (var i = 0; i < SkyAdsABCData.length; i++) {

        var aa = '<div class="scaleitem"  style="display: none">';
        var bb = '<p>' + SkyAdsABCData[i].Number + '、' + SkyAdsABCData[i].Title + '</p>';
        var cc = '<input name="' + SkyAdsABCData[i].Number + '" data-Dimension="' + SkyAdsABCData[i].Dimension + '" class="rdolist" type="radio"  value="' + SkyAdsABCData[i].score + '">';
        var dd = '<label class="rdobox unchecked items"  data-num=' + SkyAdsABCData[i].Number + ' onclick="return do_next(' + i + ')"><span class="radiobox-content">是</span> </label>';
        var ee = '<input name="' + SkyAdsABCData[i].Number + '" data-Dimension="' + SkyAdsABCData[i].Dimension + '" class="rdolist" type="radio" value="0">';
        var ff = '<label class="rdobox unchecked items"  onclick="do_next(' + i + ')"><span class="radiobox-content">否</span> </label>';

        $('#adsabc').append(aa + bb + cc + dd + ee + ff);


    }

    var prebtn = '<div class="scalebtn"><button type="button" id="id_pre_link"  style="display: none" class="skybtn skybtn-success" data-num="0"  onclick="return do_prev()">上 一 题</button> ';

    var sumbitbtn = ' <button type="submit" id="id_show_result"  style="display: none" class="skybtn skybtn-primary"  onclick="return do_result()">提交答案</button></div>';
    var hh = '</div>';
    $('#adsabc').append(prebtn + sumbitbtn + hh);

}


function do_prev() {
    var j = Number($('#id_pre_link').attr("data-num"));


    $('#id_show_result').hide();
    $('#id_pre_link').attr("data-num", j - 1);
    $('.scaleitem')[j - 1].style.display = "";
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
        $('#id_pre_link').attr("data-num", k + 1);

    } else {
        $('#id_show_result').show();
        $('#id_pre_link').attr("data-num", k);
    }

}

function do_result() {

    //总得分
    var aa = "";

    for (var i = 1; i < SkyAdsABCData.length + 1; i++) {
        var aa = aa + $('[name="' + i + '"]:checked').val() + ",";
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

