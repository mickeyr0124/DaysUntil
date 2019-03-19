Imports System.Xml
Imports System.Data
Imports System.IO
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.Font = New Font("ComicSans", 20, FontStyle.Regular)
        Me.DataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Red


        Dim filePath As String = Environ("USERPROFILE") & "\Google Drive\"
        If Directory.Exists(filePath) Then
            'Program may proceed
            Dim xmlFile As XmlReader
            xmlFile = XmlReader.Create(filePath & "\Travel\DaysUntil.xml", New XmlReaderSettings())
            Dim ds As New DataSet
            ds.ReadXml(xmlFile)
            ds.Tables(0).Columns.Add("Days Until")
            For Each r As DataRow In ds.Tables(0).Rows
                Dim d As Date = r("Date")
                Dim n As Integer = d.Subtract(Today).Days
                r("Days Until") = n
            Next
            DataGridView1.DataSource = ds.Tables(0)
            For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
                If Me.DataGridView1.Rows(i).Cells("Days Until").Value < 30 Then
                    For j = 0 To Me.DataGridView1.ColumnCount - 1
                        Me.DataGridView1.Rows(i).Cells(j).Style.ForeColor = Color.Red
                    Next
                End If
            Next
        Else
            MsgBox("You must install Dropbox on this computer in order for this application to function properly.")
            End
        End If

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Me.DataGridView1.ClearSelection()
    End Sub
End Class
