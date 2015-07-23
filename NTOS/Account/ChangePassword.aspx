<%@ Page Title="Change Password" Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="NTOS.Account.ChangePassword" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/content/Site.css" rel="stylesheet" type="text/css" />
    <webopt:BundleReference ID="BundleReference1" runat="server" Path="~/Content/themes/base/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width" />
</head>
<body>
    <form id="form1" runat="server">
         <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#f2f2f2"
            height="100%">
            <tr>
                <td valign="top" height="65">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background-image: url(../images/bg_header.gif); height: 65px; background-repeat: repeat-x;">
                        <tr>
                            <td width="30%" rowspan="2">
                                <%--<a href="../Diary.aspx" title="NETworks Tours Online System">--%>
                                    <img src="../images/logo_project.png" alt="NETworks Tours Online System"
                                       border="0" /><%--</a>--%>
                            </td>
                            <td width="35%" rowspan="2" align="center" valign="middle" style="color: #323a3f; font-size: 25px; font-weight: bold;">NETworks Tours Online System
                            </td>
                            <td width="35%" valign="middle" align="right" style="padding: 2px 20px 0 0; font-size: 12px; color: #666666; font-weight: bold;">
                                
                            </td>
                        </tr>
                        <tr>
                            <td width="35%" valign="middle" align="right" style="padding: 2px 40px 0 0; font-size: 12px; color: #666666; font-weight: bold;">
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" height="30" style="background-color:#3a4f63;">
                </td>
            </tr>
            <tr>
                <td>
                    </td>
            </tr>
            <tr>
                <td style="padding:0 0 0 50px">
                   <h2>
                    Change Password
                  </h2>
                    <p>
                        Use the form below to change your password.&nbsp;
                        New passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
                    </p>
                    <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/Diary.aspx" EnableViewState="false" RenderOuterTable="false" 
         SuccessPageUrl="ChangePasswordSuccess.aspx" OnChangingPassword="ChangeUserPassword_ChangingPassword">
        <ChangePasswordTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="ChangeUserPasswordValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="changePassword">
                    <legend>Account Information</legend>
                    <p>
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required." 
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                             CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required." 
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                             ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"/>
                    <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Change Password" 
                         ValidationGroup="ChangeUserPasswordValidationGroup"/>
                </p>
            </div>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
                 </td>
            </tr>
            <tr valign="bottom" height="72">
                <td height="18" style="background-image: url(../images/bg_footer.gif); background-repeat: repeat-x; height: 71px; text-align: center; color: #ffffff; font-size: 11px; padding: 0 0 2px 0;">Copyright &copy; <%: DateTime.Now.Year %>, NETworks Presentations, Inc.</td>
            </tr>
        </table>
  </form>
</body>
</html>
