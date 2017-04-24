//量表定义
     var SkyAdsABCTitle = '自闭症行为（ABC）量表';
     var SkyAdsABCInfo = '《自闭症行为量表》——ABC量表，由KRUG于1978年编制，表中列出57项自闭症儿童的行为特征，包括感觉能力（S）、交往能力（R）、运动能力（B）、语言能力（L）和自我照顾能力（S）五个方面。要求评定者与儿童至少共同生活3-6周，填写者与儿童生活至少半年以上的教师。评分时，对每一项作“是”与“否”的判断。“是”评记“∨”符号，“否”不打号。把“是”的项目合计累分，总分≥31分为自闭症筛查界限分；总分>53分作为自闭症诊断界限分（参考值）';
     var SkyAdsABCData = [
         {
             Number: '1',
             Title: '喜欢长时间的自身旋转1',
             Dimension: 'A',
             score: '4'

         },
        {
             Number: '2',
             Title: '学会做一件简单的事，但很快就忘记',
             Dimension: 'B',
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
     for (var i = 0; i < SkyAdsABCData.length;i++)
     {
        
          var aa = '<div class="test_contents" style="display: none"> <p class="descs fb h3">' + SkyAdsABCData[i].Number +'、'+ SkyAdsABCData[i].Title + '</p>';
         var aab = '<div class="sels_list">';
         var bb = '<div class="items"> <p class="i_top"></p><p class="i_mid"> <span class="sels"> <input type="radio" value="' + SkyAdsABCData[i].score + '" name="' + SkyAdsABCData[i].Dimension + '" /></span>是</p><p class="i_bot"></p> </div>';
         var cc = '<div class="items"> <p class="i_top"></p><p class="i_mid"> <span class="sels"> <input type="radio" value="' + SkyAdsABCData[i].score + '" name="' + SkyAdsABCData[i].Dimension + '" /></span>否</p><p class="i_bot"></p> </div>';
         var ccd = '</div></div>';
         $('#adsabc').append(aa + aab + bb + cc + ccd);
     
     }
 }
   
//初始化量表
 $('.adsabc').adsabcInit();






