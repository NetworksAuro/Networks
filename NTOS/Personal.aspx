<%@ Page Title="Personnel" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true"
    CodeBehind="Personal.aspx.cs" Inherits="NTOS.Personal" %>

<%@ Register Src="~/PersonalShow.ascx" TagName="Show" TagPrefix="UC" %>
<%@ Register Src="~/City.ascx" TagName="City" TagPrefix="UC1" %>
<%@ Register Src="~/City.ascx" TagName="OtherCity" TagPrefix="UC" %>
<%@ Register Src="~/Docx.ascx" TagName="Docx" TagPrefix="UC" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        #divacewidth1 {
            min-width: 160px !important;
            width: auto !important;
            height: 180px !important;
        }

          .FixedHeader {
            position: absolute;
            font-weight: bold;
               }     

            #divacewidth1 div {
                width: auto !important;
            }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        function comparedate(date1, date2, msg) {
            var d1 = $('#' + date1).val();
            var d2 = $('#' + date2).val();
            dateN1 = new Date(d1.split("/")[2], d1.split("/")[0] - 1, d1.split("/")[1]);
            dateN2 = new Date(d2.split("/")[2], d2.split("/")[0] - 1, d2.split("/")[1]);
            if (dateN1 > dateN2) {
                alert(msg);
                $('#' + date1).val("");
                $('#' + date2).val("");
                return false;
            }
            return true;
        }
        function checkdatemax(date1, msg) {
            var txt1 = $('#' + date1);
            var d1 = txt1.val();
            date1 = new Date(d1.split("/")[2], d1.split("/")[0] - 1, d1.split("/")[1]);
            date2 = new Date();
            if (date1 > date2) {
                alert(msg);
                txt1.val("");
                return false;
            }
            return true;
        }

    </script>
    <script type="text/javascript">
        function setPersonalID(source, eventargs) {
            $('#<%=hdnpersonalid.ClientID %>').val(eventargs.get_value());
        }
        function ClearPersonalID() {
            $('#<%=hdnpersonalid.ClientID %>').val("0");
        }

        $("#Form1").one("change", ":input", function () {

            var user_role = '<%= Session["userrole"].ToString() %>';
             var role_status = (user_role == 'reader') ? 0 : 1;
             if (role_status != 0)
                 $('#hdn_modify_status').val("1");

         });

    </script>

    <asp:HiddenField ID="hdnpersonalid" runat="server" />
    <asp:HiddenField ID="hdnstatus" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float:left; width:1900px;">
            <table   cellpadding="0" cellspacing="0" border="0" width="100%">

                <tr>
                    <td colspan="8" align="center">
                        <div class="alerttd">
                            <asp:ValidationSummary ID="vslist" runat="server" DisplayMode="BulletList" HeaderText="Field validations failed! Please check and correct!" CssClass="valsummary" />
                            <asp:Label ID="lblerrmsg" runat="server" CssClass="valsummary"></asp:Label>
                            <div id="divmsg" runat="server" class="msgbox">
                            </div>
                        </div>
                    </td>
                </tr>
                        <tr>

                           <td class="contentpadding" colspan="15">
                            <asp:Label ID="lblHeader" CssClass="largefont" runat="server" Visible="false" Text="Label"></asp:Label>
                        </td> 
                        
                    </tr>
                <tr>
                     <td class="contentpadding" colspan="13">
                            <asp:Label ID="lblHeader1" runat="server" Visible="false" Text="Label"></asp:Label>
                        </td> 
                </tr>
                <tr>
                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label style="color: #00ACD7">First Name </label>
                    </td>
                    <td align="left" style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtfirstname" CssClass="txtmixcaps" runat="server" OnTextChanged="txtfirstname_TextChanged"
                            AutoPostBack="true" MaxLength="30" onchange="ClearPersonalID();"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="acefirstname" runat="server" TargetControlID="txtfirstname"
                            CompletionListElementID="divacewidth" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                            ServiceMethod="Getpersonalname" OnClientItemSelected="setPersonalID" FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                        <asp:RequiredFieldValidator ID="rfvfirstname" ToolTip="Enter first name!" runat="server" ErrorMessage="*" ControlToValidate="txtfirstname"
                            CssClass="asterisk"></asp:RequiredFieldValidator>
                    </td>

                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label style="color: #00ACD7;">Phone(Direct) </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtphoneland" MaxLength="20" runat="server"></asp:TextBox>
                    </td>

                     <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label>DOB </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtdob" runat="server"></asp:TextBox>
                        <asp:CompareValidator runat="server" ID="cvdob" Type="Date" ControlToValidate="txtdob" Operator="DataTypeCheck"
                            ErrorMessage="#" ForeColor="Red" ToolTip="Enter valid date (mm/dd/yyyy)"></asp:CompareValidator>
                        <ajaxToolkit:CalendarExtender ID="cedob" runat="server" TargetControlID="txtdob">
                        </ajaxToolkit:CalendarExtender>
                    </td>

                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label>Facebook </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtfacebook" MaxLength="30" runat="server"></asp:TextBox>
                    </td>

                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label style="color: #00ACD7;">Company </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtcompany" CssClass="txtmixcaps" runat="server" MaxLength="50"></asp:TextBox>
                        <div id="divacewidth1">
                        </div>
                        <ajaxToolkit:AutoCompleteExtender ID="acecompany" runat="server" TargetControlID="txtcompany"
                            CompletionListElementID="divacewidth1" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                            ServiceMethod="Getcompanyname" FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                        <asp:RequiredFieldValidator ID="rfvcompany" ToolTip="Enter company!" runat="server" ErrorMessage="*" ControlToValidate="txtcompany"
                            CssClass="asterisk"></asp:RequiredFieldValidator>
                    </td>
                    
                    
                    
                    

                    <td rowspan="15" width="20%" style="padding: 0px 10px 0px 5px; width:auto;">
                            <div style="margin: 0px 0px 0px 50px;"<UC:Docx ID="ucdocx" runat="server" />
                        </td>
                    
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                           <div style="height: 30px; overflow: auto" id="divengagement">
                                       <asp:GridView ID="gvrEngagement"    OnRowDataBound="gvrEngagement_RowDataBound" 
                                          runat="server"  HeaderStyle-CssClass="FixedHeader" HeaderStyle-ForeColor="Black"  HeaderStyle-BackColor="YellowGreen" 
                                           AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke">
                                                                <Columns>

                                                                   
                                                                      <asp:TemplateField   HeaderText="Show" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                            <p><a class="a_link" style="text-decoration: none!important;" href="<%#Eval("Link")%>" title="Edit"><%#Eval("[sname]")%></a> </p>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="City/State" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                              <%#Eval("CITY_STATE")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                      <asp:TemplateField HeaderText="Start Date" HeaderStyle-Width="80px" ItemStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                           
                                                                                <%#Eval("sdate")%>
                                                                                 
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="End Date" HeaderStyle-Width="80px" ItemStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                           <%#Eval("edate")%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Presenter" HeaderStyle-Width="80px" ItemStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                             <%#Eval("pname")%>
                                                                         
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Status" HeaderStyle-Width="80px" ItemStyle-Width="80px" >
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
                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label style="color: #00ACD7;">Middle Name </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtmiddlename" CssClass="txtmixcaps" runat="server" MaxLength="30"></asp:TextBox>
                    </td>

                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label style="color: #00ACD7;">Phone(Cell) </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtphonecell" MaxLength="20" runat="server"></asp:TextBox>
                    </td>

                     <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label>Marital Status </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtmaterialstatus" MaxLength="10" runat="server"></asp:TextBox>
                    </td>

                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label>Twitter </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txttwitter" MaxLength="30" runat="server"></asp:TextBox>
                    </td>


                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label>Date of Hire </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtdateifhire" runat="server"></asp:TextBox>
                        <asp:CompareValidator runat="server" ID="cvdateifhire" Type="Date" ControlToValidate="txtdateifhire" Operator="DataTypeCheck"
                            ErrorMessage="#" ForeColor="Red" ToolTip="Enter valid date (mm/dd/yyyy)"></asp:CompareValidator>
                        <ajaxToolkit:CalendarExtender ID="cedateifhire" runat="server" TargetControlID="txtdateifhire">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    
                   
                   
                    </tr>
                <tr>
                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label style="color: #00ACD7;">Last Name </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtlastname" CssClass="txtmixcaps" runat="server" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvlastname" ToolTip="Enter last name!" runat="server" ErrorMessage="*" ControlToValidate="txtlastname" CssClass="asterisk"></asp:RequiredFieldValidator>
                    </td>

                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label>Fax </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtfax" MaxLength="20" runat="server"></asp:TextBox>
                    </td>
                    
                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label style="color: #00ACD7;">Employee Type </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:DropDownList SkinID="ddlmedium" ID="ddlemployeetype" runat="server">
                        </asp:DropDownList>
                    </td>                  
                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label>Webpage </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtwebpage" MaxLength="60" runat="server"></asp:TextBox>
                    </td>
                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label>Termination Date </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txttermanitationdate" runat="server" OnTextChanged="txttermanitationdate_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <asp:CompareValidator runat="server" ID="cvtermdate" Type="Date" ControlToValidate="txttermanitationdate" Operator="DataTypeCheck"
                            ErrorMessage="#" ForeColor="Red" ToolTip="Enter valid date (mm/dd/yyyy)"></asp:CompareValidator>
                        <ajaxToolkit:CalendarExtender ID="ceterminationdate" runat="server" TargetControlID="txttermanitationdate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    
                </tr>

                <tr>
                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label style="color: #00ACD7;">Title </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:DropDownList SkinID="ddlmedium" ID="ddltitle" runat="server">
                            <asp:ListItem>SELECT</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label>Phone(Other) </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtphoneother" MaxLength="20" runat="server"></asp:TextBox>
                    </td>

                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label style="color: #00ACD7;">Employee Status </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:DropDownList ID="ddlemployeestatus" SkinID="ddlmedium" runat="server"></asp:DropDownList>
                    </td>
                    
                    <td align="right" style="padding: 0px 5px 0px 10px; width:auto;">
                        <label>Email </label>
                    </td>
                    <td style="padding: 0px 10px 0px 5px; width:auto;">
                        <asp:TextBox ID="txtemail" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                    
                    
                    
                    <td></td>
                    <td></td>
                </tr>



            </table>

            <table  cellpadding="0" cellspacing="0" align="left" border="0" style="margin-top:30px;">
                <tr>
                    <td class="contentpadding">
                        <asp:Panel ID="pnlpersonal" runat="server">
                            <table  cellpadding="0" cellspacing="0" align="left" border="0">
                                <%--<tr>

                                    <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33"
                                        colspan="9">
                                        <a href="Personal.aspx" id="lnknew" runat="server" visible="false" class="txtnew">New</a>
                                        <asp:Label ID="lblhead" runat="server">Create Personnel</asp:Label>
                                    </td>
                                </tr>--%>
                                
                                
                                <tr>
             
                                    <td class="txt_contentheader linebottom" align="left" style="width:550px;">
                                        <label class="heading">Residential Address </label>
                                    </td>
                                    <td class="txt_contentheader linebottom" align="left">
                                        <label class="heading">Other Address</label>
                                        </td>
                                </tr>

                                <tr>
                                    <td colspan="2">

                                        <table>
                                            <tr>
                                                <td valign="top" style="text-align: right; padding: 0px 5px 0px 5px; width:auto;">Street address</td>
                                                <td style="padding: 0px 10px 0px 0px; width:430px;">
                                                    <div style="position:absolute; margin: 34px 0px 0px -60px; text-align:right;">City<br />Zip Code</div>
                                                    <UC1:City ID="ucresidcity" runat="server" /></td>
                                                <td valign="top" style="text-align: right; padding: 0px 5px 0px 5px; width:auto;">Street address</td>
                                                <td style="padding: 0px 10px 0px 0px; width:auto;">
                                                    <div style="position:absolute; margin: 34px 0px 0px -60px; text-align:right;">City<br />Zip Code</div>
                                                    <UC:OtherCity ID="ucothercity" runat="server" /></td>
                                            </tr>
                                        </table>



                                        <table>
                                            
                                <tr>
                                    <td  colspan="2">&nbsp;   
                                   <div ></div>
                                        <asp:Panel ID="pnlucshow" runat="server" CssClass="ucpnl" Enabled="true">
                                            <div style="min-height: 120px">
                                                <UC:Show ID="ucshow" runat="server" />
                                            </div>
                                        </asp:Panel>
                                    </td>
                                    <td colspan="3"  valign="top">&nbsp;   
                                    <div></div>
                                        <%--<asp:Panel ID="pnlucdocx" runat="server" CssClass="ucpnl" Enabled="true" Width ="450px">
                                            <div style="min-height: 120px">
                                                <UC:Docx ID="ucdocx" runat="server" />
                                            </div>
                                        </asp:Panel>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>

                                        </table>


                                    </td>

                                </tr>
                                
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
