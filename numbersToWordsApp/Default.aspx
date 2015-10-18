<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="numbersToWordsApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="numbersToWordsForm" runat="server">
    <div>
        <asp:TextBox ID="numberInput" runat="server"></asp:TextBox>
        <asp:Button ID="submitNumber" runat="server" Text="Convert to Words" OnClick="submitNumber_Click" />
    </div>
    <div>
        <asp:Label ID="numberInWords" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
