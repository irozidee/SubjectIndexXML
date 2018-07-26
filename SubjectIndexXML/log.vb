Public Class log
    Public Shared Error_SQL As String = "logs\Error_SQL.txt"
    Public Shared Error_XML As String = "logs\Error_XML.txt"
    Public Shared Error_XML_Legislation As String = "logs\Error_XML_Legislation.txt"
    Public Shared Sub Clear_Log_Files()
        My.Computer.FileSystem.CreateDirectory("logs")
        My.Computer.FileSystem.WriteAllText(Error_SQL, "", False)
        My.Computer.FileSystem.WriteAllText(Error_XML, "", False)
        My.Computer.FileSystem.WriteAllText(Error_XML_Legislation, "", False)
    End Sub
End Class
