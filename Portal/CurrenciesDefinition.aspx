<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="CurrenciesDefinition.aspx.cs" Inherits="Portal.CurrenciesDefinition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="SPinputsInput">
            <table id="tblin" class="inputFields" width="100%"  rules="rows" align="center" style="font-family:Tahoma;font-size:11px;font-weight:normal">
                <tr>
                       <td width="24%">
                       <label class="checkbox" for="txtInputCurrencyNumericCode">*کد ارز</label>
                   </td >
                      <td width="24%" class="isMandetory">
                          <asp:TextBox class="numeric" ID="txtInputCurrencyNumericCode" runat="server"></asp:TextBox>
                      </td >
                       <td width="24%">
                        <label class="checkbox" for="txtExchangeRate">*نرخ ارز</label>
                    </td>
                    <td width="24%" class="isMandetory">
                                <asp:TextBox class="numeric" ID="txtExchangeRate" runat="server"></asp:TextBox>
                    </td>
            
                </tr>
          
                <tr>
                    <td width="24%">
                        <label class="checkbox" for="txtEntity">*کشور</label>
                    </td>
                    <td width="24%" class="isMandetory">
                         <asp:TextBox ID="txtEntity" runat="server"></asp:TextBox>
                    </td>
                   <td width="24%"><label class="checkbox" for="txtCurrencyType">*عنوان ارز</label></td >
                      <td width="24%" class="isMandetory">
                          <asp:TextBox ID="txtCurrencyType" runat="server"></asp:TextBox>
                      </td >
                </tr>
                 <tr>
                    <td width="24%">
                        <label class="checkbox" for="txtAlphabeticCode">*کدالفبایی کشور</label>
                    </td>
                    <td width="24%" class="isMandetory">
                                <asp:TextBox ID="txtAlphabeticCode" runat="server"></asp:TextBox>
                    </td>
                
                </tr>
       
            </table>

            <br />
            <div id="SaveBtn" class="btnwrapper center" >
                <a ID="btnCancel" runat="server" class="butt enseraf" title="انصراف">انصراف</a>
                &nbsp;
                <a ID="btnConfirm" runat="server" class="butt sabt sendToServer" title="ثبت">ثبت</a>
            </div>
            <br />
        </div>
    </form>
</body>
</html>
