<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="danh-sach-don-hang.aspx.cs" Inherits="hauphatpottery.Pages.danh_sach_don_hang" %>

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
            Danh Sách Đơn Hàng
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a href="chi-tiet-don-hang.aspx" title="Thêm đơn hàng" class="k-button"><span
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
                        <asp:DropDownList runat="server" ID="ddlCustomer" CssClass="k-textbox textbox" AppendDataBoundItems="true" DataTextField="Fullname" DataValueField="Id">
                            <asp:ListItem Value="0" Text="--Chọn Khách Hàng--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="k-textbox textbox" AppendDataBoundItems="true">
                            <asp:ListItem Value="0" Text="--Chọn Trạng Thái--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Đang chờ"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Sản xuất"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Hoàn tất"></asp:ListItem>
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
                <dx:ASPxGridView ID="ASPxGridView1_Order" runat="server" AutoGenerateColumns="False"
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
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã Đơn Hàng" FieldName="CODE"  Width="120px">
                            <DataItemTemplate>
                                <a href="<%# getlink(Eval("ID")) %>" title='<%# Eval("CODE")%>'>
                                    <%# getShortString(Eval("CODE"), 40)%></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Khách hàng" Width="250px">
                            <DataItemTemplate>
                                <a href="<%# getlinkCus(Eval("CUSTOMER_ID")) %>">
                                    <%# getShortString(getNameCus(Eval("CUSTOMER_ID")), 40)%></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Trạng thái" Width="100px">
                            <DataItemTemplate>
                                    <%# getStatusName(Eval("STATUS"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Thời Hạn Giao Hàng" FieldName="DEADLINE_DATE"  Width="120px">
                            <DataItemTemplate>
                                    <%# getDate(Eval("DEADLINE_DATE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>     
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ghi chú">
                            <DataItemTemplate>
                                    <%# getShortString(Eval("Note"), 40)%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày tạo" FieldName="CREATE_DATE"  Width="100px">
                            <DataItemTemplate>
                                    <%# getDate(Eval("CREATE_DATE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Người tạo">
                            <DataItemTemplate>
                                <%#getUser(Eval("CREATOR_ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>   
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Nguyên liệu cần cho đơn hàng" FieldName="ID" Width="200">
                            <DataItemTemplate>
                                    <a href="<%# getlink_nguyenlieucan(Eval("ID")) %>">
                                    Nguyên liệu cần cho đơn hàng</a>
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
