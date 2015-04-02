<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="danh-sach-size-san-pham-chi-tiet.aspx.cs" Inherits="hauphatpottery.Pages.danh_sach_size_san_pham_chi_tiet" %>

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
            Danh Sách Size Sản Phẩm Chi Tiết
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div id="div-search">
        <table>
            <tbody>
                <tr>
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
                        <input class="k-textbox" width="40" id="txtD" name="txtD" type="text" value="" runat="server" placeholder="D" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập D"
                                                    ControlToValidate="txtD" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <input class="k-textbox" width="40" id="txtH" type="text" value="" runat="server" placeholder="H" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Chưa nhập H"
                                                    ControlToValidate="txtH" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <input class="k-textbox" width="40" id="txtL" type="text" value="" runat="server" placeholder="L" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Chưa nhập L"
                                                    ControlToValidate="txtL" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <input class="k-textbox" width="40" id="txtW" type="text" value="" runat="server" placeholder="W" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Chưa nhập W"
                                                    ControlToValidate="txtW" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
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
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1_ProductDetail_Size" runat="server" AutoGenerateColumns="False"
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
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="D">
                            <DataItemTemplate>
                                    <%# Eval("D")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="H">
                            <DataItemTemplate>
                                    <%# Eval("H")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="L">
                            <DataItemTemplate>
                                    <%# Eval("L")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="W">
                            <DataItemTemplate>
                                    <%# Eval("W")%>
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="G2" />
</asp:Content>
