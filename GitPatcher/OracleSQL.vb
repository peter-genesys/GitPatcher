﻿Imports Oracle.ManagedDataAccess.Client ' VB.NET

Public Class OracleSQL


    Public Shared Function QueryToString(ByVal sqlQuery As String, ByVal resultColumn As String) As String

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor



        Dim conn As New OracleConnection(Globals.getARMconnection)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader
        Dim answer As String

        Try

            conn.Open()


            cmd = New OracleCommand(sqlQuery, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            dr.Read()

            answer = dr.Item(resultColumn)

            dr.Close()

            conn.Close()
            conn.Dispose()


        Catch ex As Exception ' catches any error
            MessageBox.Show(ex.Message.ToString())
        Finally
            ' In a real application, put cleanup code here.

        End Try

        Cursor.Current = cursorRevert

        Return answer


    End Function


    Public Shared Function QueryToCollection(ByVal sqlQuery As String, ByVal resultColumn As String) As Collection

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim result As Collection = New Collection

        Dim conn As New OracleConnection(Globals.getARMconnection)
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Try

            conn.Open()

            cmd = New OracleCommand(sqlQuery, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            While (dr.Read())
                result.Add(dr.Item(resultColumn), dr.Item(resultColumn))
            End While

            dr.Close()
            conn.Close()
            conn.Dispose()


        Catch ex As Exception ' catches any error
            MessageBox.Show(ex.Message.ToString())
        Finally
            ' In a real application, put cleanup code here.

        End Try

        If result.Count = 0 Then
            Logger.Dbg("No records found.")
        End If


        Cursor.Current = cursorRevert

        Return result

    End Function

    Public Shared Function GetModifiedApps() As Collection
        'Get a list of modified apps
        Dim results As Collection = New Collection
        results = OracleSQL.QueryToCollection(
            "select 'f'||q.APP_ID app_id " &
            "from arm_app_queue q " &
                ",apex_applications a " &
            "where q.app_id = a.application_id " &
            "group by app_id " &
            "having max(a.last_updated_on) > max(q.installed_on)", "app_id")
        Return results
    End Function

    Public Shared Function GetUnappliedPatches() As Collection
        'Get a list of unapplied patches
        Dim results As Collection = New Collection
        results = OracleSQL.QueryToCollection(
            "select patch_name from ARM_UNAPPLIED_V", "patch_name")
        Return results
    End Function

    Public Shared Function GetUnpromotedPatches() As Collection
        'Get a list of unpromoted patches
        Dim results As Collection = New Collection
        results = OracleSQL.QueryToCollection(
            "select patch_name from ARM_UNPROMOTED_V", "patch_name")
        Return results
    End Function



End Class
