<%@ control language="C#" autoeventwireup="true" inherits="UserControls_Remarks, App_Web_remarks.ascx.6c0a2e64" %>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
                    <!--asp:BoundField DataField="Content" HeaderText="Remarks">
                        <ItemStyle CssClass="ms-formbody" />
                        <HeaderStyle CssClass="ms-headertable" Width="30%" ForeColor="White"/>
                    </asp:BoundField-->
            <asp:GridView ID="gridRemarks" BorderColor="#92AEC9" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#FFFFFF" GridLines="Both" AllowPaging="True" Width="100%" TabIndex="49">
                <Columns>
                    <asp:BoundField DataField="CreateAt" HeaderText="Date / Time" DataFormatString="{0:HH:mm MMM dd, yyyy}">
                        <HeaderStyle CssClass="ms-headertable" HorizontalAlign="Center" Width="20%" ForeColor="White" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UserName" HeaderText="User" >
                        <HeaderStyle CssClass="ms-headertable" HorizontalAlign="Center" Width="20%" ForeColor="White"/>
                    </asp:BoundField>                    
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <TEXTAREA ROWS="4" COLS="30" wrap="soft" readonly="true"><%# DataBinder.Eval(Container.DataItem, "Content") %>
                            </TEXTAREA>
                        </ItemTemplate>
                        <HeaderStyle CssClass="ms-headertable" Width="30%" ForeColor="White"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Attachments">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "GetLinks") %>
                        </ItemTemplate>
                         <ItemStyle CssClass="ms-formlabel" />                         
                         <HeaderStyle CssClass="ms-headertable" HorizontalAlign="Center" Width="30%" ForeColor="White"/>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />

                <HeaderStyle CssClass="GridHeaderStyle"/>
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td id="hid">
            <table cellpadding="2" cellspacing="0" border="1" width="100%" style="border-collapse: collapse" id="tblRemarks" runat="server">
                <tr class="ms-graybground">
                    <td colspan=4 class="ms-headertable" >
                        Add your remarks:</td>
                </tr>
                <tr class="ms-lightgraybground">                                
                    <td colspan=2 style="width: 50%">
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="5" Width="280" CssClass="ms-textarea" TabIndex="50"></asp:TextBox></td>
                    <td colspan=2 valign="top" style="width: 50%" align=left>
                        &nbsp;<input id="objUploadFile" class="ms-filebroswer" style="width: 80%" type="file" runat="server" tabindex="51" />
                        <asp:Button ID="btnUpLoad" runat="server" CssClass="ms-filebroswer" OnClick="btnUpLoad_Click" Text="Upload" TabIndex="52" CausesValidation="False" /><br /> 
                        <table cellpadding="0" cellspacing="4" border="0" id="tblUploadedFiles" runat="server" width="100%">
                            <tr>
                            <td><asp:GridView ID="grdDownloadLinks" runat="server" GridLines="Horizontal"
                                AutoGenerateColumns="False" Width="100%" BorderWidth="0px" ShowHeader="false" CellPadding="0" TabIndex="53" >
                                        <Columns>
                                             <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="linkDownload" Text='<%# DataBinder.Eval(Container.DataItem, "FileName") %>'                                                         
                                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"Link")%>' Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                                 <ItemStyle CssClass="ms-formlabel" />
                                            </asp:TemplateField>
                                             <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="linkDelete" Text="Delete" CommandName="Remove"
                                                    EnableViewState="true" CausesValidation="false" OnClientClick="return confirm('Do you really want to remove the attachment file?');"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AttachmentId") %>'                                                    
                                                    OnCommand="lbt_DelCommand"                                                    
                                                    ></asp:LinkButton>
                                                </ItemTemplate>
                                                 <ItemStyle CssClass="ms-formlabel" />
                                            </asp:TemplateField>
                                        </Columns>
                                        </asp:GridView></td>                                    
                            </tr>
                        </table>
                        </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
</table>