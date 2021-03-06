﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="export.aspx.cs" Inherits="NCCBM.Query.zj.export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false" ShowHeader="false" OnRowCreated="gridView_RowCreated">
            <Columns >
                <asp:BoundField DataField="id" />
                <asp:BoundField DataField="riqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="fuzejiandu" />
                <asp:BoundField DataField="shigongduiwu" />
                <asp:BoundField DataField="jinghao" />
                <asp:BoundField DataField="wanzuanjingshen" />
                <asp:BoundField DataField="kaizuanriqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="wanzuanriqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="wanjingshijian" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="zuanjingzhouqi" />
                <asp:BoundField DataField="zuanjixinghao" />
                <asp:BoundField DataField="ykys_riqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="ykys_cunzaiwenti" />
                <asp:BoundField DataField="ykys_shifoutongyikaizuan" />
                <asp:BoundField DataField="ykys_fuyanqingkuang" />
                <asp:BoundField DataField="ekys_riqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="ekys_cunzaiwenti" />
                <asp:BoundField DataField="ekys_shifoutongyikaizuan" />
                <asp:BoundField DataField="ekys_fuyanqingkuang" />
                <asp:BoundField DataField="btsj_biaotaoxiashen" />
                <asp:BoundField DataField="sctgsj_changjiagangji" />
                <asp:BoundField DataField="sctgsj_waiguanjiancha" />
                <asp:BoundField DataField="sctgsj_bianhaohecha" />
                <asp:BoundField DataField="sctgsj_taoguanxiashen" />
                <asp:BoundField DataField="sctgsj_rujingchangtao" />
                <asp:BoundField DataField="sctgsj_shengyuchangtao" />
                <asp:BoundField DataField="sctgsj_rujingduantao" />
                <asp:BoundField DataField="sctgsj_shengyuduantao" />
                <asp:BoundField DataField="sctgsj_duantaoweizhi" />
                <asp:BoundField DataField="qxsj_quxinhuici" />
                <asp:BoundField DataField="qxsj_zongshouhuolv" />
                <asp:BoundField DataField="gjsgsj_gujingdui" />
                <asp:BoundField DataField="gjsgsj_qianzhiye" />
                <asp:BoundField DataField="gjsgsj_zhushuinijiangliang" />
                <asp:BoundField DataField="gjsgsj_tijingliang" />
                <asp:BoundField DataField="gjsgsj_pengyaqingkuang" />
                <asp:BoundField DataField="gjsgsj_shuinijiangmidu" />
                <asp:BoundField DataField="sy_riqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="sy_yajiangqingkuang" />
                <asp:BoundField DataField="wjjc_jingkoushuiping" />
                <asp:BoundField DataField="wjjc_jingkougaodu" />
                <asp:BoundField DataField="wjjc_jingkouhanjie" />
                <asp:BoundField DataField="beizhu1" />
                <asp:BoundField DataField="bianhao" />
                <asp:BoundField DataField="xiaojieriqi" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="jsjg_yikai" />
                <asp:BoundField DataField="jsjg_biaotao" />
                <asp:BoundField DataField="jsjg_erkai" />
                <asp:BoundField DataField="jsjg_chantao" />
                <asp:BoundField DataField="mcsj_3" />
                <asp:BoundField DataField="mcsj_5" />
                <asp:BoundField DataField="mcsj_11" />
                <asp:BoundField DataField="mcsj_qitameiceng" />
                <asp:BoundField DataField="jszlsj_baxinju" />
                <asp:BoundField DataField="jszlsj_zuidajingxie" />
                <asp:BoundField DataField="jszlsj_zuidaweiyi" />
                <asp:BoundField DataField="jszlsj_zuidaquanjiao" />
                <asp:BoundField DataField="jszlsj_zxd1_zuidalianxu" />
                <asp:BoundField DataField="jszlsj_zxd1_lianxusandian" />
                <asp:BoundField DataField="jszlsj_zxd2_zuidalianxu" />
                <asp:BoundField DataField="jszlsj_zxd2_lianxusandian" />
                <asp:BoundField DataField="jszlsj_quanjingduan" />
                <asp:BoundField DataField="jszlsj_meicengduan" />
                <asp:BoundField DataField="xtggjsj_dctg_duantaoshejiweizhi" />
                <asp:BoundField DataField="xtggjsj_dctg_duantaoshiceweizhi" />
                <asp:BoundField DataField="xtggjsj_dctg_zuliuhuanshendu" />
                <asp:BoundField DataField="xtggjsj_yuzushendu" />
                <asp:BoundField DataField="xtggjsj_dctg_meicengduanjieguweizhi" />
                <asp:BoundField DataField="xtggjsj_gjqk_shuinifanshen" />
                <asp:BoundField DataField="xtggjsj_diancepingjia" />
                <asp:BoundField DataField="beizhu2" />
                <asp:BoundField DataField="jbsj_jingbie" />
                <asp:BoundField DataField="jbsj_jingxing" />
                <asp:BoundField DataField="jbsj_diliweizhi" />
                <asp:BoundField DataField="jbsj_shejijingshen" />
                <asp:BoundField DataField="sjjsjg_yikai" />
                <asp:BoundField DataField="sjjsjg_taoguanxiashen" />
                <asp:BoundField DataField="sjjsjg_erkai" />
                <asp:BoundField DataField="sjjsjg_chantaoxiashen" />
                <asp:BoundField DataField="dxsj_jingkouzuobiao_x" />
                <asp:BoundField DataField="dxsj_jingkouzuobiao_y" />
                <asp:BoundField DataField="dxsj_jingkouhaiba" />
                <asp:BoundField DataField="dxsj_jingdichuishen" />
                <asp:BoundField DataField="dxsj_badianzuobiao_x" />
                <asp:BoundField DataField="dxsj_badianzuobiao_y" />
                <asp:BoundField DataField="dxsj_badianfangwei" />
                <asp:BoundField DataField="dxsj_badianchuishen" />
                <asp:BoundField DataField="dxsj_badianweiyi" />
                <asp:BoundField DataField="dxsj_cipianjiao" />
                <asp:BoundField DataField="dxsj_damenfangxiang" />
                <asp:BoundField DataField="dxsj_zaoxieduan" />
                <asp:BoundField DataField="dxsj_shejijingxie" />
                <asp:BoundField DataField="place" />
                <asp:BoundField DataField="fujian" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
