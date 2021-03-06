﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="chi-tiet-don-hang.aspx.cs" Inherits="hauphatpottery.Pages.chi_tiet_don_hang" %>

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
            Chi Tiết Đơn Hàng
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
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
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
                                                <span style="color: Red;">*</span>&nbsp;Mã đơn hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtCode" runat="server" class="text" 
                                                    CssClass="k-textbox textbox" Width="400px"></asp:TextBox>                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Chưa nhập mã đơn hàng"
                                                    ControlToValidate="txtCode" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Khách hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList runat="server" ID="ddlCustomer" CssClass="k-textbox textbox" 
                                                    AppendDataBoundItems="true" DataTextField="FullName" DataValueField="Id" 
                                                    Width="400px">
                                                    <asp:ListItem Value="0" Text="--Chọn Khách Hàng--"></asp:ListItem>
                                                </asp:DropDownList>                                               
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Chưa chọn khách hàng"
                                                    ControlToValidate="ddlCustomer" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Thời hạn giao hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">                                                
                                                <uc1:pickerAndCalendar ID="pickerAndCalendarDeadlineDate" runat="server" />
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Thời gian bắt đầu
                                            </td>
                                            <td align="left" nowrap="nowrap">                                                
                                                <uc1:pickerAndCalendar ID="pickerAndCalendarStartDate" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="trStatus" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Trạng thái
                                            </td>
                                            <td align="left" nowrap="nowrap">  
                                                <asp:RadioButtonList ID="rdlStatus" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="Đang chờ" Selected="true"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Sản xuất"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Hoàn tất"></asp:ListItem>
                                                </asp:RadioButtonList>                                       
                                                <%--<asp:DropDownList runat="server" ID="ddlStatus" CssClass="k-textbox textbox" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="1" Text="Đang chờ"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Sản xuất"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Hoàn tất"></asp:ListItem>
                                                </asp:DropDownList>                                             
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Chưa chọn trạng thái"
                                                    ControlToValidate="ddlStatus" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>--%>
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
                        <dx:TabPage Text="Danh sách chi tiết" Name="danhsachchitiet">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                        <div id="div-search">
                                        <asp:Panel ID="pnContract" runat="server" DefaultButton="lbtnSaveDetail">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td align="left" nowrap="nowrap">
                                                            <asp:DropDownList runat="server" ID="ddlProduct" CssClass="k-textbox textbox" 
                                                                AppendDataBoundItems="true" DataTextField="Code" DataValueField="Id" 
                                                                Width="120" AutoPostBack="True" 
                                                                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" Text="--Chọn Sản Phẩm--"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
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
                                                            <asp:TextBox ID="txtPrice" runat="server" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);" Width="100" placeholder="Đơn giá"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Chưa nhập đơn giá"
                                                                                        ControlToValidate="txtPrice" Display="None" ForeColor="Red" ValidationGroup="G21"
                                                                                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td align="left" nowrap="nowrap">
                                                            <asp:DropDownList runat="server" ID="ddlProductDetailSize" CssClass="k-textbox textbox" Width="200">
                                                            </asp:DropDownList>                                               
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Chưa chọn size"
                                                                ControlToValidate="ddlProductDetailSize" Display="None" ForeColor="Red" ValidationGroup="G21"
                                                                CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxColorEdit runat="server" ID="ColorEdit1" Color="#f7f7f7" >
                                                            </dx:ASPxColorEdit>
                                                        </td>
                                                        <%--<td align="left" nowrap="nowrap">
                                                            <cc1:ColorPicker ID="ColorPicker1" runat="server"/>                                                            
                                                        </td>--%>
                                                        <td align="left" nowrap="nowrap">
                                                            <asp:TextBox ID="txtColor2" runat="server" class="text" CssClass="k-textbox textbox" placeholder="Màu sắc ghi chú"></asp:TextBox>                                                            
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
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số lượng đặt" FieldName="Quantity"  Width="100px">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(Eval("Quantity"))%> <%# getProductDetailSize(Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID"))%>
                                                                </DataItemTemplate>
                                                                <FooterTemplate>
                                                                    <strong> <%# getSumQuantity()%></strong> 
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Đơn giá" FieldName="Quantity"  Width="100px">
                                                                <DataItemTemplate>
                                                                        <%# getFormatQuantity(Eval("Price"))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Thành tiền" FieldName="Quantity"  Width="100px">
                                                                <DataItemTemplate>
                                                                        <%# getFormatQuantity(Eval("Subtotal"))%>
                                                                </DataItemTemplate>
                                                                <FooterTemplate>
                                                                    <strong><%# getSumSubtotal()%></strong> 
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Màu sắc"  Width="150px">
                                                                <DataItemTemplate>
                                                                        <span style='color:<%# Eval("Color1")%>'><%# Eval("Color1") + " - " + Eval("Color2") %></span
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL thô đã làm" Width="140px">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(getSoluongDalam(Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL tinh đã làm(Sơn)" Width="140px">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(getSoluongTinhSonDalam(Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>   
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL tinh đã làm(Chét)" Width="140px">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(getSoluongTinhChetDalam(Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>  
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL tinh đã làm(Nhám)" Width="140px">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(getSoluongTinhNhamDalam(Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>  
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL đã kiểm tra" Width="140px">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(getSoluongKiemtra(Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL xuất" Width="100px">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(getSoluongXuat(Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>    
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="SL còn lại">
                                                                <DataItemTemplate>
                                                                    <%# getFormatQuantity(getSoluongConlai(Eval("QUANTITY"), Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"), Eval("PRODUCT_DETAIL_SIZE_ID")))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="">
                                                                <DataItemTemplate>
                                                                    <asp:LinkButton ID="lnkHistory" runat="server" Text="Lịch sử xuất"></asp:LinkButton>
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
                                                                                <asp:Repeater ID="rptHistory" runat="server" DataSource='<%# getListHistory(Eval("ORDER_ID"), Eval("PRODUCT_DETAIL_ID"))%>'>
                                                                                    <HeaderTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="width: 100px" valign="middle" align="center">
                                                                                                    Mã
                                                                                                </td>
                                                                                                <td style="width: 100px" valign="middle" align="center">
                                                                                                    Số lượng xuất
                                                                                                </td>
                                                                                                <td valign="middle" align="left">                                
                                                                                                    Ngày xuất
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
                                                            </dx:GridViewDataTextColumn>--%>
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
                        <dx:TabPage Text="Thời gian giao hàng" Name="thoigiangiaohang">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                    <table border="0" cellspacing="1" cellpadding="3">
                                        <tr>
                                            <td align="left" nowrap="nowrap">                                                
                                                <uc1:pickerAndCalendar ID="pickerAndCalendarDeadline" runat="server" />
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNoteDeadline" runat="server" class="text" CssClass="k-textbox textbox" Width="400" placeholder="Ghi chú"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbtnSaveDeadline" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                                        OnClick="lbtnSaveDeadline_Click"><img alt="Lưu thông tin" src="../Images/icon-20-save.png" /></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbtnDeleteDeadline" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                                                ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDeleteDeadline_Click"><img alt="Xóa" src="../Images/icon-20-trash.png" /></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <dx:ASPxGridView ID="ASPxGridViewDeadline" runat="server" AutoGenerateColumns="False"
                                         KeyFieldName="ID" Theme="Aqua" onhtmlrowprepared="ASPxGridView1_Order_HtmlRowPrepared">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="45px">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                                                <DataItemTemplate>
                                                    <%# Container.ItemIndex + 1 %></DataItemTemplate>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Thời gian giao hàng" Width="110px">
                                                <DataItemTemplate>
                                                        <%# getDate(Eval("DEADLINE_DATE"))%>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Trạng thái"  Width="100px">
                                                <DataItemTemplate>
                                                        <%# getDeadlineStatus(Eval("STATUS"))%>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ghi chú"  Width="200px">
                                                <DataItemTemplate>
                                                        <%# Eval("NOTE")%>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>  
                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="#" FieldName="ID" Width="200">
                                                <DataItemTemplate>
                                                        <a href="<%# getlink_deadline(Eval("ID")) %>">
                                                        Số lượng</a>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>   
                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="#" Width="200">
                                                <DataItemTemplate>                                                        
                                                        <asp:LinkButton ID="lnkCheck" runat="server" OnClick="lnkCheck_Click" OnClientClick="return confirm('Bạn chắc chắn tắt cảnh báo?')" Text="Tắt cảnh báo" CommandArgument='<%# Eval("ID") %>' Visible='<%# getVisibleTat(Eval("ID")) %>'></asp:LinkButton>                                                
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>                                       
                                        </Columns>
                                        <SettingsPager PageSize="30">
                                        </SettingsPager>
                                    </dx:ASPxGridView>

                                    <table border="0" cellspacing="1" cellpadding="3" id="trDeadlineDetail" runat="server" visible="false">
                                        <tr>
                                            <td colspan="4"><strong>Thời gian giao hàng: <asp:Literal ID="lbDeadlineDate" runat="server"></asp:Literal></strong></td>
                                            </tr>
                                            <tr>
                                            <td><asp:DropDownList runat="server" ID="ddlProductDetailDeadline" 
                                                CssClass="k-textbox textbox" Width="200">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ErrorMessage="Chưa chọn sản phẩm"
                                                ControlToValidate="ddlProductDetailDeadline" Display="None" ForeColor="Red" ValidationGroup="G29"
                                                CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                            <td>
                                                <asp:Textbox id="txtQuantityDeadline" runat="server" class="k-textbox" width="100" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"
                            name="txtQuantityDeadline" type="text" value="" runat="server" placeholder="Số lượng"></asp:Textbox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ErrorMessage="Chưa nhập số lượng"
                                                    ControlToValidate="txtQuantityDeadline" Display="None" ForeColor="Red" ValidationGroup="G29"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator333" runat="server" ErrorMessage="Chưa nhập số lượng"
                                                    ControlToValidate="txtQuantityDeadline" Display="None" 
                            ForeColor="Red" ValidationGroup="G29"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                            </td>                                            
                                            <td>
                                                <asp:LinkButton ID="lbtnSaveDeadlineDetail" ToolTip="Lưu thông tin" CssClass="k-button" runat="server" ValidationGroup="G29"
                                        OnClick="lbtnSaveDeadlineDetail_Click"><img alt="Lưu thông tin" src="../Images/icon-20-save.png" /></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbtnDeleteDeadlineDetail" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                                                ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDeleteDeadlineDetail_Click"><img alt="Xóa" src="../Images/icon-20-trash.png" /></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <dx:ASPxGridView ID="ASPxGridViewDeadlineDetail" runat="server" AutoGenerateColumns="False"
                                                     KeyFieldName="ID" Theme="Aqua">
                                                    <Columns>                                            
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                                                            <DataItemTemplate>
                                                                <%# Container.ItemIndex + 1 %></DataItemTemplate>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sản phẩm chi tiết" Width="210px">
                                                            <DataItemTemplate>
                                                                    <%# getProductDetailDeadline(Eval("ORDER_DETAIL_ID"))%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số lượng"  Width="100px">
                                                            <DataItemTemplate>
                                                                    <%# Eval("QUANTITY")%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ghi chú"  Width="200px">
                                                            <DataItemTemplate>
                                                                    <%# Eval("NOTE")%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn> --%>                                      
                                                    </Columns>
                                                    <SettingsPager PageSize="30">
                                                    </SettingsPager>
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
                <asp:ValidationSummary ID="ValidationSummary24" runat="server" ForeColor="Red" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="G29" />
            </td>
        </tr>
    </table>
</asp:Content>
