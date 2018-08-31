Option Strict Off
Option Explicit On
Option Infer Off
Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports System.Text
Imports System.Net


Public Class Form5

    Private Property hash_hex As Object

    Function md5_hash(ByVal file_name As String) As Object
        Return hash_generator("md5", file_name)
    End Function

    Function hash_generator(ByVal hash_type As String, ByVal file_name As String) As Object

        Dim hash As Object
        hash = MD5.Create

        Dim hashValue() As Byte

        Dim filestream As FileStream = File.OpenRead(file_name)
        filestream.Position = 0
        hashValue = hash.ComputeHash(filestream)
        Dim hash_hex As Object
        hash_hex = PrintByteArray(hashValue)

        filestream.Close()

        Return hash_hex
    End Function

    Public Function PrintByteArray(ByRef array() As Byte) As Object

        Dim hex_value As String = ""

        Dim i As Integer
        For i = 0 To array.Length - 1

            hex_value += array(i).ToString("x2")
        Next i

        Return hex_value.ToLower
    End Function



    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim path As String = OpenFileDialog1.FileName
            txtFilePath.Text = path

            Dim sample As String
            sample = md5_hash(path)
            txtHash.Text = md5_hash(path)

            Using f As System.IO.FileStream = System.IO.File.OpenRead("md5.txt")
                Using s As System.IO.StreamReader = New System.IO.StreamReader(f)
                    While Not s.EndOfStream
                        Dim line As String = s.ReadLine

                        If (line = sample) Then
                            lblResult.Text = "Infected!"
                            lblResult.ForeColor = Color.Red
                        Else
                            lblResult.Text = "Clean!"
                            lblResult.ForeColor = Color.Green
                        End If
                    End While
                End Using
            End Using
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not txtHash.Text = "" Then
            My.Computer.Clipboard.SetText(txtHash.Text)
            MsgBox("MD5 copier !")
        End If
    End Sub

    Private Sub txtHash_TextChanged(sender As Object, e As EventArgs) Handles txtHash.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txtHash.Clear()
        txtFilePath.Clear()
    End Sub

    Private Sub txtFilePath_TextChanged(sender As Object, e As EventArgs) Handles txtFilePath.TextChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim FILE_NAME As String = "md5.txt"

        If System.IO.File.Exists(FILE_NAME) = True Then
            Process.Start(FILE_NAME)
        Else
            MsgBox("File Does Not Exist")
        End If
    End Sub
End Class
