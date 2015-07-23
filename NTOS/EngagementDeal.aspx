<%@ Page Title="Engagement Deal" MasterPageFile="~/Engagement.Master" Language="C#" AutoEventWireup="true" CodeBehind="EngagementDeal.aspx.cs" Inherits="NTOS.EngagementDeal" %>

<%@ Register Src="~/EngagementDeal.ascx" TagPrefix="ucdeal1" TagName="EngagementDeal1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .percentage, .decimal17, .dollor17 {
            width: auto;
        }
      
    .unwatermarked { 
       
        width:83px;
 }

     .watermarked { 
       
        width:85px;
        padding:2px 0 0 2px;
       
}
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--  <script type="text/javascript">
        $(document).load = function () {

            calexit();
        };
        function calexit() {
            debugger;
            if ($('#hdn_modify_status').val() == 1) {
                $(function () {
                    $("#dialog-exit").dialog({
                        resizable: false, width: 250, height: 150, modal: true, hide: 'fade', show: 'fade', buttons:
                            {
                                "Yes": function () {
                                    $('#dialog-exit').dialog("close");
                                    window.close();
                                },
                                "No": function () {
                                    $('#dialog-exit').dialog("close");
                                }
                            }
                    });

                });
            }
            else {
                window.close();
            }
            return false;
        }
        $("#Form1").one("change", ":input", function () {
            debugger;
            $('#hdn_modify_status').val("1");
        });
    </script>--%>
    <asp:HiddenField ID="hdn_modify_status" runat="server" Value="0" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div align="center"> <asp:Label ID="lbl_deal" runat="server" Text="Please Create Engagement First" ForeColor="Red"></asp:Label></div>  
            <div runat="server" id="div_deal" visible="false" style="padding-left:50px;align-self:flex-start; margin-top:-20px;">
                <table cellspacing="0" cellpadding="0" style="padding: 0px 0px 0px 0px;">
                <tr><td align="right" style="padding: 0px 70px 0px 0px;">
                <asp:LinkButton ID="lnknewdealtemplate" style="text-decoration:underline;" runat="server" Text="Add Template" OnClick="lnknewdealtemplate_Click" OnClientClick="return confirm('Do you want to create new template?');" CausesValidation="false"></asp:LinkButton>
                </td></tr>
                <tr><td style="padding: 0px 0px 0px 0px;">
                <ucdeal1:EngagementDeal1 runat="server" ID="EngagementDeal1"  />
                </td></tr>
                </table>
            </div>

            <asp:HiddenField ID="hdn_engagementid" runat="server" />
            <asp:HiddenField ID="hdn_showid" runat="server" />
            <asp:HiddenField ID="hdn_dealid" runat="server" />
            <asp:HiddenField ID="hdn_schedulecount" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
