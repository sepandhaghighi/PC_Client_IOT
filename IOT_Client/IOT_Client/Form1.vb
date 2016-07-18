﻿Imports System.IO
Imports System.Text
Imports System.Net
Public Class Form1
    Dim user_address As String = "http://sepandhaghighi.github.io/elec_3/user.txt"
    Dim pass_address As String = "http://sepandhaghighi.github.io/elec_3/pass.txt"
    Dim access_address As String = "http://sepandhaghighi.github.io/elec_3/access.txt"
    Dim client As New WebClient

    Dim user_reader As StreamReader
    Dim pass_reader As StreamReader
    Dim access_reader As StreamReader
    Dim number_of_user As Integer
    Public Shared user_index As Integer = 1
    Dim access As String

    Public Sub read_data()
        On Error GoTo read_data_label
        If Len(TextBox1.Text) = 0 Or Len(TextBox2.Text) = 0 Then
            Label6.Visible = True
            Button1.Enabled = False
            Timer1.Enabled = True
        Else
            user_reader = New StreamReader(client.OpenRead(user_address))
            pass_reader = New StreamReader(client.OpenRead(pass_address))
            number_of_user = Int(user_reader.ReadLine())
            For i = 1 To number_of_user
                If TextBox1.Text = user_reader.ReadLine And Val(TextBox2.Text) = Int(pass_reader.ReadLine) Then
                    user_index = i
                    user_reader.Close()
                    pass_reader.Close()
                    Form2.Label3.Text = read_access()
                    Form2.Label4.Text = TextBox1.Text

                    Form2.Visible = True
                    Me.Visible = False
                    Exit For

                End If
            Next
            user_reader.Close()
            pass_reader.Close()
            client.Dispose()
            Label5.Visible = True
            Button1.Enabled = False
            Timer1.Enabled = True
        End If
        Exit Sub
read_data_label:
        Label3.Visible = True
        Button1.Enabled = False
        Timer1.Enabled = True
    End Sub
    Public Function read_access() As String
        For i = 1 To user_index
            access_reader = New StreamReader(client.OpenRead(access_address))
            access = access_reader.ReadLine

        Next
        access_reader.Close()
        client.Dispose()
        Return access
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Show" Then
            TextBox2.UseSystemPasswordChar = False
            TextBox2.PasswordChar = ""
            Button3.Text = "Hide"
        Else
            TextBox2.UseSystemPasswordChar = True
            TextBox2.PasswordChar = "*"
            Button3.Text = "Show"
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If My.Computer.Network.IsAvailable Then

            read_data()
        Else
            Label4.Visible = True
            Button1.Enabled = False
            Timer1.Enabled = True
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        Label6.Visible = False
        Button1.Enabled = True
        Timer1.Enabled = False
    End Sub
End Class
