<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="xuat-san-pham.aspx.cs" Inherits="hauphatpottery.Pages.xuat_san_pham" %>

<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="DevExpress.Web.v12.1" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v12.1" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v12.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register src="../Calendar/pickerAndCalendar.ascx" tagname="pickerAndCalendar" tagprefix="uc1" %>
<%@ Register Assembly="Karpach.WebControls" Namespace="Karpach.WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function FormatNumber(obj) {
        var strvalue;
        if (eval(obj))
            strvalue = eval(obj).value;
        else
            strvalue = obj;
        var num;
        num = strvalue.toString().replace(/\$|\,/g, '');
        if (isNaN(num))
            num = "";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num * 100 + 0.50000000001);
        num = Math.floor(num / 100).toString();
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' +
   num.substring(num.length - (4 * i + 3));
        //return (((sign)?'':'-') + num); 
        eval(obj).value = (((sign) ? '' : '-') + num);
    }    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header" style="padding-bottom: 5px;">
        <div class="title">
            Xuất sản phẩm
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveClose" ToolTip="Lưu và đóng" CssClass="k-button"
                runat="server" OnClick="lbtnSaveClose_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-saveclose.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveNew" ToolTip="Lưu và thêm mới" CssClass="k-button"
                runat="server" OnClick="lbtnSaveNew_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save-new.png" /></asp:LinkButton>
            &nbsp;<%--<asp:LinkButton ID="lbtnDelete" ToolTip="Xóa" CssClass="k-button" runat="server"
                OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>--%>
            &nbsp;<asp:LinkButton ID="lbtnClose" ToolTip="Đóng" CssClass="k-button" runat="server"
                OnClick="lbtnClose_Click"><img alt="Đóng" src="../Images/icon-32-cancel.png" /></asp:LinkButton>
        </div>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="1" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" TabSpacing="3px"
                    Height="100%" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Thông tin chung" Name="thongtinchung">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>                                                
                                                <asp:Label ID="Lberrors" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Mã xuất
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtCode" runat="server" class="text" 
                                                    CssClass="k-textbox textbox" Width="400px"></asp:TextBox>                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Chưa nhập mã xuất hàng"
                                                    ControlToValidate="txtCode" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Đơn hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList runat="server" ID="ddlOrder" CssClass="k-textbox textbox" 
                                                    AppendDataBoundItems="true" DataTextField="Code" DataValueField="Id" 
                                                    Width="400px" AutoPostBack="True" 
                                                    OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="--Chọn Đơn Hàng--"></asp:ListItem>
                                                </asp:DropDownList>                                               
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Chưa chọn đơn hàng"
                                                    ControlToValidate="ddlOrder" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Thời hạn giao hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList runat="server" ID="ddlOrderDeadline" CssClass="k-textbox textbox" 
                                                    Width="400px">
                                                </asp:DropDownList>                                               
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ErrorMessage="Chưa chọn thời hạn giao hàng"
                                                    ControlToValidate="ddlOrderDeadline" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Ngày xuất
                                            </td>
                                            <td align="left" nowrap="nowrap">                                                
                                                <uc1:pickerAndCalendar ID="pickerAndCalendarDeliDate" runat="server" />
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;"></span>&nbsp;Ghi chú
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNote" runat="server" class="text" CssClass="k-textbox textbox" TextMode="MultiLine" Width="400" Rows="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Danh sách chi tiết" Name="danhsachchitiet" Visible="false">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                        <div id="div-search">
                                        <asp:Panel ID="pnContract" runat="server" DefaultButton="lbtnSaveDetail">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td align="left" nowrap="nowrap">
                                                            <asp:DropDownList runat="server" ID="ddlProductDetail" 
                                                                CssClass="k-textbox textbox" DataTextField="Code" DataValueField="Id" 
                                                                Width="200" AutoPostBack="True" 
                                                                OnSelectedIndexChanged="ddlProductDetail_SelectedIndexChanged">
                                                                <%--<asp:ListItem Value="0" Text="--Chọn Sản Phẩm Chi Tiết--"></asp:ListItem>--%>
                                                            </asp:DropDownList>                                               
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa chọn sản phẩm chi tiết"
                                                                ControlToValidate="ddlProductDetail" Display="None" ForeColor="Red" ValidationGroup="G21"
                                                                CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        </td>     
                                                        <td align="left" nowrap="nowrap">
                                                            <asp:DropDownList runat="server" ID="ddlProductDetailSize" CssClass="k-textbox textbox" Width="200">
                                                            </asp:DropDownList>                                               
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Chưa chọn size"
                                                                ControlToValidate="ddlProductDetailSize" Display="None" ForeColor="Red" ValidationGroup="G21"
                                                                CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        </td>                                                   
                                                        <td>
                                                            <%--<asp:TextBox ID="txtQuantity" runat="server" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);" Width="70" placeholder="Số lượng"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtQuantity" runat="server" class="text" CssClass="k-textbox textbox" onkeypress="return digits(this, event, false, false);" Width="70" placeholder="Số lượng"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập số lượng"
                                                                                        ControlToValidate="txtQuantity" Display="None" ForeColor="Red" ValidationGroup="G21"
                                                                                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ErrorMessage="Chưa nhập số lượng"
                                                                                                                ControlToValidate="txtQuantity" Display="None" 
                                                                                        ForeColor="Red" ValidationGroup="G2"
                                                                                                                CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtnSaveDetail" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                                                    OnClick="lbtnSaveDetail_Click" ValidationGroup="G21"><img alt="Lưu thông tin" src="../Images/icon-20-save.png" /></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtnDeleteDetail" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                                                            ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDeleteDetail_Click"><img alt="Xóa" src="../Images/icon-20-trash.png" /></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            </asp:Panel>
                                        </div>
                                        <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
                                            border: 1px solid #aecaf0">
                                            <tr>
                                                <td>
                                                    <dx:ASPxGridView ID="ASPxGridView1_OrderDetail" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" KeyFieldName="ID" Theme="Aqua" Settings-ShowFooter="True">
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="45px">
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                                                                <DataItemTemplate>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                </DataItemTemplate>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã sản phẩm" Width="100px">
                                                                <DataItemTemplate>
                                                                        <span onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ToolTip/ToolTip.aspx?oid=<%# getProductIdByProductDetailId(Eval("PRODUCT_DETAIL_ID")) %>'); return false;"><%# getProductDetailCode(Eval("Product_Detail_Id"))%></span> 
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sản phẩm chi tiết" Width="250px">
                                                                <DataItemTemplate>
                                                                        <span onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ToolTip/ToolTip.aspx?oid=<%# getProductIdByProductDetailId(Eval("PRODUCT_DETAIL_ID")) %>'); return false;"><%# getProductDetailName(Eval("Product_Detail_Id"))%></span> 
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Loại hàng" Width="150px">
                                                                <DataItemTemplate>
                                                                        <%# getTypeName(Eval("Product_Detail_Id"))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số lượng đặt"  Width="100px">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(getSoluongDat(Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%> <%# getProductDetailSize(Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID"))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL đã kiểm tra" Width="140px">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(getSoluongKiemtra(Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn> 
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL xuất" Width="100px">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(Eval("QUANTITY"))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>    
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL còn lại">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(getSoluongConlai(Eval("QUANTITY"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>                                                            
                                                        </Columns>
                                                        <Settings ShowHorizontalScrollBar="true" />
                                                        <Settings VerticalScrollableHeight="350" />
                                                        <Settings ShowVerticalScrollBar="true" />
                                                        <SettingsPager PageSize="30">
                                                        </SettingsPager>
                                                        <Settings ShowVerticalScrollBar="True" ShowHorizontalScrollBar="True" VerticalScrollableHeight="350"></Settings>
                                                    </dx:ASPxGridView>
                                                </td>
                                            </tr>
                                        </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>                        
                    </TabPages>
                    <LoadingPanelImage Url="~/App_Themes/Aqua/Web/Loading.gif">
                    </LoadingPanelImage>
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />

                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px"></Paddings>

                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
<Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px"></Border>
                    </ContentStyle>
                </dx:ASPxPageControl>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G2" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="G21" />                
            </td>
        </tr>
    </table>
</asp:Content>
