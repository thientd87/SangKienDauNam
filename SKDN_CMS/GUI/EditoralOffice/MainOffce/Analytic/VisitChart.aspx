<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitChart.aspx.cs" Inherits="Nextcom.Analytics.VisitChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Thống kê lượt truy cập</title>
</head>
<body style="margin:0px">
    <DCWC:Chart ID="Chart1" runat="server" ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)"
		Width="990px" Height="350px" BorderLineColor="26, 59, 105" Palette="Dundas" BorderLineStyle="Solid"
		BackGradientEndColor="White" BackGradientType="TopBottom" BorderLineWidth="2"
		BackColor="#D3DFF0">
		<Legends>
			<DCWC:Legend BackColor="Transparent" Docking="Top" Title="Khách" TitleFont="Arial, 14px"
				AutoFitText="False" Name="Default">				    
			</DCWC:Legend>
		</Legends>
		<BorderSkin SkinStyle="Emboss"></BorderSkin>
		<Series>
			<DCWC:Series MarkerStyle="Circle" ShowLabelAsValue="True" ChartArea="Chart Area 1"
				XValueType="DateTime" ChartType="Spline" Name="Lượt khách" BorderColor="180, 26, 59, 105">
			</DCWC:Series>
            <DCWC:Series MarkerStyle="Square" ShowLabelAsValue="True" ChartArea="Chart Area 1"
				XValueType="DateTime" ChartType="Spline" Name="Lượt xem" BorderColor="180, 26, 59, 105">
			</DCWC:Series>
		</Series>
		<ChartAreas>
			<DCWC:ChartArea Name="Chart Area 1" BorderColor="64, 64, 64, 64" BackGradientEndColor="White"
				BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientType="TopBottom">
				<AxisX LineColor="64, 64, 64, 64" LabelsAutoFit="False" IntervalAutoMode="VariableCount">
					<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
					<LabelStyle Font="Trebuchet MS, 11px" Format="d"></LabelStyle>
				</AxisX>
				<AxisY LineColor="64, 64, 64, 64" LabelsAutoFit="False">
					<LabelStyle Font="Trebuchet MS, 11px" Format="d"></LabelStyle>
					<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
				</AxisY>
			</DCWC:ChartArea>
		</ChartAreas>
	</DCWC:Chart>
    

</body>
</html>
