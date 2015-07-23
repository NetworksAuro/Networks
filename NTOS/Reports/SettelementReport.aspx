<%@ Page Title="Settlement Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SettelementReport.aspx.cs" Inherits="NTOS.Reports.SettelementReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .modalPopup {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }

        .imgbtn {
            vertical-align: bottom;
            padding: 20px 0 0 40px;
        }
    </style>
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
    </script>
    <script type="text/javascript" language="javascript">
        function showexcelimg() {
            $('#<%= trexcel.ClientID %>').hide();
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
                            <table width="100%" cellpadding="0" cellspacing="0" align="left" class="tbl_border">
                                <tr>
                                    <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33">Settlement Reports
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" valign="bottom">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="width: 85%;" align="right" valign="middle" class="contentpadding">Show&nbsp;:&nbsp;
                                                    <asp:DropDownList SkinID="ddlmedium" ID="ddlShow" runat="server" OnSelectedIndexChanged="ddlShow_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvShow" runat="server" ControlToValidate="ddlShow"
                                                        InitialValue="0"
                                                        CssClass="asterisk" ToolTip="Select Show!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;City/State&nbsp;:&nbsp;
                    <asp:DropDownList AutoPostBack="true" ID="ddlCity"  SkinID="ddlmedium" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCity"
                                                        InitialValue="0"
                                                        CssClass="asterisk" ToolTip="Select City!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Venue&nbsp;:&nbsp;
                                                <asp:DropDownList  SkinID="ddlmedium" ID="ddlVenue" AutoPostBack="true" OnSelectedIndexChanged="ddlVenue_SelectedIndexChanged"
                                                    runat="server">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Start Date&nbsp;:&nbsp;
                                                <asp:DropDownList  SkinID="ddlmedium" ID="ddlCreateddate" runat="server" OnSelectedIndexChanged="ddlCreateddate_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCreateddate"
                                                        InitialValue="0"
                                                        CssClass="asterisk" ToolTip="Select Start Date!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Week&nbsp;:&nbsp;
                                                    <asp:DropDownList ID="ddlweek" SkinID="ddlmedium" runat="server" OnSelectedIndexChanged="ddlweek_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlweek"
                                                        InitialValue="0"
                                                        CssClass="asterisk" ToolTip="Select Start Date!" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblEngtEndDate" runat="server" Width="80px" Visible="false"></asp:Label>

                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnShowRpt" runat="server" OnClientClick="showpop();" Text="Extract" OnClick="btnShowRpt_Click" />
                                                    &nbsp;</td>
                                                <td style="width: 15%;"></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr runat="server" id="trexcel" visible="false">
                                                <td align="center" colspan="2">
                                                <a href="#" class="excel" runat="server" id="lnkexcel" style="border:0"><asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Excel.png" Width="80" Height="80" BorderWidth="0" /> </a>
                                                    </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="14" style="width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="padding_leftright10" align="center">
                                                    <%-- <rsweb:reportviewer id="rptviewer" width="100%" runat="server" waitcontroldisplayafter="0"
                                                        asyncrendering="False">
                                                    </rsweb:reportviewer>--%>
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
