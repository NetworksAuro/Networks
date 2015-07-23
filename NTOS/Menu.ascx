<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="NTOS.Menu" %>
<asp:UpdatePanel ID="upl" runat="server">
    <ContentTemplate>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr class="nav">
                <td>

                    <ul class="dropdown" style="line-height:10px">
                        <li><a visible="false" id="a_diary" runat="server"  href="/Diary.aspx">Diary</a>
                        </li>
                        <li>
                           <%-- <a id="a_engt" runat="server" href="Search.aspx?title='Engagement Schedule' &engmt_id=0 &type=1">Engagements</a>--%>
                              <asp:HyperLink ID="h_engt" runat="server"   NavigateUrl= "Search.aspx?title=EngagementSchedule&engmt_id=0&type=1">Engagements</asp:HyperLink>
                        </li>

                        <li>
                                 <asp:HyperLink ID="h_presenter" runat="server"  onclick="return checkstatus('Presenter');" NavigateUrl="SearchAll.aspx?title=Presenter&type=1">Presenter</asp:HyperLink>
                           <%-- <a id="a_presenter" runat="server" Onclick="return checkstatus('Presenter');"    href="/Presenter.aspx">Presenter</a>--%>
                        </li>
                        <li>
                             <asp:HyperLink ID="h_venue" runat="server" onclick="return checkstatus('Venue');" NavigateUrl="SearchAll.aspx?title=Venue&type=1">Venue</asp:HyperLink>
                           <%-- <a id="a_venue" runat="server" href="/Venue.aspx">Venues</a>--%>
                        </li>
                        <li>
                             <asp:HyperLink ID="h_show" runat="server" onclick="return checkstatus('Show');" NavigateUrl="SearchAll.aspx?title=Show&type=1">Show</asp:HyperLink>
                            <%--<a id="a_show" runat="server" href="/Show.aspx">Shows</a>--%>
                        </li>
                        <li>
                            <asp:HyperLink ID="h_conatacts" runat="server" onclick="return checkstatus('Personnel');" NavigateUrl="SearchAll.aspx?title=Personnel&type=1">Contacts</asp:HyperLink>
                          <%--  <a id="a_conatacts" runat="server" href="/Personal.aspx">Contacts</a>--%>
                        </li>
                        <li><a href="#" id="a_reports" runat="server">Reports</a>
                            <ul>

                                <li><a href="../Reports/SettelementReport.aspx">Settlement </a></li>
                                <li><a href="../Reports/BreakEvenReport.aspx">Break Even </a></li>
                                <li><a href="../Reports/ProFormaReport.aspx">Pro-Forma </a></li>
                                <li><a href="../Reports/MarketHistoryReport.aspx">Market History </a></li>
                                <li><a href="../Reports/SettlemetCvrSheetReport.aspx">Coversheet </a></li>
                                <li><a href="../Reports/RouteReport.aspx">Route </a></li>
                                <li><a href="#">On Demand </a></li>
                            </ul>
                        </li>
                        <%-- <li runat="server" id="mnew">
                            <a href="#">New</a>
                            <ul>
                                <li>
                                    <a href="/Metro.aspx">Metro</a>
                                </li>
                                <li>
                                    <a class="menulink" href="/Venue.aspx">Venue</a>
                                </li>
                                <li>
                                    <a href="/Show.aspx">Show</a>
                                </li>
                                <li>
                                    <a href="/Presenter.aspx">Presenter</a>
                                </li>
                                <li>
                                    <a href="/Personal.aspx">Personnel</a>
                                </li>
                                <li>
                                    <a href="/EngagementSchedule.aspx">Engagement</a>
                                </li>
                            </ul>
                        </li>--%>
                        <%-- <li><a href="/Search.aspx">Search</a>
                        </li>
                        <li runat="server" id="mtemplate"><a href="#">Templates</a>
                            <ul>
                                <li>
                                    <a href="/Deal.aspx">Deal&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                </li>
                            </ul>
                        </li>--%>

                        <li runat="server" id="madmin"><a runat="server" id="a_admin" href="#">Admin</a>
                            <ul>

                                <li><a href="/Country.aspx">Countries</a></li>
                                <li><a href="/State.aspx">States</a></li>
                                <li><a href="/City.aspx">Cities</a></li>
                                <li><a href="/Title.aspx">Titles</a></li>
                                <li><a href="/Timezone.aspx">Timezones</a></li>
                                <li><a href="/LookUpList.aspx">Lookup Lists</a></li>
                                <li><a href="/AdminStatus.aspx">Show Status</a></li>
                                <li runat="server" id="mtemplate"><a href="#">Templates » </a>
                                    <ul>
                                        <li><a href="/Deal.aspx">Deal</a></li>

                                    </ul>
                                </li>
                                <li id="liusers" runat="server"><a href="/Users.aspx">Manage Users</a></li>

                                 <li id="liexcel" runat="server"><a href="ExcelImport.aspx">Excel Import</a></li>
                            </ul>
                        </li>
                    </ul>

                </td>
                <td valign="middle" align="right" style="padding-right: 25px">Recent Navigations :&nbsp;
                                <asp:DropDownList ID="ddlHistory" Width ="150px"  ToolTip="Last 10 links" AutoPostBack="true" OnSelectedIndexChanged="ddlHistory_SelectedIndexChanged"
                                    runat="server">
                                </asp:DropDownList>

                    </td>
              <td valign="bottom" align="center">
                       <a href="../Images/NTOS-UserManual.pdf" style="border: 0;padding-right: 15px;" target="_blank">
                                    <img src="../Images/help.png" alt="Help"
                                        title="Click to view User Manual" style="border: 0; padding-left: 3px" width="16px" height="16px" /></a>
                </td>
               
              
            </tr>
        </table>

    </ContentTemplate>
</asp:UpdatePanel>
