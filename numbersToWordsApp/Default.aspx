<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="numbersToWordsApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Numbers To Words</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <div class="panel panel-primary center-block">
            <div class="panel-heading">
                <h3 class="panel-title">Convert a Number into Words</h3>
            </div>
            <div class="panel-body">
                <form id="Form1" runat="server">
                <div class="input-group">
                    <asp:TextBox ID="numberInput" runat="server" class="form-control" placeholder="Enter a number to convert"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button ID="Button1" runat="server" Text="Convert to Words" OnClick="submitNumber_Click" class="btn btn-success"/>
                    </span>
                </div>
                <div class="text-center result-text form-control">
                    <asp:Label ID="numberInWords" runat="server" Text=""></asp:Label>
                </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
