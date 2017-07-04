/////////////////////////////////////////////////////
//可移动的显示窗口(实际是一个层)
//
//本模块大部分函数带mw前缀
//
//add by zealot   2003/12/31
////////////////////////////////////////////////////

function mwInitD()
{
	mwGetYear();
	mwGetMonth();
	mwGetDay();
}

function mwInitM()
{
	
	mwGetYear();
	mwGetMonth();

}

function mwInitJD()
{
	
	mwGetYear();
	mwGetJD();

}

function mwGetYear()
{
	var i;
	var szTemp = "";

	for (i=0;i<gAryYear.length;i++)
	{
		if (gAryYear[i].split("=")[1] == gStrYearVal)
		{
			szTemp += '<option value=' + gAryYear[i].split("=")[1] + ' selected>' + gAryYear[i].split("=")[0] + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>';
		}
		else
		{
			szTemp += '<option value=' + gAryYear[i].split("=")[1] + '>' + gAryYear[i].split("=")[0] + '</option>';
		}
	}
	
	spanTimeYear.innerHTML = "<select name='selTimeYear' onchange=javascript:meYearMenuChange()>" + szTemp + "</select>";
	gStrSelYear = document.all.selTimeYear.options[document.all.selTimeYear.selectedIndex].value;

}

function mwGetMonth()
{
	var i;
	var szTemp = "";
    
	for (i=0;i<gAryMonth.length;i++)
	{
		if (gAryMonth[i].split("=")[1] == gStrMonth)
		{
			if (gAryMonth[i].split("=")[1] < "10")
			{
				szTemp += '<option value=' + gAryMonth[i].split("=")[1] + ' selected>' + gAryMonth[i].split("=")[0] + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>';
			}
			else
			{
				szTemp += '<option value=' + gAryMonth[i].split("=")[1] + ' selected>' + gAryMonth[i].split("=")[0] + '&nbsp;&nbsp;&nbsp;</option>';
			}
		}
		else
		{
			szTemp += '<option value=' + gAryMonth[i].split("=")[1] + '>' + gAryMonth[i].split("=")[0] + '</option>';
		}
	}
			
	spanTimeMonth.innerHTML = "<select name='selTimeMonth' onchange=javascript:meMonthMenuChange('')>" + szTemp + "</select>";
	gStrSelMonth = document.all.selTimeMonth.options[document.all.selTimeMonth.selectedIndex].value;
	
}

function mwGetDay()
{
	var i;
	var szTemp = "";

	for (i=1;i<32;i++)
	{
		if (i.toString() == gStrDay)
		{
			if (i< 10)
			{
				szTemp += '<option value=0' + i.toString() + ' selected>' + '0' + i.toString() + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>';
			}
			else
			{
				szTemp += '<option value=' + i.toString() + ' selected>' + i.toString() + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>';
			}
		}
		else
		{
			if (i< 10)
			{
				szTemp += '<option value=0' + i.toString() + ' >' + '0' + i.toString() + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>';
			}
			else
			{
				szTemp += '<option value=' + i.toString() + ' >' + i.toString() + '&nbsp;&nbsp;&nbsp;</option>';
			}
		}
	}
	
	spanTimeDay.innerHTML = "<select name='selTimeDay' onchange=javascript:meDayMenuChange('')>" + szTemp + "</select>";
	gStrSelDay = document.all.selTimeDay.options[document.all.selTimeDay.selectedIndex].value;
	
}

function mwGetJD()
{
	var i;
	var szTemp = "";

	for (i=0;i<gAryJD.length;i++)
	{
		if (gAryJD[i].split("=")[1] == gStrJD)
		{
			szTemp += '<option value=' + gAryJD[i].split("=")[1] + ' selected>' + gAryJD[i].split("=")[0] + '</option>';
		}
		else
		{
			szTemp += '<option value=' + gAryJD[i].split("=")[1] + '>' + gAryJD[i].split("=")[0] + '</option>';
		}
	}
			
	spanTimeJD.innerHTML = "<select name='selTimeJD' onchange=javascript:meJDMenuChange('')>" + szTemp + "</select>";
	gStrSelJD = document.all.selTimeJD.options[document.all.selTimeJD.selectedIndex].value;
	
}

//选择年菜单改变时的处理函数
function meYearMenuChange()
{
	
	gStrSelYear = document.all.selTimeYear.options[document.all.selTimeYear.selectedIndex].value;
		
}

//选择月菜单改变时的处理函数
function meMonthMenuChange()
{
	
	gStrSelMonth = document.all.selTimeMonth.options[document.all.selTimeMonth.selectedIndex].value;
		
}

//选择月菜单改变时的处理函数
function meJDMenuChange()
{
	
	gStrSelJD = document.all.selTimeJD.options[document.all.selTimeJD.selectedIndex].value;
		
}

function meDayMenuChange()
{
	gStrSelDay = document.all.selTimeDay.options[document.all.selTimeDay.selectedIndex].value;
}
function mwGetJXR()
{
	var szTemp="";
	var i;
	var gAryJXR;
	gAryJXR = gJXR.split(";");
	for (i=0;i<gAryJXR.length;i++)
	{
		if (i==0){
			szTemp += '<option value=' + gAryJXR[i].split("=")[0] + ' selected>'  + gAryJXR[i].split("=")[1] + '&nbsp;</option>';		
		}
		else
		{
			szTemp += '<option value=' + gAryJXR[i].split("=")[0] + '>' +  gAryJXR[i].split("=")[1] + '&nbsp;</option>';		
		}
	}
	spanJXR.innerHTML = "<select name='selJXR' onchange=javascript:meJXRMenuChange()>" + szTemp + "</select>";
	gStrSelJXR = document.all.selJXR.options[document.all.selJXR.selectedIndex].value;
}

function meJXRMenuChange()
{
	gStrSelJXR = document.all.selJXR.options[document.all.selJXR.selectedIndex].value;
}

function mwSelJXR(vSelJXR)
{
	var szTemp="";
	var i;
	var gAryJXR;
	gAryJXR = gJXR.split(";");
	for (i=0;i<gAryJXR.length;i++)
	{
		if (gAryJXR[i].split("=")[0]==vSelJXR){
			document.all.selJXR.options[i].selected=true;
		}
	}
	gStrSelJXR=vSelJXR;	
}

function mwGetYJR()
{
	var szTemp="";
	var i;
	var gAryYJR;
	gAryYJR = gYJR.split(";");
	for (i=0;i<gAryYJR.length;i++)
	{
		if (i==0){
			szTemp += '<option value=' + gAryYJR[i].split("=")[0] + ' selected>'  + gAryYJR[i].split("=")[1] + '&nbsp;</option>';		
		}
		else
		{
			szTemp += '<option value=' + gAryYJR[i].split("=")[0] + '>' +  gAryYJR[i].split("=")[1] + '&nbsp;</option>';		
		}
	}
	spanYJR.innerHTML = "<select name='selYJR' onchange=javascript:meYJRMenuChange()>" + szTemp + "</select>";
	
	gStrSelYJR = document.all.selYJR.options[document.all.selYJR.selectedIndex].value;
}

function meYJRMenuChange()
{
	gStrSelYJR = document.all.selYJR.options[document.all.selYJR.selectedIndex].value;
}

function mwSelYJR(vSelYJR)
{
	var szTemp="";
	var i;
	var gAryYJR;
	gAryYJR = gYJR.split(";");
	for (i=0;i<gAryYJR.length;i++)
	{
		if (gAryYJR[i].split("=")[0]==vSelYJR){
			document.all.selYJR.options[i].selected=true;
		}
	}
	gStrSelYJR=vSelYJR;	
}


function mwGetJJR()
{
	var szTemp="";
	var i;
	var gAryJJR;
	gAryJJR = gJJR.split(";");
	for (i=0;i<gAryJJR.length;i++)
	{
		if (i==0){
			szTemp += '<option value=' + gAryJJR[i].split("=")[0] + ' selected>'  + gAryJJR[i].split("=")[1] + '&nbsp;</option>';		
		}
		else
		{
			szTemp += '<option value=' + gAryJJR[i].split("=")[0] + '>' +  gAryJJR[i].split("=")[1] + '&nbsp;</option>';		
		}
	}
	spanJJR.innerHTML = "<select name='selJJR' onchange=javascript:meJJRMenuChange()>" + szTemp + "</select>";
	gStrSelJJR = document.all.selJJR.options[document.all.selJJR.selectedIndex].value;
}

function meJJRMenuChange()
{
	gStrSelJJR = document.all.selJJR.options[document.all.selJJR.selectedIndex].value;
}

function mwSelJJR(vSelJJR)
{
	var szTemp="";
	var i;
	var gAryJJR;
	gAryJJR = gJJR.split(";");
	for (i=0;i<gAryJJR.length;i++)
	{
		if (gAryJJR[i].split("=")[0]==vSelJJR){
			document.all.selJJR.options[i].selected=true;
		}
	}
	gStrSelJJR=vSelJJR;	
}


function mwSelYear(vSelYear)
{	
	var i;	

	for (i=0;i<gAryYear.length;i++)
	{
		if (gAryYear[i].split("=")[1] == vSelYear)
		{
			document.all.selTimeYear.options[i].selected=true;
		}
	}
	gStrSelYear=vSelYear;
}

function mwSelMonth(vSelMonth)
{
	var i;
	//var szTemp = "";

	for (i=0;i<gAryMonth.length;i++)
	{
		if (gAryMonth[i].split("=")[1] == vSelMonth)
		{
			document.all.selTimeMonth.options[i].selected=true;
		}
	}
	gStrSelMonth=vSelMonth;
}

function mwSelDay(vSelDay)
{
	var i;
	var szTemp = "";
	for (i=1;i<32;i++)	
	{
		if (i< 10)
		{
			szTemp = '0' + i.toString();
		}
		else
		{
			szTemp =  i.toString();
		}
		if (szTemp == vSelDay)
		{
			document.all.selTimeDay.options[i-1].selected=true;
		}
	}
	gStrSelDay=vSelDay;
}
function mwSelJD(vSelJD)
{
	var i;
	for (i=0;i<gAryJD.length;i++)
	{
		if (gAryJD[i].split("=")[1] == vSelJD)
		{
			gStrSelJD = vSelJD;
			document.all.selTimeJD.options[i].selected=true;
		}
	}
}
function slideopen(){
	parent.bar.innerHTML = '4';
	parent.main_foot.rows = '*,10,250,15';
}
function mwGetTime()
{
	var i;
	var szTemp;
	for (i=0;i<24;i++)
	{
		if (i.toString() == gStrTime)
		{
			if (i < 10)
			{
				szTemp += '<option value=0' + i + ' selected>0' + i + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>';
			}
			else
			{
				szTemp += '<option value=' + i + ' selected>' + i + '&nbsp;&nbsp;&nbsp;</option>';
			}
		}
		else
		{
			if (i < 10)
			{
				szTemp += '<option value=0' + i + '>0' + i + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>';
			}
			else
			{
				szTemp += '<option value=' + i + '>' + i + '&nbsp;&nbsp;&nbsp;</option>';
			}
		}
	}

	spanTime.innerHTML = "<select name='selTime' onchange=javascript:meTimeMenuChange()>" + szTemp + "</select>";

	gStrSelTime = document.all.selTime.options[document.all.selTime.selectedIndex].value;
	
}
function mwSelTime(vSelTime)
{
	var i;
	var szTemp = "";

	for (i=0;i<24;i++)
	{
		if(i<10){
			szTemp="0" + i.toString();
		}else{
			szTemp=i.toString();
		}
		if (szTemp == vSelTime)
		{
			document.all.selTime.options[i].selected=true;
		}
	}
	gStrSelTime=vSelTime;
}

function meTimeMenuChange()
{
	gStrSelTime = document.all.selTime.options[document.all.selTime.selectedIndex].value;
}
