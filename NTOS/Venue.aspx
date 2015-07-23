<%@ Page Title="Venue" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Venue.aspx.cs" Inherits="NTOS.Venue" %>

<%@ Register Src="~/Docx.ascx" TagName="Docx" TagPrefix="UC" %>
<%@ Register Src="~/PresenterContactPerson.ascx" TagName="ConPerson" TagPrefix="UC" %>
<%@ Register Src="~/City.ascx" TagName="City" TagPrefix="UC1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
     <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
               }     
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/jscript" src="Scripts/autoNumeric 1.9.15.js"></script>
    <script type="text/javascript">
        function focusadd() {
            document.getElementById('<%=uccity.FindControl("txtaddress").ClientID%>').focus();
        }
        function setMetrocityID(source, eventargs) {
            $('#<%=hdnmetrocityid.ClientID %>').val(eventargs.get_value());

        }
        function setVenueID(source, eventargs) {
            $('#<%=hdnvenueid.ClientID %>').val(eventargs.get_value());
        }
        function ClearVenueID() {
            $('#<%=hdnvenueid.ClientID %>').val("0");
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        jQuery(function ($) {
            $('.Numericv').autoNumeric({ aSep: '', vMax: '999999999', vMin: '-999999999', nBracket: '(,)' });
        });
        function EndRequestHandler(sender, args) {
            $('.Numericv').autoNumeric({ aSep: '', vMax: '999999999', vMin: '-999999999', nBracket: '(,)' });
        }

        $("#Form1").one("change", ":input", function () {

            var user_role = '<%= Session["userrole"].ToString() %>';
             var role_status = (user_role == 'reader') ? 0 : 1;
             if (role_status != 0)
                 $('#hdn_modify_status').val("1");

         });

    </script>

    <asp:UpdatePanel ID="upnlvenue" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnvenueid" runat="server" />
            <asp:HiddenField ID="hdncityid" runat="server" />
            <asp:HiddenField ID="hdnmetrocityid" runat="server" />
            <asp:HiddenField ID="hdnfrommetro" runat="server" Value="0" />
            <asp:Panel ID="pnlvenue" runat="server">
                <div style="float:left; padding: 0px 0px 0px 0px;  margin: 0px 0px 0px 0px;">
                <table cellpadding="0" cellspacing="0" style="float:left;  width:100% ; padding:0px;">
                    <tr>
                        <td colspan="7" align="center" style="width:auto; padding: 0px 5px 0px 10px;">
                            <div class="alerttd">
                                <asp:ValidationSummary ID="vslist" runat="server" DisplayMode="BulletList" HeaderText="Field validations failed! Please check and correct!" CssClass="valsummary" />
                                <asp:Label ID="lblerrmsg" runat="server" CssClass="valsummary"></asp:Label>
                                <div id="divmsg" runat="server" class="msgbox">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>

                           <td class="contentpadding" colspan="8" style="padding: 0px 0px 20px 0px; text-align:left;">
                            <asp:Label ID="lblHeader" CssClass="largefont" Visible="false" runat="server" Text="Label"></asp:Label>
                        </td> 
                        
                    </tr>

                    <tr>
                        <td colspan="8">
                            <table>
                                <tr>

                                    <td valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="width:1490px;">
                                <tr>
                        <td valign="top" align="right" style="width:auto; padding: 0px 5px 0px 10px;">Venue&nbsp;Name</td>
                        <td valign="top" align="left" style="width:auto; padding: 0px 5px 0px 10px;"><asp:TextBox ID="txtvenuname" Width="200px" CssClass="txtmixcaps" TabIndex="1" runat="server" MaxLength="100" onchange="ClearVenueID();" OnTextChanged="txtvenuname_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="aceVenuename" runat="server" TargetControlID="txtvenuname"
                                CompletionListElementID="divacewidth" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                                CompletionInterval="60" OnClientItemSelected="setVenueID"
                                ServiceMethod="GetVenueName">
                            </ajaxToolkit:AutoCompleteExtender>
                            <asp:RequiredFieldValidator ID="rfvvenue" runat="server" ErrorMessage="*" ToolTip="Enter venue name!" ControlToValidate="txtvenuname"
                                CssClass="asterisk"></asp:RequiredFieldValidator>
                        </td>
                        <td valign="top" align="right" style="width:auto; padding: 0px 5px 0px 10px;">Capacity</td>
                        <td valign="top" align="left" style="width:auto; padding: 0px 10px 0px 5px;">&nbsp;<asp:TextBox ID="txtcapacity" runat="server" TabIndex="2" CssClass="Numericv"></asp:TextBox>
                        </td>
                        
                    <%--    <td style="text-align: right;" width="120px"></td>--%>
                                    <td  rowspan="2" valign="top" style="width:auto; padding: 0px 5px 0px 5px;">
                                        <table width="100%;">
                                            <tr>
                                                <td rowspan="2">Street&nbsp;Address
                                                   <br />
                                                  <br />
                                                   <div style="margin: 10px 0px 0px 0px; text-align:right;"> City
                                                    <br />
                                                
                                                    ZipCode</div>
                                                </td>
                                                <td  rowspan="2" valign="baseline">
                                                    <UC1:City ID="uccity" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                   <%--     <div style="position:absolute; margin: 71px 0px 0px -50px; text-align:right; line-height:30px;">City<br>ZipCode</div>
                                        <div style="margin:0px 0px 0px 3px;"></div>--%>
                                    </td>
                        
                        <td rowspan="2"  style="width:auto; padding: 0px 0px 0px 15px;" valign="top">
                            <UC:Docx ID="ucdocx" runat="server" />
                        </td></tr>

                                <tr>
                        <td class="contentpadding" align="right" valign="top" style="width:auto; padding: 0px 5px 0px 10px;">Notes</td>
                        <td align="left" valign="top" style="width:auto; padding: 0px 10px 0px 5px;"><asp:TextBox ID="txtnotes" runat="server"  rows="2" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td align="right" valign="top" style="width:auto; padding: 0px 5px 0px 10px;">Delivery/Directions</td>
                        <td align="left" valign="top" style="width:auto; padding: 0px 10px 0px 5px;">&nbsp;<asp:TextBox ID="txtdeliveryDirection" runat="server" TabIndex="3" onblur="focusadd();" MaxLength="600"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>

                       
                            
                           </td>
                    </tr>

                    <tr>
                        <td colspan="5">
                            <asp:Panel ID="pnluc" runat="server" CssClass="ucpnl" Enabled="true" ToolTip="Save Venue">
                                            <table width="100%">
                                                <tr>
                                                    <div class="t-border">
                                                        <td>
                                                            <UC:ConPerson ID="uccontactperson" runat="server" />
                                                        </td>
                                                   
                                                   
                                                  
                                                    
                                                 
                                                </tr>
                                            </table>
                                        </asp:Panel>
                        </td>


                    </tr>

                            </table>

                    </td>
                                    <td>
                            <table>
                                <tr>
                                    <td>
                                                                       <asp:Label ID="lblEnggrid" runat="server" Text="Engagegment" Visible="false" style="font-weight:bold; font-size:14px;"></asp:Label>
                                       
                                              <div style="height: 180px;width:460px;  overflow:scroll" id="divengagement">
                                       <asp:GridView ID="gvrEngagement" OnRowDataBound="gvrEngagement_RowDataBound" runat="server" HeaderStyle-ForeColor="Black"  HeaderStyle-BackColor="Silver" 
                                           AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke">
                                                                <Columns>

                                                                   
                                                                      <asp:TemplateField   HeaderText="Show" HeaderStyle-Width="60px" ItemStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <p><a class="a_link" style="text-decoration: none!important;" href="<%#Eval("Link")%>" title="Edit"><%#Eval("[sname]")%></a> </p>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="City/State" HeaderStyle-Width="60px" ItemStyle-Width="180px">
                                                                        <ItemTemplate>
                                                                              <%#Eval("CITY_STATE")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                      <asp:TemplateField HeaderText="Start <br/> Date" HeaderStyle-Width="52px" ItemStyle-Width="180px">
                                                                        <ItemTemplate>
                                                                           
                                                                                <%#Eval("sdate")%>
                                                                                 
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="End <br/> Date" HeaderStyle-Width="52px" ItemStyle-Width="180px">
                                                                        <ItemTemplate>
                                                                           <%#Eval("edate")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Presenter" HeaderStyle-Width="80px" ItemStyle-Width="180px">
                                                                        <ItemTemplate>
                                                                             <%#Eval("pname")%>
                                                                         
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Status" HeaderStyle-Width="80px" ItemStyle-Width="180px" >
                                                                        <ItemTemplate>
                                                                            <%#Eval("status")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                          </div>
                                    </td>
                                </tr>
                                <tr>
 
                                                        <td>
                                          <asp:Label ID="lblPresenter" runat="server" Text="Presenter" Visible="false" style="font-weight:bold; font-size:14px;"></asp:Label>
                                       
                                              <div style="height: 180px;width:360px; overflow: auto">
                                       <asp:GridView ID="grdVenue" OnRowDataBound="grdVenue_RowDataBound"  runat="server"  HeaderStyle-CssClass="FixedHeader" HeaderStyle-ForeColor="Black"  HeaderStyle-BackColor="Silver" 
                                           AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke">
                                                                <Columns>

                                                                   
                                                                      <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="70px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                            <p><a class="a_link" style="text-decoration: none!important;" href="<%#Eval("Link")%>" title="Edit"><%#Eval("[pname]")%></a> </p>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="City/State" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                                                        <ItemTemplate>
                                                                              <%#Eval("CITY_STATE")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                      <asp:TemplateField HeaderText="First<br/> Name" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="60px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                           
                                                                                <%#Eval("fname")%>
                                                                                 
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Last Name" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                           <%#Eval("lname")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Phone" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="70px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                             <%#Eval("phone")%>
                                                                         
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   </Columns>
                                                            </asp:GridView>
                                          </div>   
                                                          
                                                            </td> 
                                </tr>
                            </table>

                        </td>
                                </tr>
                            </table>


                        </td>
                    </tr>

                   
                    <tr style="visibility: hidden">
                        <td align="right">Metro City</td>
                        <td align="left">
                            <asp:DropDownList ID="ddlmetrocity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmetrocity_SelectedIndexChanged" SkinID="ddlmedium">
                            </asp:DropDownList>
                        </td>
                        <td align="right">Metro State</td>
                        <td align="left">
                            <asp:Label ID="lblmetrostate" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="width: 100px">ZipCode</td>
                    </tr>
                    <tr>
                        <td colspan="9">
                                                                
                            </td>
                    </tr>
                </table>

               </div>
                    
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

