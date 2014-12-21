<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SRCalendarSynchronizationWebControl._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SR Calendar Synchronizer Controller</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel DefaultButton="btnSubmit" ID="Panel1" runat="server">
        <center>
            <h2>
                Login</h2>
        </center>
        <table cellpadding="4">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Id:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbId" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tbId" ID="rfvId" runat="server" ErrorMessage="Field required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox TextMode="Password" ID="tbPassword" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="tbPassword" ID="rfvPassword" runat="server"
                        ErrorMessage="Field required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /></td>
            </tr>
        </table>
        <br />
        
        <br />
        <center><asp:Label ID="lblError" Visible="false" ForeColor="red" runat="server" Text=""></asp:Label></center>
    </asp:Panel>
    </form>
</body>
</html>
