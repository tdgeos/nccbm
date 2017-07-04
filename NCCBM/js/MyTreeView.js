// JScript 文件
/**************************************************************************
   ASP.NET 2.0 TreeView控件 客户端CheckBox Chanage事件,兼容IE,OPERA,FIREFOX
   By YJPC789 2007-9-15
1. 每勾选一个节点就要遍历勾选它的所有子节点         setChildChecked
    同时循环勾选父节点                                                     setParentChecked

2. 每取消勾选一个节点就要遍历取消它的所有子节点  setChildUnChecked
    如果同级其他的节点都未勾选,则循环取消父节点     setParentUnChecked

   CheckBox的ID形如:TreeView1n1CheckBox
   树ID+'n'+编号+'CheckBox' 编号从0开始, 
   它的所有子节点都会在ID为TreeView1n1Nodes的Div中.
**************************************************************************/

//TreeView onclick触发事件
function client_OnTreeNodeChecked(event)
{
    //得到当前所Click的对象
    var objNode;
    
    if(!public_IsObjectNull(event.srcElement))
    {
        //IE
        objNode = event.srcElement;
    }
    else
    {
        //FF
        objNode = event.target;
    }

    //判断是否Click的CheckBox
    if(!public_IsCheckBox(objNode))
        return;

    var objCheckBox = objNode;
    //根据CheckBox状态进行相应处理
    if(objCheckBox.checked==true)
    {
        //递归选中父节点的CheckBox
        setParentChecked(objCheckBox);
        
        //递归选中所有的子节点
        setChildChecked(objCheckBox);
    }
    else
    {        
        //递归取消选中所有的子节点
        setChildUnChecked(objCheckBox);
        
        //递归取消选中父节点(如果当前节点的所有其他同级节点也都未被选中).
        setParentUnChecked(objCheckBox);
    }
}

//判断对象是否为空
function public_IsObjectNull(element)
{
    if(element==null || element == "undefined")
        return true;
    else
        return false;
}

//判断对象是否为CheckBox
function public_IsCheckBox(element)
{
    if(public_IsObjectNull(element))
        return false;
        
    if(element.tagName!="INPUT" || element.type!="checkbox")
        return false;
    else
        return true;
}
//得到包含所有子节点的Node(Div对象)
function public_CheckBox2Node(element)
{
    var objID = element.getAttribute("ID");
    objID = objID.substring(0,objID.indexOf("CheckBox")); 
    return document.getElementById(objID+"Nodes");
}
//得到父节点的CheckBox
function public_Node2CheckBox(element)
{
    var objID = element.getAttribute("ID");
    objID = objID.substring(0,objID.indexOf("Nodes")); 
    return document.getElementById(objID+"CheckBox");
}
//得到本节点所在的Node(Div对象)
function public_GetParentNode(element) 
{
    var parent = element.parentNode;
    var upperTagName = "DIV";
    //如果这个元素还不是想要的tag就继续上溯
    while (parent && (parent.tagName.toUpperCase() != upperTagName)) 
    {
        parent = parent.parentNode ? parent.parentNode : parent.parentElement;
    }
    return parent;
}


//设置节点的父节点Checked
function setParentChecked(currCheckBox)
{ 
    var objParentNode= public_GetParentNode(currCheckBox);
    if(public_IsObjectNull(objParentNode))
        return;    
    
    var objParentCheckBox = public_Node2CheckBox(objParentNode);

    if(!public_IsCheckBox(objParentCheckBox))
        return; 
        
    objParentCheckBox.checked = true;
    setParentChecked(objParentCheckBox);
}

//当父节点的所有子节点都未被选中时,设置父节点UnChecked
function setParentUnChecked(currCheckBox)
{
    var objParentNode= public_GetParentNode(currCheckBox);
    if(public_IsObjectNull(objParentNode))
        return;   
    //判断currCheckBox的同级节点是否都为UnChecked.
    if(!IsMyChildCheckBoxsUnChecked(objParentNode))
        return;    
    
    var objParentCheckBox = public_Node2CheckBox(objParentNode);

    if(!public_IsCheckBox(objParentCheckBox))
        return; 
        
    objParentCheckBox.checked = false;    
    setParentUnChecked(objParentCheckBox);
}

//设置节点的子节点UnChecked
function setChildUnChecked(currObj)
{
    var currNode;
    if(public_IsCheckBox(currObj))
    {
        currNode = public_CheckBox2Node(currObj);
        if (public_IsObjectNull(currNode))
            return;
    }
    else
        currNode = currObj;
       
    var currNodeChilds = currNode.childNodes;
    var count = currNodeChilds.length; 
    for(var i=0;i<count;i++)
    {
        var childCheckBox = currNodeChilds[i];
        if(public_IsCheckBox(childCheckBox))
        {
            childCheckBox.checked = false;
        }
        setChildUnChecked(childCheckBox); 
    }
}

//设置节点的子节点Checked
function setChildChecked(currObj)
{ 
    var currNode;
    if(public_IsCheckBox(currObj))
    {
        currNode = public_CheckBox2Node(currObj);
        if (public_IsObjectNull(currNode))
            return;
    }
    else
        currNode = currObj;
        
    var currNodeChilds = currNode.childNodes;
    var count = currNodeChilds.length; 
    for(var i=0;i<count;i++)
    {
        var childCheckBox = currNodeChilds[i];
        if(public_IsCheckBox(childCheckBox))
        {
            childCheckBox.checked = true;
        }
        setChildChecked(childCheckBox); 
    }
}

//判断该节点的子节点是否都为UnChecked
function IsMyChildCheckBoxsUnChecked(currObj)
{
    var retVal = true;
    
    var currNode;
    if(public_IsCheckBox(currObj) && currObj.checked == true)
    {
        return false;
    }
    else
        currNode = currObj;
       
    var currNodeChilds = currNode.childNodes;
    var count = currNodeChilds.length; 
    for(var i=0;i<count;i++)
    {
        if (retVal == false)
            break;
        var childCheckBox = currNodeChilds[i];
        if(public_IsCheckBox(childCheckBox) && childCheckBox.checked == true)
        {
            retVal = false;
            return retVal;
        }
        else
            retVal = IsMyChildCheckBoxsUnChecked1(childCheckBox);         
    }
    return retVal;
}
