<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="nguyen-lieu-can-cho-don-hang.aspx.cs" Inherits="hauphatpottery.Pages.nguyen_lieu_can_cho_don_hang" %>

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
            Danh Sách Nguyên Liệu Cần Cho Đơn Hàng
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
                        <asp:DropDownList runat="server" ID="ddlOrder" 
                            CssClass="k-textbox textbox" AppendDataBoundItems="true" DataTextField="Code" 
                            DataValueField="Id" Width="200" AutoPostBack="True" 
                            onselectedindexchanged="ddlOrder_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Chọn Đơn Hàng--"></asp:ListItem>
                        </asp:DropDownList>
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
                <dx:ASPxGridView ID="ASPxGridView1_Order_Material" runat="server" AutoGenerateColumns="False"
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
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã sản phẩm chi tiết" FieldName="PRODUCT_DETAIL_ID"  Width="120px">
                            <DataItemTemplate>
                                    <%# getShortString(getCodeProductDetail(Eval("PRODUCT_DETAIL_ID")), 40)%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>--%>
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sản phẩm chi tiết" FieldName="PRODUCT_DETAIL_ID"  Width="250px">
                            <DataItemTemplate>
                                    <%# getShortString(getNameProductDetail(Eval("PRODUCT_DETAIL_ID")), 40)%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>--%>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Nguyên liệu" FieldName="MATERIAL_ID"  Width="250px">
                            <DataItemTemplate>
                                    <%# getShortString(getNameMaterial(Eval("MATERIAL_ID")), 40)%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số lượng">
                            <DataItemTemplate>
                                <%# Convert.ToString(Eval("QUANTITY")) + " " + getNameUnit(Eval("MATERIAL_ID"))%>
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
