<%@ Page Title="Show" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="NTOS.Show" %>

<%@ Register Src="~/Docx.ascx" TagName="Docx" TagPrefix="UC" %>
<%@ Register Src="~/City.ascx" TagName="City" TagPrefix="UC" %>
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
        function setShowID(source, eventargs) {
            $('#<%=hdnshowid.ClientID%>').val(eventargs.get_value());
        }
        function clearshowid() {
            $('#<%=hdnshowid.ClientID%>').val("0");
        }

        function TextValidation(Source, args) {
            args.isValid = false;
        }
    </script>
    <script type="text/javascript">
        jQuery(function ($) {
            $('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99' });
            $('.Numeric').autoNumeric({ vMax: '999999999999999', vMin: '-999999999999999', nBracket: '(,)' });
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            $('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99' });
            $('.Numeric').autoNumeric({ vMax: '999999999999999', vMin: '-999999999999999', nBracket: '(,)' });
            CheckAll();

        }

        function CheckAll(thisid) {
            thisid = (thisid != undefined) ? $('#' + thisid.id.replace('chk', 'txtid'))[0] : thisid;
            //debugger;
            var t = $('#tblrep')[0];
            var nextpref = 0;
            var rep = $('#<%=repbind.ClientID%>');
            var ctrls = document.getElementsByTagName('input');
            var showlist = "", delimeter = "", showidlist = "", cid = "";
            for (var i = 1; i < t.rows.length; i++) {
                var lblshow = $(rep.selector + '_lblrepshowname_' + (i - 1))[0];
                var txtid = $(rep.selector + '_txtid_' + (i - 1))[0];
                var chk = $(rep.selector + '_chk_' + (i - 1))[0];
                if (chk.checked == true) {
                    showlist = showlist + delimeter + $(lblshow).text();
                    delimeter = ",";
                    cid = ($(txtid).val() == "") ? "0" : $(txtid).val();
                    if ((cid == "" || cid == "0") && $(txtid)[0].style.display != "none") {
                        $(txtid).val(nextpref);
                        alert('Enter preference id!');
                        $(txtid).focus();
                        return false;
                        break;
                    }
                    var arr = showidlist.split(',');
                    for (var k = 0; k < arr.length && cid != ""; k++) {
                        if (arr[k] == cid) {
                            alert('Already exists!');
                            $(txtid).focus();
                            $(txtid).val("");
                            break;
                        }
                    }
                    showidlist = showidlist + $(txtid).val() + ",";
                    nextpref = (parseInt(cid) < parseInt(nextpref)) ? parseInt(nextpref) : parseInt(cid)
                    //if ($(txtid).val() == "")
                    //    $(txtid).val(parseInt(nextpref + 1));
                }
                enabletxtbox(chk);

            }
            var txtshowlist = $('#<%=txtshowlist.ClientID%>');
            txtshowlist.val(showlist);
            if ((thisid != undefined) & $(thisid).val() == "")
                $(thisid).val(parseInt(nextpref + 1));
            return true;
        }
        function enabletxtbox(thisid) {
            // debugger;
            var txtid = thisid.id.replace('chk', 'txtid');

            var dv = $('#div1');
            $('#' + txtid)[0].readOnly = false;
            var flg1 = ($(thisid)[0].checked == true) ? "block" : "none";
            var txtval = ($(thisid)[0].checked == true) ? $('#' + txtid).val() : "";
            $('#' + txtid).val(txtval);
            $('#' + txtid)[0].style.display = flg1;
        }

        $("#Form1").one("change", ":input", function () {

            var user_role = '<%= Session["userrole"].ToString() %>';
             var role_status = (user_role == 'reader') ? 0 : 1;
             if (role_status != 0)
                 $('#hdn_modify_status').val("1");

         });



      

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnshowid" runat="server" />
            <asp:HiddenField ID="hdnshowname" runat="server" />
            <asp:HiddenField ID="hdncityid" runat="server" />
            <asp:Panel ID="pnlshow" runat="server">
                <div  style="float:left; width:1900px;">
                <table  cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="12" class="alerttd">
                            <div>
                                <asp:ValidationSummary ID="vslist" runat="server" DisplayMode="BulletList" HeaderText="Field validations failed! Please check and correct!" CssClass="valsummary" />
                            </div>
                            <asp:Label ID="lblerrmsg" runat="server" CssClass="valsummary"></asp:Label>
                            <div id="divmsg" runat="server" class="msgbox">
                            </div>
                        </td>
                    </tr>
                     <tr>

                           <td class="contentpadding" colspan="15">
                            <asp:Label ID="lblHeader" CssClass="largefont" Visible="false" runat="server" Text="Label"></asp:Label>
                        </td> 
                        
                    </tr>
                    <tr>
                        <td align="right" height="50px" style="width:auto; padding: 0px 5px 0px 10px;"><label>Show Name</label></td>
                        <td align="left" style="width:auto; padding: 18px 10px 0px 5px;">
                            <asp:TextBox ID="txtshowname" CssClass="txtmixcaps txtreq" runat="server" autocomplete="off" OnTextChanged="txtshowname_TextChanged"
                                AutoPostBack="true" onkeyup="clearshowid();" MaxLength="200" ToolTip="Enter show name!"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="aceshowname" runat="server" TargetControlID="txtshowname"
                                CompletionListElementID="divacewidth" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="60"
                                ServiceMethod="Getshownames" OnClientItemSelected="setShowID" FirstRowSelected="true">
                            </ajaxToolkit:AutoCompleteExtender>
                            <asp:RequiredFieldValidator ID="rfvshowname" runat="server" ControlToValidate="txtshowname"
                                CssClass="asterisk" ToolTip="Enter show name!" ErrorMessage="*"></asp:RequiredFieldValidator></td>


                        <td align="right"  style="width:auto; padding: 0px 5px 0px 10px;">Federal&nbsp;ID</td>
                        <td align="left"  style="width:auto; padding: 0px 10px 0px 5px;">
                            <asp:TextBox ID="txtfederalid" MaxLength="15" runat="server" CssClass="Numeric"></asp:TextBox></td>





                        <td align="right"  style="width:auto; padding: 0px 5px 0px 10px;">Wkly&nbsp;Operating&nbsp;Expense</td>
                        <td align="left"  style="width:auto; padding: 0px 10px 0px 5px;">
                            <asp:TextBox ID="txtwklyopexp" runat="server" CssClass="dollor"></asp:TextBox></td>


                        <td align="right"  style="width:auto; padding: 0px 5px 0px 10px;">Overhead&nbsp;Nut</td>
                        <td align="left"  style="width:auto; padding: 0px 10px 0px 5px;">
                            <asp:TextBox ID="txtoverheadnut" runat="server" CssClass="dollor"></asp:TextBox></td>
                        <td rowspan="15" valign="top"   style="width:auto; padding: 0px 10px 0px 10px;">                         
                               <div style="margin: 0px 0px 0px 50px;"><UC:Docx ID="ucdocx" runat="server" /></div>
                        </td>

                        <td rowspan="15" valign="top"   style="width:auto; padding: 0px 10px 0px 10px;">                         

                            <asp:Label ID="lblEnggrid"  runat="server" Text="Engagegment" Visible="false" style="font-weight:bold; font-size:14px;"></asp:Label>
                                       
                                              <div style="height: 200px; width:450px; overflow: auto" id="divengagement">
                                       <asp:GridView ID="gvrEngagement" OnRowDataBound="gvrEngagement_RowDataBound" AllowSorting="true" OnSorting="gvrEngagement_Sorting"  runat="server"  HeaderStyle-CssClass="FixedHeader" HeaderStyle-ForeColor="Black"  HeaderStyle-BackColor="Silver" 
                                           AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" Width="502px" >
                                                                <AlternatingRowStyle BackColor="WhiteSmoke" />
                                                                <Columns>

                                                                   
                                                                       <asp:TemplateField HeaderText="Presenter" HeaderStyle-Width="91px" ItemStyle-Width="91px">
                                                                        <ItemTemplate>
                                                                                <p><a class="a_link" style="text-decoration: none!important;" href="<%#Eval("Link")%>" title="Edit"><%#Eval("[pname]")%></a> </p>
                                                                         
                                                                        </ItemTemplate>
                                                                           <HeaderStyle Width="100px" />
                                                                           <ItemStyle Width="70px" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="City/State" HeaderStyle-Width="100px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                              <%#Eval("CITY_STATE")%>
                                                                        </ItemTemplate>
                                                                         
                                                                    </asp:TemplateField>
                                                                    
                                                                      <asp:TemplateField HeaderText="Start Date" HeaderStyle-Width="100px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                           
                                                                                <%#Eval("sdate")%>
                                                                                 
                                                                        </ItemTemplate>
                                                                          <HeaderStyle Width="100px" />
                                                                          <ItemStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="End Date" HeaderStyle-Width="100px" ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                           <%#Eval("edate")%>
                                                                        </ItemTemplate>
                                                                          <HeaderStyle Width="100px" />
                                                                          <ItemStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    
                                                                     <asp:TemplateField HeaderText="Status" HeaderStyle-Width="100px" ItemStyle-Width="80px" >
                                                                        <ItemTemplate>
                                                                            <%#Eval("status")%>
                                                                        </ItemTemplate>
                                                                         <HeaderStyle Width="100px" />
                                                                         <ItemStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle BackColor="YellowGreen" CssClass="FixedHeader" ForeColor="Black" />
                                                            </asp:GridView>
                                          </div>
                        </td>
                        </tr>
                    <tr>
                        <td align="right" valign="top" style="width:auto; padding: 0px 5px 0px 10px;">Variabl&nbsp;Royalities</td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtvariableroyalities" MaxLength="10" runat="server"></asp:TextBox></td>

                        <td align="right" valign="top" style="width:auto; padding: 0px 5px 0px 10px;">Show&nbsp;begin&nbsp;date</td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtshowbegindate" runat="server" CssClass="txtreq" onkeyup="highlight();"></asp:TextBox>
                            <asp:CompareValidator runat="server" ID="cvshowbegindate" Type="Date" ControlToValidate="txtshowbegindate" Operator="DataTypeCheck"
                                ErrorMessage="#" ForeColor="Red" ToolTip="Enter valid date (mm/dd/yyyy)"></asp:CompareValidator>

                            <asp:RequiredFieldValidator ID="rfvshowbegindate" runat="server" ControlToValidate="txtshowbegindate"
                                CssClass="asterisk" ToolTip="Enter show begin date!" ErrorMessage="
                                            "></asp:RequiredFieldValidator>
                            <ajaxToolkit:CalendarExtender ID="ceshowbegindate" runat="server" TargetControlID="txtshowbegindate">
                            </ajaxToolkit:CalendarExtender>
                        </td>


                        <td align="right" valign="top" style="width:auto; padding: 0px 5px 0px 10px;">Company&nbsp;Manager</td>
                        <td align="left" valign="top">
                            <asp:DropDownList ID="ddlcompanymgr" runat="server"></asp:DropDownList></td>
                        <td></td>
                        <td></td>
                               
                 
                        
                    </tr>
                    
                </table>



                <table  cellpadding="0" cellspacing="0" style=" margin-left:18px;">


                    <tr>
                        <td class="padding_leftright10">
                          
                            <table>
                                <tr>

                                    <td>
                                        <div class="txt_contentheader uchead">
                                            <div style="float: left;">

                                                <label class="heading">
                                                    Corporate Information
                                                </label>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="txt_contentheader uchead">
                                            <div style="float: left;">


                                                <label class="heading">
                                                    Pro-forma Dependent Show Precedence</label>

                                            </div>
                                        </div>
                                    </td>
                                    <td style="width:1000px">

                                    </td>
                                          

                                </tr>



                                <tr>
                                    <div class="t-border">
                                        <td width="700px">
                                            <table style="float: left; margin-left: 20px; margin-right:30px; margin-top:30px;">
                                               <%-- <tr>
                                                    <td>&nbsp;</td>
                                                </tr>--%>

                                                <tr>
                                                    <td align="right" style="width:auto; padding: 0px 5px 0px 5px;">Name</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtname" runat="server" CssClass="txtmixcaps txtreq" MaxLength="200" onkeyup="highlight();"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvname"  runat="server" ControlToValidate="txtname" CssClass="asterisk" ErrorMessage="*" ToolTip="Enter name!"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" style="width:auto; padding: 0px 5px 0px 10px;">Street Address</td>
                                                    <td rowspan="3" style="text-align: left">
                                                        <UC:City ID="uccity" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="0">City</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="right"">Zip Code</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </div>
                                    <div class="t-border">
                                        <td width="500px">
                                            <table border="0" style="float: left; margin-top:-70px;">

                                                <tr valign="top">
                                                    <td>
                                                        <asp:TextBox ID="txtshowlist" runat="server" Width="500px"></asp:TextBox>
                                                        <asp:Panel ID="pnlcolumn" runat="server" BackColor="#cccccc" Height="100px" ScrollBars="Auto" Width="300px">
                                                            <table id="tblrep">
                                                                <tr class="gridviewheader" valign="top">
                                                                    <th>&nbsp;</th>
                                                                    <th>Show Name</th>
                                                                    <th style="width: 90px">Preference</th>
                                                                </tr>
                                                                <asp:Repeater ID="repbind" runat="server" OnItemDataBound="repbind_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr id="trcrow" runat="server" class='<%# Container.ItemIndex % 2 == 0 ? "gdvrowstyle" : "gdvaltrowstyle"%>'>
                                                                            <td>
                                                                                <asp:CheckBox ID="chk" runat="server" onclick="javascript:CheckAll(this);" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblrepshowname" runat="server" Text='<%#Eval("Show_Name") %>' Width="160px">
                                                                                </asp:Label>
                                                                                <asp:HiddenField ID="hdnrepshowid" runat="server" Value='<%#Eval("Show_id") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <div id="div1" style="display: block">
                                                                                    <asp:TextBox ID="txtid" runat="server" CssClass="Numeric" onchange="CheckAll(this);" Style="display: none" Text='<%#Eval("Show_pro_preference") %>' Width="80px"></asp:TextBox>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </asp:Panel>
                                                        <ajaxToolkit:HoverMenuExtender ID="hmecolumn" runat="server" PopDelay="25" PopupControlID="pnlcolumn" PopupPosition="Bottom" TargetControlID="txtshowlist">
                                                        </ajaxToolkit:HoverMenuExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </div>
                                </tr>



                                </div>
                            </table>

                        </td>

                    </tr>


                </table>
                </div>
            </asp:Panel>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
