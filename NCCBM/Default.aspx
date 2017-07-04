<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="NCCBM.Default" %>
<%@ Register Namespace="NCCBM.Menu" TagPrefix="cc" Assembly="NCCBM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function SetCwinHeight(obj) {
            var cwin = obj;
            if (document.getElementById) {
                if (cwin && !window.opera) {
                    if (cwin.contentDocument && cwin.contentDocument.body.offsetHeight)
                        cwin.height = cwin.contentDocument.body.offsetHeight + 20; //FF NS
                    else if (cwin.Document && cwin.Document.body.scrollHeight)
                        cwin.height = cwin.Document.body.scrollHeight + 10; //IE
                }
                else {
                    if (cwin.contentWindow.document && cwin.contentWindow.document.body.scrollHeight)
                        cwin.height = cwin.contentWindow.document.body.scrollHeight; //Opera
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float:left;border-left: 2px solid #eeeeee; margin-top:0px;">
        <!-- 左侧菜单 -->
        <div style="float:left;margin-top:10px; margin-left:-1px;">
            <center style="font-size:16px; color:#fff; margin-bottom:-26px; margin-top:0px;">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </center>
            <cc:MenuComposite ID="menuComposite_left" runat="server" />
            <div class="arrowbucross"></div>
        </div>

        <div style="float:right;border-right: 2px solid #eeeeee; margin-left:5px;">
            <!-- 四级菜单 -->
            <div style="clear:left">
                <iframe src="null.htm" name="rightmenu" id="rightmenu" frameborder="0" scrolling="no" style="width:850px;height:35px; margin-top:2px"></iframe>
            </div>
            <!-- 子页面区域 -->
            <div style="clear:right; height:100%;">
                <iframe src="null.htm" name="rightmain" id="rightmain" frameborder="0" scrolling="no" style="width:850px; min-height:400px;" onload="SetCwinHeight(this)"></iframe>
            </div>
        </div>
    </div>
    <div style="display:inline-block;width:980px; height:80px;margin-top:0px;text-indent:-9999px; 
        text-decoration:none; background-image:url(css/res/bottom.jpg); background-repeat:repeat-x;border-radius:4px;"></div>
    <div style="text-align: center; margin-top:-70px">
        <h4 style="color:#333; margin-top:-10px">版权所有：中联煤层气国家工程研究中心有限责任公司</h4>
        <h4 style="color:#333; margin-top:-10px">技术支持:北京思行伟业数码科技有限公司</h4>
        <h6 style="color:#333; margin-top:-10px">
            Copyright &copy; 2012&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            推荐分辨率:&nbsp;1024&nbsp;X&nbsp;768
        </h6>
    </div>
</asp:Content>


    

