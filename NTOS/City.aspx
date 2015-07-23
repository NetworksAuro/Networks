<%@ Page Title="Manage Cities" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="City.aspx.cs" Inherits="NTOS.City1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #tblrep {
            margin: 3px;
        }
            .txtwrap {
         word-wrap:break-word;
         width:400px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                  <table width="100%" cellpadding="0" cellspacing="0" align="left">

                <tr>
                    <td class="contentpadding">
                <table width="100%" cellpadding="0" cellspacing="0" align="left" >
               <%-- <tr>
                    <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33"
                        colspan="8">City List
                    </td>
                </tr>--%>
                    <tr>
                        <td>
                            <div class="alerttd">
                                <asp:ValidationSummary ValidationGroup="add" ID="vslist" runat="server" DisplayMode="BulletList" HeaderText="Field validations failed! Please check and correct!" CssClass="valsummary" />
                                <asp:Label ID="lblerrmsg" runat="server" CssClass="valsummary"></asp:Label>
                                <div id="divmsg" runat="server" class="msgbox">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                       <td align="left">
                         Search by City&nbsp;:&nbsp; <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" Text="Search" CssClass="butt" />
                            &nbsp;&nbsp;<asp:Button ID="btnclearsearch"  runat="server" OnClick="btnclearsearch_Click" Text="Clear" CssClass="butt" />
                            <br />&nbsp;</td> 
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:GridView ID="grdCity" PageSize="20" AllowPaging="true" runat="server" AutoGenerateColumns="False" ShowFooter="true" 
                                CellPadding="4" GridLines="None" OnRowCancelingEdit="grdCity_RowCancelingEdit" OnRowCommand="grdCity_RowCommand" 
                                OnRowDeleting="grdCity_RowDeleting" OnRowEditing="grdCity_RowEditing" OnRowUpdating="grdCity_RowUpdating" 
                                OnPageIndexChanging="grdCity_PageIndexChanging" OnRowDataBound="grdCity_RowDataBound">
                               <HeaderStyle CssClass="gridviewheader" />
                                        <AlternatingRowStyle CssClass="gdvaltrowstyle" />
                                        <FooterStyle CssClass="gridviewheader" />
                                        <RowStyle CssClass="gdvrowstyle" />
                                        <EditRowStyle CssClass="gdveditrowstyle" />
                                        <PagerStyle CssClass="gdvpagingstyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Country">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle  HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfCountryid" runat="server" Value='<%#Bind("country_id") %>' />
                                        <asp:Label ID="lblCountry" Text='<%#Bind("country_name") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hdfCountryid" runat="server" Value='<%#Bind("country_id") %>' />
                                        <asp:DropDownList SkinID="ddlmedium" AutoPostBack="true" ID="ddlCountryE" runat="server" OnSelectedIndexChanged="ddlCountryE_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ValidationGroup="add" CssClass="asterisk" InitialValue="-Select-" ID="rqEdt" runat="server" ControlToValidate="ddlCountryE" ToolTip="Please select country" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <%--<ajaxToolkit:ListSearchExtender ID="lsSearchCountryEdt" runat="server" TargetControlID="ddlCountryE" PromptText="Search here.."></ajaxToolkit:ListSearchExtender>--%>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList SkinID="ddlmedium" AutoPostBack="true" ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ValidationGroup="add" CssClass="asterisk" InitialValue="-Select-" ID="rq1F" runat="server" ControlToValidate="ddlCountry" ToolTip="Please select country" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <%--<ajaxToolkit:ListSearchExtender ID="lsSearchCountry" runat="server" TargetControlID="ddlCountry" PromptText="Search here.."></ajaxToolkit:ListSearchExtender>--%>
                                    </FooterTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle  HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdfStateid" runat="server" Value='<%#Bind("State_id") %>' />
                                            <asp:Label ID="lblState" Text='<%#Bind("State_name") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="hdfStateid" runat="server" Value='<%#Bind("State_id") %>' />
                                            <asp:DropDownList SkinID="ddlmedium" ID="ddlStateE" runat="server"></asp:DropDownList>
                                             <asp:RequiredFieldValidator CssClass="asterisk" ValidationGroup="add" InitialValue="-Select-" ID="rqsteE" runat="server" ControlToValidate="ddlStateE" ToolTip="Please Select state" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList SkinID="ddlmedium" ID="ddlState" runat="server" onchange="highlight();"></asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="asterisk" ValidationGroup="add" InitialValue="-Select-" ID="rq1" runat="server" ControlToValidate="ddlState" ToolTip="Please Select state" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle  HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdfCityid" runat="server" Value='<%#Bind("City_id") %>' />
                                            <asp:Label ID="lblCity" Text='<%#Bind("City_name") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="hdfCityid" runat="server" Value='<%#Bind("City_id") %>' />
                                            <asp:TextBox MaxLength="30" ID="txtCityEdit" Text='<%#Bind("City_name") %>' runat="server" CssClass="txtmixcaps"></asp:TextBox>
                                            <asp:RequiredFieldValidator CssClass="asterisk" ValidationGroup="add" ID="rqCEdt" runat="server" ControlToValidate="txtCityEdit" ToolTip="Please enter city" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCityName" CssClass="txtmixcaps"  MaxLength="30" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator CssClass="asterisk" ValidationGroup="add" ID="rq" runat="server" ControlToValidate="txtCityName" ToolTip="Please enter city" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Zip">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle  HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblZip" Text='<%#Bind("Zip") %>' runat="server" CssClass="txtwrap" Width="400px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtZipEdit" Text='<%#Bind("Zip") %>' runat="server"></asp:TextBox>
                                            <asp:Panel BackColor="#cccccc" ID="pnlcolumn" Width="150px" runat="server" BorderWidth="1px" BorderColor="Black" Height="150px" ScrollBars="Auto">
                                            <table id="tblrep">
                                                <tr class="gridviewheader" valign="top">                                                   
                                                    <th>Zip Code</th>                                                    
                                                </tr>
                                                <asp:Repeater ID="repzipE" runat="server">                                                    
                                                    <ItemTemplate>
                                                        <tr class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle"%>' runat="server" id="trcrow">                                                                                                                        
                                                            <td>                                                            
                                                                    <asp:TextBox ID="txtzipcode" Text='<%#Eval("ZipCode") %>' Width="120px" MaxLength="10" CssClass="Numeric" runat="server"></asp:TextBox>                                                            
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftezipE" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="txtzipcode" ValidChars="-" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </asp:Panel>
                                        <ajaxToolkit:HoverMenuExtender ID="hmecolumn"
                                            runat="server"
                                            TargetControlID="txtZipEdit"
                                            PopupControlID="pnlcolumn"
                                            PopupPosition="Top"
                                            OffsetX="6"
                                            PopDelay="25">
                                        </ajaxToolkit:HoverMenuExtender>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
                                             <asp:Panel BackColor="#cccccc" ID="pnlcolumn" Width="150px" runat="server" BorderWidth="1px" BorderColor="Black" Height="150px" ScrollBars="Auto">
                                            <table id="tblrep">
                                                <tr class="gridviewheader" valign="top">                                                   
                                                    <th>Zip Code</th>                                                    
                                                </tr>
                                                <asp:Repeater ID="repzipF" runat="server">                                                    
                                                    <ItemTemplate>
                                                        <tr class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle"%>' runat="server" id="trcrow">                                                                                                                        
                                                            <td>                                                            
                                                                    <asp:TextBox ID="txtzipcode" Text='<%#Eval("ZipCode") %>' Width="120px" MaxLength="10" CssClass="Numeric" runat="server"></asp:TextBox>                                                            
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftezipF" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="txtzipcode" ValidChars="-" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </asp:Panel>
                                        <ajaxToolkit:HoverMenuExtender ID="hmecolumn"
                                            runat="server"
                                            TargetControlID="txtZip"
                                            PopupControlID="pnlcolumn"
                                            PopupPosition="Top"
                                            OffsetX="6"
                                            PopDelay="25">
                                        </ajaxToolkit:HoverMenuExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Status">
                                          <HeaderStyle  HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" Text='<%#Bind("city_active_flag") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                         <asp:HiddenField ID="hdfStatus" runat="server" Value='<%#Bind("city_active_flag") %>' />
                                        <asp:CheckBox ID="chkStatus" runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:ImageButton CausesValidation="false" ImageUrl="~/Images/edit.png" ToolTip="Edit" ID="lnkEdit" runat="server" CommandName="Edit" />
                                             <asp:ImageButton CausesValidation="false"  ID="lnkDelete" runat="server" CommandName="Delete" />
                                                    <ajaxToolkit:ConfirmButtonExtender ID="cnfm" runat="server" TargetControlID="lnkDelete"></ajaxToolkit:ConfirmButtonExtender>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/save.png" ValidationGroup="add" ID="lnkUpdate" ToolTip="Update" runat="server" CommandName="Update" />
                                            <asp:ImageButton ImageUrl="~/Images/back.png" CausesValidation="false" ID="lnkCancel" ToolTip="Cancel" runat="server" CommandName="Cancel" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="btnSave" CommandName="Save" ValidationGroup="add" runat="server" ImageUrl="~/Images/add-btn.png" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                        </td></tr></table>
            </ContentTemplate>
        </asp:UpdatePanel>

    </center>
</asp:Content>
