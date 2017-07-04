// JScript 文件
function catch_keydown(select_obj) 
{     
    //对DropDownList的按键按回车时捕捉，实现快速定位            
    switch(event.keyCode) 
    { 
        case 13: //Enter; 
            var aa=select_obj.options[0].text;
            var qq=aa.length; 
            if (select_obj.options.length)
            { 
                for (var i=0;i< select_obj.options.length ;i++)
                { 
                    if (select_obj.options[i].value.substr(0,qq)==aa)
                    { 
                        select_obj.selectedIndex=i;
                        alert(select_obj.options[i].value);
                        return;
                     }
                 }
             }
            event.returnValue = false; 
            break; 
     } 
 }
 
 
 function Fcalendar(field,strTimeUrl)
{
   var rtn = window.showModalDialog(strTimeUrl,"","dialogWidth:290px;dialogHeight:250px;status:no;help:no;scrolling=no;scrollbars=no");
   if(rtn!=null)
    field.value=rtn;
    return;
} 
function GetToEmp(strUrl)
{
   var rtn = window.showModalDialog(strUrl,"emp","dialogWidth:290px;dialogHeight:250px;status:no;help:no;scrolling=no;scrollbars=no");
   return;
} 
function getEmployee()
{
    var Employee_Name= document.all.hidEmp.value;
    var Employee_ID = document.all.hidEmpID.value;
	if(opener.frmDisplay.txtTo_Emp){
	    
		opener.frmDisplay.txtTo_Emp.value = Employee_Name;
		opener.frmDisplay.txtTo_Emp_ID.value = Employee_ID;	
	}
    
    window.close();
}
function myPrint(iType)
{ 
    switch(iType)
    {
        case 0:
           document.all.WebBrowser.ExecWB(6,1);
           break;
        case 1:
           document.all.WebBrowser.ExecWB(7,1);
           break;
        case 2:
           document.all.WebBrowser.ExecWB(8,1);
           break;   
    }         
}
function DrawImage(ImgD,iwidth,iheight){
    //参数(图片,允许的宽度,允许的高度)
    //图片按比例缩放
    var flag=false;
    var image=new Image();
    image.src=ImgD.src;
    if(image.width>0 && image.height>0){
    flag=true;
    if(image.width/image.height>= iwidth/iheight){
        if(image.width>iwidth){  
        ImgD.width=iwidth;
        ImgD.height=(image.height*iwidth)/image.width;
        }else{
        ImgD.width=image.width;  
        ImgD.height=image.height;
        }
        ImgD.alt=image.width+"×"+image.height;
        }
    else{
        if(image.height>iheight){  
        ImgD.height=iheight;
        ImgD.width=(image.width*iheight)/image.height;        
        }else{
        ImgD.width=image.width;  
        ImgD.height=image.height;
        }
        ImgD.alt=image.width+"×"+image.height;
        }
    }
} 
