Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim bcClient As New BCCommunicator.Communicator()

        Dim tenantID As String = "PUT YOUR TENANT ID HERE -- NOTE: you will probably want this stored in encrypted storage or at least a config file"
        Dim clientID As String = "PUT YOUR CLIENT ID HERE -- NOTE: you will probably want this stored in encrypted storage or at least a config file"
        Dim clientSecret As String = "PUT YOUR CLIENT SECRET HERE -- NOTE: you will probably want this stored in encrypted storage or at least a config file"

        'NOTE:  you will want To store the refresh token For future use
        Dim refreshToken As String = "PUT YOUR REFRESH TOKEN HERE -- probably a good idea to store it in a database, etc."

        Dim currentToken As New BCCommunicator.Token()

        'NOTE:  if you have a current token, use it. check the expiration date first.  if it is expired.  get a new one
        'you should only have to refresh if you dont have a valid currentToken
        'call the library call to refresh the token

        currentToken = bcClient.RefreshToken(tenantID, clientID, clientSecret, refreshToken)


        ' NOTE:  you should store the currentToken token in a database or config file so that you can use it until it expires.
    End Sub
End Class
