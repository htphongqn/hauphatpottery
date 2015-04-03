<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="hinh-dang.aspx.cs" Inherits="hauphatpottery.Pages.hinh_dang" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v12.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register src="../UIs/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Hình Dạng
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
                        Mã
                    </td>
                    <td>
                        <input class="k-textbox k-input search-noidung fill-input" width="100" id="txtCode"
                            name="txtName" type="text" value="" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập mã"
                                                    ControlToValidate="txtCode" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        Tên hình dạng
                    </td>
                    <td>
                        <input class="k-textbox k-input search-noidung fill-input" width="200" id="txtName"
                            name="txtName" type="text" value="" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Chưa nhập tên hình dạng"
                                                    ControlToValidate="txtName" Display="None" ForeColor="Red" ValidationGroup="G2"
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
        </asp:Panel>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>                
                <dx:ASPxGridView ID="ASPxGridView1_Shape" runat="server" AutoGenerateColumns="False"
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
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Mã" FieldName="Code"  Width="100px">
                            <DataItemTemplate>
                                    <%# Eval("Code")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên Hình dạng" FieldName="Name"  Width="150px">
                            <DataItemTemplate>
                                    <%# Eval("Name")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="D" FieldName="D"  Width="50px">
                            <DataItemTemplate>
                                <asp:CheckBox id="chkD" runat="server" checked='<%# Eval("D")%>'></asp:CheckBox>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="H" FieldName="H"  Width="50px">
                            <DataItemTemplate>
                                <asp:CheckBox id="chkH" runat="server" checked='<%# Eval("H")%>'></asp:CheckBox>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="L" FieldName="L"  Width="50px">
                            <DataItemTemplate>
                                <asp:CheckBox id="chkL" runat="server" checked='<%# Eval("L")%>'></asp:CheckBox>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="W" FieldName="W"  Width="50px">
                            <DataItemTemplate>
                                <asp:CheckBox id="chkW" runat="server" checked='<%# Eval("W")%>'></asp:CheckBox>
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
    <uc1:MessageBox ID="MessageBox1" runat="server" /> 
</asp:Content>
