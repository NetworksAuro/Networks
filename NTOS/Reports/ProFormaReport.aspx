<%@ Page Title="Pro-Forma Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProFormaReport.aspx.cs" Inherits="NTOS.Reports.ProFormaReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var status = 0;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //debugger;
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null && status == 1) {
                popup.show();

            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
                status = 0;
            }
        }
        function showpop() {
            var isValid = Page_ClientValidate("");
            if (isValid == true)
                status = 1;
        }
        function hide() {
            if ($('#tbl1')[0].rows[2] != null) {
                $('#tbl1')[0].rows[2].style.display = 'none';
                //$('#tbl1')[0].rows[2].hide("Blind", 1000);
            }
        }
        function anc() {
            $('#<%= lnkexcel.ClientID %>')[0].click();
            // window.open(url, 'Download');
        }
    </script>
    <center>
        <asp:UpdatePanel ID="upl1" runat="server" RenderMode="Block">
            <ContentTemplate>
                <asp:HiddenField ID="hdnfilepath" runat="server" />
                <asp:UpdateProgress ID="UpdateProgress" runat="server">
                    <ProgressTemplate>
                        <asp:Image ID="Image1" Width="100px" Height="100px" ImageUrl="~/Images/process1.gif"
                            AlternateText="Processing" runat="server" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                    PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
                <table cellpadding="4" width="100%" border="0">
                    <tr>
                        <td class="contentpadding" valign="bottom">
                            <table width="100%" cellpadding="0" cellspacing="0" align="left"  border="0">
                              <%--  <tr>
                                    <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33">Pro-Forma Report
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td align="center">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" valign="bottom">
                                        <table width="100%" border="0" id="tbl1">
                                            <tr>
                                                <td style="width: 85%;" align="center" valign="middle" class="contentpadding">Show&nbsp;:&nbsp;
                                                    <asp:DropDownList SkinID="ddlmedium" ID="ddlShow" runat="server" onchange="hide();highlight();">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvShow" runat="server" ControlToValidate="ddlShow"
                                                        InitialValue="0"
                                                        CssClass="asterisk" ToolTip="Select Show!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date
                                                    &nbsp;&nbsp;
                                                    <asp:TextBox ID="txtDate" runat="server" onchange="highlight();"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="cedate" runat="server" TargetControlID="txtDate" PopupButtonID="txtDate"></ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvdate" runat="server" ControlToValidate="txtDate"
                                                        CssClass="asterisk" ToolTip="Select Date!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;
                                        <asp:Button ID="btnShowRpt" CssClass ="butt"  runat="server" Text="Extract" OnClick="btnShowRpt_Click" OnClientClick="showpop();" />
                                                    &nbsp;</td>

                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                             <tr>
                                                <td align="center">
                                                <asp:Label ID="lblerror" runat="server" ForeColor="Orange" Font-Bold="true"></asp:Label>

                                                </td></tr>
                                            <tr runat="server" id="trexcel" visible="false">
                                                <td align="center">
                                                    <a href="#" class="excel" runat="server" id="lnkexcel" style="border: 0">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Excel.png" Width="80" Height="80" BorderWidth="0" />
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>

        </asp:UpdatePanel>
    </center>
</asp:Content>
