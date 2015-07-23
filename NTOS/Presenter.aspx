<%@ Page Title="Presenter" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Presenter.aspx.cs" Inherits="NTOS.Presenter" %>

<%@ Register Src="~/City.ascx" TagName="City" TagPrefix="UC" %>
<%@ Register Src="~/Docx.ascx" TagName="Docx" TagPrefix="UC" %>
<%@ Register Src="~/PresenterContactPerson.ascx" TagName="ConPerson" TagPrefix="UC" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
           
               }
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/jscript" src="Scripts/autoNumeric 1.9.15.js"></script>
    <script type="text/javascript">
        function SetPresenterId(source, eventargs) {
            $('#<%=hdnpresenterid.ClientID %>').val(eventargs.get_value());
        }
        function ClearPresenterID() {
            $('#<%=hdnpresenterid.ClientID %>').val("0");
        }

       

        function PrintContent1() {

            var DocumentContainer = document.getElementById('divengagement');
            var WindowObject = window.open("Engagements", "PrintWindow", "width=800,height=650,top=50,left=25,toolbars=no,scrollbars=yes,status=no,resizable=yes");
            WindowObject.document.write();
            WindowObject.document.write('<link href="~/content/Site.css" rel="stylesheet" type="text/css" />')
            WindowObject.document.writeln(DocumentContainer.innerHTML);
            WindowObject.document.close();
            WindowObject.focus();
            WindowObject.print();
            WindowObject.close();
        }


        $("#Form1").one("change", ":input", function () {
          
            var user_role = '<%= Session["userrole"].ToString() %>';
                   var role_status = (user_role == 'reader') ? 0 : 1;
                   if (role_status != 0)
                       $('#hdn_modify_status').val("1");

               });



        
    </script>


    <asp:HiddenField ID="hdnpresenterid" runat="server" />
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlpresenter" runat="server">
                <table  cellpadding="0" cellspacing="0" width="1900" style="padding:0px;">
                    
                    <tr>
                        <td class="contentpadding" style="text-align:left; padding:0px;">
                            <table width="100%"  cellpadding="0" cellspacing="0" align="left" border="0" style="padding:0px; text-align:left; float:left;">
                                <%--<tr>
                                    <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33" colspan="8">
                                        <a href="Presenter.aspx" id="lnknew" runat="server" visible="false" class="txtnew">New</a>
                                        <asp:Label ID="lblhead" runat="server">Create Presenter</asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="6">
                                        <div class="alerttd">
                                            <asp:ValidationSummary ID="vslist" runat="server" DisplayMode="BulletList" HeaderText="Field validations failed! Please check and correct!" CssClass="valsummary" />
                                            <asp:Label ID="lblerrmsg" runat="server" CssClass="valsummary"></asp:Label>
                                            <div id="divmsg" runat="server" class="msgbox">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                        <td class="contentpadding" colspan="6" align="left" style="text-align:left; padding: 0px 0px 20px 0px;">
                            <asp:Label ID="lblHeader" CssClass="largefont" Visible="false" runat="server" Text="Label"></asp:Label>
                        </td> 
                        
                    </tr>
                                <tr>
                                    <td class="padding_leftright10" valign="top"  width="130"  height="35">Presenter&nbsp;Name</td>
                                    <td valign="top" width="300"><asp:TextBox ID="txtpresentername" MaxLength="100" Columns="50" onkeyup="ClearPresenterID();" CssClass="txtmixcaps" runat="server" OnTextChanged="txtpresentername_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="acepresentername" runat="server" TargetControlID="txtpresentername"
                                            CompletionListElementID="divacewidth" DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ShowOnlyCurrentWordInCompletionListItem="true"
                                            MinimumPrefixLength="1" EnableCaching="true" OnClientItemSelected="SetPresenterId" CompletionSetCount="1" CompletionInterval="100"
                                            ServiceMethod="Getpresentername"  FirstRowSelected="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                                        <asp:RequiredFieldValidator ID="rfvpresentername" ToolTip="Enter presenter name!" runat="server" ErrorMessage="*" ControlToValidate="txtpresentername"
                                            CssClass="asterisk"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="padding_leftright10" valign="top" style="text-align: right;" width="150">Street Address</td>
                                    <td  valign="top"  rowspan="2"  width="470">
                                      <div style="position:absolute; margin: 30px 0px 0px -60px; text-align:right; line-height:30px;">City<br>ZipCode</div>
                                   
                                        <UC:City ID="uccity" runat="server" />
                                    </td>


                                    <td width="550" rowspan="8" valign="top">
                                                            <div style="float:left; margin: 0px 0px 0px 0px;"><UC:Docx ID="ucdocx" runat="server" /></div>
                                                        </td>
                              
                                    <td></td>

                                                       
 
                                    <td rowspan="8" width="600">
                                        <table>
                                            <tr>
                                                <td>
                                                   <asp:Label ID="lblEnggrid" runat="server" Text="Engagegment" Visible="false" style="font-weight:bold; font-size:14px;"></asp:Label>
                                       
                                              <div style="height: 180px; overflow: auto" id="divengagement">
                                       <asp:GridView ID="gvrEngagement"    OnRowDataBound="gvrEngagement_RowDataBound" 
                                          runat="server"  HeaderStyle-CssClass="FixedHeader" HeaderStyle-ForeColor="Black"  HeaderStyle-BackColor="Silver" 
                                           AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke">
                                           
                                          
                                                                <Columns>

                                                                   
                                                                      <asp:TemplateField   HeaderText="Show" HeaderStyle-Width="80px" ItemStyle-Width="80px" >
                                                                        <ItemTemplate>
                                                                            <p><a class="a_link" style="text-decoration: none!important;" href="<%#Eval("Link")%>" title="Edit"><%#Eval("[sname]")%></a> </p>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="City/State" HeaderStyle-Width="80px" ItemStyle-Width="80px" >
                                                                        <ItemTemplate>
                                                                              <%#Eval("CITY_STATE")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                      <asp:TemplateField HeaderText="Start <br/> Date" HeaderStyle-Width="53px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                           
                                                                                <%#Eval("sdate")%>
                                                                                 
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="End <br/> Date" HeaderStyle-Width="53px" ItemStyle-Width="80px" >
                                                                        <ItemTemplate>
                                                                           <%#Eval("edate")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Venue" HeaderStyle-Width="80px" ItemStyle-Width="80px" >
                                                                        <ItemTemplate>
                                                                             <%#Eval("vname")%>
                                                                         
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Status" HeaderStyle-Width="80px" ItemStyle-Width="80px">
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

                                                            <asp:Label ID="lblVenuegrid" runat="server" Text="Venue" Visible="false" style="font-weight:bold; font-size:14px;"></asp:Label>
                                                            <div style="height: 180px; overflow: auto">
                                       <asp:GridView ID="grdVenue"   runat="server" style="height:250px; overflow-y:scroll" HeaderStyle-CssClass="FixedHeader" HeaderStyle-ForeColor="Black"  HeaderStyle-BackColor="Silver" 
                                           AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="grdVenue_RowDataBound">
                                                                <Columns>

                                                                   
                                                                      <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                            <p><a class="a_link" style="text-decoration: none!important;" href="<%#Eval("Link")%>" title="Edit"><%#Eval("[vname]")%></a> </p>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="City/State" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                              <%#Eval("CITY_STATE")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                      <asp:TemplateField HeaderText="First Name" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                           
                                                                                <%#Eval("fname")%>
                                                                                 
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Last Name" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                           <%#Eval("lname")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Phone" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" ItemStyle-Width="80px">
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
                                       <%-- <div style="height:250px; width:700px;overflow:auto" id="gridviewContainer">--%>
                                    
                                        <%--  </div>   --%>            
                                    </td>

                                </tr>
                                <tr>
                                      <td class="padding_leftright10" valign="top"><div  style="margin-top:-60px;">Notes</div></td>
                                    <td valign="top" style="width:70px; padding:0px;"><div  style="margin-top:-60px;"><asp:TextBox ID="txtnotes" runat="server" TextMode="MultiLine" Rows="2" Columns="50"></asp:TextBox></div>
                                    </td>
                                    <td colspan="2">&nbsp;</td>
                                    <td align="right" valign="top"><table>
                                      

                                          

                                                      </table></td>
                                    
                                         
                                
                                    
                                </tr>
                                <tr>
                                     <td colspan="4">
                                         <asp:Panel ID="pnluc" runat="server" CssClass="ucpnl" Enabled="true">
                                             <table>
                                                 <tr>
                                                     <div class="t-border">
                                                         <td>
                                                             <UC:ConPerson ID="uccontactperson" runat="server" />
                                                         </td>
                                                     </div>
                                                    

                                                 </tr>
                                             </table>
                                         </asp:Panel>
                                     </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                </td>
                </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="contentpadding">
                            <table cellpadding="0" cellspacing="0" align="left" border="0">
                                <%-- <tr>
                                    <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33" colspan="9">
                                        <a href="Venue.aspx" id="lnknew" runat="server" visible="false" class="txtnew">New</a>
                                        <asp:Label ID="lblhead" runat="server">Create Venue</asp:Label>
                                    </td>
                                </tr>--%>
                              
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
