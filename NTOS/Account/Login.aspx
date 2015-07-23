<%@ Page Title="Log in" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NTOS.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Log In</title>
    <link href="~/content/Site.css" rel="stylesheet" type="text/css" />
    <webopt:BundleReference ID="BundleReference1" runat="server" Path="~/Content/themes/base/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <table  width="95%" style="margin: 0px 30px 0px 30px;background-color: #ffffff; align-self: center; " cellspacing="0" cellpadding="0">
                <tr style="background-color :#E9F5FC">
                    <td valign="top" height="30px">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="height: 30px; background-repeat: repeat-x;">
                            <tr>
                                <td width="30%" rowspan="2" valign="top">
                                    <%--<a href="Diary.aspx" title="NETworks Tours Online System">--%>
                                    <img src="../images/logo_project.jpg" alt="NETworks Tours Online System"
                                        border="0" title="NETworks Tours Online System" /><%--</a>--%>
                                </td>
                                <td width="35%" rowspan="2" align="center" valign="middle" style="color: #323a3f; font-size: 25px; font-weight: bold;"></td>
                                <td width="35%" valign="middle" align="right" style="padding: 2px 20px 0 0; font-size: 12px; color: #666666; font-weight: bold;"></td>
                            </tr>
                            <tr>
                                <td width="35%" valign="middle" align="right" style="padding: 2px 40px 0 0; font-size: 12px; color: #666666; font-weight: bold;"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" height="30" style="background-color: #ffffff;"></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="padding: 0 0 0 50px">
                        <h2>Log In
                        </h2>
                        <p>
                            Please enter your NetworksTours user name and password.
                        <%--<asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink> if you don't have an account.--%>
                        </p>
                        <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" OnAuthenticate="LoginUser_Authenticate" DestinationPageUrl="~/Diary.aspx">
                            <LayoutTemplate>
                                <span class="failureNotification">
                                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                </span>
                                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                                    ValidationGroup="LoginUserValidationGroup" />
                                <div class="accountInfo">
                                    <fieldset class="login">
                                        <legend>Account Information</legend>
                                        <p>
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                            <%--<ajaxToolkit:TextBoxWatermarkExtender ID="wmUserName" runat="server" TargetControlID="UserName" WatermarkText="Enter your 'networkstours' login id"></ajaxToolkit:TextBoxWatermarkExtender>--%>
                                        </p>
                                        <p>
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                            <asp:CheckBox ID="RememberMe" runat="server" />
                                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>

                                        </p>
                                        <p class="submitButton">
                                    </fieldset>

                                    <asp:Button ID="LoginButton" CssClass="butt" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" />
                                    </p>
                                </div>
                            </LayoutTemplate>
                        </asp:Login>
                    </td>
                </tr>
                 <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
            </table>
        </div>
    </form>
    <div>
        <table  width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr valign="bottom">
                <td height="18px" style="margin: 0px 10px 0px 10px; background-repeat: repeat-x; height: 18px; text-align: center; font-size: 11px; padding: 0 0 2px 0;">
                    <div style=" width: 100%">&nbsp; Copyright &copy; <%: DateTime.Now.Year %>, NETworks Presentations, Inc.</div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
