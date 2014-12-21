<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Controller.aspx.cs" Inherits="SRCalendarSynchronizationWebControl.Controller" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SR Calendar Synchronizer Controller</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center><h2>Synchronizer Control</h2></center>
        <asp:Panel ID="pnlSynchronizationControls" GroupingText="Synchronization Controls" runat="server">
        <table>
            <tr>
                <td>Status:</td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Button CausesValidation="false" ID="btnEnable" runat="server" 
                        Text="Enable" onclick="btnEnable_Click" />
                </td>
                <td>
                    <asp:Button ID="btnDisable" CausesValidation="false" runat="server" 
                        Text="Disable" onclick="btnDisable_Click" />
                </td>
            </tr>
            <tr>
                <td>Synchronization periodicity (in minutes)</td>
                <td>
                    <asp:TextBox ReadOnly="false" ID="tbSynchronizationPeriodicity" runat="server"></asp:TextBox>
                    <asp:RangeValidator MinimumValue="1" MaximumValue="99999999" ControlToValidate="tbSynchronizationPeriodicity" ID="rvSynchronizationPeriodicity" runat="server" ErrorMessage="Value must be > 0"></asp:RangeValidator>
                </td>
                <td colspan="2">
                    <asp:Button ID="btnUpdateSynchronizationPeriodicity" runat="server" 
                        Text="Update Periodicity" onclick="btnUpdateSynchronizationPeriodicity_Click" />
                </td>
            </tr>
        </table>
        </asp:Panel>  
        <asp:Panel ID="pnlSecurityControls" GroupingText="Security Controls" runat="server">
            <table>
                <tr>
                    <td>User:
                    </td>
                    <td>
                        <asp:TextBox ID="tbId" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="tbId" ID="rfvId" runat="server" ErrorMessage="Field required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Password:
                    </td>
                    <td>
                        <asp:TextBox TextMode="Password" ID="tbPassword" runat="server"></asp:TextBox>
                        <asp:Label ID="lblPasswordMustNotBeBlank" ForeColor="Red" Visible="false" runat="server" Text="Must not be blank"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnUpdateCredentials" runat="server" Text="Update Credentials" 
                            onclick="btnUpdateCredentials_Click" />
                    </td>
                </tr>                
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
