Imports System.Net.Http
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Private submissions As List(Of Dictionary(Of String, String))
    Private currentIndex As Integer = 0

    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using client As New HttpClient()
            Dim response = Await client.GetStringAsync("http://localhost:3000/read?index=" & currentIndex)
            submissions = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, String)))(response)
            ShowSubmission(currentIndex)
        End Using
    End Sub

    Private Sub ShowSubmission(index As Integer)
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim submission = submissions(index)
            lblName.Text = submission("name")
            lblEmail.Text = submission("email")
            lblPhone.Text = submission("phone")
            lblGithub.Text = submission("github_link")
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            ShowSubmission(currentIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            ShowSubmission(currentIndex)
        End If
    End Sub

    Private Sub ViewSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        End If
    End Sub
End Class
