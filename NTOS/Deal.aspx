<%@ Page Title="Deal" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Deal.aspx.cs" Inherits="NTOS.Deal" %>
<%@ Register Src="~/EngagementDeal.ascx" TagPrefix="ucDeal" TagName="EngagementDeal" %>
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
    <style type="text/css">
    a.dp-choose-date {
        float: left;
        height: 30px;
        width: 30px;
        padding: 0;
        margin: 5px 3px 0;
        display: block;
        text-indent: -2000px;
        overflow: hidden;
        background: url('/images/calendar.png') no-repeat;
    }

        a.dp-choose-date.dp-disabled {
            background-position: 0 -20px;
            cursor: default;
        }

    input.dp-applied {
        width: 140px;
        float: left;
    }
</style>
      <table  cellpadding="0" cellspacing="0"  align="left" >
        
        <tr>
            <td class="contentpadding">
               <table  cellpadding="0" cellspacing="0"  align="left" >
               <%-- <tr><td align="center" valign="middle"  height="33">
                    <table border="0" width="100%">
                        <tr>
                        <td align="left" style="width:3%" class="padding_left10">
                        <a style="visibility:hidden;" href="Deal.aspx" target="_self" runat="server" id="link_new">New</a></td>
                        <td style="width:97%" align="center"><asp:Label ID="lbl_header" runat="server" Text=""></asp:Label></td>
                        </tr></table>
                    </td>
                </tr>--%>
                <tr><td align="center"><asp:Label ID="lblerrmsg" runat="server" Font-Bold="true"/>&nbsp;</td></tr>
                <tr>
                   
                    <td >
                        <div id="div_dealtemp" runat="server">
                            <asp:Panel ID="pnldealenable" runat="server">
                        <ucDeal:EngagementDeal runat="server" id="EngagementDeal" />
                                </asp:Panel>
                            </div>
                        </td>
                </tr>
                <tr><td>&nbsp;</td></tr>
               </table>
           </td>
        </tr>
    </table>
</asp:Content>
