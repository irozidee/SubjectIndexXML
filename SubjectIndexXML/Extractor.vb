Imports System.Text.RegularExpressions
Public Class Extractor
    Public Shared Function FindCitation(ByVal StringToSearch As String) 'As String
        Dim pattern As String = ""
        Dim foundCitation As String = ""
        Dim i As Integer = 1
        Dim status As Boolean = False
        Do While status = False
            Select Case i
                Case Is = 1
                    pattern = "(\([0-9]{4,4}\))\s([0-9]{1,4})\s([A-Z|a-z]{2,4})\s([0-9]{1,5})" '[2010] 1 MLRA 999 
                Case Is = 2
                    pattern = "(\[[0-9]{4,4}\])\s([A-Z|a-z]{2,4})\s([A-Z|a-z]{2,4})\s([0-9]{1,5})" '[2010] AC AC 999 
                Case Is = 3
                    pattern = "(\[[0-9]{4,4}\])\s([0-9]{1,3})\s([A-Z|a-z]{2,6})\s([0-9]{1,5})" '[2010] 1 MLRAU 999 
                Case Is = 4
                    pattern = "(\[[0-9]{4,4}\])\s([A-Z|a-z]{2,6})\s([0-9]{1,5})" '[2010] MLRAU 999 
                Case Is = 5
                    pattern = "([0-9]{1,3})\s([A-Z|a-z]{2,6})\s([0-9]{1,5})" ' 2000 XXXXXX 11111
                Case Is = 6
                    pattern = "(\[[0-9]{4,4}\])\s([0-9]{1,3})\s([A-Z|a-z]{2,4})\s([A-Z|a-z]{2,4})\s([0-9]{1,5})" ' [9999] 999 XXXX XXXX 99999
                Case Is = 7
                    pattern = "(\[[0-9]{4,4}\])\s([0-9]{1,3})\s([A-Z|a-z]{2,6})\s\([\w+]{1,10}\)\s([0-9]{1,5})"
                Case Else
                    status = True
                    foundCitation = "ERROR FOR - " & StringToSearch
                    Return foundCitation
            End Select
            Dim m As Match
            Dim r As Regex
            r = New Regex(pattern)
            m = r.Match(StringToSearch)

            If m.Success() Then
                foundCitation = m.Value
                status = True
            Else
                i = i + 1
            End If
        Loop
        Return foundCitation
    End Function
    Public Class ReferredCases
        Public Shared Function FindReferredCasesType(ByVal StringToSearch As String) As String
            StringToSearch = StrConv(StringToSearch, VbStrConv.Lowercase)
            If StringToSearch.Contains("refd") Then
                Return "refd"
            ElseIf StringToSearch.Contains("folld") Then
                Return "folld"
            ElseIf StringToSearch.Contains("foll") Then
                Return "foll"
            ElseIf StringToSearch.Contains("dist") Then
                Return "dist"
            ElseIf StringToSearch.Contains("ovrld") Then
                Return "ovrld"
            ElseIf StringToSearch.Contains("not foll") Then
                Return "not foll"
            ElseIf StringToSearch.Contains("dirujuk") Then
                Return "dirujuk"
            Else
                Return "refd"
            End If
        End Function
        Public Shared Function FindReferredCasesTitle(ByVal StringToSearch As String) As String
            Dim foundCitation As String = FindCitation(StringToSearch)
            Dim result As String = StringToSearch.Replace(foundCitation, "")
            Return result.Trim.Replace("<i>", "").Replace("</i>", "").Replace("amp;amp;","amp;")
        End Function
    End Class
    Public Class ReferredLegislations
        Public Shared Function Extract_Legislation_Sub_No_Type(ByVal s As String) As String
            Dim result As String
            If s.Contains("O ") And (s.Contains("r ") Or s.Contains("rr")) Then
                result = s
            Else
                If s.Contains("ss ") Or s.Contains("s ") Or s.Contains("section") Then
                    result = "s"
                ElseIf s.Contains("reg ") Or s.Contains("regs ") Or s.Contains("reg") Then
                    result = "reg"
                ElseIf s.Contains("art ") Or s.Contains("arts ") Or s.Contains("article") Then
                    result = "art"
                ElseIf s.Contains("rr ") Or s.Contains("r ") Or s.Contains("rule") Then
                    result = "r"
                ElseIf s.Contains("item ") Or s.Contains("items ") Then
                    result = "item"
                Else
                    result = s
                End If
            End If
            Return result
        End Function

        Public Shared Function Replace_Legislation_Sub_No_Type(ByVal s As String) As String
            Dim result As String
            result = s.Replace("ss", "s").Replace("regs", "reg").Replace("arts", "art").Replace("rr", "r") _
                .Replace("items", "item").Replace("Items", "item")
            Return result
        End Function
    End Class

    Public Shared Function FindRegexPattern(ByVal str As String) As String
        Dim rst As String = "NO MATCH"
        'Dim reg1 As String = " (\[(\d){4,4})\] ((\d){1,3}) (\w+) ((\d+){1,5})" '(A)(3)(E)(3)
        'Dim reg2 As String = "(\[(\d){4,4})\] (\w+) ((\d+){1,5})" '(2A)(3)
        Dim reg1 As String = "(\([0-9]{1,10}\))(\([A-Z|a-z]{1,10}\))"
        Dim reg2 As String = "(\([A-Z|a-z]{1,10}\))"
        Dim reg3 As String = "(\([0-9]{1,10}\))"


        Dim m1 As Match
        Dim m2 As Match
        Dim m3 As Match
        Dim r1 As Regex
        Dim r2 As Regex
        Dim r3 As Regex

        r1 = New Regex(reg1)
        m1 = r1.Match(str)
        If m1.Success() Then
            rst = reg1
        End If

        r2 = New Regex(reg2)
        m2 = r2.Match(str)
        If m2.Success() Then
            rst = reg2
        End If

        r3 = New Regex(reg3)
        m3 = r3.Match(str)
        If m3.Success() Then
            rst = reg3
        End If

        Return rst
    End Function
End Class
