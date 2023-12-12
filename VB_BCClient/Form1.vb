Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim bcClient As New BCCommunicator.Communicator()

        Dim tenantID As String = ""
        Dim clientID As String = ""
        Dim clientSecret As String = ""
        Dim scope As String = "https://api.businesscentral.dynamics.com/.default"

        Dim currentToken As New BCCommunicator.Token()

        currentToken = bcClient.CreateNewToken(tenantID, clientID, clientSecret, scope)

        Dim returnInformation As String
        returnInformation = bcClient.RetrieveData(tenantID, currentToken.access_token, "companies")



        ' NOTE:  you should store the currentToken token in a database or config file so that you can use it until it expires.
    End Sub
End Class
