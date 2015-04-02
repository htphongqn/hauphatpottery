<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="maps.aspx.cs" Inherits="CatTrang.vi_vn.maps" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../ajax/ajax.js" type="text/javascript"></script>
    <script src="../ajax/ajax-tooltip.js" type="text/javascript"></script>
    <script src="../ajax/ajax-dynamic-content.js" type="text/javascript"></script>
<script>

    // stores the device context of the canvas we use to draw the outlines
    // initialized in myInit, used in myHover and myLeave
    var hdc;

    // shorthand func
    function byId(e) { return document.getElementById(e); }

    // takes a string that contains coords eg - "227,307,261,309, 339,354, 328,371, 240,331"
    // draws a line from each co-ord pair to the next - assumes starting point needs to be repeated as ending point.
    function drawPoly(coOrdStr) {
        var mCoords = coOrdStr.split(',');
        var i, n;
        n = mCoords.length;

        hdc.beginPath();
        hdc.moveTo(mCoords[0], mCoords[1]);
        for (i = 2; i < n; i += 2) {
            hdc.lineTo(mCoords[i], mCoords[i + 1]);
        }
        hdc.lineTo(mCoords[0], mCoords[1]);
        hdc.stroke();
    }

    function drawRect(coOrdStr) {
        var mCoords = coOrdStr.split(',');
        var top, left, bot, right;
        left = mCoords[0];
        top = mCoords[1];
        right = mCoords[2];
        bot = mCoords[3];
        hdc.strokeRect(left, top, right - left, bot - top);
    }

    function myHover(element) {
        var hoveredElement = element;
        var coordStr = element.getAttribute('coords');
        var areaType = element.getAttribute('shape');

        switch (areaType) {
            case 'polygon':
            case 'poly':
                drawPoly(coordStr);
                break;

            case 'rect':
                drawRect(coordStr);
        }
    }

    function myLeave() {
        var canvas = byId('myCanvas');
        hdc.clearRect(0, 0, canvas.width, canvas.height);
    }

    function myInit() {
        // get the target image
        var img = byId('img-imgmap');

        var x, y, w, h;

        // get it's position and width+height
        x = img.offsetLeft;
        y = img.offsetTop;
        w = img.clientWidth;
        h = img.clientHeight;

        // move the canvas, so it's contained by the same parent as the image
        var imgParent = img.parentNode;
        var can = byId('myCanvas');
        imgParent.appendChild(can);

        // place the canvas in front of the image
        can.style.zIndex = 1;

        // position it over the image
        can.style.left = x + 'px';
        can.style.top = y + 'px';

        // make same size as the image
        can.setAttribute('width', w + 'px');
        can.setAttribute('height', h + 'px');

        // get it's context
        hdc = can.getContext('2d');

        // set the 'default' values for the colour/width of fill/stroke operations
        hdc.fillStyle = 'gradient';
        hdc.strokeStyle = 'blue';
        hdc.lineWidth = 5;
    }
</script>
<style>
canvas {
	pointer-events: none;       /* make the canvas transparent to the mouse - needed since canvas is position infront of image */
	position: absolute;
}
.BBTOnlineTooltip
{
    z-index:999;
}
</style>
<title></title>

</head>
<body onload='myInit()'>
    <form id="form1" runat="server">
    <canvas id='myCanvas'></canvas>
<!-- gets re-positioned in myInit(); -->
<center>
  <img src='../Images/hoan-vu_so-do.jpg' usemap='#imgmap_css_container_imgmap' class='imgmap_css_container' title='imgmap' alt='imgmap' id='img-imgmap' />
  <map id='imgmap' name='imgmap_css_container_imgmap'>  
    <%--<area href="" shape="circle" coords="303,315,13" />
    <area href="/diamond-bay-golf-villas/diamond-bay-golf-villas.html" target="_parent" shape="circle" coords="155,185,13" />
    <area href="3.html" shape="circle" coords="451,448,13" />
    <area href="4.html" shape="circle" coords="593,526,13" />
    <area href="5.html" shape="circle" coords="327,452,13" />--%>
    <area href="area7.html" shape="poly" onMouseOver='myHover(this);' onMouseOut='myLeave();' coords="55,192,44,214,43,225,69,233,79,202" alt="imgmap-6" title="imgmap-6" class="imgmap-area" id="imgmap-area-6" />
    <area href="area8.html" shape="poly" onMouseOver='myHover(this);' onMouseOut='myLeave();' coords="309,406,293,430,312,446,330,422" alt="imgmap-7" title="imgmap-7" class="imgmap-area" id="imgmap-area-7" />
    <%--5--%>    
    <a href="javascript:void(0)" title="" onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ajax/vi-vnToolTip.aspx?oid=58'); return false;">
    <area href="/universe-crown-convention-center/universe-crown-convention-center.html" shape="poly" onMouseOver='myHover(this);' onMouseOut='myLeave();' coords="197,386,188,356,203,331,212,319,406,459,515,521,487,600,444,607,406,597,419,520,409,515,382,506,318,468,267,464,241,457,245,429" alt="imgmap-0" title="imgmap-0" class="imgmap-area" id="imgmap-area-0" />
    </a>
    <%--1--%>    
    <a href="javascript:void(0)" title="" onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ajax/vi-vnToolTip.aspx?oid=31'); return false;">
    <area href="/diamond-bay-resort-ii-pro/diamond-bay-resort-ii.html" shape="poly" onMouseOver='myHover(this);' onMouseOut='myLeave();' coords="363,343,391,337,356,402,344,404,228,314,234,308,244,309,270,323,301,314,331,320" alt="imgmap-1" title="imgmap-1" class="imgmap-area" id="imgmap-area-1" />
    </a>
    <%--3--%>    
    <a href="javascript:void(0)" title="" onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ajax/vi-vnToolTip.aspx?oid=28'); return false;">
    <area href="/diamond-bay-resort-spa/diamond-bay-resort-spa1.html" shape="poly" onMouseOver='myHover(this);' onMouseOut='myLeave();' coords="468,482,444,473,405,450,363,417,361,407,400,352,405,396,423,409,487,399,523,409,525,417" alt="imgmap-2" title="imgmap-2" class="imgmap-area" id="imgmap-area-2" />
    </a>
    <%--4--%>    
    <a href="javascript:void(0)" title="" onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ajax/vi-vnToolTip.aspx?oid=32'); return false;">
    <area href="/diamond-bay-driving-range-golf-academy/diamond-bay-driving-range-golf-academy.html" shape="poly" onMouseOver='myHover(this);' onMouseOut='myLeave();' coords="654,542,578,511,571,526,574,536,661,570,668,556" alt="imgmap-3" title="imgmap-3" class="imgmap-area" id="imgmap-area-3" />
    </a>
    <%--6--%>    
    <%--<a href="javascript:void(0)" title="" onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ajax/vi-vnToolTip.aspx?oid=30'); return false;">--%>
    <area href="javascript:void(0)" shape="poly" onMouseOver='myHover(this);' onMouseOut='myLeave();' coords="618,80,603,101,569,113,551,129,530,155,534,169,555,177,570,188,581,205,591,226,609,236,632,218,624,190,612,185,620,152,640,132,657,137,671,132,659,88,635,90" alt="imgmap-4" title="imgmap-4" class="imgmap-area" id="imgmap-area-4" />
    <%--</a>--%>
    <%--2--%>    
    <a href="javascript:void(0)" title="" onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ajax/vi-vnToolTip.aspx?oid=30'); return false;">
    <area href="/diamond-bay-golf-course/diamond-bay-golf-course.html" target="_parent" shape="poly" onMouseOver='myHover(this);' onMouseOut='myLeave();' coords="207,80,229,71,232,25,279,18,317,71,351,105,344,133,368,154,362,164,395,183,417,188,438,200,442,214,399,266,382,256,363,253,332,239,313,238,280,222,171,281,96,328,61,332,41,318,8,248,38,162,47,151,41,136,58,117,71,119,88,102,92,72,127,66,152,85,183,80" alt="imgmap-5" title="imgmap-5" class="imgmap-area" id="imgmap-area-5" />   
    </a>
  </map>
</center>
    </form>
</body>
</html>
