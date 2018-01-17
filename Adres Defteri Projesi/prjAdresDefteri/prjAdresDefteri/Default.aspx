<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="prjAdresDefteri.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       
       
        <div>
           <asp:TextBox ID="searchBox" runat="server"></asp:TextBox>
           <asp:Button ID="searchButton" runat="server" Text="Ara" OnClick="searchButton_Click" /> 
            <asp:Button ID="reset" runat="server" Text="Temizle" OnClick="reset_Click" />
    <br />
    
    <br />
    <br />
        </div>
        <div>
          
            <asp:GridView OnRowCommand="gwAdresDefteri_RowCommand" OnRowDeleting="gwAdresDefteri_RowDeleting" OnRowEditing="gwAdresDefteri_RowEditing" OnRowUpdating="gwAdresDefteri_RowUpdating" OnRowCancelingEdit="gwAdresDefteri_RowCancelingEdit" ID="gwAdresDefteri" runat="server" ShowHeaderWhenEmpty="True" CellPadding="3"   AutoGenerateColumns="False" ShowFooter="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" DataKeyNames="AdresID">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />

                <Columns>

                   
                    <asp:TemplateField HeaderText="Ad">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Ad") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAd" Text='<%# Eval("Ad") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAdFooter" Text='<%# Eval("Ad") %>' runat="server"> </asp:TextBox>
                         </FooterTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Soyad">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Soyad") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSoyad" Text='<%# Eval("Soyad") %>' runat="server"> </asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtSoyadFooter" Text='<%# Eval("Soyad") %>' runat="server"> </asp:TextBox>
                         </FooterTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="TelefonNo">
                        <ItemTemplate>  
                            <asp:Label Text='<%# Eval("TelefonNo") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTelefonNo" Text='<%# Eval("TelefonNo") %>' runat="server"> </asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTelefonNoFooter" Text='<%# Eval("TelefonNo") %>' runat="server"> </asp:TextBox>
                         </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EMail">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("EMail") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEMail" Text='<%# Eval("EMail") %>' runat="server"> </asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEMailFooter" Text='<%# Eval("EMail") %>' runat="server"> </asp:TextBox>
                         </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Düzenle" Width="20px" Height="20px"/>
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Sil" Width="20px" Height="20px"/>
                             
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Güncelle" Width="20px" Height="20px"/>
                             <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Vazgeç" Width="20px" Height="20px"/>
                             
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ImageUrl="~/Images/add.png" runat="server" CommandName="AddNew" ToolTip="Ekle" Width="20px" Height="20px"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label runat="server" Text="" ForeColor="Green" ID="txtBasarili"></asp:Label>
            <br />
            <asp:Label runat="server" Text="" ForeColor="Red" ID="txtHata"></asp:Label>
        </div>
    </form>
</body>
</html>
