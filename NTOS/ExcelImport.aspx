<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExcelImport.aspx.cs" Inherits="NTOS.ExcelImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #divupload {
            border: solid 1px;
            padding: 10px;
            margin: 10px 10px 10px 20px;
            width: 25%;
            height: 50px;
        }

        #divexcel {
            border: solid 0px;
            margin: 0 0 0 20px;
            display: block;
            padding: 10px;
            width: 25%;
        }

            #divexcel [type="submit"] {
                display: block;
                margin: 0 0 10px 0;
                width: 150px !important;
                border: 1px solid;
                font-family: arial;
                font-size: 12px;
                font-weight: bold;
                padding: 6px 12px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function confirmbox(msg) {
            return confirm(msg);
        }

    </script>
    <%--<script type="text/javascript">
        var status = 1;
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
    </script>--%>
    <%--<asp:UpdatePanel ID="uppr" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                    <ProgressTemplate>
                        <asp:Image ID="Image1" Width="100px" Height="100px" ImageUrl="~/Images/process1.gif"
                            AlternateText="Processing" runat="server" />
                        <asp:Label ID="lblfilename1" runat="server"></asp:Label>
                    </ProgressTemplate>                
                </asp:UpdateProgress>
                <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                    PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
            
            <br />
            
            
            
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:Label ID="lblErrmsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>

    <div id="divupload" title="Upload">
        <%--<ajaxToolkit:AjaxFileUpload runat="server" MaximumNumberOfFiles="10" />--%>
        <asp:Button runat="server" Text="Remove All" ID="btncleanfolder" OnClick="btncleanfolder_Click" />
        <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" />
        <asp:Button runat="server" Text="Upload" ID="btnupload" OnClick="btnupload_Click" />
    </div>
    <div id="divexcel">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnvalidate" runat="server" Text="Validate" OnClick="btnvalidate_Click" ToolTip="Validate the excel files" />
                </td>
                <td>
                    <asp:Label ID="lblfilename" runat="server"></asp:Label></td>
                <td><a href="#" class="excel" runat="server" id="lnkvalexcel" style="border: 0">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Excel.png" Width="50" Height="50" BorderWidth="0" />
                </a>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnwritecolumns" runat="server" OnClick="btnwritecolumns_Click" Text="Write Excel columns" ToolTip="Write excel first columns (boxoffice and summary sheets) to excel" />
                </td>
                <td></td>
                <td><a href="#" class="excel" runat="server" id="lnkexcelclms" style="border: 0">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Excel.png" Width="50" Height="50" BorderWidth="0" />
                </a>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnvalfromexcel" runat="server" OnClick="btnvalfromexcel_Click" Text="Load from Excel" ToolTip="Move data from excel to temp table" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnMoveps" runat="server" Text="Move to Price Scale" OnClick="btnMoveps_Click" ToolTip="Move price scale data to temp table" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnTemptoMain" runat="server" Text="Load Temp to Main" OnClick="btnTemptoMain_Click" ToolTip="Move data from Temp table to Main tables"
                        OnClientClick="return confirmbox('Are you sure?');" /></td>
            </tr>
            <asp:HiddenField ID="hdnvalidation_status" runat="server" />
        </table>
    </div>
    <asp:Button ID="btnkillprocess" runat="server" OnClick="btnkillprocess_Click" />
</asp:Content>
