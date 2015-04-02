<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="nguyen-lieu-can-cho-san-pham.aspx.cs" Inherits="hauphatpottery.Pages.nguyen_lieu_can_cho_san_pham" %>

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
            Danh Sách Nguyên Liệu Cần Cho Sản Phẩm
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div id="div-search">
    <asp:Panel ID="pnContract" runat="server" DefaultButton="lbtnSave">
        <table>
            <tbody>
                <tr>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="G2" />
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddLProductDetail" 
                            CssClass="k-textbox textbox" AppendDataBoundItems="true" DataTextField="Code" 
                            DataValueField="Id" Width="200" AutoPostBack="True" 
                            onselectedindexchanged="ddLProductDetail_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Chọn sản phẩm chi tiết--"></asp:ListItem>
                        </asp:DropDownList>                                               
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa chọn sản phẩm chi tiết"
                            ControlToValidate="ddLProductDetail" Display="None" ForeColor="Red" ValidationGroup="G2"
                            CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlMaterial" CssClass="k-textbox textbox" 
                            AppendDataBoundItems="true" DataTextField="Name" DataValueField="Id" 
                            AutoPostBack="True" onselectedindexchanged="ddlMaterial_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Chọn nguyên liệu--"></asp:ListItem>
                        </asp:DropDownList>                                               
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Chưa chọn nguyên liệu"
                            ControlToValidate="ddlMaterial" Display="None" ForeColor="Red" ValidationGroup="G2"
                            CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <input class="k-textbox" width="40" id="txtQuantity"
                            name="txtQuantity" type="text" value="" runat="server" placeholder="Số lượng" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập số lượng"
                                                    ControlToValidate="txtQuantity" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lbUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-20-save.png" /></asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                        ToolTip="Xóa quản trị" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-20-trash.png" /></asp:LinkButton>
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
                <dx:ASPxGridView ID="ASPxGridView_ProductDetail_Material" runat="server" AutoGenerateColumns="False"
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
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Nguyên liệu" FieldName="MATERIAL_ID"  Width="250px">
                            <DataItemTemplate>
                                    <%# getShortString(getNameMaterial(Eval("MATERIAL_ID")), 40)%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số lượng">
                            <DataItemTemplate>
                                <%# Convert.ToString(Eval("QUANTITY")) + " " + getNameUnit(Eval("MATERIAL_ID")) %>
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
