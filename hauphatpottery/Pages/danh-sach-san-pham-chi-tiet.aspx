<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="danh-sach-san-pham-chi-tiet.aspx.cs" Inherits="hauphatpottery.Pages.danh_sach_san_pham_chi_tiet" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v12.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
<div id="header">
        <div class="title">
            Danh Sách Sản Phẩm Chi Tiết
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a href="chi-tiet-san-pham-chi-tiet.aspx" title="Thêm sản phẩm chi tiết" class="k-button"><span
                class="p-i-add"></span></a> &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                    ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div id="div-search">
    <asp:Panel ID="pnContract" runat="server" DefaultButton="lbtnSearch">
        <table>
            <tbody>
                <tr>
                    <td>
                        <label>
                            Từ khóa
                        </label>
                    </td>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lbtnDeleteKeyword" />
                        </Triggers>
                        <ContentTemplate>
                        <input class="k-textbox k-input search-noidung fill-input" width="300" id="txtKeyword"
                            name="txtKeyword" type="text" value="" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlProduct" CssClass="k-textbox textbox" AppendDataBoundItems="true" DataTextField="Code" DataValueField="Id">
                            <asp:ListItem Value="0" Text="--Chọn Sản Phẩm--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click"><span class="p-i-search"></span></asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton CssClass="k-button" ID="lbtnDeleteKeyword" ToolTip="Xóa tìm kiếm"
                            runat="server" OnClick="lbtnDeleteKeyword_Click"><span class="p-i-clear"></span></asp:LinkButton>
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
                <dx:ASPxGridView ID="ASPxGridView1_ProductDetail" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua">
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
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Hình ảnh" FieldName="IMAGE" Width="200px">
                            <DataItemTemplate>
                            <img src="<%# getImage(Eval("IMAGE"))%>" />                                
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>--%>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã Sản Phẩm Chi Tiết" FieldName="CODE"  Width="250px">
                            <DataItemTemplate>
                                <a href="<%# getlink(Eval("ID")) %>" title='<%# Eval("CODE")%>' onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ToolTip/ToolTip.aspx?oid=<%# Eval("PRODUCT_ID") %>'); return false;">
                                    <%# getShortString(Eval("CODE"), 40)%></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên Sản Phẩm Chi Tiết" FieldName="NAME"  Width="250px">
                            <DataItemTemplate>
                                <a href="<%# getlink(Eval("ID")) %>" title='<%# Eval("NAME")%>' onmouseout="BBTOnline_HideTooltip();" onmouseover="BBTOnline_ShowTooltip('../ToolTip/ToolTip.aspx?oid=<%# Eval("PRODUCT_ID") %>'); return false;">
                                    <%# getShortString(Eval("NAME"), 40)%></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>     
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Loại Hàng">
                            <DataItemTemplate>
                                <%#getType(Eval("TYPE_ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>  
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="M2" FieldName="M2">
                            <DataItemTemplate>
                                    <%# Eval("M2")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>       
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Trọng Lượng" FieldName="WEIGHT">
                            <DataItemTemplate>
                                    <%# Convert.ToDecimal(Eval("WEIGHT")) > 0 ? Convert.ToString(Eval("WEIGHT")) : "" %>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Nguyên liệu cần cho sản phẩm" FieldName="ID" Width="200">
                            <DataItemTemplate>
                                    <a href="<%# getlink_nguyenlieucan(Eval("ID")) %>" title='<%# Eval("CODE")%>'>
                                    Nguyên liệu cần cho sản phẩm</a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Size">
                            <DataItemTemplate>
                                <a href="<%# getlinkproductdetailsize(Eval("ID")) %>">Xem</a>
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
