Imports LibGit2Sharp

Friend Class WF_rebase

    Shared Sub exportData()

        'Confirm run against non-VM target
        If Globals.getDB <> "VM" Then
            Dim result As Integer = MessageBox.Show("Confirm source is " & Globals.getDB & Environment.NewLine &
                                                    "Data will be spooled from " & Globals.getDB & ".", "Confirm Source", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        'Check existance of exp_data file.
        Dim exportFilename As String = Nothing

        'Look for exp_data_<org>.sql or exp_data.sql
        exportFilename = "exp_data_" & Globals.getOrgCode & ".sql"
        If Not FileIO.fileExists(Globals.getRepoScriptsDir & exportFilename) Then
            exportFilename = "exp_data.sql"
            If Not FileIO.fileExists(Globals.getRepoScriptsDir & exportFilename) Then
                Throw New System.Exception("exp_data file not found in dir: " & Globals.getRepoScriptsDir)
            End If
        End If

        'Use Host class to execute with a master script.
        Host.RunMasterScript("prompt Exporting Data" &
            Environment.NewLine & "DEFINE database = '" & Globals.getDATASOURCE & "'" &
            Environment.NewLine & "@" & Globals.getRunConfigDir & Globals.getOrgCode & "_" & Globals.getDB & ".sql" &
            Environment.NewLine & "@" & exportFilename &
            Environment.NewLine & "exit;" _
          , Globals.getRepoScriptsDir)
    End Sub



    Shared Function rebaseBranch(ByVal iBranchType As String _
                               , ByVal iDBtarget As String _
                               , ByVal iRebaseBranchOn As String _
                               , Optional ByVal iPatching As Boolean = True _
                               , Optional ByVal iAppChanges As Boolean = True _
                               , Optional ByVal iDBChanges As Boolean = True _
                               , Optional ByVal iRebasePatch As Boolean = False) As String

        Dim l_tagA As String = Nothing
        Dim l_tagB As String = Nothing

        Dim tag_num_padding As Integer = 2
        Common.checkBranch(iBranchType)

        Dim l_tag_prefix As String = Nothing
        If iBranchType = "hotfix" Then
            l_tag_prefix = iDBtarget.Substring(0, 1)
        End If


        Dim currentBranchLong As String = GitOp.CurrentBranch()
        Dim currentBranchShort As String = Globals.currentBranch
        Dim callStashPop As Boolean = False

        Dim title As String

        If iRebasePatch Then
            title = "Quick Rebase - DB and Apex"
        ElseIf iPatching Then
            title = "Slave Rebase - DB and Apex"
        ElseIf iAppChanges And iDBChanges Then
            title = "Standalone Rebase - DB and Apex"
        ElseIf iDBChanges Then
            title = "Standalone Rebase - DB changes only"
        ElseIf iAppChanges Then
            title = "Standalone Rebase - Apex changes only"
        End If


        Dim rebasing As ProgressDialogue = New ProgressDialogue(title & " - branch " & currentBranchLong)

        Dim l_max_tag As Integer = 0


        For Each thisTag As Tag In GitOp.getTagList(Globals.currentBranch & ".")
            Try
                Dim tag_no As String = Common.getLastSegment(thisTag.FriendlyName, ".").Substring(0, tag_num_padding)

                If tag_no > l_max_tag Then
                    l_max_tag = tag_no
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
                MsgBox("Problem with formatting of tagname: " & thisTag.FriendlyName & "  This tag may need to be deleted.")
            End Try

        Next


        Dim l_tag_base As String = l_max_tag + 1
        If iPatching Then
            l_tag_base = l_tag_prefix & l_tag_base.PadLeft(tag_num_padding, "0")
        Else
            l_tag_base = "00"
            l_tagA = currentBranchShort & "." & l_tag_base & "A"
            l_tagB = currentBranchShort & "." & l_tag_base & "B"
        End If


        If iRebasePatch Then
            l_tag_base = l_max_tag 'just return the current tag base.
        End If

        rebasing.MdiParent = GitPatcher
        'EXPORT-APPS-MINE
        rebasing.addStep("Export Apex Apps to " & iBranchType & " branch: " & currentBranch(), True, "Using ApexAppExporter, export from the VM any apps you have changed.", iAppChanges And Not iRebasePatch) ' or the Apex Export workflow")
        'SMARTGEN
        rebasing.addStep("Use SmartGen to spool changed config data: " & currentBranch(), True,
                                    "Did I change any config data?  " &
                                    "Do I need to spool any table changes or generate related objects?  " &
                                    "If so, logon to SmartGen, generate and/or spool code. " &
                                    "Use db-spooler to spool the objects to the local filesystem. " &
                                    "Then commit it too.", False) 'iAppChanges Or iDBChanges
        'EXPORT-DATA
        rebasing.addStep("Export data: " & currentBranch(), False,
                         "Export data using the db-spooler script " & Environment.NewLine &
                         Globals.getRepoPath & "tools\db-spooler\script\exp_data.sql  " & Environment.NewLine & Environment.NewLine &
                         "If there are other objects that need to be generated and/or spooled please use SmartGen.  For example" & Environment.NewLine &
                         "Tables should be spooled if they have been changed, along with any related generated objects, such as tapis and views.", (iAppChanges Or iDBChanges) And Not iRebasePatch)


        'COMMIT
        rebasing.addStep("Commit to Branch: " & currentBranchLong, False, "Commit or Revert to ensure the current branch [" & currentBranchShort & "] contains no staged changes.", (iAppChanges Or iDBChanges))
        'STASH-SAVE
        rebasing.addStep("Stash Save: " & currentBranchLong, False, "Stash Save to ensure the current branch [" & currentBranchShort & "] contains no staged changes.", (iAppChanges Or iDBChanges))
        'SWITCH-MASTER
        rebasing.addStep("Switch to " & iRebaseBranchOn & " branch", True, "If you get an error concerning uncommitted changes.  Please resolve the changes and then RESTART this process to ensure the switch to " & iRebaseBranchOn & " branch is successful.", (iAppChanges Or iDBChanges) And Not iRebasePatch)
        'PULL-MASTER
        rebasing.addStep("Pull from Origin", True, "Pull from the master branch.", (iAppChanges Or iDBChanges) And Not iRebasePatch)

        'TAG-A-MASTER-PATCHING
        rebasing.addStep("Tag " & iRebaseBranchOn & " HEAD with " & currentBranchShort & "." & l_tag_base & "A", True, "Will Tag the " & iRebaseBranchOn & " head commit for patch comparisons. Asks for the tag value in format 99, but creates tag " & currentBranchShort & ".99A", iPatching And Not iRebasePatch)
        'TAG-A-MASTER-REBASE-APPS
        rebasing.addStep("Tag " & iRebaseBranchOn & " HEAD with " & currentBranchShort & "." & l_tag_base & "A", True, "Will Tag the " & iRebaseBranchOn & " head commit for apex app comparisons.", Not iPatching And iAppChanges And Not iRebasePatch)


        'RETURN-FEATURE
        rebasing.addStep("Return to branch: " & currentBranchLong, True, "Return to the feature branch", (iAppChanges Or iDBChanges) And Not iRebasePatch)
        'REBASE-FEATURE
        rebasing.addStep("Rebase Branch: " & currentBranchLong & " From Upstream:" & iRebaseBranchOn, True,
                         "Please select the Upstream Branch:" & iRebaseBranchOn & " from the Tortoise Rebase Dialogue" & Environment.NewLine & Environment.NewLine &
                         "Resolving Conflicts in APEX Files:" & Environment.NewLine &
                         "Rebasing involves merging your changes onto the master version.  The interactive rebase will pause when a rebasing a commit that produces a conflict. " & Environment.NewLine &
                         "Conflicts will ofter occur when another developer has modified a file since you started your feature, or last rebased." & Environment.NewLine &
                         "The usual suspect is the last_updated fields." & Environment.NewLine &
                         "Inspect all conflicts with the TortoiseGIT conflict resolution tool, by double-clicking on the red filename." & Environment.NewLine &
                         "The tool will display conflicts and changes.  Only conflicts need to be resolved.  Do not attempt to resolve changes.  Make sure you know the difference." & Environment.NewLine &
                         "Some conflicts can simply be resolved in favour of the feature branch by Right-Click on the file, but please inspect first. EG:" & Environment.NewLine &
                         " + create_application.sql - usually only differs on the last_updated fields." & Environment.NewLine &
                         "     but may have other application level changes - please inspect when resolving." & Environment.NewLine &
                         " + init.sql - changes are usually in the comments only" & Environment.NewLine &
                         "     resolve in favour of feature branch." & Environment.NewLine &
                         " + page_XXXXX.sql - pages often conflict on the last_updated fields." & Environment.NewLine &
                         "   use the TortoiseGIT conflict tool to 'choose my block' for conflicts only.  " & Environment.NewLine &
                         "   Once all conflicts are resolved, save the file, and mark as resolved." & Environment.NewLine & Environment.NewLine &
                         "When all conflicted files are resolved, press Commit to continue with the interactive rebase.",
                         iAppChanges Or iDBChanges)
        'TAG-B-FEATURE
        rebasing.addStep("Tag Branch: " & currentBranchLong & " HEAD with " & currentBranchShort & "." & l_tag_base & "B", True, "Will Tag the " & iBranchType & " head commit for patch comparisons. Creates tag " & currentBranchShort & ".99B.", (iPatching Or iAppChanges) And Not iRebasePatch)
        'REVERT-VM
        rebasing.addStep("Restore to a clean VM snapshot", True, "GitPatcher will restore your VM to a clean VM snapshot." &
                         Environment.NewLine & "Before running patches, restore to a clean VM snapshot created prior to the development of the current feature.", iDBChanges)
        'PATCH-RUNNER
        rebasing.addStep("Use PatchRunner to run Unapplied Patches", True, "Use PatchRunner to run Unapplied Patches", iAppChanges Or iDBChanges)
        'IMPORT-APPS-QUEUED
        rebasing.addStep("Import any queued apps: " & currentBranch(), True, "Any Apex Apps that were included in a patch, must be reinstalled now. ", iAppChanges Or iDBChanges)
        'IMPORT-APPS-MINE
        'Needed for any Standalone rebase that includes app changes
        rebasing.addStep("Re-Import my changed apps: " & currentBranch(), True, "Any Apex Apps that were changed and exported by me, must be reinstalled now, since the VM has been reverted. ", Not iPatching And iAppChanges And Not iRebasePatch)
        'DELETE-TAGS-REBASE-APPS
        rebasing.addStep("Delete Tags " & currentBranchShort & "." & l_tag_base & "A" & " and " & currentBranchShort & "." & l_tag_base & "B", True, "Will delete the temporary tags.", Not iPatching And iAppChanges And Not iRebasePatch)


        'STASH-POP
        rebasing.addStep("Stash Pop: " & currentBranchLong, False, "Stash Pop if a Stash Save was used previously, and especially, if we are not also making a patch.", Not iPatching And Not iRebasePatch)
        'SNAPSHOT
        rebasing.addStep("Post-Rebase Snapshot", True, "Before creating new patches, snapshot the VM again.  Use this snapshot as a quick restore to point restest patches that have failed, on first execution.", iAppChanges Or iDBChanges)

        rebasing.Show()

        Do Until rebasing.isStarted
            Common.Wait(1000)
        Loop

        Try
            'Confirm run against non-VM target
            If Globals.getDB <> "VM" Then
                Dim result As Integer = MessageBox.Show("Confirm target is " & Globals.getDB &
      Chr(10) & "The current database is " & Globals.getDB & ".", "Confirm Target", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    l_tag_base = "CANCEL"
                    Throw New Exception("User cancelled - cancelling rebase.")
                End If
            End If


            'EXPORT-APPS-MINE
            If rebasing.toDoNextStep() Then
                'Export Apex to branch

                'Start the ApexAppExporter and wait until it closes.
                Dim GitPatcherChild As ApexAppExporter = New ApexAppExporter

            End If

            'SMARTGEN
            If rebasing.toDoNextStep() Then

                MsgBox("Please logon to SmartGen and generate/spool objects", MsgBoxStyle.Exclamation, "SmartGen")

            End If




            'EXPORT-DATA
            If rebasing.toDoNextStep() Then
                Try
                    exportData()
                Catch ex As Exception
                    'Continue after data export error. Prob missing exp_data.sql
                    MsgBox(ex.Message)
                End Try
            End If


            'COMMIT
            If rebasing.toDoNextStep() Then
                'User chooses to commit, but don't bother unless the checkout is also dirty (meaning there is at least 1 staged or unstaged change)
                If GitOp.IsDirty() Then
                    Logger.Dbg("User chose to commit and the checkout is also dirty")

                    'Committing changed files to GIT
                    'MsgBox("Checkout is dirty, files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout is dirty")
                    '"Commit or Revert to ensure the current branch [" & currentBranchShort & "] contains no uncommitted changes."
                    Tortoise.Commit(Globals.getRepoPath, currentBranchShort, True)

                End If
            ElseIf Not rebasing.IsDisposed Then 'ignore if form has been closed.
                'User chooses to NOT to commit, but commit anyway if there is at least 1 staged change
                'Committing changed files to GIT"
                If GitOp.ChangedFiles() > 0 Then
                    Logger.Dbg("User chose NOT to commit but the checkout has staged changes")

                    MsgBox("Files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout has changes")
                    '"Commit or Revert to ensure the current branch [" & currentBranchShort & "] contains no uncommitted changes."
                    Tortoise.Commit(Globals.getRepoPath, currentBranchShort, True)

                End If


            End If

            'STASH-SAVE
            If rebasing.toDoNextStep() Then
                'User chooses to StashSave, but don't bother unless the checkout is also dirty (meaning there is at least 1 staged or unstaged change)
                If GitOp.IsDirty() Then
                    Logger.Dbg("User chose to commit and the checkout is also dirty")

                    'StashSave changes
                    'MsgBox("Checkout is dirty, files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout is dirty")
                    Tortoise.StashSave(Globals.getRepoPath, "Ensure the current branch [" & currentBranchShort & "] is free of uncommitted changes.", True)

                    callStashPop = True

                End If
            ElseIf Not rebasing.IsDisposed Then 'ignore if form has been closed.
                'User chooses to NOT to StashSave, but commit anyway if there is at least 1 staged change
                'Committing changed files to GIT"
                If GitOp.ChangedFiles() > 0 Then
                    Logger.Dbg("User chose NOT to commit but the checkout has staged changes")

                    MsgBox("Files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout has changes")
                    Tortoise.StashSave(Globals.getRepoPath, "Stash Save to ensure the current branch [" & currentBranchShort & "] contains no staged changes.", True)

                    callStashPop = True

                End If

            End If

            'SWITCH-MASTER
            If rebasing.toDoNextStep() Then
                'Switch to develop branch
                'GitBash.Switch(Globals.getRepoPath, iRebaseBranchOn)
                GitOp.SwitchBranch(iRebaseBranchOn)
            End If

            'PULL-MASTER
            If rebasing.toDoNextStep() Then
                'Pull from origin/develop
                GitOp.pullCurrentBranch()
            End If

            'TAG-A-MASTER-PATCHING
            If rebasing.toDoNextStep() Then
                'Tag the head
                'Confirm Sequence
                l_tag_base = InputBox("Tagging current HEAD of " & iRebaseBranchOn & ".  Please confirm 2 digit sequence for this patch.", "Create Tag for this patch", l_tag_base)
                l_tag_base = l_tag_base.Trim

                'Check seq is not null
                If String.IsNullOrEmpty(l_tag_base) Then
                    Throw New Exception("No sequence given - cancelling rebase.")
                End If

                'Check seq is numeric
                Dim l_tag_seq As Integer
                Try
                    l_tag_seq = l_tag_base
                Catch ex As Exception
                    MsgBox(ex.Message)
                    l_tag_base = ""
                    Throw New Exception("Numeric conversion error - cancelling rebase.")
                End Try

                'Check seq is in range 1-99
                If l_tag_seq < 1 Or l_tag_seq > 99 Then
                    l_tag_base = ""
                    Throw New Exception("Sequence outside of range 1-99 - cancelling rebase.")
                End If

                'Write the A Tag
                l_tagA = currentBranchShort & "." & l_tag_base & "A"
                rebasing.updateStepDescription(7, "Tag " & iRebaseBranchOn & " HEAD with " & l_tagA)
                GitOp.createTagHead(l_tagA)

            End If

            'TAG-A-MASTER-REBASE-APPS
            If rebasing.toDoNextStep() Then
                'Tag the head
                l_tagA = currentBranchShort & "." & l_tag_base & "A"
                rebasing.updateStepDescription(8, "Tag " & iRebaseBranchOn & " HEAD with " & l_tagA)
                GitOp.createTagHead(l_tagA)

            End If


            'RETURN-FEATURE
            If rebasing.toDoNextStep() Then
                'Return to branch
                GitOp.SwitchBranch(currentBranchLong)
            End If

            'REBASE-FEATURE
            If rebasing.toDoNextStep() Then
                'Rebase branch
                Tortoise.Rebase(Globals.getRepoPath)
            End If

            'TAG-B-FEATURE
            If rebasing.toDoNextStep() Then
                'Tag Branch
                l_tagB = currentBranchShort & "." & l_tag_base & "B"
                rebasing.updateStepDescription(11, "Tag Branch: " & currentBranchLong & " HEAD with " & l_tagB)
                GitOp.createTagHead(l_tagB)

            End If

            'REVERT-VM
            If rebasing.toDoNextStep() Then
                'Revert VM
                If My.Settings.VBoxName = "No VM" Then
                    MsgBox("Please create a snapshot of your current VM state, and then revert to a state prior the work about to be patched.", MsgBoxStyle.Exclamation, "Revert VM")
                Else
                    WF_virtual_box.revertVM(currentBranchShort & "." & l_tag_base & "-wip", True, "clean")
                End If


            End If

            Dim l_no_unapplied_patches As Boolean = True

            'PATCHRUNNER-UNAPPLIED
            If rebasing.toDoNextStep() Then
                'Use PatchRunner to run Unapplied Patches
                Dim GitPatcherChild As PatchRunner = New PatchRunner(l_no_unapplied_patches, "Unapplied")

            End If

            Dim l_no_queued_apps As Boolean = True

            'IMPORT-APPS-QUEUED
            If rebasing.toDoNextStep() Then
                'Install queued Apex Apps.
                'Start the ApexAppInstaller and wait until it closes.
                Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller(l_no_queued_apps, "Queued")


            End If

            Dim l_no_my_apps As Boolean = True

            'IMPORT-APPS-MINE
            If rebasing.toDoNextStep() Then
                'Install my Apex Apps.
                'Start the ApexAppInstaller and wait until it closes.
                Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller(l_no_my_apps, "All", l_tagA, l_tagB)


            End If

            'DELETE-TAGS-REBASE-APPS
            If rebasing.toDoNextStep() Then
                GitOp.deleteTag(l_tagA)
                GitOp.deleteTag(l_tagB)
            End If

            'STASH-POP
            If rebasing.toDoNextStep(Not callStashPop, callStashPop) Then
                'Do we need to confirm this step?
                Tortoise.StashPop(Globals.getRepoPath, True)

            End If

            rebasing.updateStepDescription(11, "Tag Branch: " & currentBranchLong & " HEAD with " & l_tagB)

            'SNAPSHOT-POST-REBASE
            'Skip if found no patches and no apps
            If rebasing.toDoNextStep(l_no_unapplied_patches And l_no_queued_apps And l_no_my_apps) Then
                'Snapshot VM - Post-Rebase
                If My.Settings.VBoxName = "No VM" Then
                    MsgBox("Before creating new patches, snapshot the VM again.", MsgBoxStyle.Exclamation, "Post-Rebase Snapshot")
                Else
                    WF_virtual_box.takeSnapshot(PatchRunner.GetlastSuccessfulPatch & "-post-rebase-" & currentBranchShort & "." & l_tag_base)
                End If

            End If

            'Finish
            rebasing.toDoNextStep()

        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            rebasing.setToCompleted()
            rebasing.Close()
        End Try

        Return l_tag_base

    End Function




    Shared Function tagBranch(ByVal iBranchType As String _
                               , ByVal iDBtarget As String _
                               , ByVal iRebaseBranchOn As String) As String

        Dim l_tagA As String = Nothing
        Dim l_tagB As String = Nothing

        Dim tag_num_padding As Integer = 2
        Common.checkBranch(iBranchType)

        Dim l_tag_prefix As String = Nothing
        If iBranchType = "hotfix" Then
            l_tag_prefix = iDBtarget.Substring(0, 1)
        End If


        Dim currentBranchLong As String = GitOp.CurrentBranch()
        Dim currentBranchShort As String = Globals.currentBranch
        Dim callStashPop As Boolean = False

        Dim title As String = "Tagging the version"
        'If iPatching Then
        '    title = "Slave Rebase - DB and Apex"
        'ElseIf iAppChanges And iDBChanges Then
        '    title = "Standalone Rebase - DB and Apex"
        'ElseIf iDBChanges Then
        '    title = "Standalone Rebase - DB changes only"
        'ElseIf iAppChanges Then
        '    title = "Standalone Rebase - Apex changes only"
        'End If


        Dim rebasing As ProgressDialogue = New ProgressDialogue(title & " - branch " & currentBranchLong)

        Dim l_max_tag As Integer = 0


        For Each thisTag As Tag In GitOp.getTagList(Globals.currentBranch & ".")
            Try
                Dim tag_no As String = Common.getLastSegment(thisTag.FriendlyName, ".").Substring(0, tag_num_padding)

                If tag_no > l_max_tag Then
                    l_max_tag = tag_no
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
                MsgBox("Problem with formatting of tagname: " & thisTag.FriendlyName & "  This tag may need to be deleted.")
            End Try

        Next


        Dim l_tag_base As String = l_max_tag + 1
        If True Then
            l_tag_base = l_tag_prefix & l_tag_base.PadLeft(tag_num_padding, "0")
        Else
            l_tag_base = "00"
            l_tagA = currentBranchShort & "." & l_tag_base & "A"
            l_tagB = currentBranchShort & "." & l_tag_base & "B"
        End If

        rebasing.MdiParent = GitPatcher

        'SWITCH-MASTER
        rebasing.addStep("Switch to " & iRebaseBranchOn & " branch", True, "If you get an error concerning uncommitted changes.  Please resolve the changes and then RESTART this process to ensure the switch to " & iRebaseBranchOn & " branch is successful.", True)

        'TAG-A-MASTER-PATCHING
        rebasing.addStep("Tag " & iRebaseBranchOn & " HEAD with " & currentBranchShort & "." & l_tag_base & "A", True, "Will Tag the " & iRebaseBranchOn & " head commit for patch comparisons. Asks for the tag value in format 99, but creates tag " & currentBranchShort & ".99A", True)

        'RETURN-FEATURE
        rebasing.addStep("Return to branch: " & currentBranchLong, True, "Return to the " & iBranchType & " branch", True)

        'TAG-B-FEATURE
        rebasing.addStep("Tag Branch: " & currentBranchLong & " HEAD with " & currentBranchShort & "." & l_tag_base & "B", True, "Will Tag the " & iBranchType & " head commit for patch comparisons. Creates tag " & currentBranchShort & ".99B.", True)

        rebasing.Show()

        Do Until rebasing.isStarted
            Common.Wait(1000)
        Loop

        Try
            'Confirm run against non-VM target
            If Globals.getDB <> "VM" Then
                Dim result As Integer = MessageBox.Show("Confirm target is " & Globals.getDB &
      Chr(10) & "The current database is " & Globals.getDB & ".", "Confirm Target", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    l_tag_base = "CANCEL"
                    Throw New Exception("User cancelled - cancelling rebase.")
                End If
            End If


            'SWITCH-MASTER
            If rebasing.toDoNextStep() Then
                'Switch to develop branch
                'GitBash.Switch(Globals.getRepoPath, iRebaseBranchOn)
                GitOp.SwitchBranch(iRebaseBranchOn)
            End If

            ''PULL-MASTER
            'If rebasing.toDoNextStep() Then
            '    'Pull from origin/develop
            '    GitOp.pullCurrentBranch()
            'End If

            'TAG-A-MASTER-PATCHING
            If rebasing.toDoNextStep() Then
                'Tag the head
                'Confirm Sequence
                l_tag_base = InputBox("Tagging current HEAD of " & iRebaseBranchOn & ".  Please confirm 2 digit sequence for this patch.", "Create Tag for this patch", l_tag_base)
                l_tag_base = l_tag_base.Trim

                'Check seq is not null
                If String.IsNullOrEmpty(l_tag_base) Then
                    Throw New Exception("No sequence given - cancelling rebase.")
                End If

                'Check seq is numeric
                Dim l_tag_seq As Integer
                Try
                    l_tag_seq = l_tag_base
                Catch ex As Exception
                    MsgBox(ex.Message)
                    l_tag_base = ""
                    Throw New Exception("Numeric conversion error - cancelling rebase.")
                End Try

                'Check seq is in range 1-99
                If l_tag_seq < 1 Or l_tag_seq > 99 Then
                    l_tag_base = ""
                    Throw New Exception("Sequence outside of range 1-99 - cancelling rebase.")
                End If

                'Write the A Tag
                l_tagA = currentBranchShort & "." & l_tag_base & "A"
                rebasing.updateStepDescription(1, "Tag " & iRebaseBranchOn & " HEAD with " & l_tagA)
                GitOp.createTagHead(l_tagA)

            End If



            'RETURN-FEATURE
            If rebasing.toDoNextStep() Then
                'Return to branch
                GitOp.SwitchBranch(currentBranchLong)
            End If

            ''REBASE-FEATURE
            'If rebasing.toDoNextStep() Then
            '    'Rebase branch
            '    Tortoise.Rebase(Globals.getRepoPath)
            'End If

            'TAG-B-FEATURE
            If rebasing.toDoNextStep() Then
                'Tag Branch
                l_tagB = currentBranchShort & "." & l_tag_base & "B"
                rebasing.updateStepDescription(3, "Tag Branch: " & currentBranchLong & " HEAD with " & l_tagB)
                GitOp.createTagHead(l_tagB)

            End If


            'Finish
            rebasing.toDoNextStep()

        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            rebasing.setToCompleted()
            rebasing.Close()
        End Try

        Return l_tag_base

    End Function



End Class
