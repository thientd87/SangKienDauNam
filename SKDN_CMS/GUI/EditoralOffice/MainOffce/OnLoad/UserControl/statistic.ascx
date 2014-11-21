<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="statistic.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.OnLoad.UserControl.statistic" %>
<%@ Register TagPrefix="dcwc" Namespace="Dundas.Charting.WebControl" Assembly="DundasWebChart" %>
<style>
    .ContentBox
    {
        border:1px solid #b8c1ca;
	    border-top:none;
	    padding-left:8px;
	    visibility:visible;
    }
    .HeadBox
    {
        border:1px solid #b8c1ca;
	    background-color:#e5e5e5;
	    text-align:center;
	    font:12px Arial;
	    font-weight:bold;
	    padding-top:2px;
	    padding-bottom:2px;
	    padding-left:3px;
    }
    
</style>
<TABLE cellSpacing="0" cellPadding="5" width="100%" border="0">
    <tr>
        <td colspan="2" width="100%" class="HeadBox">
             Biểu đồ thống kê số lượng người truy cập
        </td>
    </tr>
	
	<TR>
		<TD align="center"  class="ContentBox">
			<DCWC:CHART id="Chart1" runat="server" Width="650px" Height="296px"  
				 BackGradientType="TopBottom" ImageUrl="~/images/ChartImages/ChartPic_#SEQ(300,3)"
				ImageType="Jpeg"   CssClass="DisplayNone" Palette="Dundas">
				<legends>
			        <dcwc:legend  LegendStyle="Table"  Alignment="Center" AutoFitText="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" ></dcwc:legend>
		        </legends>
				<ChartAreas>
					<dcwc:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderStyle="Solid" BackGradientEndColor="White"
						BackColor="OldLace" ShadowColor="Transparent" BackGradientType="TopBottom">
						<Area3DStyle YAngle="25" Perspective="9" Light="Realistic" XAngle="40" RightAngleAxes="False"
							WallWidth="3" Clustered="True"></Area3DStyle>
						<AxisY LineColor="64, 64, 64, 64">
							<LabelStyle Font="Trebuchet MS, 8.25pt"></LabelStyle>
							<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
						</AxisY>
						<AxisX LineColor="64, 64, 64, 64" labelsautofit="False">
							<labelstyle font="Trebuchet MS, 8.25pt" interval="1"></labelstyle>
					        <majorgrid interval="Auto" linecolor="64, 64, 64, 64"></majorgrid>
					        <majortickmark interval="1" enabled="False"></majortickmark>
						</AxisX>
					</dcwc:ChartArea>
				</ChartAreas>
			</DCWC:CHART>
		</TD>
	</TR>
</TABLE>