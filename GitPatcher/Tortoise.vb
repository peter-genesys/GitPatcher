﻿Public Class Tortoise
    Public Shared Sub Commit(ByVal i_WorkingDir As String, ByVal i_logmsg As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Commit(i_WorkingDir, i_logmsg)
    End Sub

    Public Shared Sub Log(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.ShowLog(i_WorkingDir)
    End Sub

    Public Shared Sub Add(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Add(i_WorkingDir)
    End Sub

    Public Shared Sub Pull(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Pull(i_WorkingDir)
    End Sub


    Public Shared Sub Push(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Push(i_WorkingDir)
    End Sub

    Public Shared Sub Merge(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Merge(i_WorkingDir)
    End Sub

    Public Shared Sub Switch(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Switch(i_WorkingDir)
    End Sub

    Public Shared Sub Resolve(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Resolve(i_WorkingDir)
    End Sub

    Public Shared Sub Revert(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Revert(i_WorkingDir)
    End Sub

    Public Shared Sub Tag(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Tag(i_WorkingDir)
    End Sub

    Public Shared Sub Rebase(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Rebase(i_WorkingDir)
    End Sub

    Public Shared Sub Sync(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.Sync(i_WorkingDir)
    End Sub

    Public Shared Sub StashSave(ByVal i_WorkingDir As String, ByVal i_msg As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.StashSave(i_WorkingDir, i_msg)
    End Sub

    Public Shared Sub StashPop(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New TortoiseFascade(i_wait)
        client.StashPop(i_WorkingDir)
    End Sub

End Class
