<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="xuat-nguyen-lieu.aspx.cs" Inherits="hauphatpottery.Pages.xuat_nguyen_lieu" %>

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
            Xuất Nguyên Liệu
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Chưa nhập mã xuất nguyên liệu"
                                                    ControlToValidate="txtCode" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Đơn hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                    <asp:DropDownList runat="server" ID="ddlOrder" CssClass="k-textbox textbox" AppendDataBoundItems="true" DataTextField="Code" DataValueField="Id" Width="400px">
                                                        <asp:ListItem Value="0" Text="--Chọn Đơn hàng--"></asp:ListItem>
                                                    </asp:DropDownList>                                               
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Chưa chọn đơn hàng"
                                                    ControlToValidate="ddlOrder" Display="None" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Ngày xuất
                                            </td>
                                            <td align="left" nowrap="nowrap">                                                
                                                <uc1:pickerAndCalendar ID="pickerAndCalendarDate" runat="server" />
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
                                                            <asp:DropDownList runat="server" ID="ddlMaterial" AppendDataBoundItems="true" 
                                                                AutoPostBack="True" onselectedindexchanged="ddlMaterial_SelectedIndexChanged"
                                                                CssClass="k-textbox textbox" DataTextField="Name" DataValueField="Id" Width="200">
                                                                <asp:ListItem Value="0" Text="--Chọn Nguyên Liệu--"></asp:ListItem>
                                                            </asp:DropDownList>                                               
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa chọn nguyên liệu"
                                                                ControlToValidate="ddlMaterial" Display="None" ForeColor="Red" ValidationGroup="G21"
                                                                CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                                        </td>                                                        
                                                        <td>
                                                            <asp:TextBox ID="txtQuantity" runat="server" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);" Width="70" placeholder="Số lượng"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập số lượng"
                                                                                        ControlToValidate="txtQuantity" Display="None" ForeColor="Red" ValidationGroup="G21"
                                                                                        CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ErrorMessage="Chưa nhập số lượng"
                                                                                        ControlToValidate="txtQuantity" Display="None" 
                                                                ForeColor="Red" ValidationGroup="G21"
                                                                                        CssClass="tlp-error" InitialValue="0" >*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbUnit" runat="server"></asp:Label>
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
                                                    <dx:ASPxGridView ID="ASPxGridView1_MaterialDetail" runat="server" AutoGenerateColumns="False"
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
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên nguyên liệu" Width="100px">
                                                                <DataItemTemplate>
                                                                      <%# getMaterialName(Eval("MATERIAL_ID"))%>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số lượng xuất" FieldName="Quantity"  Width="100px">
                                                                <DataItemTemplate>
                                                                    <%# Convert.ToString(Eval("QUANTITY")) + " " + getNameUnit(Eval("MATERIAL_ID")) %>
                                                                </DataItemTemplate>
                                                                <%--<FooterTemplate>
                                                                    <strong> <%# getSumQuantity()%></strong> 
                                                                </FooterTemplate>--%>
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
