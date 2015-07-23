<%@ Page Title="Manage Lookup List" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" EnableTheming="false" CodeBehind="LookUpList.aspx.cs"
    Inherits="NTOS.LookUpList" %>

<%@ Register Src="~/Lookuplist.ascx" TagPrefix="uc1" TagName="Lookuplist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        div .chosen-container {
            min-width: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <table width="800px" cellpadding="0" cellspacing="0" align="left">

                <tr>
                    <td class="contentpadding">
                        <table width="800px" cellpadding="0" cellspacing="0" align="left">
                            <%--  <tr>
                                <td align="center" valign="middle" class="bg_tbl_header txt_contentheader" height="33"
                                    colspan="8">Lookup List
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="padding_leftright20" align="center">
                                    <ajaxToolkit:Accordion ID="acc_lst" runat="server" CssClass="accordion" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Panes>
                                            <ajaxToolkit:AccordionPane ID="scheduletype" runat="server">
                                                <Header>Engagement Schedule Type</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_scheduletype" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="pricescalestatus" runat="server">
                                                <Header>Engagement Pricescale Status</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_pricescalestatus" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>

                                            <ajaxToolkit:AccordionPane ID="contractstatus" runat="server">
                                                <Header>Engagement Contract Status</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_contractstatus" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>

                                            <ajaxToolkit:AccordionPane ID="offerstatus" runat="server">
                                                <Header>Engagement Offer Status</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_offerstatus" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>

                                            <ajaxToolkit:AccordionPane ID="expensestatus" runat="server">
                                                <Header>Engagement Expense Status</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_expensestatus" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>

                                            <ajaxToolkit:AccordionPane ID="memostatus" runat="server">
                                                <Header>Engagement Memo Status</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_memostatus" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>

                                            <ajaxToolkit:AccordionPane ID="engagementstatus" runat="server">
                                                <Header>Engagement Engagement Status</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_engagementstatus" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="apemployeetype" runat="server">
                                                <Header>Employee Type</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_employeetype" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="apemployeestatus" runat="server">
                                                <Header>Employee Status</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_employeestatus" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="apemployeestatusdealother" runat="server">
                                                <Header>Deal Others</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_Dealothers" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="ap_localOthers" runat="server">
                                                <Header>Local & Documented Expenses</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_LocalOthers" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="ap_DocumentOthers" runat="server" Visible="false">
                                                <Header>Document Expenses Others</Header>
                                                <Content>
                                                    <uc1:Lookuplist runat="server" ID="lst_DocumentOthers" />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                        </Panes>
                                    </ajaxToolkit:Accordion>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
