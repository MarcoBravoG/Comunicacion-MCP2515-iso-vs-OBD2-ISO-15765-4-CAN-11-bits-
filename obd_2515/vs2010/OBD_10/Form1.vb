Imports System.Threading
Public Class Form1
    Dim inPort, buf As String
    Dim tx(2), rx(8) As Byte

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If sp1.IsOpen Then sp1.Close()
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetPorts()
    End Sub
    Sub GetPorts()
        For Each sp As String In My.Computer.Ports.SerialPortNames ' Show all available COM ports.
            cmb1.Items.Add(sp)
        Next
    End Sub
    Private Sub cmb1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb1.SelectedIndexChanged
        On Error Resume Next
        sp1.PortName = cmb1.SelectedItem
        sp1.BaudRate = 9600
        sp1.DataBits = 8
        sp1.Open()
        Label2.Text = "Port Open"
    End Sub

    Private Sub rtb()
        Dim h As String
        inPort = ""

        For i = 0 To 7
            If rx(i) < 16 Then h = "0" & Hex(rx(i)) Else h = Hex(rx(i))
            inPort = inPort & " " & h
        Next
        inPort = inPort & vbCr

        buf = 0
        buf = rtb1.Text + inPort
        rtb1.Text = buf
    End Sub
    
    Private Sub reply()

        For i = 0 To 100
            Thread.Sleep(10)
            If sp1.BytesToRead > 7 Then
                sp1.Read(rx, 0, sp1.BytesToRead)
                Exit Sub
            End If
        Next
    End Sub
    
    Private Sub Button01_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button01.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = 1
        sp1.Write(tx, 0, 2)
        reply()
        Text01.Text = Hex(rx(3)) & " " & Hex(rx(4)) & " " & Hex(rx(5)) & " " & Hex(rx(6)) & " " & Hex(rx(7))
        rtb()
    End Sub
    Private Sub Button04_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button04.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = 4
        sp1.Write(tx, 0, 2)
        reply()
        Text04.Text = FormatNumber(rx(3) / 2.55, 1) & " %"
        rtb()
    End Sub
    Private Sub Button05_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button05.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = 5
        sp1.Write(tx, 0, 2)
        reply()
        Text05.Text = rx(3) - 40 & " °C"
        rtb()
    End Sub
    Private Sub Button06_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button06.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = 6
        sp1.Write(tx, 0, 2)
        reply()
        Text06.Text = FormatNumber(rx(3) / 1.28 - 100, 1) & " %"
        rtb()
    End Sub

    Private Sub Button07_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button07.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = 7
        sp1.Write(tx, 0, 2)
        reply()
        Text07.Text = rx(3) / 1.28 - 100 & " %"
        rtb()
    End Sub

    Private Sub Button0B_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button0B.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &HB
        sp1.Write(tx, 0, 2)
        reply()
        Text0B.Text = rx(3) & " kPa"
        rtb()
    End Sub

    Private Sub Button0C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button0C.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &HC
        sp1.Write(tx, 0, 2)
        reply()
        Text0C.Text = (rx(3) * 256 + rx(4)) / 4 & " RPM"
        rtb()
    End Sub

    Private Sub Button0D_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button0D.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &HD
        sp1.Write(tx, 0, 2)
        reply()
        Text0D.Text = rx(3) & " km/h"
        rtb()
    End Sub
    Private Sub Button0E_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button0E.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &HE
        sp1.Write(tx, 0, 2)
        reply()
        Text0E.Text = rx(3) / 2 - 64 & " °"
        rtb()
    End Sub

    Private Sub Button0F_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button0F.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &HF
        sp1.Write(tx, 0, 2)
        reply()
        Text0F.Text = rx(3) - 40 & " °C"
        rtb()
    End Sub

    Private Sub Button11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button11.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &H11
        sp1.Write(tx, 0, 2)
        reply()
        Text11.Text = FormatNumber(rx(3) / 2.55, 1) & " %"
        rtb()
    End Sub

    Private Sub Button13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button13.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &H13
        sp1.Write(tx, 0, 2)
        reply()
        Text13.Text = rx(3) & " " & rx(4)
        rtb()
    End Sub

    Private Sub Button14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button14.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &H14
        sp1.Write(tx, 0, 2)
        reply()
        Text14.Text = Format(rx(3) / 200, "00.000") & " V,  " & rx(4) * 100 / 128 - 100 & " %"
        rtb()
    End Sub

    Private Sub Button15_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button15.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &H15
        sp1.Write(tx, 0, 2)
        reply()
        Text15.Text = Format(rx(3) / 200, "00.000") & " V,  " & rx(4) * 100 / 128 - 100 & " %"
        rtb()
    End Sub

    Private Sub Button1C_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1C.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &H1C
        sp1.Write(tx, 0, 2)
        reply()
        Text1C.Text = rx(3) & " " & rx(4)
        rtb()
    End Sub

    Private Sub Button20_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button20.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &H20
        sp1.Write(tx, 0, 2)
        reply()
        Text20.Text = Hex(rx(3)) & " " & Hex(rx(4)) & " " & Hex(rx(5)) & " " & Hex(rx(6)) & " " & Hex(rx(7))
        rtb()
    End Sub

    Private Sub Button21_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button21.Click
        'On Error Resume Next
        tx(0) = 1
        tx(1) = &H21
        sp1.Write(tx, 0, 2)
        reply()
        Text21.Text = rx(3) * 256 + rx(4) & " km"
        rtb()
    End Sub

    Private Sub ButtonPID_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPID.Click
        'On Error Resume Next
        tx(0) = Convert.ToInt16(Mid(TextPID.Text, 1, 2), 16) 'convert hex to int
        tx(1) = Convert.ToInt16(Mid(TextPID.Text, 3, 2), 16) 'convert hex to int
        sp1.Write(tx, 0, 2)
        reply()

        rtb()
    End Sub
    Private Sub ButtonClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
        rtb1.Text = ""
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        rtb1.SaveFile("obd2_2515.txt")
    End Sub


End Class
