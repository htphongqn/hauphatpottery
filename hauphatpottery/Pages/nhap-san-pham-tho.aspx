<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="nhap-san-pham-tho.aspx.cs" Inherits="hauphatpottery.Pages.nhap_san_pham_tho" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v12.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

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
<div id="header">
        <div class="title">
            Nhập Sản Phẩm Thô
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div id="div-search">
    <asp:Panel ID="pnContract" runat="server">
        <table>
            <tbody>
                <tr>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="G2" />
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlOrder" 
                            CssClass="k-textbox textbox" AppendDataBoundItems="true" DataTextField="Code" 
                            DataValueField="Id" Width="200" AutoPostBack="True" 
                            onselectedindexchanged="ddlOrder_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Chọn Đơn Hàng--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlProductDetail" 
                            CssClass="k-textbox textbox" DataTextField="Code" 
                            DataValueField="Id" Width="200"  AutoPostBack="True" 
                                                                OnSelectedIndexChanged="ddlProductDetail_SelectedIndexChanged">
                            <%--<asp:ListItem Value="0" Text="--Chọn Sản Phẩm Chi Tiết--"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Chưa chọn sản phẩm"
                            ControlToValidate="ddlProductDetail" Display="None" ForeColor="Red" ValidationGroup="G2"
                            CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <input class="k-textbox" width="40" id="txtQuantity" onkeypress="return digits(this, event, false, true);"
                            name="txtQuantity" type="text" value="" runat="server" placeholder="Số lượng" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập số lượng"
                                                    ControlToValidate="txtQuantity" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ErrorMessage="Chưa nhập số lượng"
                                                    ControlToValidate="txtQuantity" Display="None" 
                            ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlProductDetailSize" CssClass="k-textbox textbox" Width="200">
                        </asp:DropDownList>                                               
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Chưa chọn size"
                            ControlToValidate="ddlProductDetailSize" Display="None" ForeColor="Red" ValidationGroup="G2"
                            CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnSave" ToolTip="Nhập sản phẩm" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G2">Nhập</asp:LinkButton>
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
                <dx:ASPxGridView ID="ASPxGridView1_Order_ProductDetail" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua">
                    <Columns>
                        <%--<dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="45px">
                        </dx:GridViewCommandColumn>--%>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                            <DataItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã sản phẩm chi tiết" FieldName="PRODUCT_DETAIL_ID"  Width="120px">
                            <DataItemTemplate>
                                    <span onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ToolTip/ToolTip.aspx?oid=<%# getProductIdByProductDetailId(Eval("PRODUCT_DETAIL_ID")) %>'); return false;"><%# getShortString(getCodeProductDetail(Eval("PRODUCT_DETAIL_ID")), 40)%></span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sản phẩm chi tiết" FieldName="PRODUCT_DETAIL_ID"  Width="250px">
                            <DataItemTemplate>
                                    <%# getShortString(getNameProductDetail(Eval("PRODUCT_DETAIL_ID")), 40)%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>--%>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sản phẩm chi tiết" FieldName="PRODUCT_DETAIL_ID"  Width="250px">
                            <DataItemTemplate>
                                    <span onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ToolTip/ToolTip.aspx?oid=<%# getProductIdByProductDetailId(Eval("PRODUCT_DETAIL_ID")) %>'); return false;"><%# getShortString(getProductDetailName(Eval("PRODUCT_DETAIL_ID")), 40)%></span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số lượng đặt">
                            <DataItemTemplate>
                                <%# getFormatQuantity(Eval("QUANTITY"))%> <%# getProductDetailSize(Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>       
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL thô đã làm" Width="140px">
                            <DataItemTemplate>
                                <%# getFormatQuantity(getSoluongDalam(Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>          
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL còn lại">
                            <DataItemTemplate>
                                <%# getFormatQuantity(getSoluongConlai(Eval("QUANTITY"), Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lnkHistory" runat="server" Text="Lịch sử nhập"></asp:LinkButton>
                                <asp:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="ModalPopupBG" 
                                    PopupDragHandleControlID="PopupHeaderMessage" CancelControlID="lnkClose"
                                    TargetControlID="lnkHistory" PopupControlID="pnlPopup" Drag="True" RepositionMode="None">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="pnlPopup" runat="server" class="popupConfirmation" Style="display: none;">
                                    <div class="popup_Container">
                                        <div class="popup_Titlebar" id="PopupHeaderMessage">
                                            <div class="TitlebarLeft">
                                                <%# getShortString(getProductDetailName(Eval("PRODUCT_DETAIL_ID")), 40)%>
                                            </div>
                                            <asp:LinkButton ID="lnkClose" runat="server" CssClass="TitlebarRight" CausesValidation="false"/>
                                        </div>
                                        <div class="popup_Body">    
                                            <asp:Repeater ID="rptHistory" runat="server" DataSource='<%# getListHistory(Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID"))%>'>
                                                <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 100px" valign="middle" align="center">
                                                                Mã
                                                            </td>
                                                            <td style="width: 100px" valign="middle" align="center">
                                                                Số lượng nhập
                                                            </td>
                                                            <td valign="middle" align="left">                                
                                                                Ngày nhập
                                                            </td>
                                                            <td valign="middle" align="left">                                
                                                                Ghi chú
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                        <tr>
                                                            <td style="width: 100px" valign="middle" align="center">
                                                                <%# getShortString(getCodeProductDetail(Eval("PRODUCT_DETAIL_ID")), 40)%>
                                                            </td>
                                                            <td style="width: 60px" valign="middle" align="center">
                                                                <%# Eval("QUANTITY")%>
                                                            </td>
                                                            <td valign="middle" align="left">                                
                                                                <%# getDate(Eval("CREATE_DATE"))%>
                                                            </td>
                                                            <td valign="middle" align="left">                                
                                                                <%# Eval("NOTE")%>
                                                            </td>
                                                        </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowHorizontalScrollBar="true" />
                    <Settings VerticalScrollableHeight="350" />
                    <Settings ShowVerticalScrollBar="true" />
                    <SettingsPager PageSize="30">
                    </SettingsPager>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
