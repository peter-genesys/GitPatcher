Public Class GPTrees
    ' Shared Sub Collapse_Click(ByVal sender As Object, ByVal e As EventArgs, ByRef aTreeView As TreeView)
    '     ' Disable redrawing of treeView1 to prevent flickering  
    '     ' while changes are made.
    '     aTreeView.BeginUpdate()
    '
    '     ' Collapse all nodes of treeView1.
    '     aTreeView.CollapseAll()
    '
    '     ' Enable redrawing of treeView1.
    '     aTreeView.EndUpdate()
    ' End Sub
    '
    ' Shared Sub Expand_Click(ByVal sender As Object, ByVal e As EventArgs, ByRef aTreeView As TreeView)
    '     ' Disable redrawing of treeView1 to prevent flickering  
    '     ' while changes are made.
    '     aTreeView.BeginUpdate()
    '
    '     ' Expand all nodes of treeView1. 
    '     aTreeView.ExpandAll()
    '
    '     ' Enable redrawing of treeView1.
    '     aTreeView.EndUpdate()
    ' End Sub
    '

    Shared Sub showCheckedNodesButton_Click(ByVal sender As Object, ByVal e As EventArgs, ByRef aTreeView As TreeView)
        ' Disable redrawing of treeView1 to prevent flickering  
        ' while changes are made.
        aTreeView.BeginUpdate()

        ' Collapse all nodes of treeView1.
        aTreeView.CollapseAll()

        ' Add the CheckForCheckedChildren event handler to the BeforeExpand event. 
        AddHandler aTreeView.BeforeExpand, AddressOf CheckForCheckedChildren

        ' Expand all nodes of treeView1. Nodes without checked children are  
        ' prevented from expanding by the checkForCheckedChildren event handler.
        aTreeView.ExpandAll()

        ' Remove the checkForCheckedChildren event handler from the BeforeExpand  
        ' event so manual node expansion will work correctly. 
        RemoveHandler aTreeView.BeforeExpand, AddressOf CheckForCheckedChildren

        ' Enable redrawing of treeView1.
        aTreeView.EndUpdate()
    End Sub 'showCheckedNodesButton_Click

    ' Prevent expansion of a node that does not have any checked child nodes. 
    Shared Sub CheckForCheckedChildren(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs)
        If Not HasCheckedChildNodes(e.Node) Then
            e.Cancel = True
        End If
    End Sub 'CheckForCheckedChildren

    ' Returns a value indicating whether the specified  
    ' TreeNode has checked child nodes. 
    Shared Function HasCheckedChildNodes(ByVal node As TreeNode) As Boolean
        If node.Nodes.Count = 0 Then
            Return False
        End If
        Dim childNode As TreeNode
        For Each childNode In node.Nodes
            If childNode.Checked Then
                Return True
            End If
            ' Recursively check the children of the current child node. 
            If HasCheckedChildNodes(childNode) Then
                Return True
            End If
        Next childNode
        Return False
    End Function 'HasCheckedChildNodes


    Shared Sub treeChange_Click(ByRef sender As Object, ByVal e As EventArgs, ByRef aTreeView As TreeView)
 
        If sender.text = "Collapse" Then
            sender.text = "Expand"
            aTreeView.CollapseAll()
        ElseIf sender.text = "Expand" Then
            sender.text = "Contract"
            aTreeView.ExpandAll()
        ElseIf sender.text = "Contract" Then
            sender.text = "Collapse"
            showCheckedNodesButton_Click(sender, e, aTreeView)
        End If

    End Sub


    Shared Function AddNode(ByRef nodes As TreeNodeCollection, ByVal fullPath As String, ByVal remainderPath As String, Optional ByVal delim As String = "\") As Boolean
        Dim first_segment As String = Common.getFirstSegment(remainderPath, delim)
        Dim remainder As String = Common.dropFirstSegment(remainderPath, delim)

        Dim lFound As Boolean = False

        'First try to find the node
        For Each node In nodes
            Logger.Note("node.FullPath", node.FullPath)
            Logger.Note("fullPath", fullPath)
            Logger.Note("InStr", InStr(fullPath, node.FullPath))
            If fullPath = node.FullPath Then
                'If node.FullPath.ToString = fullPath Then
                'Yay found it nothing to do
                Logger.Dbg("Yay found it nothing to do")
                Return True
                'Node Full path must match first part of given full path, and current node must match exactly current segment
            ElseIf InStr(fullPath, node.FullPath.ToString) = 1 And first_segment = node.text Then
                'Found a parent node at least, lets look for children
                Logger.Dbg("Found a parent node at least, lets look for children")
                lFound = AddNode(node.nodes, fullPath, remainder)
            End If

        Next

        If Not lFound And Not String.IsNullOrEmpty(first_segment) Then
            'Need to make a node
            Logger.Dbg("Need to make a node for " & first_segment)
            Dim newNode As TreeNode = New TreeNode(first_segment)
            nodes.Add(newNode)
            'If newNode.FullPath = fullPath Then
            If String.IsNullOrEmpty(remainder) Then
                'We made the node!
                Logger.Dbg("We made the node!")
                lFound = True
            Else
                'Now follow this child
                Logger.Dbg("Now follow this child")
                'newNode.  .ShowCheckBox = False 'Hide checkbox of any parent node.
                lFound = AddNode(newNode.Nodes, fullPath, remainder)
            End If

        End If
        If Not lFound Then
            MsgBox("Oops not found. Bad coding?")
        End If
        Return lFound



    End Function




    'NB - PAB There is protection against event recursion and then explicit recursion, but could have left it up to event recursion.!!

    ' Updates all child tree nodes recursively. 
    Shared Sub CheckAllChildNodes(treeNode As TreeNode, nodeChecked As Boolean)
        Dim node As TreeNode
        For Each node In treeNode.Nodes
            node.Checked = nodeChecked
            If node.Nodes.Count > 0 Then
                ' If the current node has child nodes, call the CheckAllChildsNodes method recursively. 
                CheckAllChildNodes(node, nodeChecked)
            End If
        Next node
    End Sub

    'Non-recursive
    Shared Sub CheckChildNodes(treeNode As TreeNode, nodeChecked As Boolean)
        Dim node As TreeNode
        For Each node In treeNode.Nodes
            node.Checked = nodeChecked
            If nodeChecked Then
                node.ExpandAll()
            End if
        Next node
    End Sub


    '  ' NOTE   This code can be added to the BeforeCheck event handler instead of the AfterCheck event. 
    '  ' After a tree node's Checked property is changed, all its child nodes are updated to the same value. 
    '  Shared Sub node_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles treeView1.AfterCheck
    '      ' The code only executes if the user caused the checked state to change. 
    '      If e.Action <> TreeViewAction.Unknown Then
    '          If e.Node.Nodes.Count > 0 Then
    '              ' Calls the CheckAllChildNodes method, passing in the current  
    '              ' Checked value of the TreeNode whose checked state changed.  
    '              Me.CheckAllChildNodes(e.Node, e.Node.Checked)
    '          End If
    '      End If
    '  End Sub

 

    Shared Sub ReadCheckedNodes(ByRef treeNode As TreeNode, ByRef iChosenPatches As Collection, ByVal itemsOnly As Boolean)


        Dim node As TreeNode

        If treeNode.Nodes.Count > 0 Then
            For Each node In treeNode.Nodes
                ReadCheckedNodes(node, iChosenPatches, itemsOnly)
            Next node
        Else
            If treeNode.Checked Or Not itemsOnly Then
                iChosenPatches.Add(treeNode.FullPath)
            End If

        End If

    End Sub

 

End Class
