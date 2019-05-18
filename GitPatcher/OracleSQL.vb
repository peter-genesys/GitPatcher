Imports Oracle.ManagedDataAccess.Client ' VB.NET

Public Class OracleSQL

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
        Dim modifiedApps As Collection = New Collection
        modifiedApps = OracleSQL.QueryToCollection(
            "select 'f'||q.APP_ID app_id " &
            "from arm_app_queue q " &
                ",apex_applications a " &
            "where q.app_id = a.application_id " &
            "group by app_id " &
            "having max(a.last_updated_on) > max(q.installed_on)", "app_id")
        Return modifiedApps
    End Function

End Class
