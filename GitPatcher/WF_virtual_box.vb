Imports System.Text.RegularExpressions

Public Class WF_virtual_box

    Shared Sub getVMs(ByRef iVMList As Collection, ByVal itype As String)

        Dim allVMs As String = Nothing

        Host.check_StdOut("VBoxManage list " & itype, allVMs, Globals.getVBoxDir, False)

        Dim strArr() As String
        Dim vmIndex As Integer
        strArr = allVMs.Split("""")

        For vmIndex = 1 To strArr.Length Step 2

            If vmIndex < strArr.Length Then
                'MsgBox(strArr(vmIndex))
                iVMList.Add(strArr(vmIndex), strArr(vmIndex))
            End If
        Next

    End Sub

    Shared Sub takeSnapshot(ByRef newSnapshotName As String)
        Dim result As String = Nothing
        Host.check_StdOut("VBoxManage snapshot """ & My.Settings.VBoxName & """ take """ &
                  newSnapshotName & """", result, Globals.getVBoxDir, True)
        Logger.Debug(result)
    End Sub

    Shared Sub startVM(ByRef type As String)
        Dim result As String = Nothing
        Host.check_StdOut("VBoxManage startvm """ & My.Settings.VBoxName & """ --type " & type, result, Globals.getVBoxDir, True)
        Logger.Debug(result)
    End Sub

    Shared Sub controlVM(switch)
        Dim result As String = Nothing
        Host.check_StdOut("VBoxManage controlvm """ & My.Settings.VBoxName & """ " & switch, result, Globals.getVBoxDir, True)
        Logger.Debug(result)
    End Sub





    Shared Sub revertVM(ByRef newSnapshotName As String, ByRef createSnapshot As Boolean, ByRef revertType As String)

        'Dim title As String
        Dim currentBranchShort As String = Globals.currentBranch


        Dim reverting As ProgressDialogue = New ProgressDialogue("Restore VM " & My.Settings.VBoxName)

        reverting.MdiParent = GitPatcher
        'WIP Snapshot
        reverting.addStep("Create a WIP VM snapshot " & newSnapshotName, createSnapshot, "GitPatcher will use VBoxManage to create a VM snaphot to save current state of the VM.", True)
        'VM Poweroff
        reverting.addStep("Powerdown the VM", True, "GitPatcher will use VBoxManage to immediately powerdown the VM.", True)
        'Restore a clean snapshot
        reverting.addStep("Restore to a " & revertType & " VM snapshot", True,
                          "GitPatcher will use VBoxManage to restore the VM to a " & revertType & " snapshot.  " & Environment.NewLine & Environment.NewLine &
                          "CLEAN" & Environment.NewLine &
                          "A clean snapshot is one taken prior to the development of the current feature." & Environment.NewLine & Environment.NewLine &
                          "POST-REBASE" & Environment.NewLine &
                          "A post-rebase snapshot is one taken after rebase of the current feature, before running the current patch.", True)
        'VM StartUp
        reverting.addStep("Startup the VM", True, "GitPatcher will use VBoxManage to startup the VM at the restored snapshot.", True)

        reverting.Show()

        Do Until reverting.isStarted
            Common.Wait(200)
        Loop

        Dim snapshotResult As String = Nothing

        'WIP Snapshot
        If reverting.toDoNextStep() Then

            takeSnapshot(newSnapshotName)

        End If

        'VM Poweroff
        If reverting.toDoNextStep() Then
            controlVM("poweroff")
        End If

        'Restore a clean snapshot
        If reverting.toDoNextStep() Then
            Host.check_StdOut("VBoxManage snapshot """ & My.Settings.VBoxName & """ list --machinereadable", snapshotResult, Globals.getVBoxDir, False)
            Logger.Debug(snapshotResult)

            Dim snapshotNames As Collection = New Collection
            Dim snapshotUUIDs As Collection = New Collection
            Dim lCurrentSnapshotName As String = Nothing


            Dim strArr() As String
            Dim ssIndex As Integer
            strArr = snapshotResult.Split(Chr(10))

            Dim currentUUID As String = Nothing

            'First find current snapshot uuid
            For ssIndex = 0 To strArr.Length - 1
                'Filter out unneeded rows.
                If strArr(ssIndex).Contains("CurrentSnapshotUUID") Then

                    'Find the "Shapshot UUID" in double quotes
                    Dim strArr2() As String
                    strArr2 = strArr(ssIndex).Split("""")

                    'Find UUID
                    Dim uuid As String = strArr2(1)

                    currentUUID = uuid

                    Logger.Debug("currentUUID =" & currentUUID)

                End If
            Next


            'Next - Find every Snapshot Name and Snapshot UUID
            For ssIndex = 0 To strArr.Length - 1
                'Filter out unneeded rows.
                If strArr(ssIndex).Contains("SnapshotName") And
                    Not strArr(ssIndex).Contains("CurrentSnapshotNode") And
                    Not strArr(ssIndex).Contains("CurrentSnapshotName") Then

                    'Find the "Shapshot Name" in double quotes
                    Dim strArr2() As String
                    strArr2 = strArr(ssIndex).Split("""")
                    Dim indent As String = strArr2(0)
                    Dim reg As Regex = New Regex("\d")
                    indent = indent.Replace("SnapshotName", "")
                    indent = indent.Replace("-", "+")
                    indent = reg.Replace(indent, "-")
                    indent = indent.Replace("=", "+ ")

                    Dim name As String = strArr2(1)
                    'snapshotList.Add(indent & name)

                    'Find UUID
                    strArr2 = strArr(ssIndex + 1).Split("""")
                    Dim uuid As String = strArr2(1)

                    snapshotUUIDs.Add(uuid)
                    If uuid = currentUUID Then
                        lCurrentSnapshotName = indent & name & " (current)"
                        snapshotNames.Add(lCurrentSnapshotName)
                    Else
                        snapshotNames.Add(indent & name)
                    End If

                    Logger.Debug(name & "=" & uuid)

                End If
            Next

            'Dim cleanSnapshotUUID As String
            Try
                Dim SnapshotIndex As Integer
                SnapshotIndex = ChoiceDialog.Ask("Please choose a " & revertType & " snapshot from the list of available snapshots for " & My.Settings.VBoxName, snapshotNames, lCurrentSnapshotName, "Choose a " & revertType & " snapshot", False, False, True)

                Logger.Debug("Chosen: " & snapshotNames(SnapshotIndex) & "=" & snapshotUUIDs(SnapshotIndex))

                Host.check_StdOut("VBoxManage snapshot """ & My.Settings.VBoxName & """ restore " &
            snapshotUUIDs(SnapshotIndex), snapshotResult, Globals.getVBoxDir, True)
                Logger.Debug(snapshotResult)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


        End If

        'VM StartUp
        If reverting.toDoNextStep() Then

            startVM(My.Settings.startvmType)

        End If



        'Finish
        reverting.toDoNextStep()


    End Sub


End Class
